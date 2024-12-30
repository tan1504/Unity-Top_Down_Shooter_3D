using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Interaction : Interactable
{
	private Car_HealthController carHealthController;
	private Car_Controller car;
	private Transform player;

	private float defaultPlayerScale;

	[Header("Exit Details")]
	[SerializeField] private float exitCheckRadius;
	[SerializeField] private Transform[] exitPoints;
	[SerializeField] private LayerMask whatToIgnoreForExit;

	private void Start()
	{
		carHealthController = GetComponent<Car_HealthController>();
		car = GetComponent<Car_Controller>();
		player = GameManager.instance.player.transform;

		foreach (var point in exitPoints)
		{
			point.GetComponent<MeshRenderer>().enabled = false;
			point.GetComponent<SphereCollider>().enabled = false;
		}
	}

	public override void Interaction()
	{
		base.Interaction();

		GetIntoTheCar();
	}

	private void GetIntoTheCar()
	{
		ControlsManager.instance.SwitchToCarControls();
		carHealthController.UpdateCarHealthUI();
		car.ActivateCar(true);

		defaultPlayerScale = player.localScale.x;

		player.localScale = new Vector3(0.01f, 0.01f, 0.01f);
		player.transform.parent = transform;
		player.transform.localPosition = Vector3.up / 2;

		CameraManager.instance.ChangeCameraTarget(transform, 12, 0.5f);
	}

	public void GetOutOfTheCar()
	{
		if (car.carActivate == false)
			return;

		car.ActivateCar(false);
		player.parent = null;
		player.position = GetExitPoint();
		player.localScale = new Vector3(defaultPlayerScale, defaultPlayerScale, defaultPlayerScale);

		ControlsManager.instance.SwitchToCharacterControls();
		Player_AimController aim = GameManager.instance.player.aim;

		CameraManager.instance.ChangeCameraTarget(aim.GetAimCameraTarget(), 8.5f);
	}

	private Vector3 GetExitPoint()
	{
		for (int i = 0; i < exitPoints.Length; i++)
		{
			if (IsExitClear(exitPoints[i].position))
				return exitPoints[i].position;
		}

		return exitPoints[0].position;
	}

	private bool IsExitClear(Vector3 point)
	{
		Collider[] colliders = Physics.OverlapSphere(point, exitCheckRadius, ~whatToIgnoreForExit);

		return colliders.Length == 0;
	}

	private void OnDrawGizmos()
	{
		if (exitPoints.Length > 0)
		{
			foreach (var point in exitPoints)
			{
				Gizmos.DrawWireSphere(point.position, exitCheckRadius);
			}
		}
	}
}
