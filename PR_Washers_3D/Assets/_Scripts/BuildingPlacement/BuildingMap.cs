using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BuildingMap {

	private static Dictionary<Vector2Int, Building> buildings = new Dictionary<Vector2Int, Building>();

	public static bool AddBuilding(Vector2Int position, GameObject building) {
		if (buildings.ContainsKey(position)) {
			return false;
		} else {

			GameObject newGO = GameObject.Instantiate(building, HexGrid.GetCenterOfCell(position), Quaternion.identity);
			Building buildingScript = newGO.GetComponent<Building>();

			if (buildingScript) {
				buildings.Add(position, buildingScript);
				return true;
			}
			return false;
		}
	}

	public static bool RemoveNode(Vector2Int position) {
		if (buildings.ContainsKey(position)) {
			buildings.Remove(position);
			return true;
		} else {
			return false;
		}
	}

	public static void Clear() {
		buildings.Clear();
	}

	public static Building GetNodeAt(Vector2Int position) {
		if (buildings.ContainsKey(position)) {
			return buildings[position];
		} else {
			return null;
		}
	}

	public static void OnTick() {
		foreach (KeyValuePair<Vector2Int, Building> building in buildings) {
			building.Value.OnTick();
		}
	}

}
