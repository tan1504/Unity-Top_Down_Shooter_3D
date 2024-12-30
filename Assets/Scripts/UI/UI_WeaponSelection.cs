using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_WeaponSelection : MonoBehaviour
{
	[SerializeField] private GameObject nextUIToSwitchOn;
    public UI_SelectedWeaponWindow[] selectionWeapon;

	[Header("Warning info")]
	[SerializeField] private TextMeshProUGUI warningText;
	[SerializeField] private float disappearingSpeed = 0.25f;
	private float currentWarningAlpha;
	private float targetWarningAlpha;

	private void Start()
	{
		selectionWeapon = GetComponentsInChildren<UI_SelectedWeaponWindow>();
	}

	private void Update()
	{
		if (currentWarningAlpha > targetWarningAlpha)
		{
			currentWarningAlpha -= disappearingSpeed * Time.deltaTime;
			warningText.color = new Color(1, 1, 1, currentWarningAlpha);
		}
	}

	public void ConfirmWeaponSelection()
	{
		if (AtLeastOneWeaponSelection())
		{
			UI.instance.SwitchTo(nextUIToSwitchOn);
			UI.instance.StartLevelGeneration();
		}
		else
			ShowWarningMessage("Select at least one weapon");
	}

	private bool AtLeastOneWeaponSelection() => SelectedWeaponData().Count > 0;

	public List<WeaponData_SO> SelectedWeaponData()
	{
		List<WeaponData_SO> selectedData = new List<WeaponData_SO>();

		foreach (UI_SelectedWeaponWindow weapon in selectionWeapon)
		{
			if (weapon.weaponData != null)
				selectedData.Add(weapon.weaponData);
		}

		return selectedData;
	}

	public UI_SelectedWeaponWindow FindEmptySlot()
	{
		for (int i = 0; i < selectionWeapon.Length; i++)
		{
			if (selectionWeapon[i].IsEmpty())
				return selectionWeapon[i];
		}

		return null;
	}

	public UI_SelectedWeaponWindow FindSlotWithWeaponOftype(WeaponData_SO weaponData)
	{
		for (int i = 0;i < selectionWeapon.Length;i++)
		{
			if (selectionWeapon[i].weaponData == weaponData)
				return selectionWeapon[i];
		}

		return null;
	}

	public void ShowWarningMessage(string message)
	{
		warningText.color = Color.white;
		warningText.text = message;

		currentWarningAlpha = warningText.color.a;
		targetWarningAlpha = 0;
	}
}
