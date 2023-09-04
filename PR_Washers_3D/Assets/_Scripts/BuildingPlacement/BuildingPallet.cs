using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPallet : MonoBehaviour {

	[SerializeField] private Building[] buildingPrefabs;

	public int BuildingsCount => buildingPrefabs.Length;

	public Building GetBuilding(int index) => buildingPrefabs[index];
	public GameObject GetBuildingGO(int index) => buildingPrefabs[index].gameObject;

}
