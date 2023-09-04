using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResourceGraph {

	private static Dictionary<Vector2Int, ResourceNode> nodes = new Dictionary<Vector2Int, ResourceNode>();

	public static bool AddNode(ResourceNode node) {
		if (nodes.ContainsKey(node.Position)) {
			return false;
		} else {
			nodes.Add(node.Position, node);
			return true;
		}
	}

	public static bool RemoveNode(Vector2Int position) {
		if (nodes.ContainsKey(position)) {
			nodes.Remove(position);
			return true;
		} else {
			return false;
		}
	}

	public static void Clear() {
		nodes.Clear();
	}

	public static ResourceNode GetNodeAt(Vector2Int position) {
		if (nodes.ContainsKey(position)) {
			return nodes[position];
		} else {
			return null;
		}
	}

	public static void OnTick() {
		foreach (KeyValuePair<Vector2Int, ResourceNode> node in nodes) {
			node.Value.OnTick();

			node.Value.AttemptOutput();
		}
	}

}
