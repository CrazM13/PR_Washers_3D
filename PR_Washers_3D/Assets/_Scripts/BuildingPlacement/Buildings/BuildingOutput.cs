using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingOutput : Building {

	protected override ResourceNode GenerateNode() {
		return new ResourceConverterNode(Position);
	}

}
