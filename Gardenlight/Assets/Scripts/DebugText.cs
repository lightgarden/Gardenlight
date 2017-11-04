using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugText : MonoBehaviour {

	public Text Debug;

	//Basically when testing for stuff where you want to know the stats
	//Helpful for debugging

	void Update () 
	{
		Debug.text = "Water Level: " + GetComponent<PlayerActions>().returnSomething();
	}
}
