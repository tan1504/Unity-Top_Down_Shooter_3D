using System;
using UnityEngine;

public class Player_AimController : MonoBehaviour
{
	private Player player;
	private PlayerControls controls;

	[Header("Aim Visual - Laser")]
	[SerializeField] private LineRenderer aimLaser; // This component is on the weapon holder (child of the Player)

	[Header("Aim Infor")]
	[SerializeField] private Transform aim;

	[SerializeField] private bool isAimingPrecisly;
	[SerializeField] private bool isLockingToTarget;

	[Header("Camera Control Infor")]
	[SerializeField] private Transform cameraTarget;
	[Range(0f, 2f)]
	[SerializeField] private float minCameraDistance;
	[Range(2f, 3f)]
	[SerializeField] private float maxCameraDistance;
	[Range(3f, 5f)]
	[SerializeField] private float cameraSensetivity;

	[SerializeField] private LayerMask aimLayerMask;

	private Vector2 mouseInput;
	private RaycastHit lastMouseHitPosition;

	void Start()
	{
		player = GetComponent<Player>();
		AssignInputEvents();
	}

	private void Update()
	{
		if (player.health.isDead)
			return;

		if (player.controlsEnabled == false)
			return;

		if (Input.GetKeyDown(KeyCode.P))
			isAimingPrecisly = !isAimingPrecisly;

		if (Input.GetKeyDown(KeyCode.L))
			isLockingToTarget = !isLockingToTarget;

		UpdateAimVisuals();
		UpdateAimPosition();
		UpdateCameraPosition();
	}

	public Transform GetAimCameraTarget()
	{
		cameraTarget.position = player.transform.position;
		return cameraTarget;
	}
	public void EnableAimLaser(bool enable) => aimLaser.enabled = enable;

	private void UpdateAimVisuals()
	{
		aimLaser.enabled = player.weaponController.WeaponReady();
		if (aimLaser.enabled == false)
			return;

		WeaponModel weaponModel = player.weaponVisuals.CurrentWeaponModel();

		weaponModel.transform.LookAt(aim);
		weaponModel.gunPoint.LookAt(aim);

		Transform gunPoint = player.weaponController.GunPoint();
		Vector3 laserDirection = player.weaponController.BulletDirection();

		float gunDistance = player.weaponController.CurrentWeapon().gunDistance;
		float laserTipLength = 0.5f; 

		Vector3 endPoint = gunPoint.position + laserDirection * gunDistance;

		if (Physics.Raycast(gunPoint.position, laserDirection, out RaycastHit hit, gunDistance))
		{
			endPoint = hit.point;
			laserTipLength = 0f;
		}

		aimLaser.SetPosition(0, gunPoint.position);
		aimLaser.SetPosition(1, endPoint);
		aimLaser.SetPosition(2, endPoint + laserDirection * laserTipLength);
	}

	private void UpdateAimPosition()
	{
		Transform target = Target();

		if (target != null && isLockingToTarget)
		{
			if (target.GetComponent<Renderer>() != null)
				aim.position = target.GetComponent<Renderer>().bounds.center;
			else
				aim.position = target.position;

			return;
		}

		aim.position = GetMouseHitInfor().point;
		if (!isAimingPrecisly)
			aim.position = new Vector3(aim.position.x, transform.position.y + 1, aim.position.z);
	}

	public Transform Aim() => aim;
	
	public Transform Target()		// WTF
	{
		Transform target = null; 

        if (GetMouseHitInfor().transform.GetComponent<Target>() != null)
        {
            target = GetMouseHitInfor().transform;
        }

		return target;
    }

	public bool CanAimPrecisly() => isAimingPrecisly;

	public RaycastHit GetMouseHitInfor()
	{
		Ray ray = Camera.main.ScreenPointToRay(mouseInput);

		if (Physics.Raycast(ray, out var hitInfor, Mathf.Infinity, aimLayerMask))
		{
			lastMouseHitPosition = hitInfor;
			return hitInfor;
		}

		return lastMouseHitPosition;
	}

	#region Camera 

	private void UpdateCameraPosition()
	{
		cameraTarget.position = 
			Vector3.Lerp(cameraTarget.position, DesiredCameraPosition(), cameraSensetivity * Time.deltaTime);
	}
	public Vector3 DesiredCameraPosition()
	{
		float actualMaxCameraDistance = player.movement.moveInput.y < -0.5f ? minCameraDistance : maxCameraDistance;

		Vector3 desiredAimPosition = GetMouseHitInfor().point;
		Vector3 aimDirection = (desiredAimPosition - transform.position).normalized;

		float distanceToDisiredPosition = Vector3.Distance(transform.position, desiredAimPosition);
		float clampedDistance = Mathf.Clamp(distanceToDisiredPosition, minCameraDistance, actualMaxCameraDistance);

		desiredAimPosition = transform.position + aimDirection * clampedDistance;
		desiredAimPosition.y = transform.position.y + 1;

		return desiredAimPosition;
	}

	#endregion

	private void AssignInputEvents()
	{
		controls = player.controls;
		controls.Character.Aim.performed += context => mouseInput = context.ReadValue<Vector2>();
		controls.Character.Aim.canceled += context => mouseInput = Vector2.zero;
	}
}
