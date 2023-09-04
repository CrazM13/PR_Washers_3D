using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceTransportNode : ResourceNode {

	private int maxResources;

	public ResourceTransportNode(Vector2Int position, int maxResources) : base(position) {
		this.maxResources = maxResources;
	}

	public override bool IsNodeActive() {
		foreach (KeyValuePair<string, int> heldResource in Input) {
			if (heldResource.Value > 0) return true;
		}

		return false;
	}

	public override int GetMaxAcceptableResource(string resource) {
		int currentLoad = 0;
		foreach (KeyValuePair<string, int> heldResource in Input) {
			currentLoad += heldResource.Value;
		}

		return maxResources - currentLoad;
	}

	public override void OnTick() {
		MoveInputToOutput();
	}
}
