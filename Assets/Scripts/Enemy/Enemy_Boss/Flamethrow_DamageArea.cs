using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrow_DamageArea : MonoBehaviour
{
    private Enemy_Boss enemy;

	private float damageCooldown;
	private float lastTimeDamaged;
	private int flameDamage;

	private void Awake()
	{
		enemy = GetComponentInParent<Enemy_Boss>();
		damageCooldown = enemy.flameDamageCooldown;
		flameDamage = enemy.flameDamage;
	}

	private void OnTriggerStay(Collider other)
	{
		if (enemy.flamethrowActive == false)
			return;

		if (Time.time - lastTimeDamaged < damageCooldown)
			return;

		IDamageable damageable = other.GetComponent<IDamageable>();

		if (damageable != null)
		{
			damageable.TakeDamge(flameDamage); 
			lastTimeDamaged = Time.time;        // Update the last time damge was appiled
			damageCooldown = enemy.flameDamageCooldown;		// For easier testing we're updating
															// cooldown everytime we damage enemy
		}
	}
}
