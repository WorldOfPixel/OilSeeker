using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGenerator {
	public int seed;
	public static int width = 1024;
	public static int height = 256;

	public float scale = 16f;
	public float smoothness = 2f;

	public int [,] data;

	public void Run()
	{
		seed = Random.Range(0, 999);
		data = new int[width, height];

		GenerateTerrain();
		GenerateMines(-1);
	}

	void GenerateTerrain()
	{
		for(int i = 0; i < width; i++)
		{
			int noise = GetNoise(width, i / 4);
			for(int j = height - noise; j < height; j++)
			{
				if(j > height - 16)
					data[i, j] = -1;
			}
			noise = GetNoise(i, i / 4);
			for(int j = 0; j < height - noise; j++)
			{	
				if(j > height - noise - 32)
					data[i, j] = 0;	
				else
					data[i, j] = 1;	
			}
		}
	}

	void GenerateMines(int tile, int numMines = 128, int mMinSize = 4, int mMaxSize = 256)
	{
		int holeSize = 2;
		for(int i = 0; i < numMines; i++)
		{
			int mSize = Random.Range(mMinSize, mMaxSize);
			int xpos = Random.Range(0, width);
			int ypos = Random.Range(0, height);
			for(int j = 0; j < mSize; j++)
			{
				for(int x = -holeSize; x <= holeSize; x++)
				{
					for(int y = -holeSize; y <= holeSize; y++)
					{
						if(!(x == 0 && y == 0))
						{
							Vector2 tilePos = new Vector2(xpos + x, ypos + y);
							if(tilePos.x > 0 && tilePos.x < width && tilePos.y > 0 && tilePos.y < height)
								data[(int)tilePos.x, (int)tilePos.y] = (int)tile;
						}
					}
				}

				xpos += Random.Range(-1, 2);
				ypos += Random.Range(-1, 2);

				// TODO verify.
			}
		}
	}

	int GetNoise(int x, int y)
	{
		float xCoord = (float)x / width * scale + seed;
		float yCoord = (float)y / height * scale + seed;

		float noise = Mathf.PerlinNoise(xCoord * smoothness, yCoord * smoothness); 

		return Mathf.RoundToInt(noise * scale);
	}
}
