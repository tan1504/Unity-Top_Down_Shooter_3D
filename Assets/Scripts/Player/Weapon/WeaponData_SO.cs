using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon System/Weapon Data")]
public class WeaponData_SO : ScriptableObject
{
    public string weaponName;

	[Header("Bullet")]
	public int bulletDamage;

	[Header("Magazine Details")]
	public int bulletsInMagazine;
	public int magazineCapacity;
	public int totalBulletReserve;

	[Header("Regular Shot")]
	public ShootType shotType;
	public int bulletsPerShot = 1;
	public float fireRate;

	[Header("Burst Shot")]
	public bool burstAvailable;
	public bool burstActive;
	public int burstBulletsPerShot;
	public float burstFireRate;
	public float burstFireDelay = 0.1f;

	[Header("Weapon Spread")]
    public float baseSpread;
    public float maximumSpread;
    public float spreadIncreaseRate = 0.15f;

    [Header("Weapon Generics")]
    public WeaponType weaponType;
	[Range(1, 3)]
	public float reloadSpeed = 1;
	[Range(1, 3)]
	public float equipSpeed = 1;
	[Range(4, 25)]
	public float gunDistance = 4;
	[Range(4, 10)]
	public float cameraDistance = 6;

	[Header("UI elements")]
	public Sprite weaponIcon;
	public string weaponInfo;
}
