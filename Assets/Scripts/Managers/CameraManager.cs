using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

	private CinemachineVirtualCamera virtualCamera;
	private CinemachineFramingTransposer transposer;

	[Header("Camera Distance")]
	[SerializeField] private bool canChangeCameraDistance;
	[SerializeField] private float distanceChangeRate;
	[SerializeField] private float targetCameraDistance;

	private void Awake()
	{
		if (instance == null)
			instance = this;
		else
		{
			Debug.LogWarning("You had more than 1 camera");
			Destroy(gameObject);
		}

		virtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
		transposer = virtualCamera.GetComponentInChildren<CinemachineFramingTransposer>();

	}

	private void Update()
	{
		UpdateCameraDistance();
	}

	private void UpdateCameraDistance()
	{
		if (canChangeCameraDistance == false)
			return;

		float currentDistance = transposer.m_CameraDistance;

		if (Mathf.Abs(targetCameraDistance - currentDistance) > 0.001f)
		{
			transposer.m_CameraDistance =
				Mathf.Lerp(currentDistance, targetCameraDistance, distanceChangeRate * Time.deltaTime);
		}
	}

	public void ChangeCameraDistance(float distance) => targetCameraDistance = distance;

	public void ChangeCameraTarget(Transform target, float cameraDistance = 10, float newLookAheadTime = 0)
	{
		virtualCamera.Follow = target;
		transposer.m_LookaheadTime = newLookAheadTime;
		ChangeCameraDistance(cameraDistance);
	}
}
