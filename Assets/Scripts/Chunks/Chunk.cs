using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Chunk : MonoBehaviour {
	public int x;
	public int y;
	public ChunkData chunkData;
	public Tile[] tiles;
	// Use this for initialization
	void Start () 
	{
		GenerateChunkData();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void GenerateChunkData()
	{
		chunkData = new ChunkData();
	}

	public void MakeChunk()
	{
		for(int i = - ChunkData.size / 2; i < ChunkData.size / 2; i++)
		{
			for(int j = - ChunkData.size / 2; j < ChunkData.size / 2; j++)
			{
				Tile tile = Instantiate(tiles[(int)chunkData.data[i + ChunkData.size / 2, j + ChunkData.size / 2]], new Vector3(transform.position.x + (float)i * (Tile.SIZE / 1f), transform.position.y + (float)j * (Tile.SIZE / 1f), 0f), Quaternion.identity);
				tile.transform.SetParent(this.transform);
			}
		}
	}
}
