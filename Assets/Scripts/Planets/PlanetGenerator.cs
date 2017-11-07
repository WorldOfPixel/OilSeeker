using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGenerator {
	public static int sizeX = 512;
	public static int sizeY = 64;
	public int [,] data;
	public void Run()
	{
		GenerateShell();
		GenerateTerrain();
		GenerateMines();
	}
	void GenerateShell()
	{
		data = new int[sizeX, sizeY];
		for(int i = 0; i < sizeX; i++)
			for(int j = 0; j < sizeY; j++)
				data[i, j] = 0;
	}
	void GenerateTerrain()
	{
		for(int i = 0; i < sizeX; i++)
		{
			for(int j = 0; j < sizeY; j++)
			{
				if(j == 12)
				{
					int perlin = Mathf.RoundToInt(GetNoise(i + 4) * 24);
					for(int k = 0; k < sizeY - perlin; k++)
					{
						data[i, k] = 1;
					}
				}
			}
		}
	}
	void GenerateMines()
	{

	}
	float GetNoise(float x)
	{
		float noise = Mathf.PerlinNoise(x  * (1 / (float)(sizeX / 3f)), x  * (1 / (float)(sizeX / 3f))); 
		return noise;
	}
}
