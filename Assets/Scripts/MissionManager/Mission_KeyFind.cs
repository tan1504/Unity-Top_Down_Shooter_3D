using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Key - Mission", menuName = "Missions/Key Mission")]

public class Mission_KeyFind : Mission
{
	[SerializeField] private GameObject key;
	private bool keyFound;

	public override void StartMission()
	{
		MissionObject_Key.OnKeyPickUp += PickupKey;

		UI.instance.inGameUI.UpdateMissionInfor("Find a key-holder. Retrive the key.");

		Enemy enemy = LevelGenerator.instance.GetRandomEnemy();
		enemy.GetComponent<Enemy_DropController>()?.GiveKey(key);
		enemy.MakeEnemyVIP();
	}

	public override bool MissionCompleted()
	{
		return keyFound;
	}

	private void PickupKey()
	{
		keyFound = true;
		MissionObject_Key.OnKeyPickUp -= PickupKey;

		UI.instance.inGameUI.UpdateMissionInfor("You've got the key dude! \n Get to the evacuation point right now!");
	}
}
