using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceConverterNode : ResourceNode {

	public ResourceConverterNode(Vector2Int position) : base(position) { /* MT */ }

	public override int GetMaxAcceptableResource(string resource) {
		return int.MaxValue;
	}

	public override bool IsNodeActive() {
		return false;
	}

	public override void OnTick() {
		int value = 0;

		string[] resources = new string[Input.Count];
		Input.Keys.CopyTo(resources, 0);

		foreach (string resource in resources) {
			value += Input[resource];

			Input[resource] = 0;
		}

		GameData.Money += value;
	}
}
