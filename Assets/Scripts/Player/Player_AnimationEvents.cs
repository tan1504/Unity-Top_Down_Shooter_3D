using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_AnimationEvents : MonoBehaviour
{
	private Player_WeaponVisuals visualController;
	private Player_WeaponController weaponController;

	private void Start()
	{
		visualController = GetComponentInParent<Player_WeaponVisuals>();
		weaponController = GetComponentInParent<Player_WeaponController>();
	}

	public void ReloadIsOver()
	{
		visualController.MaximizeRigWeight();
		weaponController.CurrentWeapon().RefillBullets();

		weaponController.SetWeaponReady(true);
		weaponController.UpdateWeaponUI();
	}

	public void ReturnWeightToZero()
	{
		visualController.MaximizeRigWeight();
		visualController.CurrentWeaponModel().reloadSFX.Stop();
		visualController.MaximizeLeftHandIKWeight();
	}

	public void WeaponEquippingIsOver()
	{ 
		weaponController.SetWeaponReady(true);
	}

	public void SwitchOnWeaponModel() => visualController.SwitchOnCurrentWeaponModel();
}
