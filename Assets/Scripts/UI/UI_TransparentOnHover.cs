using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_TransparentOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	private Dictionary<Image, Color> originalImageColors = new Dictionary<Image, Color>();
	private Dictionary<TextMeshProUGUI, Color> originalTextColor = new Dictionary<TextMeshProUGUI, Color>();

	private bool hasUIWeaponSlots;
	private Player_WeaponController weaponController;

	private void Start()
	{
		hasUIWeaponSlots = GetComponentInChildren<UI_WeaponSlot>();
		if (hasUIWeaponSlots )
			weaponController = FindObjectOfType<Player_WeaponController>();

		// Get images components anf their original colors
		foreach (var image in GetComponentsInChildren<Image>())
		{
			originalImageColors[image] = image.color;
		}

		// Get TextMeshProGUI components anf their original colors
		foreach (var text in GetComponentsInChildren<TextMeshProUGUI>())
		{
			originalTextColor[text] = text.color;
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		// Set all images to trangsparent
		foreach (var image in originalImageColors.Keys)
		{
			var color = image.color;
			color.a = 0.15f;
			image.color = color;
		}

		// Set all texts to trangsparent
		foreach (var text in originalTextColor.Keys)
		{
			var color = text.color;
			color.a = 0.15f;
			text.color = color;
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		// Restore original color for images
		foreach(var image in originalImageColors.Keys)
		{
			image.color = originalImageColors[image];
		}

		// Restore original color for text
		foreach(var text in originalTextColor.Keys)
		{
			text.color = originalTextColor[text];
		}

		weaponController?.UpdateWeaponUI();
	}
}
