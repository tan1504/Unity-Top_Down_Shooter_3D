using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AxleType { Front, Back }

[RequireComponent(typeof(WheelCollider))]
public class Car_Wheel : MonoBehaviour
{
    public AxleType axelType;
    public WheelCollider cd {  get; private set; }
	public TrailRenderer trail {  get; private set; }
	public GameObject model;

	private float defaultSideStiffness;

	private void Awake()
	{
		cd = GetComponent<WheelCollider>();
		trail = GetComponentInChildren<TrailRenderer>();

		if (model == null )
			model = GetComponentInChildren<MeshRenderer>().gameObject;
	}

	public void SetDefaultStiffness(float newValue)
	{
		defaultSideStiffness = newValue;
		RestoreDefaultStiffness();
	}

	public void RestoreDefaultStiffness()
	{
		WheelFrictionCurve sidewayFriction = cd.sidewaysFriction;

		sidewayFriction.stiffness = defaultSideStiffness;
		cd.sidewaysFriction = sidewayFriction;
	}
}
