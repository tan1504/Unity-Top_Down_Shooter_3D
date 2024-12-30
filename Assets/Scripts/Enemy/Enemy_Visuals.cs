using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public enum Enemy_MeleeWeaponType { OneHand,  Throw,  Unarmed }
public enum Enemy_RangeWeaponType { Pistol, Revolver, AutoRifle, Shotgun, Sniper, Random}

public class Enemy_Visuals : MonoBehaviour
{
	public GameObject currentWeaponModel { get; private set; }
    public GameObject grenadeModel;

    [Header("Corruption Visuals")]
    [SerializeField] private GameObject[] corruptionCrystals;
    [SerializeField] private int corruptionAmount;

    [Header("Color")]
    [SerializeField] private Texture[] colorTextures;
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;

    [Header("Rig References")]
    [SerializeField] private Transform leftHandIK;
    [SerializeField] private Transform leftElbowIK;
    //[SerializeField] private Rig rig;
    [SerializeField] private TwoBoneIKConstraint leftHandIKConstraint;
    [SerializeField] private MultiAimConstraint weaponAimConstraint;

    private float leftHandTargetWeight;
    private float weaponAimTargetWeight;
    private float rigChangeRate;

    private void Update()
    {
        if (leftHandIKConstraint != null)
            leftHandIKConstraint.weight = AdjustIKWeight(leftHandIKConstraint.weight, leftHandTargetWeight);

        if (weaponAimConstraint != null)
            weaponAimConstraint.weight = AdjustIKWeight(weaponAimConstraint.weight, weaponAimTargetWeight);
    }

    public void EnableGrenadeModel(bool active) => grenadeModel?.SetActive(active);

    public void EnableWeaponModel(bool active)
	{
		currentWeaponModel?.gameObject.SetActive(active);
	}

    public void EnableSecondaryWeaponModel(bool active)
    {
        FindSecondaryWeaponModel()?.SetActive(active);
    }

	public void EnableWeaponTrail(bool enable)
    {
        Enemy_WeaponModel currentWeaponScript = currentWeaponModel.GetComponent<Enemy_WeaponModel>();
        currentWeaponScript.EnableTrailEffect(enable);
    }

	public void SetupLook()
    {
        SetupRandomColor();
        SetupRandomWeapon();
        SetupRandomCorruption();
    }

    private void SetupRandomCorruption()
    {
        List<int> availableIndex = new List<int>();
        corruptionCrystals = CollectCorruptionCrystals();

        corruptionAmount = Random.Range(0, corruptionCrystals.Length);

        for (int i = 0; i < corruptionCrystals.Length; i++)
        {
            availableIndex.Add(i);
            corruptionCrystals[i].SetActive(false);
        }

        for (int i = 0;i < corruptionAmount; i++)
        {
            if (availableIndex.Count == 0)
                break;

            int randomIndex = Random.Range(0, availableIndex.Count);
            int objectIndex = availableIndex[randomIndex];

            corruptionCrystals[objectIndex].SetActive(true);
            availableIndex.RemoveAt(randomIndex);
        }
    }

    private void SetupRandomWeapon()
	{
        bool thisEnemyIsMelee = GetComponent<Enemy_Melee>() != null;
        bool thisEnemyIsRange = GetComponent<Enemy_Range>() != null;

        if (thisEnemyIsMelee)
		    currentWeaponModel = FindMeleeWeaponModel();

        if (thisEnemyIsRange)
            currentWeaponModel = FindRangeWeaponModel();

		currentWeaponModel.SetActive(true);

		OverrideAnimatorControllerIfCan();
	}

	private void SetupRandomColor()
    {
        int randomIndex = Random.Range(0, colorTextures.Length);

        Material newMat = new Material(skinnedMeshRenderer.material);

        newMat.mainTexture = colorTextures[randomIndex];

        skinnedMeshRenderer.material = newMat;
    }

	private GameObject FindMeleeWeaponModel()
	{
        Enemy_WeaponModel[]  weaponModels = GetComponentsInChildren<Enemy_WeaponModel>(true);
        Enemy_MeleeWeaponType weaponType = GetComponent<Enemy_Melee>().weaponType;
		List<Enemy_WeaponModel> filteredWeaponModels = new List<Enemy_WeaponModel>();   // (****) empty list

		foreach (var weaponModel in weaponModels)
		{
			if (weaponModel.weaponType == weaponType)
				filteredWeaponModels.Add(weaponModel);
		}

		//if (filteredWeaponModels.Count == 0)    // This code helps avoid bugs if filteredWeaponModels is null  (****)
		//	return null;

		int randomWeaponIndex = Random.Range(0, filteredWeaponModels.Count);

		return filteredWeaponModels[randomWeaponIndex].gameObject;
	}

	private GameObject FindRangeWeaponModel()
	{
        Enemy_RangeWeaponModel[] weaponModels = GetComponentsInChildren<Enemy_RangeWeaponModel>(true);
        Enemy_RangeWeaponType weaponType = GetComponent<Enemy_Range>().weaponType;

        foreach (var weaponModel in weaponModels)
        {
            if (weaponModel.weaponType == weaponType)
            {
                SwitchAnimatorLayer(((int)weaponModel.weaponHoldType));
                SetupLeftHandIK(weaponModel.leftHandTarget, weaponModel.leftElbowTarget);
                return weaponModel.gameObject;
            }
        }

        Debug.Log("No range weaponmodel found");
        return null;
	}
     
	private GameObject[] CollectCorruptionCrystals()
	{
		Enemy_CorruptionCrystal[] crystalComponents = GetComponentsInChildren<Enemy_CorruptionCrystal>(true);
		GameObject[] corruptionCrystals = new GameObject[crystalComponents.Length];

		for (int i = 0; i < crystalComponents.Length; i++)
		{
			corruptionCrystals[i] = crystalComponents[i].gameObject;
		}

        return corruptionCrystals;
	}

    private GameObject FindSecondaryWeaponModel()
    {
        Enemy_SecondaryRangeWeaponModel[] weaponModels = GetComponentsInChildren<Enemy_SecondaryRangeWeaponModel>(true);
        Enemy_RangeWeaponType weaponType = GetComponentInParent<Enemy_Range>().weaponType;

        foreach (var weaponModel in weaponModels)
        {
            if (weaponModel.weaponType == weaponType)
                return weaponModel.gameObject;
        }

        return null;
    }

	private void OverrideAnimatorControllerIfCan()
	{
		AnimatorOverrideController overrideController = 
            currentWeaponModel.GetComponent<Enemy_WeaponModel>()?.overrideController;

        if (overrideController != null)
        {
            GetComponentInChildren<Animator>().runtimeAnimatorController = overrideController;
        }
	}

	private void SwitchAnimatorLayer(int layerIndex)
	{ 
        Animator anim = GetComponentInChildren<Animator>();

		for (int i = 0; i < anim.layerCount; i++)
		{
			anim.SetLayerWeight(i, 0);
		}

		anim.SetLayerWeight(layerIndex, 1);
	}

    public void EnableIK(bool enableLeftHand, bool enableAim, float changeRate = 10)
    {
        //rig .weight = enable ? 1 : 0; 
        rigChangeRate = changeRate;
        leftHandTargetWeight = enableLeftHand ? 1 : 0;
        weaponAimTargetWeight = enableAim ? 1 : 0;
    }

    private void SetupLeftHandIK(Transform leftHandTarget, Transform leftElbowTarget)
    {
        leftHandIK.localPosition = leftHandTarget.localPosition;
        leftHandIK.localRotation = leftHandTarget.localRotation;

        leftElbowIK.localPosition = leftElbowTarget.localPosition;
        leftElbowIK.localRotation = leftElbowTarget.localRotation;
    }

    private float AdjustIKWeight(float currentWeight, float targetWeight)
    {
        if (Mathf.Abs(currentWeight - targetWeight) > 0.05f)
            return Mathf.Lerp(currentWeight, targetWeight, rigChangeRate * Time.deltaTime);
        else
            return targetWeight;   
    }
}
