using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetData {
	public static int sizeX = 128;
	public static int sizeY = 32;
	public float [,] data;
	public PlanetData()
	{
		data = new float[sizeX, sizeY];
	}
	
}

public class ChunkData {
	public static int size = 8;
	public int x;
	public int y; 
	public float [,] data = new float[8, 8];

	public ChunkData()
	{
		data = new float[size, size];
	}
}

public class Planet : MonoBehaviour {
	public PlanetData planetData;
	//public ChunkData [,] chunkData;
	public Chunk chunk;
	public GameObject player;
	public float renderDist = 4;
	private Dictionary<Vector2, Chunk> chunkMap;
	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindWithTag("Player");
		chunkMap = new Dictionary<Vector2, Chunk>();

		GeneratePlanetData();
		//GenerateChunkData();
	}
	
	// Update is called once per frame
	void Update () 
	{
		FindChunksToLoad();
		DeleteChunks();
	}

	void GeneratePlanetData()
	{
		planetData = new PlanetData();

		for(int i = 0; i < PlanetData.sizeX; i++)
		{
			for(int j = 0; j < PlanetData.sizeY; j++)
			{
				if(i == 0 || i == PlanetData.sizeX - 1 || j == 0 || j == PlanetData.sizeY - 1)
					planetData.data[i, j] = 1;
				else
					planetData.data[i, j] = 0;
			}
		}
	}

	// void GenerateChunkData()
	// {
	// 	int chunkDataX = PlanetData.sizeX / ChunkData.size;
	// 	int chunkDataY = PlanetData.sizeY / ChunkData.size;

	// 	chunkData = new ChunkData[chunkDataX, chunkDataY];

	// 	for(int i = 0; i < chunkDataX; i++)
	// 	{
	// 		for(int j = 0; j < chunkDataY; j++)
	// 		{
	// 			for(int i1 = 0; i1 < ChunkData.size; i1++)
	// 			{
	// 				for(int j1 = 0; j1 < ChunkData.size; j1++)
	// 				{
	// 					chunkData[i, j] = new ChunkData();
	// 					chunkData[i, j].data[i1, j1] = planetData.data[i1 + (i * ChunkData.size), j1 + (j * ChunkData.size)];
	// 				}
	// 			}
	// 		}
	// 	}
	// }

	void FindChunksToLoad()
	{
		int x = (int)transform.position.x;
		int y = (int)transform.position.y;

		for(int i = x - ChunkData.size; i < x + (2 * ChunkData.size); i += ChunkData.size)
		{
			for(int j = y - ChunkData.size; j < y + (2 * ChunkData.size); j += ChunkData.size)
			{
				MakeChunkAt(i, j);
			}
		}
	}

	void MakeChunkAt(int x, int y)
	{
		x = Mathf.FloorToInt(x / (float)ChunkData.size) * ChunkData.size;
		y = Mathf.FloorToInt(y / (float)ChunkData.size) * ChunkData.size;

		if(chunkMap.ContainsKey(new Vector2(x, y)) == false)
		{
			int x1 = x / ChunkData.size;
			int y1 = y / ChunkData.size;

			if(x1 < 0)
				x1 = (PlanetData.sizeX / ChunkData.size) - Mathf.Abs((x1 - (Mathf.FloorToInt(x1 / (PlanetData.sizeX / ChunkData.size)) * (PlanetData.sizeX / ChunkData.size))));

			if(x1 > PlanetData.sizeX / ChunkData.size - 1) 
				x1 = (x1 - (Mathf.FloorToInt(x1 / (PlanetData.sizeX / ChunkData.size)) * (PlanetData.sizeX / ChunkData.size)));

			if(y1 < 0) return;
			if(y1 > PlanetData.sizeY / ChunkData.size - 1) return;

			Chunk currentChunk = Instantiate(chunk, new Vector3(x, y, 0f), Quaternion.identity);
			currentChunk.GenerateChunkData();

			for(int i = 0; i < ChunkData.size; i++)
			{
				for(int j = 0; j < ChunkData.size; j++)
				{
					currentChunk.chunkData.data[i, j] = planetData.data[(x1 * ChunkData.size) + i, (y1 * ChunkData.size) + j];
				}
			}
			
			currentChunk.MakeChunk();
			chunkMap.Add(new Vector2(x, y), currentChunk);
		}
	}

	void DeleteChunks()
	{
		List<Chunk> deleteChunks = new List<Chunk>(chunkMap.Values);
		Queue<Chunk> deleteQueue = new Queue<Chunk>();

		for(int i = 0; i < deleteChunks.Count; i++)
		{
			float distance = Vector3.Distance(transform.position, deleteChunks[i].transform.position);
			if(distance > renderDist * ChunkData.size)
			{
				deleteQueue.Enqueue(deleteChunks[i]);
			}
		}
		while(deleteQueue.Count > 0)
		{
			Chunk chunk = deleteQueue.Dequeue();
			chunkMap.Remove(chunk.transform.position);
			Destroy(chunk.gameObject);
		}
	}
}
