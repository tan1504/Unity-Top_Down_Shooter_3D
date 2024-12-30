using UnityEngine;

public enum WeaponType
{
	Pistol,
	Revolver,
	AutoRifle,
	Shotgun,
	Sniper
}

public enum ShootType
{
	Single,
	Auto
}

[System.Serializable] // Makes class visible in the inspector

public class Weapon
{
	public WeaponType weaponType;
	public int bulletDamage;

	#region Regular Mode Variables
	public ShootType shootType;
	public int bulletsPerShot { get; private set; }
	private float defaultFireRate;
	public float fireRate = 1; // bullets per second
	private float lastShootTime;
	#endregion

	#region Burst Mode Variables
	private bool burstAvailable;
	public bool burstActive;
	private int burstBulletsPerShot;
	private float burstFireRate;
	public float burstFireDelay { get; private set; }
	#endregion

	[Header("Magazine Details")]
	public int bulletsInMagazine;
	public int magazineCapacity;
	public int totalBulletReserve;

	#region Weapon Generic Infor
	public float reloadSpeed { get; private set; } // how fast character reloads weapon
	public float equipSpeed { get; private set; } // how fast character equips weapon
	public float gunDistance { get; private set; }
	public float cameraDistance { get; private set; }
	#endregion

	#region Weapon Spread Variables
	[Header("Spread Details")]
	private float baseSpread;
	private float currentSpread = 2;
	private float maximumSpread = 3;
	private float spreadIncreaseRate = 0.15f;
	private float lastSpreadUpdateTime;
	private float spreadCooldown = 1;
	#endregion

	public WeaponData_SO weaponData {  get; private set; } // serves as default weapon data

	public Weapon(WeaponData_SO weaponData)
	{
		bulletDamage = weaponData.bulletDamage;
		bulletsInMagazine = weaponData.bulletsInMagazine;
		magazineCapacity = weaponData.magazineCapacity;
		totalBulletReserve = weaponData.totalBulletReserve;

		fireRate = weaponData.fireRate;
		weaponType = weaponData.weaponType;

		bulletsPerShot = weaponData.bulletsPerShot;
		shootType = weaponData.shotType;

		burstAvailable = weaponData.burstAvailable;
		burstActive = weaponData.burstActive;
		burstBulletsPerShot = weaponData.burstBulletsPerShot;	
		burstFireRate = weaponData.burstFireRate;
		burstFireDelay = weaponData.burstFireDelay;

		baseSpread = weaponData.baseSpread;
		maximumSpread = weaponData.maximumSpread;
		spreadIncreaseRate = weaponData.spreadIncreaseRate;

		reloadSpeed = weaponData.reloadSpeed;
		equipSpeed = weaponData.equipSpeed;
		gunDistance = weaponData.gunDistance;	
		cameraDistance = weaponData.cameraDistance;

		defaultFireRate = fireRate;
		this.weaponData = weaponData;
	}

	public bool CanShoot() => HaveEnoughBullet() && ReadyToFire();

	private bool ReadyToFire()
	{
		if (Time.time > lastShootTime + 1 / fireRate)
		{
			lastShootTime = Time.time;
			return true;
		}

		return false;
	}

	#region Burst Methods

	public bool BurstActivated()
	{
		if (weaponType == WeaponType.Shotgun)
		{
			Debug.Log("Shotgun nek");
			burstFireDelay = 0;
			return true;
		}

		return burstActive;
    }

	public void ToggleBurst()
	{
		if (burstAvailable == false)
			return;

		burstActive = !burstActive;

		if (burstActive)
		{
			bulletsPerShot = burstBulletsPerShot;
			fireRate = burstFireRate;
		}
		else
		{
			bulletsPerShot = 1;
			fireRate = defaultFireRate;
		}
	}

	#endregion

	#region Spread Methods
	public Vector3 ApplySpread(Vector3 originDirection)
	{
		UpdateSpread();

		float randValue = Random.Range(-currentSpread, currentSpread);

		Quaternion spreadRotation = Quaternion.Euler(randValue, randValue / 2, randValue);

		return spreadRotation * originDirection;
	}

	private void UpdateSpread()
	{
		if (Time.time > lastSpreadUpdateTime + spreadCooldown)
			currentSpread = baseSpread;
		else
			IncreaseSpread();

		lastShootTime = Time.time;
	}

	public void IncreaseSpread()
	{
		currentSpread = Mathf.Clamp(currentSpread + spreadIncreaseRate, baseSpread, maximumSpread);
	}

	#endregion

	#region Reload Methods

	private bool HaveEnoughBullet() => bulletsInMagazine > 0;

	public bool CanReload()
	{
		if (bulletsInMagazine == magazineCapacity)
			return false;

		if (totalBulletReserve > 0)
			return true;

		return false;
	}

	public void RefillBullets()
	{
		int bulletsToReload = magazineCapacity - bulletsInMagazine;

		if (totalBulletReserve < bulletsToReload)
			bulletsToReload = totalBulletReserve;

		bulletsInMagazine += bulletsToReload;
		totalBulletReserve -= bulletsToReload;
	}

	#endregion
}
