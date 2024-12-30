using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_WeaponController : MonoBehaviour
{
	private const float REFERENCE_BULLET_SPEED = 20f;
	// This is the default speed from which our mass formula is derived
	[SerializeField] private LayerMask whatIsAlly;
	[Space]
	private Player player;
	private Animator animator;

	[SerializeField] private List<WeaponData_SO> defaultWeaponData;
	[SerializeField] private Weapon currentWeapon;
	private bool weaponReady;

	[Header("Bullet Details")]
	[SerializeField] private float bulletImpactForce;
	[SerializeField] private GameObject bulletPrefab;
	[SerializeField] private float bulletSpeed;

	[SerializeField] private Transform weaponHolder;

	[Header("Inventory")]
	[SerializeField] private int maxWeaponSlots = 2;
	[SerializeField] private List<Weapon> weaponSlots;

	[SerializeField] private GameObject weaponPickupPrefab;

	private bool isShooting;

	private void Start()
	{
		player = GetComponent<Player>();
		animator = GetComponentInChildren<Animator>();
		AssignInputEvents();
	}

	private void Update()
	{
		if (isShooting) 
			Shoot();
	}

	#region Slots Management - Pickup/Equip/Drop/Ready Weapon

	public void SetDefaultWeapon(List<WeaponData_SO> newWeponData)
	{
		defaultWeaponData = new List<WeaponData_SO>(newWeponData);
		weaponSlots.Clear();

		foreach(WeaponData_SO weaponData in defaultWeaponData)
		{
			PickupWeapon(new Weapon(weaponData));
		}

		EquipWeapon(0);
	} 

	private void EquipWeapon(int index)
	{
		if (index >= weaponSlots.Count)
			return;

		SetWeaponReady(false);

		currentWeapon = weaponSlots[index];
		player.weaponVisuals.PlayWeaponEquipAnimation();

		//CameraManager.instance.ChangeCameraDistance(currentWeapon.cameraDistance); 

		UpdateWeaponUI();
	}

	private void DropWeapon()
	{
		if (HasOnlyOneWeapon())
			return;
		CreateWeaponOnTheGround();

		weaponSlots.Remove(currentWeapon);
		EquipWeapon(0);
	}

	private void CreateWeaponOnTheGround()
	{
		GameObject droppedWeapon = ObjectPool.instance.GetObject(weaponPickupPrefab, transform);
		droppedWeapon.GetComponent<Pickup_Weapon>()?.SetupPickupWeapon(currentWeapon, transform);
	}

	public void PickupWeapon(Weapon newWeapon)
	{

        if (WeaponInSlots(newWeapon.weaponType) != null)
        {
			WeaponInSlots(newWeapon.weaponType).totalBulletReserve += newWeapon.bulletsInMagazine;
			return;
        }

        if (weaponSlots.Count >= maxWeaponSlots && newWeapon.weaponType != currentWeapon.weaponType)
		{
			int weaponIndex = weaponSlots.IndexOf(currentWeapon);

			player.weaponVisuals.SwitchOffWeaponModels();
			weaponSlots[weaponIndex] = newWeapon;

			CreateWeaponOnTheGround();
			EquipWeapon(weaponIndex);
			return;
		}


		weaponSlots.Add(newWeapon);
		player.weaponVisuals.SwitchOnBackupWeaponModel();

		UpdateWeaponUI();
	}

	public void SetWeaponReady(bool ready)
	{
		weaponReady = ready;

		if (ready)
			player.sound.weaponReady.Play();
	}
	public bool WeaponReady() => weaponReady;

	#endregion

	public void UpdateWeaponUI()
	{
		UI.instance.inGameUI.UpdateWeaponUI(weaponSlots, currentWeapon);
	}

	private IEnumerator BurstFire()
	{
		SetWeaponReady(false);
		 
        for (int i = 1; i <= currentWeapon.bulletsPerShot; i++)
        {
			FireSingleBullet();

			yield return new WaitForSeconds(currentWeapon.burstFireDelay);

			if (i >= currentWeapon.bulletsPerShot)
				SetWeaponReady(true);
        }
    }

	private void Shoot()
	{
		if (WeaponReady() == false)
			return;

		if (currentWeapon.CanShoot() == false)
			return;

		animator.SetTrigger("Fire");

		if (currentWeapon.shootType == ShootType.Single)
			isShooting = false;

		if (currentWeapon.BurstActivated() == true)
		{
			Debug.Log("Shotgun has shot.........");
			StartCoroutine(BurstFire());
			return;
		}

		FireSingleBullet();
		TriggerEnemyDodge();
	}

	private void FireSingleBullet()
	{
		currentWeapon.bulletsInMagazine--;
		UpdateWeaponUI();
		player.weaponVisuals.CurrentWeaponModel().fireSFX.Play();

		GameObject newBullet = ObjectPool.instance.GetObject(bulletPrefab, GunPoint());

		newBullet.transform.rotation = Quaternion.LookRotation(GunPoint().forward);

		Rigidbody rbNewBullet = newBullet.GetComponent<Rigidbody>();

		Bullet bulletScript = newBullet.GetComponent<Bullet>();
		bulletScript.BulletSetup(whatIsAlly, currentWeapon.bulletDamage, currentWeapon.gunDistance, bulletImpactForce);	

		Vector3 bulletDirection = currentWeapon.ApplySpread(BulletDirection());

		rbNewBullet.mass = REFERENCE_BULLET_SPEED / bulletSpeed;
		rbNewBullet.velocity = bulletDirection * bulletSpeed;
	}

	private void Reload()
	{
		SetWeaponReady(false);
		player.weaponVisuals.PlayReloadAnimation();

		player.weaponVisuals.CurrentWeaponModel().reloadSFX.Play();
		// We do actually refill of bullets in Player_AnimationEvents;
		// We UpdateWeaponUI in Player_AnimationEvents.
	}

	public Vector3 BulletDirection()
	{
		Transform aim = player.aim.Aim();

		Vector3 direction = (aim.position - GunPoint().position).normalized;
		
		if (player.aim.CanAimPrecisly() == false && player.aim.Target() == null)
			direction.y = 0;

		return direction;
	}

	public bool HasOnlyOneWeapon() => weaponSlots.Count <= 1;

	public Weapon WeaponInSlots(WeaponType type)
	{
        foreach (Weapon weapon in weaponSlots)
        {
			if (weapon.weaponType == type)
				return weapon;
        }

		return null;
    }

	public Weapon CurrentWeapon() => currentWeapon;

	public Transform GunPoint() => player.weaponVisuals.CurrentWeaponModel().gunPoint;

	private void TriggerEnemyDodge()
	{
		Vector3 rayOrigin = GunPoint().position;
		Vector3 rayDirection = BulletDirection();

		if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hit, Mathf.Infinity))
		{
			Enemy_Melee enemy_Melee = hit.collider.gameObject.GetComponentInParent<Enemy_Melee>();

			if (enemy_Melee != null)
				enemy_Melee.ActivateDodgeRoll();
		}
	}

	#region Input Events

	private void AssignInputEvents()
	{
		PlayerControls controls = player.controls;

		controls.Character.Fire.performed += context => isShooting = true;
		controls.Character.Fire.canceled += context => isShooting = false;

		controls.Character.EquipSlot1.performed += context =>
		{
			if (weaponReady == false)
				return;
			EquipWeapon(0);
		};
		controls.Character.EquipSlot2.performed += context =>
		{
			if (weaponReady == false)
				return;
			EquipWeapon(1);
		};
		controls.Character.EquipSlot3.performed += context =>
		{
			if (weaponReady == false)
				return;
			EquipWeapon(2);
		};
		controls.Character.EquipSlot4.performed += context =>
		{
			if (weaponReady == false)
				return;
			EquipWeapon(3);
		};
		controls.Character.EquipSlot5.performed += context =>
		{
			if (weaponReady == false)
				return;
			EquipWeapon(4);
		};

		controls.Character.DropCurrentWeapon.performed += context => DropWeapon();

		controls.Character.Reload.performed += context =>
		{
            if (currentWeapon.CanReload() && WeaponReady())
				Reload();
		};

		controls.Character.ToggleWeaponMode.performed += context => currentWeapon.ToggleBurst();
	}

	#endregion

	//private void OnDrawGizmos()
	//{
	//	Gizmos.DrawLine(weaponHolder.position, weaponHolder.position + weaponHolder.forward * 25);
	//	Gizmos.color = Color.red;

	//	Gizmos.DrawLine(weaponHolder.position, aim.position + BulletDirection() * 25);
	//}
}
