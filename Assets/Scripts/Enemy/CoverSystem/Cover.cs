using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cover : MonoBehaviour
{
	private Transform player;

    [Header("Cover Points")]
    [SerializeField] private GameObject coverPointPrefab;
    [SerializeField] private List<CoverPoint> coverPoints = new List<CoverPoint>();
    [SerializeField] private float xOffset = 1;
    [SerializeField] private float yOffset = 0.2f;
    [SerializeField] private float zOffset = 1;

	private void Start()
	{
		GenerateCoverPoints();

		player = FindObjectOfType<Player>().transform;
	}

	private void GenerateCoverPoints()
	{
		Vector3[] localCoverPoints = {
			new Vector3 (0, yOffset, zOffset),		// Front
			new Vector3 (0, yOffset, -zOffset),		// Back
			new Vector3 (xOffset, yOffset, 0),		// Right
			new Vector3 (-xOffset, yOffset, 0)		// Left
		};

		foreach (Vector3 localPoint in localCoverPoints)
		{
			Vector3 worldPoint = transform.TransformPoint (localPoint);
			CoverPoint coverPoint = 
				Instantiate(coverPointPrefab, worldPoint, Quaternion.identity, transform).GetComponent<CoverPoint>();

			coverPoints.Add (coverPoint);	 
		}
	}

	public List<CoverPoint> GetValidCoverPoints(Transform enemy)
	{
		List<CoverPoint > validCoverPoints = new List<CoverPoint>();

		foreach (CoverPoint coverPoint in coverPoints)
		{
			if (IsValidCoverPoint (coverPoint, enemy))
				validCoverPoints.Add (coverPoint);
		}

		return validCoverPoints;
	}

	private bool IsValidCoverPoint(CoverPoint coverPoint, Transform enemy)
	{
		if (coverPoint.occupied)
			return false;

		if (IsFurtherestFromPlayer(coverPoint) == false)
			return false;

		if (IsCoverCloseToPlayer(coverPoint))
			return false;

		if (IsCoverBehindPlayer(coverPoint, enemy))
			return false;

		if (IsCoverCloseToLastCover(coverPoint, enemy))
			return false;

		return true;
	}

	private bool IsFurtherestFromPlayer(CoverPoint coverPoint)
	{
		CoverPoint furtherestPoint = null;
		float furtherestDistance = 0;

		foreach (CoverPoint point in coverPoints)
		{
			float distance = Vector3.Distance(point.transform.position, player.position);	

			if (distance > furtherestDistance)
			{
				furtherestDistance = distance;
				furtherestPoint = point;
			}
		}

		return furtherestPoint == coverPoint;
	}

	private bool IsCoverBehindPlayer(CoverPoint coverPoint, Transform enemy)
	{
		float distanceToPlayer = Vector3.Distance(coverPoint.transform.position, player.position);
		float distanceToEnemy = Vector3.Distance(coverPoint.transform.position, enemy.position);

		return distanceToPlayer < distanceToEnemy;
	}

	private bool IsCoverCloseToPlayer(CoverPoint coverPoint)
	{
		return Vector3.Distance(coverPoint.transform.position, player.position) < 2;
	}	

	private bool IsCoverCloseToLastCover(CoverPoint coverPoint, Transform enemy)
	{
		CoverPoint lastCover = enemy.GetComponent<Enemy_Range>().currentCover;

		return lastCover != null &&	Vector3.Distance(coverPoint.transform.position, lastCover.transform.position) < 3;
	}
}
