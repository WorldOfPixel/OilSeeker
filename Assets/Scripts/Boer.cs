using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boer : MonoBehaviour {
	public int damage = 300;
	private GameObject tilePanel;
	// Use this for initialization
	void Start () 
	{
		tilePanel = GameObject.Find("TilePanel");
		tilePanel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) ) {
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

					Text[] text = tilePanel.GetComponentsInChildren<Text>();
					text[0].text = hit.collider.gameObject.GetComponent<Tile>().data.type.ToString();
					text[1].text = "AMOUNT: " + hit.collider.gameObject.GetComponent<Tile>().data.amount.ToString();
					text[2].text = "ENDURANCE: " + hit.collider.gameObject.GetComponent<Tile>().data.endurance.ToString();

					tilePanel.transform.position = new Vector3(mousePos2D.x, mousePos2D.y, -1f);

					if(Input.GetMouseButton(0)){
						Debug.Log(PlanetGenerator.getInstance().data[chunkX * ChunkData.size + x, chunkY * ChunkData.size + y].amount);
						Debug.Log(hit.collider.gameObject.GetComponent<Tile>().data.amount);
						PlanetGenerator.getInstance().data[chunkX * ChunkData.size + x, chunkY * ChunkData.size + y].SetTileAmount(damage);
						hit.collider.gameObject.GetComponent<Tile>().data.SetTileAmount(damage);
						tilePanel.SetActive(false);
					}
					if(Input.GetMouseButton(1)){
						tilePanel.SetActive(true);
					}
				}
			}
			
		}
	}
}
