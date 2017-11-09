using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {
	//Test Values------------------------------------
	
	const string layerThatReactsOnMouseClick = "Clickable";
    const int leftMouseClick = 0;
	double DirtProductionRate = 0;
	public List<Vector2> clicks = new List<Vector2>();
	//DirtProductionRate = rndRes.NextDouble();
	double DirtCount = 0;
	
	//-----------------------------------------------
	public Button buttonStart;
	public Text DirtStatusTest;
	// Use this for initialization
	void Start () {
		InvokeRepeating("Timer",0,1); 
		
	}
	
	// Update is called once per frame
	void Update () {
		//if(Input.GetMouseButtonDown(0))
           // clicks.Add(Input.mousePosition);
		   if (Input.GetMouseButtonDown(leftMouseClick))
		   {
			  
		   }
		   if (Input.GetButtonDown("Fire1"))
		   {
 Debug.Log("butoon1");
		   }
            
	}
	
	
	private int time = 0;
	int a = 0;
	
    void Timer()
    {
		DirtProductionRate = Random.Range(0.0f, 1.0f);
        time++;
		DirtCount += DirtProductionRate;
		
		DirtStatusTest.text = DirtCount.ToString("0")+" kg";
    }

}
