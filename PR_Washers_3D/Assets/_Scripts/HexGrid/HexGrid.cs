using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HexGrid {

	public static readonly float CELL_SIZE = 2;

	private static readonly float X_OFFSET_PERCENT = 0.5f;
	private static readonly float Z_OFFSET_PERCENT = 0.75f;

	public static Vector3 GetCenterOfCell(Vector2Int cell) {

		float x = cell.x * CELL_SIZE;
		float z = cell.y * CELL_SIZE;

		bool isOddRow = Mathf.Abs(cell.y) % 2 == 1;
		if (isOddRow) x += CELL_SIZE * X_OFFSET_PERCENT;
		z += CELL_SIZE * Z_OFFSET_PERCENT;

		return new Vector3(x, 0, z);
	}

	public static Vector2Int[] GetNeighbors(Vector2Int cell) {
		bool isOddRow = cell.y % 2 == 1;

		Vector2Int[] neighbors = new Vector2Int[6] {
			cell + new Vector2Int(+1, 0),
			cell + new Vector2Int(-1, 0),

			cell + new Vector2Int((isOddRow ? 1 : -1), +1),
			cell + new Vector2Int(0, + 1),

			cell + new Vector2Int((isOddRow ? 1 : -1), -1),
			cell + new Vector2Int(0, -1)
		};

		return neighbors;
	}

	public static Vector2Int GetCellAtWorldPostion(Vector3 position) {
		int squareX = Mathf.FloorToInt(position.x / CELL_SIZE);
		int squareY = Mathf.FloorToInt(position.z / CELL_SIZE);
		Vector2Int cell = new Vector2Int(squareX, squareY);

		bool isOddRow = cell.y % 2 == 1;
		Vector2Int[] candidates = new Vector2Int[7] {
			cell,

			cell + new Vector2Int(+1, 0),
			cell + new Vector2Int(-1, 0),

			cell + new Vector2Int((isOddRow ? 1 : -1), +1),
			cell + new Vector2Int(0, + 1),

			cell + new Vector2Int((isOddRow ? 1 : -1), -1),
			cell + new Vector2Int(0, -1)
		};

		float closestDist = float.MaxValue;
		Vector2Int closestCell = candidates[0];

		foreach (Vector2Int cellCandidate in candidates) {
			float newDist = Vector3.Distance(position, GetCenterOfCell(cellCandidate));
			if (newDist < closestDist) {
				closestCell = cellCandidate;
				closestDist = newDist;
			}
		}

		return closestCell;
	}

}
