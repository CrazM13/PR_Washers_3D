using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingInput : MonoBehaviour {

	[SerializeField] private BuildingPallet pallet;
	[SerializeField] private Material allowedMaterial;
	[SerializeField] private Material disallowedMaterial;

	private Camera targetCamera;

	Vector2Int selectedTile;

	Plane groundPlane;

	int buildingSelected = -1;

	private GameObject previewRenderer;
	private bool isAllowed = false;

	// Start is called before the first frame update
	void Start() {
		targetCamera = Camera.main;

		groundPlane = new Plane(Vector3.up, 0);
	}

	// Update is called once per frame
	void Update() {
		Ray ray = targetCamera.ScreenPointToRay(Input.mousePosition);
		if (groundPlane.Raycast(ray, out float entry)) {
			selectedTile = HexGrid.GetCellAtWorldPostion(ray.GetPoint(entry));
		}

		for (int i = 0; i < 10; i++) {
			if (Input.GetKeyDown(KeyCode.Alpha0 + i)) {
				buildingSelected = i - 1;

				Destroy(previewRenderer);
				if (buildingSelected >= 0 && buildingSelected < pallet.BuildingsCount) {
					previewRenderer = Instantiate(pallet.GetBuildingGO(buildingSelected));
					SetPreviewRenderPosition(selectedTile);
					previewRenderer.GetComponent<Building>().enabled = false;
					SetPreviewRenderMaterial(isAllowed);
				}
			}
		}

		if (previewRenderer) {
			SetPreviewRenderPosition(selectedTile);
			bool isNowAllowed = BuildingMap.GetNodeAt(selectedTile) == null;
			if (isNowAllowed != isAllowed) {
				isAllowed = isNowAllowed;
				SetPreviewRenderMaterial(isAllowed);
			}
		}

		if (buildingSelected >= 0 && buildingSelected < pallet.BuildingsCount && Input.GetMouseButtonDown(0)) {
			BuildingMap.AddBuilding(selectedTile, pallet.GetBuildingGO(buildingSelected));
		}
	}

	private void SetPreviewRenderMaterial(bool isAllowed) {
		foreach (MeshRenderer meshRenderer in previewRenderer.GetComponentsInChildren<MeshRenderer>()) {
			meshRenderer.material = isAllowed ? allowedMaterial : disallowedMaterial;
		}
	}

	private void SetPreviewRenderPosition(Vector2Int selectedTile) {
		if (previewRenderer) {
			previewRenderer.transform.position = HexGrid.GetCenterOfCell(selectedTile) + Vector3.up;
		}
	}
}
