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
				int tileIndex = (int)chunkData.data[i + ChunkData.size / 2, j + ChunkData.size / 2];
				if(tileIndex == -1)
					return;
					
				Tile tile = Instantiate(tiles[tileIndex], new Vector3(transform.position.x + (float)i * (Tile.SIZE / 1f), transform.position.y + (float)j * (Tile.SIZE / 1f), 0f), Quaternion.identity);
				tile.transform.SetParent(this.transform);
			}
		}
	}
}
