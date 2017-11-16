using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class TileMap : MonoBehaviour {

	public int size = 32;
	public float tileSize = 1.0f;

	public Texture2D texture;
	public int textureSize = 8;


	public int x;
	public int y;
	public ChunkData chunkData;

	private int[,] data;
	// Use this for initialization
	void Start () {
		data = new int[size, size];

		for(int i = 0; i < size; i++)
			for(int j = 0; j < size; j++)
				data[i, j] = 0;

		data[2, 2] = -1;

		GenerateChunkData();
		CreateMesh();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GenerateChunkData()
	{
		chunkData = new ChunkData();
	}

	void Createtexture()
	{
		int numTilesX = texture.width / textureSize;
		int numTilesY = texture.height / textureSize;


	}

	void CreateMesh(){
		int numOfTiles = size * size;
		int numOfVertices = (size + 1) * (size + 1);
		int numOfTriangles = numOfTiles * 2;

		// Generate the mesh data
		Vector3[] vertices = new Vector3[numOfVertices];
		int[] triangles = new int[numOfTriangles * 3];
		Vector3[] normals = new Vector3[numOfVertices];
		Vector2[] uv = new Vector2[numOfVertices]; 

		for(int x = 0; x < (size + 1); x++)
		{
			for(int y = 0; y < (size + 1); y++)
			{
				vertices[x * (size + 1) + y] = new Vector3(x * tileSize, y * tileSize, 0);
				normals[x * (size + 1) + y] = Vector3.back;


				uv[x * (size + 1) + y] = new Vector2(1, 1);
			}
		}

		for(int x = 0; x < size; x++)
		{
			for(int y = 0; y < size; y++)
			{
				int squareIndex = x * size + y;
				int triangleIndex = squareIndex * 6;

				if(data[x, y] == -1)
					continue;

				triangles[triangleIndex + 0] = x * (size + 1) + y + 0;
				triangles[triangleIndex + 1] = x * (size + 1) + y + (size + 1) + 1;
				triangles[triangleIndex + 2] = x * (size + 1) + y + (size + 1) + 0;

				triangles[triangleIndex + 3] = x * (size + 1) + y + 1;
				triangles[triangleIndex + 4] = x * (size + 1) + y + (size + 1) + 1;
				triangles[triangleIndex + 5] = x * (size + 1) + y + 0;
			}
		}

		// Create a new mesh data and populate with the data
		Mesh mesh = new Mesh();

		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.normals = normals;
		mesh.uv = uv;

		// Assign mesh to filter/renderer/collider
		MeshFilter meshFilter = GetComponent<MeshFilter>();
		MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
		MeshCollider meshCollider = GetComponent<MeshCollider>();

		mesh.RecalculateBounds();
		mesh.RecalculateNormals();

		// Apply the mesh
		meshFilter.mesh = mesh;
		meshCollider.sharedMesh = mesh;
	}

	Vector2 UVCoordinate(int x, int y)
	{
		int x1 = 1;
		int y1 = 1;

		
		Vector2 result = new Vector2();

		return result;
	}
}
