using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameClock : MonoBehaviour {

	[SerializeField] private float tickInterval = 1;

	private float timeUntilTick = 0;

	[SerializeField, Space(20)] private UnityEvent OnTick;

	private void Start() {
		if (!GameTime.DoesChannelExist("GameClock")) GameTime.RegisterChannel("GameClock");
	}

	void Update() {
		timeUntilTick -= GameTime.GetDeltaTime("GameClock");
		if (timeUntilTick <= 0) {
			timeUntilTick = tickInterval;

			OnTick.Invoke();

			ResourceGraph.OnTick();
			BuildingMap.OnTick();
		}
	}
}
