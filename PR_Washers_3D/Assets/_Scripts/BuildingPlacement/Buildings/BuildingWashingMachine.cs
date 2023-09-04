using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingWashingMachine : Building {

	public class WashingMachineNode : ResourceNode {

		public WashingMachineNode(Vector2Int position) : base(position) { /* MT */ }

		public override int GetMaxAcceptableResource(string resource) {
			if (resource == "dirty_laundry") return 1;

			return 0;
		}

		public override void OnTick() {
			if (Input.ContainsKey("dirty_laundry") && Input["dirty_laundry"] >= 10) {
				Input["dirty_laundry"] -= 10;
				AddToOutput("wet_laundry", 10);
			}
		}

		public override bool IsNodeActive() {
			return Input.ContainsKey("dirty_laundry") && Input["dirty_laundry"] >= 10;
		}
	}

	protected override ResourceNode GenerateNode() {
		return new WashingMachineNode(Position);
	}

}
