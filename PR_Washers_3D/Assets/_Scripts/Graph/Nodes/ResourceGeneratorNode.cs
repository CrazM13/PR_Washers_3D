using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGeneratorNode : ResourceNode {

	private string resourceToGen;
	private int amountToGen;

	public ResourceGeneratorNode(Vector2Int position, string resource, int amount) : base(position) {
		this.resourceToGen = resource;
		this.amountToGen = amount;
	}

	public override int GetMaxAcceptableResource(string resource) {
		return 0;
	}

	public override bool IsNodeActive() {
		return true;
	}

	public override void OnTick() {
		AddToOutput(resourceToGen, amountToGen);
	}

}
