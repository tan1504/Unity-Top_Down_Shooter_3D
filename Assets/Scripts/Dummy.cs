using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour, IDamageable
{
    public int maxHealth = 100;
    public int currentHealth;

	[Header("Materials")]
	public MeshRenderer mesh;
	public Material whiteMat;
	public Material redMat;
	[Space]
	public float refreshCooldown;
	private float lastTimeDamaged;

	private void Start()
	{
		Refresh();
	}

	private void Update()
	{
		if (Time.time > lastTimeDamaged + refreshCooldown || Input.GetKeyDown(KeyCode.B))
			Refresh();
	}

	private void Refresh()
	{
		currentHealth = maxHealth;
		mesh.sharedMaterial = whiteMat;
	}

	public void TakeDamge(int damage)
	{
		lastTimeDamaged = Time.time;		 
		currentHealth -= damage;

		if (currentHealth <= 0)
			Die();
	}

	private void Die()
	{
		mesh.sharedMaterial = redMat;
	}
}
