using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGenerator : Building {

	[SerializeField] private string resource;
	[SerializeField] private int amount;

	protected override ResourceNode GenerateNode() {
		return new ResourceGeneratorNode(Position, resource, amount);
	}

}
