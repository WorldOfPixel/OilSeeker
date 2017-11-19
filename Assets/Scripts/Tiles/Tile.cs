using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TILE {AIR, DIRT, CLAY, SAND, SANDSTONE, LIMESTONE, SLATE, GRANITE, BASALT, ROCK, MAGMA}
public class TileData {
	public TILE type;
	public int amount = 900;
	public int endurance = 0;

	public TileData()
	{
		type = TILE.AIR;
		amount = 900;
		endurance = 0;
	}

	public void SetTileAmount(int damage)
	{
		amount -= Mathf.Clamp(damage - endurance, 0, 900);
	}

	public TileData(TileData tileData)
	{
		type = tileData.type;
		amount = tileData.amount;
		endurance = tileData.endurance;
	}

	public void SetValues(TILE type, int amount, int endurance) {
		this.type = type;
		this.amount = amount;
		this.endurance = endurance;
	}
}

public class Tile : MonoBehaviour {
	public int chunkX;
	public int chunkY;
	public int x;
	public int y;
	public bool isRandomTile;
	public Sprite [] sprites;
	public TileData data;
	public static float SIZE = 1f;

	// Use this for initialization
	void Start () 
	{
		if(!isRandomTile)
			gameObject.GetComponent<SpriteRenderer>().sprite = sprites[0];
		else
			gameObject.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(data.amount <= 0)
			Destroy(this.gameObject);
	}

	void OnClick()
	{
		Debug.Log("Click on tile");
	}
}
