using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

	public Button buttonStart;
	// Use this for initialization
	void Start () {
		InvokeRepeating("dff", 10, 3);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void dff()
	{

	}
}
