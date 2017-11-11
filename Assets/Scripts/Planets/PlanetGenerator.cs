using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

[System.Serializable]
 public class PlanetData 
 {
	public int width = 1024;
	public int height = 256;
	public int [,] data;

	public PlanetData(int width, int height)
	{
		this.width = width;
		this.height = height;

		data = new int[this.width, this.height];
	}
}

public class PlanetGenerator {
	public static PlanetGenerator Instance;

	public int seed;
	public int width = 1024;
	public int height = 256;

	public float scale = 16f;
	public float smoothness = 2f;

	public int [,] data;
	public bool isLoad {private set; get;}

	public static PlanetGenerator getInstance()
    {
        if (Instance == null)
            Instance = new PlanetGenerator();
        return Instance;
    }

	public void Save()
	{
		BinaryFormatter bf = new BinaryFormatter();

		if(!File.Exists(Application.dataPath + "/Planet.dat"))
			File.Create(Application.dataPath + "/Planet.dat");

		FileStream file = File.Open(Application.dataPath + "/Planet.dat", FileMode.Open);

		PlanetData planetData = new PlanetData(width, height);

		for(int i = 0; i < width; i++)
		{
			for(int j = 0; j < height; j++)
			{
				planetData.data[i, j] = data[i, j];
			}
		}

		bf.Serialize(file, planetData);
		file.Close();
	}
	
	public void Load()
	{
		isLoad = false;

		if(File.Exists(Application.dataPath + "/Planet.dat"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.dataPath + "/Planet.dat", FileMode.Open);

			PlanetData planetData = (PlanetData)bf.Deserialize(file);
			file.Close();

			width = planetData.width;
			height = planetData.height;

			data = new int[width, height];

			for(int i = 0; i < width; i++)
			{
				for(int j = 0; j < height; j++)
				{
					data[i, j] = planetData.data[i, j];
				}
			}
		}

		isLoad = true;
	}

	public void Generate()
	{
		isLoad = false;

		seed = Random.Range(0, 999);
		data = new int[width, height];

		GenerateTerrain();
		GenerateMines(8, 64, 2, 128, height - 64, height - 16);
		GenerateMinerals(8, 128, 8, 256, height - 64, height - 28);
		GenerateTunnels(-1, 128, 1, 256, 1024, 0, height);

		isLoad = true;
	}

	void GenerateTerrain()
	{
		// Init noise value for all 8 layers
		int [] noise = new int[8];

		//Start to generate layers
		for(int i = 0; i < width; i++)
		{
			for(int j = 0; j < 8; j++)
			{
				// Get noise for all i index collumn
				noise[j] = GetNoise(i + (height / (i + 1)), i / 4, (j + 1) * 4, 0.5f);
			}
			// A little earth fix
			noise[0] = GetNoise(i + (height / (i + 1)), i / 4, 16, 0.5f);

			for(int j = 0; j < height; j++)
			{
				// Generate layers
				if(j < noise[7])
					data[i, j] = 7;
				if(j >= noise[7] && j <= 32 + noise[6])
					data[i, j] = 6;
				if(j >= 32 + noise[6] && j <= 64 + noise[5])
					data[i, j] = 5;
				if(j >= 64 + noise[5] && j <= 96 + noise[4])
					data[i, j] = 4;
				if(j >= 96 + noise[4] && j <= 128 + noise[3])
					data[i, j] = 3;
				if(j >= 128 + noise[3] && j <= 160 + noise[2])
					data[i, j] = 2;
				if(j >= 160 + noise[2] && j <= 192 + noise[1])
					data[i, j] = 1;
				if(j >= 192 + noise[1] && j <= 224 + noise[0])
					data[i, j] = 0;
				if(j > 224 + noise[0])
					data[i, j] = -1;
			}
		}
	}

	void GenerateMines(int tile, int numOfMines = 128, int mMinSize = 4, int mMaxSize = 256, int minDepth = 0, int maxDepth = 16)
	{
		int holeSize = 1;
		for(int i = 0; i < numOfMines; i++)
		{
			int mSize = Random.Range(mMinSize, mMaxSize);
			int xpos = Random.Range(0, width);
			int ypos = Random.Range(minDepth, maxDepth);
			for(int j = 0; j < mSize; j++)
			{
				for(int x = -holeSize; x <= holeSize; x++)
				{
					for(int y = -holeSize; y <= holeSize; y++)
					{
						if(!(x == 0 && y == 0))
						{
							Vector2 tilePos = new Vector2(xpos + x, ypos + y);
							if(tilePos.x >= 0 && tilePos.x < width && tilePos.y >= minDepth && tilePos.y < maxDepth)
								if(data[(int)tilePos.x, (int)tilePos.y] != -1)
									data[(int)tilePos.x, (int)tilePos.y] = (int)tile;
						}
					}
				}

				xpos += Random.Range(-1, 2);
				ypos += Random.Range(-1, 2);

			}
		}
	}
	void GenerateMinerals(int tile, int numOfMinerals, int mMinSize = 4, int mMaxSize = 4, int minDepth = 0, int maxDepth = 16)
	{
		for (int i = 0; i < numOfMinerals; i++)
        {
			int xpos = 0;
			int ypos = 0;

			int mSize = Random.Range(mMinSize, mMaxSize);
			xpos = Random.Range(0, width);
			ypos = Random.Range(minDepth, maxDepth);

            for (int j = 0; j < mSize; j++)
            {
				if(xpos >= 0 && xpos < width && ypos >= minDepth && ypos < maxDepth) // Verify borders 
					if(data[xpos, ypos] != (int)tile) // Verify data
                		data[xpos, ypos] = (int)tile;

                xpos += Random.Range(-1, 2);
				ypos += Random.Range(-1, 2);
            }
        }
	}

	void GenerateTunnels(int tile, int numOfTunnels, int holeSize, int tMinSize, int tMaxSize, int minDepth, int maxDepth)
	{
		for(int i = 0; i < numOfTunnels; i++)
		{
			int xpos = 0;
			int ypos = 0;

			int tSize = Random.Range(tMinSize, tMaxSize);
			xpos = Random.Range(0, width);
			ypos = Random.Range(minDepth, maxDepth);

			for(int j = 0; j < tSize; j++)
			{
				for(int x = -holeSize; x <= holeSize; x++)
				{
					for(int y = -holeSize; y <= holeSize; y++)
					{
						xpos += x;
						ypos += y;

						if(xpos >= 0 && xpos < width && ypos >= minDepth && ypos < maxDepth) // Verify borders 
							data[xpos, ypos] = (int)tile;
					}
				}
	
				xpos += Mathf.RoundToInt(GetNoise(i, j, 2f, 1f) * Random.Range(-1, 2)); 
				ypos += Mathf.RoundToInt(GetNoise(i, j, 2f, 1f) * Random.Range(-1, 2)); 
			}
		}
	}

	int GetNoise(int x, int y, float scale, float smoothness)
	{
		float xCoord = (float)x / width * scale + seed;
		float yCoord = (float)y / height * scale + seed;

		float noise = Mathf.PerlinNoise(xCoord * smoothness, yCoord * smoothness); 

		return Mathf.RoundToInt(noise * scale);
	}
}
