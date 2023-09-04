using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDryer : Building {

	public class DryerNode : ResourceNode {

		public DryerNode(Vector2Int position) : base(position) { /* MT */ }

		public override int GetMaxAcceptableResource(string resource) {
			if (resource == "wet_laundry") return 1;

			return 0;
		}

		public override void OnTick() {
			if (Input.ContainsKey("wet_laundry") && Input["wet_laundry"] >= 10) {
				Input["wet_laundry"] -= 10;
				AddToOutput("laundry", 10);
			}
		}

		public override bool IsNodeActive() {
			return Input.ContainsKey("wet_laundry") && Input["wet_laundry"] >= 10;
		}
	}

	protected override ResourceNode GenerateNode() {
		return new DryerNode(Position);
	}

	public override void OnTick() {
		//if (ResourceGraph.GetNodeAt(Position).IsNodeActive()) transform.localScale = new Vector3((Mathf.Sin(Time.time) * 0.5f) + 0.5f, (Mathf.Cos(Time.time) * 0.5f) + 0.5f, (Mathf.Sin(Time.time) * 0.5f) + 0.5f);
	}
}
