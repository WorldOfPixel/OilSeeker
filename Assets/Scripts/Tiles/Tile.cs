using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
	public bool isRandomTile;
	public Sprite [] sprites;
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
		
	}
}
