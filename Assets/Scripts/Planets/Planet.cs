using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChunkData {
	public static int size = 8;
	public int x;
	public int y; 
	public TileData [,] data;

	public ChunkData()
	{
		data = new TileData[size, size];
	}
}

public class Planet : MonoBehaviour {
	public Chunk chunk;
	public GameObject player;
	public float renderDist = 4;
	private Dictionary<Vector2, Chunk> chunkMap;
	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindWithTag("Player");
		chunkMap = new Dictionary<Vector2, Chunk>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		FindChunksToLoad();
		DeleteChunks();
	}

	void FindChunksToLoad()
	{
		int x = (int)transform.position.x;
		int y = (int)transform.position.y;

		for(int i = x - ChunkData.size * 2; i < x + (4 * ChunkData.size); i += ChunkData.size)
		{
			for(int j = y - ChunkData.size * 2; j < y + (4 * ChunkData.size); j += ChunkData.size)
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
				x1 = (PlanetGenerator.getInstance().width / ChunkData.size) - Mathf.Abs((x1 - (Mathf.FloorToInt(x1 / (PlanetGenerator.getInstance().width / ChunkData.size)) * (PlanetGenerator.getInstance().width / ChunkData.size))));

			if(x1 > PlanetGenerator.getInstance().width / ChunkData.size - 1) 
				x1 = (x1 - (Mathf.FloorToInt(x1 / (PlanetGenerator.getInstance().width / ChunkData.size)) * (PlanetGenerator.getInstance().width / ChunkData.size)));

			if(y1 < 0) return;
			if(y1 > PlanetGenerator.getInstance().height/ ChunkData.size - 1) return;

			Chunk currentChunk = Instantiate(chunk, new Vector3(x, y, 0f), Quaternion.identity);
			currentChunk.GenerateChunkData();

			for(int i = 0; i < ChunkData.size; i++)
			{
				for(int j = 0; j < ChunkData.size; j++)
				{
					currentChunk.chunkData.data[i, j] = new TileData();
					currentChunk.chunkData.data[i, j].type = PlanetGenerator.getInstance().data[(x1 * ChunkData.size) + i, (y1 * ChunkData.size) + j].type;
					currentChunk.chunkData.data[i, j].amount = PlanetGenerator.getInstance().data[(x1 * ChunkData.size) + i, (y1 * ChunkData.size) + j].amount;
					currentChunk.chunkData.data[i, j].endurance = PlanetGenerator.getInstance().data[(x1 * ChunkData.size) + i, (y1 * ChunkData.size) + j].endurance;
				}
			}
			currentChunk.x = x1;
			currentChunk.y = y1;
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
