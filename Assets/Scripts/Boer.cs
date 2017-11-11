using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class Boer : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButtonUp(0)) {
			if(Camera.main.gameObject == null)
				return;

    		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    		Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

    		RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

			if (hit.collider != null) 
			{
				if(hit.collider.gameObject.CompareTag("Tile"))
				{
					int chunkX = hit.collider.gameObject.GetComponent<Tile>().chunkX;
					int chunkY = hit.collider.gameObject.GetComponent<Tile>().chunkY;
					int x = hit.collider.gameObject.GetComponent<Tile>().x;
					int y = hit.collider.gameObject.GetComponent<Tile>().y;

					//Debug.Log(chunkX);
					//Debug.Log(chunkY);

					PlanetGenerator.getInstance().data[chunkX * ChunkData.size + x, chunkY * ChunkData.size + y] = -1;
    				Destroy(hit.collider.gameObject);
				}
			}
		}
	}
}
