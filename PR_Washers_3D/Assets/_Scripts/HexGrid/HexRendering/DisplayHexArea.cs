using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayHexArea : MonoBehaviour {

	[SerializeField] private Camera targetCamera;
	[SerializeField] private MeshFilter meshFilter;
	[SerializeField] private float wireWidth;

	WireHexModel wireHexModel;
	Vector2Int lowerLeftTile;
	Vector2Int upperRightTile;

	Plane groundPlane;

	Vector2Int fromTile;
	Vector2Int toTile;

	// Start is called before the first frame update
	void Start() {
		groundPlane = new Plane(Vector3.up, 0);

		wireHexModel = new WireHexModel(wireWidth);
	}

	// Update is called once per frame
	void Update() {
		Vector3[] rays = new Vector3[4] {
			Vector3.zero,
			new Vector3(0, 1),
			Vector3.one,
			new Vector3(1, 0)
		};

		float entry;
		Vector2Int min = new Vector2Int(int.MaxValue, int.MaxValue);
		Vector2Int max = new Vector2Int(int.MinValue, int.MinValue);

		foreach (Vector3 rayDir in rays) {
			Ray ray = targetCamera.ViewportPointToRay(rayDir);
			if (groundPlane.Raycast(ray, out entry)) {
				Vector2Int newTile = HexGrid.GetCellAtWorldPostion(ray.GetPoint(entry));

				if (newTile.x < min.x) min.x = newTile.x;
				if (newTile.x > max.x) max.x = newTile.x;

				if (newTile.y < min.y) min.y = newTile.y;
				if (newTile.y > max.y) max.y = newTile.y;
			}
		}

		if (min != fromTile || max != toTile) {
			fromTile = min;
			toTile = max;

			List<Vector2Int> toDraw = new List<Vector2Int>();
			for (int x = fromTile.x; x <= toTile.x; x++) {
				for (int y = fromTile.y; y <= toTile.y; y++) {
					toDraw.Add(new Vector2Int(x, y));
				}
			}

			wireHexModel.GenerateModelData(toDraw);
			meshFilter.mesh = wireHexModel.GenerateMesh();
		}
	}
}
