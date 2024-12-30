using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_SelectedWeaponWindow : MonoBehaviour
{
    public WeaponData_SO weaponData;

    [SerializeField] private Image weaponIcon;
    [SerializeField] private TextMeshProUGUI weaponInfo;

	private void Start()
	{
        weaponData = null;
		UpdateSlotInfo(null);
	}

    public void SetWeaponSlot(WeaponData_SO newWeaponData)
    {
        weaponData = newWeaponData;
        UpdateSlotInfo(weaponData);
    }

	public void UpdateSlotInfo(WeaponData_SO weaponData)
    {
        if (weaponData == null)
        {
            weaponIcon.color = Color.clear;
            weaponInfo.text = "Select a weapon...";
            return;
        }

        weaponIcon.color = Color.white;
        weaponIcon.sprite = weaponData.weaponIcon;
        weaponInfo.text = weaponData.weaponInfo;
    }

    public bool IsEmpty() => weaponData == null;
}
