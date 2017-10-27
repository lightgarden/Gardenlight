using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlant : MonoBehaviour {

	// This program will spawn leaf prefabs a certain height above the plant seedling

	public GameObject leaf;
	//private Queue<GameObject> leafQueue;
	public bool water_status = false;



	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {
		//for testing only
//		if (Input.GetKeyDown ("space")) {
//			growPlant();
//		}
	}

	public void Water()
	{
		water_status = true;
	}


	public void Sun()
	{
		if (water_status == true) {
			growPlant();
		}
	}



	void growPlant(){

		//create a list of GameObject leaves
		//for future use perhaps
//		List<GameObject> leafObjects = new List<GameObject>(maxNumLeaves);
//
//		for (int i = 0; i < maxNumLeaves; i++) {
//
//			var leafScale = new Vector2 (leafWidth, leafHeight);
//
//
//		}


		Vector3 location = transform.position;
		//2 is randomly chosen
		location.y += 2;
		GameObject temp = Instantiate (leaf);
		temp.transform.position = location;
	}
}