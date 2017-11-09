using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGenerator {
	public static int sizeX = 1024;
	public static int sizeY = 256;
	public int [,] data;
	private int [] layerHeight;
	private int layersSize = 10;

	void SetLayers()
	{
		layerHeight = new int[layersSize];
		int k = sizeY / layersSize;
		for(int i = 0; i < layersSize; i++)
		{
			layerHeight[i] = k * (i + 1);
		}
	}

	public void Run()
	{
		SetLayers();

		GenerateShell();
		GenerateTerrain();
		GenerateMines();
	}
	void GenerateShell()
	{
		data = new int[sizeX, sizeY];
		for(int i = 0; i < sizeX; i++)
			for(int j = 0; j < sizeY; j++)
				data[i, j] = -1;
	}
	void GenerateTerrain()
	{
		Debug.Log(GetNoise(0, 1));
		Debug.Log(GetNoise(0, 2));
		Debug.Log(GetNoise(0, 3));
		Debug.Log(GetNoise(0, 4));

		
		for(int i = 0; i < sizeX; i++)
		{
			for(int j = 0; j < sizeY; j++)
			{
				int height = GetNoise((float)j / 32, (float)i / 32);
				for(int k = 0; k < sizeY - height; k++)
				{
					data[i, k] = 0;
				}
			}
		}
	}
	void GenerateMines()
	{

	}
	int GetNoise(float x, float y)
	{
		float scale = 64f;
		float noise = Mathf.PerlinNoise(1.0f / (x + 1), 1.0f / (y + 1)); 
		return (int)(noise * scale);
	}
}
