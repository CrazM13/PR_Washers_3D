using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireHexModel {
	public struct MeshData {
		public List<Vector3> Verticies { get; private set; }
		public List<int> Triangles { get; private set; }
		public List<Vector2> UVs { get; private set; }

		public MeshData(List<Vector3> verts, List<int> tris, List<Vector2> uvs) {
			Verticies = verts;
			Triangles = tris;
			UVs = uvs;
		}
	}

	private float wireWidth = 0.1f;
	private List<MeshData> faces = new List<MeshData>();
	protected Mesh mesh;

	public WireHexModel(float wireWidth) {
		this.wireWidth = wireWidth;
		this.mesh = new Mesh();
	}

	public void GenerateModelData(List<Vector2Int> toDraw) {
		faces.Clear();

		foreach (Vector2Int hex in toDraw) DrawFaces(hex);
	}

	public void GenerateModelData(Vector2Int toDraw) {
		faces.Clear();

		DrawFaces(toDraw);
	}

	public void MergeWireHexes(WireHexModel newModel) {
		this.faces.AddRange(newModel.faces);
	}

	public Mesh GenerateMesh() {
		MeshData fullMesh = CombineFaces();
		mesh.Clear();

		mesh.vertices = fullMesh.Verticies.ToArray();
		mesh.triangles = fullMesh.Triangles.ToArray();
		mesh.uv = fullMesh.UVs.ToArray();
		mesh.RecalculateNormals();

		return mesh;
	}

	private void DrawFaces(Vector2Int selected) {
		for (int point = 0; point < 6; point++) {
			faces.Add(CreateFace(HexGrid.CELL_SIZE * (1 - wireWidth) * 0.5f, HexGrid.CELL_SIZE * 0.5f, point, HexGrid.GetCenterOfCell(selected)));
		}
	}

	private MeshData CombineFaces() {
		List<Vector3> verticies = new List<Vector3>();
		List<int> triangles = new List<int>();
		List<Vector2> uvs = new List<Vector2>();

		for (int i = 0; i < faces.Count; i++) {
			verticies.AddRange(faces[i].Verticies);
			uvs.AddRange(faces[i].UVs);

			int offset = i * 4;
			foreach (int tri in faces[i].Triangles) {
				triangles.Add(tri + offset);
			}
		}

		return new MeshData(verticies, triangles, uvs);
	}

	private MeshData CreateFace(float innerRadius, float outerRadius, int point, Vector3 offset) {

		Vector3 pointA = offset + GetPoint(innerRadius, point);
		Vector3 pointB = offset + GetPoint(innerRadius, (point < 5) ? point + 1 : 0);
		Vector3 pointC = offset + GetPoint(outerRadius, (point < 5) ? point + 1 : 0);
		Vector3 pointD = offset + GetPoint(outerRadius, point);

		List<Vector3> verticies = new List<Vector3>() { pointD, pointC, pointB, pointA };
		List<int> triangles = new List<int>() { 0, 1, 2, 2, 3, 0 };
		List<Vector2> uvs = new List<Vector2>() { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1) };

		return new MeshData(verticies, triangles, uvs);
	}

	private Vector3 GetPoint(float size, int index) {
		float deg = 60 * index;
		float rad = Mathf.Deg2Rad * deg;
		return new Vector3(size * Mathf.Sin(rad), 0, size * Mathf.Cos(rad));
	}
}
