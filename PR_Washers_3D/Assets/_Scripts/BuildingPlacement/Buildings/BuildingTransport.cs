using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingTransport : Building {

	[SerializeField, Min(0)] int maxItemsPerTick = 1;

	protected override ResourceNode GenerateNode() {
		return new ResourceTransportNode(Position, maxItemsPerTick);
	}

}
