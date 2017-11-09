using UnityEngine;

public class PerlinNoise : MonoBehaviour {
	public int width = 1024;
	public int height = 256;

	public float scale = 20f;
	// Use this for initialization
	void Start () 
	{
		Renderer renderer = GetComponent<Renderer>();
		renderer.material.mainTexture = GenerateTexture();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	Texture2D GenerateTexture()
	{
		Texture2D texture = new Texture2D(width, height);

		// Generate a perlin noise for texture
		for(int x  = 0; x < width; x++)
		{
			for(int y = 0; y < height; y++)
			{
				Color color = CalculateColor(x, y);
				texture.SetPixel(x, y, color);
			}
		}

		texture.Apply();
		return texture;
	}

	Color CalculateColor(int x, int y)
	{
		float xCoord = (float)x / width * scale;
		float yCoord = (float)y / height * scale;
		float sample = Mathf.PerlinNoise(xCoord, yCoord);
		return new Color(sample, sample, sample);
	}
}
