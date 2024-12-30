using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct AmmoData
{
	public WeaponType weaponType;
	[Range(10, 100)] public int minAmount;
	[Range(10, 100)] public int maxAmount;
}
public enum AmmoBoxType
{
	smallBox,
	bigBox
}

public class Pickup_Ammo : Interactable  
{
	[SerializeField] private AmmoBoxType boxType;



	[SerializeField] private List<AmmoData> smallBoxAmmo;
	[SerializeField] private List<AmmoData> bigBoxAmmo;

	[SerializeField] private GameObject[] boxModels;

	private void Start()
	{
		SetupBoxModel();
	}
	
	public override void Interaction()
	{
		List<AmmoData> currentAmmoList = smallBoxAmmo;

		if (boxType == AmmoBoxType.bigBox)
			currentAmmoList = bigBoxAmmo;

		foreach (AmmoData ammo in currentAmmoList)
		{
			Weapon weapon = weaponController.WeaponInSlots(ammo.weaponType);

			AddBulletsToWeapon(weapon, GetBulletAmount(ammo));
		}

		ObjectPool.instance.ReturnObject(gameObject); 
	}

	private static void AddBulletsToWeapon(Weapon weapon, int amount)
	{
		if (weapon != null)
			weapon.totalBulletReserve += amount;
	}

	private int GetBulletAmount(AmmoData ammo)
	{
		int min = Mathf.Min(ammo.minAmount, ammo.maxAmount);
		int max = Mathf.Max(ammo.minAmount, ammo.maxAmount);

		int randonAmmoBullet = Random.Range(min, max);

		return randonAmmoBullet;
	}

	private void SetupBoxModel()
	{
		for (int i = 0; i < boxModels.Length; i++)
		{
			boxModels[i].gameObject.SetActive(false);

			if (i == ((int)boxType))
			{
				boxModels[i].gameObject.SetActive(true);
				UpdateMeshAndMaterial(boxModels[i].GetComponent<MeshRenderer>());
			}
		}
	}
}
