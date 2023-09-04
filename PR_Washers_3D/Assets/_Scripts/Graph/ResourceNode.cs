using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ResourceNode {

	public Vector2Int Position { get; private set; }

	protected Dictionary<string, int> Input { get; private set; }
	protected Dictionary<string, int> Output { get; private set; }

	public ResourceNode(Vector2Int position) {
		Position = position;

		Input = new Dictionary<string, int>();
		Output = new Dictionary<string, int>();
	}

	public Vector2Int[] GetConnections() {
		return HexGrid.GetNeighbors(Position);
	}

	public int AttemptInput(string resource, int amount) {
		int acceptedAmount = GetMaxAcceptableResource(resource);

		if (acceptedAmount > 0) {
			acceptedAmount = Mathf.Min(acceptedAmount, amount);
			AddToInput(resource, acceptedAmount);
		}

		return amount - acceptedAmount;
	}

	public void AttemptOutput() {
		foreach (Vector2Int connectedNode in GetConnections()) {
			ResourceNode node = ResourceGraph.GetNodeAt(connectedNode);

			if (node != null) {
				string[] resources = new string[Output.Count];
				Output.Keys.CopyTo(resources, 0);

				foreach (string resource in resources) {
					int remainder = node.AttemptInput(resource, Output[resource]);
					Output[resource] = remainder;
				}
			}
		}
	}

	public abstract int GetMaxAcceptableResource(string resource);
	public abstract bool IsNodeActive();

	public abstract void OnTick();

	public void AddToInput(string resource, int amount) {
		if (Input.ContainsKey(resource)) {
			Input[resource] += amount;
		} else {
			Input.Add(resource, amount);
		}
	}

	public void AddToOutput(string resource, int amount) {
		if (Output.ContainsKey(resource)) {
			Output[resource] += amount;
		} else {
			Output.Add(resource, amount);
		}
	}

	public void MoveInputToOutput() {
		string[] resources = new string[Input.Count];
		Input.Keys.CopyTo(resources, 0);

		foreach (string resource in resources) {
			AddToOutput(resource, Input[resource]);
			Input[resource] = 0;
		}
	}

}
