using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Data", menuName = "Enemy Data/Range Weapon Data")]

public class Enemy_RangeWeaponData_SO : ScriptableObject
{
    [Header("Weapon Details")]
    public Enemy_RangeWeaponType weaponType;
    public float fireRate = 1f;     // Bullets per second

    public int maxBulletsPerAttack = 1;
    public int minBulletsPerAttack = 1;

    public int minWeaponCooldown = 2;
    public int maxWeaponCooldown = 3;

    [Header("Bullet Details")]
    public int bulletDamage;
    [Space]
    public float bulletSpeed = 20;
    public float weaponSpread = 0.1f;

    public int GetBulletsPerAttack() => Random.Range(minBulletsPerAttack, maxBulletsPerAttack);
    public float GetWeaponCooldown() => Random.Range(minWeaponCooldown, maxWeaponCooldown);

	public Vector3 ApplyWeaponSpread(Vector3 originDirection)
	{
		float randValue = Random.Range(-weaponSpread, weaponSpread);

		Quaternion spreadRotation = Quaternion.Euler(randValue, randValue / 2, randValue);

		return spreadRotation * originDirection;
	}

}
