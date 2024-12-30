using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Car delivery - Mission", menuName = "Missions/Car delivery - Mission")]

public class Mission_CarDelivery : Mission
{
	private bool carWasDelivery;

	public override void StartMission()
	{
		FindObjectOfType<MissionObject_CarDeliveryZone>(true).gameObject.SetActive(true);

		string missionText = "Find a functional vehicle.";
		string missionDetails = "Deliver it to the evacuation point.";

		UI.instance.inGameUI.UpdateMissionInfor(missionText, missionDetails);

		carWasDelivery = false;
		MissionObject_CarToDeliver.OnCarDelivery += CarDeliveryCompleted;

		Car_Controller[] cars = GameObject.FindObjectsOfType<Car_Controller>();	

		foreach (var car in cars)
		{
			car.AddComponent<MissionObject_CarToDeliver>();
		}
	}

	public override bool MissionCompleted()
	{
		return carWasDelivery;
	}

	private void CarDeliveryCompleted()
	{
		carWasDelivery = true;

		MissionObject_CarToDeliver.OnCarDelivery -= CarDeliveryCompleted;

		UI.instance.inGameUI.UpdateMissionInfor("Get to the evacuation point right now!");
	}
}
