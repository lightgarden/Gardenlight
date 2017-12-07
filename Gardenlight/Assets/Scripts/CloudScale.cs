using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudScale : MonoBehaviour {
	// Use this for initialization
	Transform t;
	void Start () 
	{
		t = GetComponent<Transform> ();
		t.localScale = new Vector3 (t.localScale.x, t.localScale.y, 1.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
