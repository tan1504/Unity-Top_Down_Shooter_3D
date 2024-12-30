using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Interaction : MonoBehaviour
{
    private List<Interactable> interactables = new List<Interactable>();

    private Interactable closestInteractable;

	private void Start()
	{
		Player player = GetComponent<Player>();

        player.controls.Character.Interaction.performed += context => InteractWithClosest();
	}

	private void InteractWithClosest()
    {
        closestInteractable?.Interaction();
        interactables.Remove(closestInteractable);
        UpdateClosestInteractable();
    }

    public void UpdateClosestInteractable()
    {
        closestInteractable?.HighlightActive(false);

        closestInteractable = null;

        float closestDistance = float.MaxValue;

        foreach (Interactable item in interactables)
        {
            float distance = Vector3.Distance(transform.position, item.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestInteractable = item;
            }
        }

        closestInteractable?.HighlightActive(true);
    }

    public List<Interactable> GetInteractables() => interactables;
}
