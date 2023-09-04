using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour {

	public Vector2Int Position { get; private set; }

	// Start is called before the first frame update
	void Start() {
		Position = HexGrid.GetCellAtWorldPostion(transform.position);
		ResourceGraph.AddNode(GenerateNode());

		InitializeBuilding();
	}

	protected abstract ResourceNode GenerateNode();

	protected virtual void InitializeBuilding() { /* MT */ }
	public virtual void OnTick() { /* MT */ }

}
