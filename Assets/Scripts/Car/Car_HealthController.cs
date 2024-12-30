using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_HealthController : MonoBehaviour, IDamageable
{
	private Car_Controller carController;

	public int maxHealth;
	public int currentHealth;

	private bool carBroken;

	private void Start()
	{
		carController = GetComponent<Car_Controller>();
		currentHealth = maxHealth;
	}

	public void UpdateCarHealthUI()
	{
		UI.instance.inGameUI.UpdateCarHealthUI(currentHealth, maxHealth);
	}

	private void ReduceHealth(int damage)
	{
		if (carBroken)
			return;

        currentHealth -= damage;

		if (currentHealth < 0)
			BreakTheCar();
	}

	private void BreakTheCar()
	{
		carBroken = true;
		carController.BreakTheCar();
	}

	public void TakeDamge(int damage)
	{
		ReduceHealth(damage);
		UpdateCarHealthUI();
	}
}
