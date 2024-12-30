using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Granade : MonoBehaviour
{
	[SerializeField] private GameObject explosionFx;
	[SerializeField] private float impactRadius;
	[SerializeField] private float upwardsMultiplier = 1;
	private float impactPower;
    private Rigidbody rb;
	private float timer;

	private LayerMask allyLayerMask;
	private bool canExplode = true;
	private int grenaseDamage;

	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
	} 

	private void Update()
	{
		timer -= Time.deltaTime;

		if (timer < 0 && canExplode)
			Explode();
	}

	private void Explode()
	{
		canExplode = false;

		PLayExplosionFx();

		HashSet<GameObject> uniqueEntities = new HashSet<GameObject>();
		Collider[] colliders = Physics.OverlapSphere(transform.position, impactRadius);

		foreach (Collider hit in colliders)
		{
			IDamageable damageable = hit.GetComponent<IDamageable>();

			if (damageable != null)
			{
				if (IsTargetValid(hit) == false)
					continue;

				GameObject rootEntity = hit.transform.root.gameObject;
				if (uniqueEntities.Add(rootEntity) == false)
					continue;

				damageable.TakeDamge(grenaseDamage); 
			}

			ApplyPhysicalForceTo(hit);
		}
	}

	private void ApplyPhysicalForceTo(Collider hit)
	{
		Rigidbody rb = hit.GetComponent<Rigidbody>();

		if (rb != null)
			rb.AddExplosionForce(impactPower, transform.position, impactRadius, upwardsMultiplier, ForceMode.Impulse);
	}

	private void PLayExplosionFx()
	{
		GameObject newFx = ObjectPool.instance.GetObject(explosionFx, transform);
		ObjectPool.instance.ReturnObject(newFx, 1);
		ObjectPool.instance.ReturnObject(gameObject);
	}

	public void SetupGranade(LayerMask allyLayerMask, Vector3 target, float timeToTarget, float countdown, float impactPower, int grenadeDamage)
	{
		canExplode = true;

		this.grenaseDamage = grenadeDamage;
		this.allyLayerMask = allyLayerMask;
		rb.velocity = CalculateLaunchVelocity(target, timeToTarget);
		timer = countdown + timeToTarget;
		this.impactPower = impactPower;
	}

	private bool IsTargetValid(Collider collider)
	{
		// If friendly fire is enabled, all colliders are valid targets

		if (GameManager.instance.friendlyFire)
			return true;

		// If collider is on allyLayer, target is not valid
		if ((allyLayerMask.value & (1 << collider.gameObject.layer)) > 0)
			return false;

		return true;
	}

	private Vector3 CalculateLaunchVelocity(Vector3 target, float timeToTarget)
	{
		Vector3 direction = target - transform.position;	
		Vector3 directionXZ = new Vector3(direction.x, 0, direction.z);

		Vector3 velocityXZ = directionXZ / timeToTarget;

		float velocityY = 
			(direction.y - (Physics.gravity.y * Mathf.Pow(timeToTarget, 2)) / 2) / timeToTarget;

		Vector3 launchVelocity = velocityXZ + Vector3.up * velocityY;

		return launchVelocity;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, impactRadius);
	}
}
