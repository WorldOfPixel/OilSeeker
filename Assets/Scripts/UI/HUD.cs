using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {
	//Test Values------------------------------------
	Random rndRes = new Random();
	double DirtProductionRate = 0;
	//DirtProductionRate = rndRes.NextDouble();
	double DirtCount = 0;
	
	//-----------------------------------------------
	//public Button buttonStart;
	public Text DirtStatusTest;
	// Use this for initialization
	void Start () {
		InvokeRepeating("Timer",0,1);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	private int time = 0;
	int a = 0;
	
    void Timer()
    {
        time++;
		DirtCount += DirtProductionRate;
		DirtStatusTest.text = DirtCount.ToString();
    }

}
