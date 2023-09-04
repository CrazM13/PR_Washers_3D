using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayHexSelection : MonoBehaviour {

	[SerializeField] private Camera targetCamera;
	[SerializeField] private MeshFilter meshFilter;
	[SerializeField] private float selectedWireWidth;
	[SerializeField] private float borderWireWidth;

	WireHexModel selectedWireHexModel;
	WireHexModel borderWireHexModel;

	Vector2Int selectedTile;

	Plane groundPlane;

	// Start is called before the first frame update
	void Start() {
		selectedWireHexModel = new WireHexModel(selectedWireWidth);
		borderWireHexModel = new WireHexModel(borderWireWidth);

		groundPlane = new Plane(Vector3.up, 0);
	}

	// Update is called once per frame
	void Update() {
		Ray ray = targetCamera.ScreenPointToRay(Input.mousePosition);

		if (groundPlane.Raycast(ray, out float entry)) {
			Vector2Int newSelected = HexGrid.GetCellAtWorldPostion(ray.GetPoint(entry));

			if (newSelected != selectedTile) {
				selectedTile = newSelected;

				selectedWireHexModel.GenerateModelData(selectedTile);
				

				List<Vector2Int> toDraw = new List<Vector2Int>();
				bool isOddRow = Mathf.Abs(selectedTile.y) % 2 == 1;
				toDraw.Add(selectedTile + new Vector2Int(+1, 0));
				toDraw.Add(selectedTile + new Vector2Int(-1, 0));

				toDraw.Add(selectedTile + new Vector2Int(isOddRow ? 1 : -1, +1));
				toDraw.Add(selectedTile + new Vector2Int(0, +1));

				toDraw.Add(selectedTile + new Vector2Int(isOddRow ? 1 : -1, -1));
				toDraw.Add(selectedTile + new Vector2Int(0, -1));
				borderWireHexModel.GenerateModelData(toDraw);

				selectedWireHexModel.MergeWireHexes(borderWireHexModel);
				meshFilter.mesh = selectedWireHexModel.GenerateMesh();
			}
		}
	}
}
