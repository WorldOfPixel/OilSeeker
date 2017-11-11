using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityStandardAssets._2D;

public class GameManager : MonoBehaviour {

	[Header("GameObject Prefabs")]
	public GameObject player;


	[Header("UI Prefabs")]
	public GameObject panel;


	private bool isSceneReady = false;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(isSceneReady)
			return;

		if(PlanetGenerator.getInstance().isLoad)
		{
			Instantiate(player, new Vector3(0f, PlanetGenerator.getInstance().height, 0f), Quaternion.identity);
			Camera.main.gameObject.transform.position = player.transform.position;
			Camera.main.GetComponent<Camera2DFollow>().target = GameObject.FindGameObjectWithTag("Player").transform;

			panel.SetActive(false);
			isSceneReady = true;
		}
	}

	public void New()
	{
		PlanetGenerator.getInstance().Generate();

		if(PlanetGenerator.getInstance().isLoad)
			PlanetGenerator.getInstance().Save();
	}

	public void Load()
	{
		PlanetGenerator.getInstance().Load();
	}

	void OnApplicationQuit()
	{
		PlanetGenerator.getInstance().Save();
	}
}
