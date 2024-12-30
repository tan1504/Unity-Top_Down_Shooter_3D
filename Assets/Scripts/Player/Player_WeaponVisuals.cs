using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Player_WeaponVisuals : MonoBehaviour
{
	private Player player;
	private Animator animator;
	private Transform currentGun;

	[SerializeField] private WeaponModel[] weaponModels;
	[SerializeField] private BackupWeaponModel[] backupWeaponModels;

	[Header("Rig")]
	[SerializeField] private float rigWeightIncreaseRate;
	private bool shouldIncreased_RigWeight;
	private Rig rig;

	[Header("Left Hand IK")]
	[SerializeField] private float leftHandIK_IncreaseRate;
	private bool shouldBeIncreased_LeftHandIK;
	[SerializeField] private TwoBoneIKConstraint leftHandIK;
	[SerializeField] private Transform leftHandIK_Target; // Target of Left hand

	private void Start()
	{
		player = GetComponent<Player>();
		animator = GetComponentInChildren<Animator>();
		rig = GetComponentInChildren<Rig>();
		weaponModels = GetComponentsInChildren<WeaponModel>(true);
		backupWeaponModels = GetComponentsInChildren<BackupWeaponModel>(true);
	}

	private void Update()
	{
		UpdateRigWeight();
		UpdateLeftHandIKWeight();
	}

	public WeaponModel CurrentWeaponModel()
	{
		WeaponModel weaponModel = null;
		WeaponType weaponType = player.weaponController.CurrentWeapon().weaponType;

		for (int i = 0; i < weaponModels.Length; i++)
		{
			if (weaponModels[i].weaponType == weaponType) 
				weaponModel = weaponModels[i];
		}

		return weaponModel;
	}

	public void PlayReloadAnimation()
	{
		float reloadSpeed = player.weaponController.CurrentWeapon().reloadSpeed;

		animator.SetFloat("ReloadSpeed", reloadSpeed);
		animator.SetTrigger("Reload");
		SetRigWeightZero();
	}

	public void PlayWeaponEquipAnimation()
	{
		EquipType equipType = CurrentWeaponModel().equipAnimationType;
		float equipSpeed = player.weaponController.CurrentWeapon().equipSpeed;

		SetLeftHandIKWeightZero();
		SetRigWeightZero();
		animator.SetTrigger("EquipWeapon");
		animator.SetFloat("EquipType", ((float)equipType));
		animator.SetFloat("EquipSpeed", equipSpeed);
	}

	public void SwitchOnCurrentWeaponModel()
	{
		int animationIndex = ((int)CurrentWeaponModel().holdType);

		SwitchOffWeaponModels();
		SwitchOffBackupWeaponModels();

		if(player.weaponController.HasOnlyOneWeapon() == false)
			SwitchOnBackupWeaponModel();

		SwitchAnimatorLayer(animationIndex);
		CurrentWeaponModel().gameObject.SetActive(true);
		AttachLeftHand();
	}

	public void SwitchOffWeaponModels()
	{
        for (int i = 0; i < weaponModels.Length; i++)
        {
			weaponModels[i].gameObject.SetActive(false);
        }
    }

	private void SwitchOffBackupWeaponModels()
	{
		foreach (BackupWeaponModel backupModel in backupWeaponModels)
		{
			backupModel.Activate(false);
		}
	}

	public void SwitchOnBackupWeaponModel()
	{
		SwitchOffBackupWeaponModels();

		BackupWeaponModel backHangWeapon = null;
		BackupWeaponModel highHangWeapon = null;
		BackupWeaponModel leftShouderWeapon = null;
		BackupWeaponModel rightShouderWeapon = null;


        foreach (BackupWeaponModel backupModel in backupWeaponModels)
        {
			if (backupModel.weaponType == player.weaponController.CurrentWeapon().weaponType)
				continue;

            if (player.weaponController.WeaponInSlots(backupModel.weaponType) != null)
            {
                if (backupModel.HangTypeIs(HangType.LowBackHang))
					backHangWeapon = backupModel;

				if (backupModel.HangTypeIs(HangType.HighBackHang))
					highHangWeapon = backupModel;

				if (backupModel.HangTypeIs(HangType.LeftShoulderHang))
					leftShouderWeapon= backupModel;

				if (backupModel.HangTypeIs(HangType.RightShoulderHang))
					rightShouderWeapon= backupModel;
            }
        }

		backHangWeapon?.Activate(true);
		highHangWeapon?.Activate(true);
		leftShouderWeapon?.Activate(true);
		rightShouderWeapon?.Activate(true);
    }

	private void SwitchAnimatorLayer(int layerIndex)
	{
        for (int i = 0; i < animator.layerCount; i++)
        {
			animator.SetLayerWeight(i, 0);
        }

		animator.SetLayerWeight(layerIndex, 1);
    }

	#region Animation Rigging Methods

	private void AttachLeftHand()
	{
		Transform targetTransform = CurrentWeaponModel().holdPoint;

		leftHandIK_Target.localPosition = targetTransform.localPosition;
		leftHandIK_Target.localRotation = targetTransform.localRotation;
	}

	private void UpdateLeftHandIKWeight()
	{
		if (shouldBeIncreased_LeftHandIK)
		{
			leftHandIK.weight += leftHandIK_IncreaseRate * Time.deltaTime;
			if (leftHandIK.weight >= 1)
			{
				shouldBeIncreased_LeftHandIK = false;
			}
		}
	}

	private void UpdateRigWeight()
	{
		if (shouldIncreased_RigWeight)
		{
			rig.weight += rigWeightIncreaseRate * Time.deltaTime;
			if (rig.weight >= 1)
				shouldIncreased_RigWeight = false;
		}
	}

	public void MaximizeRigWeight() => shouldIncreased_RigWeight = true;

	public void MaximizeLeftHandIKWeight() => shouldBeIncreased_LeftHandIK = true;

	public void SetRigWeightZero() => rig.weight = 0f;

	public void SetLeftHandIKWeightZero() => leftHandIK.weight = 0f;

	#endregion

	/* private void CheckWeaponSwitch()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			SwitchGunOn();
			SwitchAnimatorLayer(1);
			PlayWeaponGrabAnimation(GrabType.BackGrab);
		}

		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			SwitchGunOn();
			SwitchAnimatorLayer(1);
			PlayWeaponGrabAnimation(GrabType.BackGrab);
		}

		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			SwitchGunOn();
			SwitchAnimatorLayer(1);
			PlayWeaponGrabAnimation(GrabType.BackGrab);
		}

		if (Input.GetKeyDown(KeyCode.Alpha4))
		{
			SwitchGunOn();
			SwitchAnimatorLayer(2);
			PlayWeaponGrabAnimation(GrabType.BehindShoulderGrab);
		}

		if (Input.GetKeyDown(KeyCode.Alpha5))
		{
			SwitchGunOn();
			SwitchAnimatorLayer(3);
			PlayWeaponGrabAnimation(GrabType.BehindShoulderGrab);
		}
	}*/
}
