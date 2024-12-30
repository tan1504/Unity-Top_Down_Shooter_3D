using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_DamageZone : MonoBehaviour
{
    private Car_Controller carController;

	[SerializeField] private float minSpeedToDamage = 1.5f;

    [SerializeField] private int carDamage;
	[SerializeField] private float impactForce = 150;
	[SerializeField] private float upwardMultiplier = 3;

	private void Awake()
	{
		carController = GetComponentInParent<Car_Controller>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (carController.rb.velocity.magnitude < minSpeedToDamage)
			return;	

		IDamageable damageable = other.GetComponent<IDamageable>();

		if (damageable == null)
			return;

		damageable.TakeDamge(carDamage); 

		Rigidbody rb = other.GetComponent<Rigidbody>();
		if (rb != null)
			ApplyForce(rb);
	}

	private void ApplyForce(Rigidbody rb)
	{
		rb.isKinematic = false;
		rb.AddExplosionForce(impactForce, transform.position, 3, upwardMultiplier, ForceMode.Impulse);
	}
}
