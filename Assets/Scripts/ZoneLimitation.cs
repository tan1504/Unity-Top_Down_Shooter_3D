using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneLimitation : MonoBehaviour
{
    private ParticleSystem[] lines;
    private BoxCollider zoneCollider;

	private void Start()
	{
		GetComponent<MeshRenderer>().enabled = false;
		zoneCollider = GetComponent<BoxCollider>();
		lines = GetComponentsInChildren<ParticleSystem>();
		ActivateZone(false);
	}

	private void ActivateZone(bool activate)
	{
		foreach (var line in lines)
		{
            if (activate)
            {
				line.Play();
            }
			else
			{
				line.Stop();
			}
        }

		zoneCollider.isTrigger = !activate;
	}

	IEnumerator WallActivationCo()
	{
		ActivateZone(true);

		yield return new WaitForSeconds(1);

		ActivateZone(false);
	}

	private void OnTriggerEnter(Collider other)
	{
		StartCoroutine(WallActivationCo());
		Debug.Log("It's dangerous");
	}
}
