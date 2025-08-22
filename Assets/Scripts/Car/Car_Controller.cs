using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DriveType { FrontWheelDrive, RearWheelDrive, AllWheelDrive }

[RequireComponent(typeof(Rigidbody))]
public class Car_Controller : MonoBehaviour
{
	public Car_Sound carSound {  get; private set; }

	public Rigidbody rb {  get; private set; }
	public bool carActivate {  get; private set; }
    private PlayerControls controls;
    private float moveInput;
    private float steerInput;

	[SerializeField] private LayerMask whatIsGround;

	public float speed;

	[Range(30, 60)]
	[SerializeField] private float turnSensetivity = 30;
	[Header("Car Settings")]
	[SerializeField] private DriveType driveType;
	[SerializeField] private Transform centerOfMass;
	[Range(350, 1000)]
	[SerializeField] private float carMass = 400;

	[Header("Engine Settings")]
	[SerializeField] private float currentSpeed;
	[Range(7, 12)]
	[SerializeField] private float maxSpeed = 7;
	[Range(0.5f, 10)]
	[SerializeField] private float accelerationSpeed = 2;
	[Range(1500, 5000)]
	[SerializeField] private float motorForce = 1500f;

	[Header("Brakes Settings")]
	[Range(0, 10)]
	public float frontBrakeSensetibity = 5;
	[Range(0, 10)]
	public float backBrakeSensetibity = 5;
	[Range(4000, 6000)]
	[SerializeField] private float brakePower = 5000;
	private bool isBraking;

	[Header("Drift Settings")]
	[Range(0, 1)]
	[SerializeField] private float frontDriftFactor = 0.5f; 
	[Range (0, 1)]
	[SerializeField] private float backDriftFactor = 0.5f;
	[SerializeField] private float driftDuration = 1f;
	private float driftTimer;
	private bool isDrifting;

	private Car_Wheel[] wheels;
	private UI ui;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		wheels = GetComponentsInChildren<Car_Wheel>();
		carSound = GetComponent<Car_Sound>();
		ui = UI.instance;

		controls = ControlsManager.instance.controls;
		//ControlsManager.instance.SwitchToCarControls();

		ActivateCar(false);
		AssignInputEvents();
		SetupDefaultValues();
	}

	private void SetupDefaultValues()
	{
		rb.centerOfMass = centerOfMass.localPosition;
		rb.mass = carMass;

		foreach (var wheel in wheels)
		{
			wheel.cd.mass = carMass;

			if (wheel.axelType == AxleType.Front)
				wheel.SetDefaultStiffness(frontDriftFactor);

			if (wheel.axelType == AxleType.Back)
				wheel.SetDefaultStiffness(backDriftFactor);
		}
	}

	private void Update()
	{
		if (carActivate == false) 
			return;

		speed = rb.velocity.magnitude;
		ui.inGameUI.UpdateSpeedText(Mathf.RoundToInt(speed * 10) + "km/h");

		driftTimer -= Time.deltaTime;

		if (driftTimer < 0)
			isDrifting = false;
	}

	private void FixedUpdate()
	{
		if (carActivate == false)
			return;

		ApplyTrailsOnTheGround();
		ApplyAnimationToWheels();
		ApplyDrive();
		ApplySteering();
		ApplyBrakes();
		ApplySpeedLimit();

		if (isDrifting)
			ApplyDrift();
		else
			StopDrift();
	}

	private void ApplyDrive()
	{
		currentSpeed = moveInput * accelerationSpeed * Time.deltaTime;

		float motorTorqueValue = motorForce * currentSpeed;

		foreach (var wheel in wheels)
		{
			if (driveType == DriveType.FrontWheelDrive)
			{
				if (wheel.axelType == AxleType.Front)
					wheel.cd.motorTorque = motorTorqueValue;
			}
			else if (driveType == DriveType.RearWheelDrive)
			{
				if (wheel.axelType == AxleType.Back)
					wheel.cd.motorTorque = motorTorqueValue;
			}
			else
			{
				wheel.cd.motorTorque = motorTorqueValue;
			}
		}
	}

	private void ApplySpeedLimit()
	{
		if (rb.velocity.magnitude > maxSpeed)
			rb.velocity = rb.velocity.normalized * maxSpeed;
	}

	private void ApplySteering()
	{
		foreach (var wheel in wheels)
		{
			if (wheel.axelType == AxleType.Front)
			{
				float targetSteerAngle = steerInput * turnSensetivity;
				wheel.cd.steerAngle = Mathf.Lerp(wheel.cd.steerAngle, targetSteerAngle, 0.5f);
			}
		}
	}

	private void ApplyBrakes()
	{
		

		foreach (var wheel in wheels)
		{
			bool frontBrakes = wheel.axelType == AxleType.Front;
			float brakeSensetivity = frontBrakes ? frontBrakeSensetibity : backBrakeSensetibity;

			float newBrakeTorque = brakePower * brakeSensetivity * Time.deltaTime;
			float currentBrakeTorque = isBraking ? newBrakeTorque : 0;

			wheel.cd.brakeTorque = currentBrakeTorque;
		}
	}

	private void ApplyDrift()
	{
		foreach(var wheel in wheels)
		{
			bool frontWheel = wheel.axelType == AxleType.Front;
			float driftFactor = frontWheel ? frontDriftFactor : backDriftFactor;

			WheelFrictionCurve sidewaysFriction = wheel.cd.sidewaysFriction;

			sidewaysFriction.stiffness *= (1 - driftFactor);
			wheel.cd.sidewaysFriction = sidewaysFriction;	
		}
	}

	private void StopDrift()
	{
		foreach (var wheel in wheels)
		{
			wheel.RestoreDefaultStiffness();
		}
	}

	private void ApplyAnimationToWheels()
	{
		foreach(var wheel in wheels)
		{
			Quaternion rotation;
			Vector3 position;

			wheel.cd.GetWorldPose(out position, out rotation);

			if (wheel.model != null)
			{
				wheel.model.transform.position = position;
				wheel.model.transform.rotation = rotation;
			}
		}
	}

	private void ApplyTrailsOnTheGround()
	{
		foreach (var wheel in wheels)
		{
			WheelHit hit;

			if (wheel.cd.GetGroundHit(out hit))
			{
				if (whatIsGround == (whatIsGround | (1 << hit.collider.gameObject.layer)))
				{
					wheel.trail.emitting = true;
				}
				else
				{
					wheel.trail.emitting = false;
				}
			}
			else
			{
				wheel.trail.emitting = false;
			}
		}
	}

	public void ActivateCar(bool activate)
	{
		carActivate = activate;

		if (carSound != null)
			carSound.ActivateCarSFX(activate );

		//if (carActivate)
		//	rb.constraints = RigidbodyConstraints.None;
		//else
		//	rb.constraints = RigidbodyConstraints.FreezeAll;
	}

	public void BreakTheCar()
	{
		motorForce = 0;
		isDrifting = true;
		frontDriftFactor = 0.9f;
		backDriftFactor = 0.9f;
	}

	private void AssignInputEvents()
	{
		controls.Car.Movement.performed += context =>
		{
			Vector2 input = context.ReadValue<Vector2>();

			moveInput = input.y;
			steerInput = input.x;
		};

		controls.Car.Movement.canceled += context =>
		{
			moveInput = 0;
			steerInput = 0;
		}; 

		controls.Car.Brake.performed += context =>
		{
			isBraking = true;
			isDrifting = true;
			driftTimer = driftDuration;
		};

		controls.Car.Brake.canceled += context => isBraking = false;

		controls.Car.CarExit.performed += context => GetComponent<Car_Interaction>().GetOutOfTheCar();
	}

	[ContextMenu("Focus Camera and enable")]
	public void TestThisCar()
	{
		ActivateCar(true);
		CameraManager.instance.ChangeCameraTarget(transform, 12);
	}
}
