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
				TileData tileData = chunkData.data[i + ChunkData.size / 2, j + ChunkData.size / 2];

				if(tileData.type == TILE.AIR)
					continue;

				int tileIndex = 0;

				switch(tileData.type)
				{
					case TILE.DIRT:
					tileIndex = 0;
					break;
					case TILE.CLAY:
					tileIndex = 1;
					break;
					case TILE.SAND:
					tileIndex = 2;
					break;
					case TILE.SANDSTONE:
					tileIndex = 3;
					break;
					case TILE.LIMESTONE:
					tileIndex = 4;
					break;
					case TILE.SLATE:
					tileIndex = 5;
					break;
					case TILE.GRANITE:
					tileIndex = 6;
					break;
					case TILE.BASALT:
					tileIndex = 7;
					break;
					case TILE.ROCK:
					tileIndex = 8;
					break;
				}
					
				Tile tile = Instantiate(tiles[tileIndex], new Vector3(transform.position.x + (float)i, transform.position.y + (float)j, 0f), Quaternion.identity);

				tile.chunkX = x;
				tile.chunkY = y;
				tile.x = i + ChunkData.size / 2;
				tile.y = j + ChunkData.size / 2;

				tile.data = new TileData();
				tile.data.type = tileData.type;
				tile.data.amount = tileData.amount;
				tile.data.endurance = tileData.endurance;

				tile.transform.SetParent(this.transform);
			}
		}
	}
}
