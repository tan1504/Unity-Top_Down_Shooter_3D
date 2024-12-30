using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Last defence - Mission", menuName = "Missions/Defence - Mission")]

public class Mission_LastDefence : Mission
{
	public bool defenceBegun = false;

	[Header("Cooldown and duration")]
	public float defenceDuration = 120;
	private float defenceTimer;
	public float waveCooldown = 15;
	private float waveTimer;

	[Header("Respawn Details")]
	public int amountOfRespawnPoints = 2;
	public List<Transform> respawnPoints;
	private Vector3 defencePoint;
	[Space]

	public int enemiesPerWave;
	public GameObject[] possibleEnemies;

	private string defenceTimerText;

	private void OnEnable()
	{
		defenceBegun = false;
	}

	public override void StartMission()
	{
		defencePoint = FindObjectOfType<MissionEnd_Trigger>().transform.position;
		respawnPoints = new List<Transform>(ClosestPoints(amountOfRespawnPoints));
		
		UI.instance.inGameUI.UpdateMissionInfor("Get to the evacuation point right now!");
	}

	public override bool MissionCompleted()
	{
		if (defenceBegun == false)
		{
			StartDefenceEvent();
			return false;
		}

		return defenceTimer < 0;
	}

	public override void UpdateMission()
	{
		if (defenceBegun == false)
			return;

		defenceTimer -= Time.deltaTime;
		if (defenceTimer > 0)
			waveTimer -= Time.deltaTime;

		if (waveTimer < 0)
		{
			CreateNewEnemies(enemiesPerWave);
			waveTimer = waveCooldown;
		}

		defenceTimerText = System.TimeSpan.FromSeconds(defenceTimer).ToString("mm':'ss");

		string missionText = "Defend youself till plane is ready to take off.";
		string missionDetails = "Time left: " + defenceTimerText;
		UI.instance.inGameUI.UpdateMissionInfor(missionText, missionDetails);
	}

	private void StartDefenceEvent()
	{
		waveTimer = 0.5f;
		defenceTimer = defenceDuration;
		defenceBegun = true;
	}

	public void CreateNewEnemies(int amount)
	{
		for (int i = 0; i < amount; i++)
		{
			int randomEnemyIndex = Random.Range(0, possibleEnemies.Length);
			int randomRespawnIndex = Random.Range(0, respawnPoints.Count);

			Transform randomRespawnPoint = respawnPoints[randomRespawnIndex];
			GameObject randomEnemy = possibleEnemies[randomEnemyIndex];

			randomEnemy.GetComponent<Enemy>().aggressionRange = 100;
			
			ObjectPool.instance.GetObject(randomEnemy, randomRespawnPoint);
		}
	}

	private List<Transform> ClosestPoints(int amount)
	{
		List<Transform> closestPoints = new List<Transform>();
		List<MissionObject_EnemyRespawnPoint> allPoints =
			new List<MissionObject_EnemyRespawnPoint>(FindObjectsOfType<MissionObject_EnemyRespawnPoint>());

		while (allPoints.Count > 0 && closestPoints.Count < amount)
		{
			float shortestDistance = float.MaxValue;
			MissionObject_EnemyRespawnPoint closestPoint = null;

			foreach (var point in allPoints)
			{
				float distance = Vector3.Distance(point.transform.position, defencePoint);

				if (distance < shortestDistance)
				{
					shortestDistance = distance;
					closestPoint = point;
				}
			}

			if (closestPoints != null)
			{
				closestPoints.Add(closestPoint.transform);
				allPoints.Remove(closestPoint);
			}
		}

		return closestPoints;
	}
}
