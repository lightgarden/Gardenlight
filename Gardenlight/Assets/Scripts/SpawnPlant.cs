using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlant : MonoBehaviour {

	// This program will spawn leaf prefabs a certain height above the plant seedling

	public GameObject leaf;
	public GameObject stalk;
	List<GameObject> leafObjects;
	public bool water_status = false;



	// Use this for initialization
	void Start () {


	}

	// Update is called once per frame
	void Update () {
		//for testing only
		if (Input.GetKeyDown ("space")) {
			growPlant();
		}
	}

	public void water()
	{
		water_status = true;
        print("water is true");
	}


	public void sun()
	{
		if (water_status == true) {
            print("plant is growing");
            growPlant();
		}
	}



	void growPlant(){

		Vector3 location = transform.position;
		//Vector2 size_x = transform.localScale.x;


		Instantiate(stalk);
		//seting the scale of the stalk
		//stalk.transform.localScale = new Vector2(transform.localScale.x, Mathf.Abs(leafLocation1.y)+4.0f*Mathf.Abs(leafScale.y));
		stalk.transform.localScale = new Vector2(transform.localScale.x, Random.Range(3.0f,4.0f));

		Vector2 stalkScale = stalk.transform.localScale;
		//setting the location of the stalk (the same as the seed/plant)
		stalk.transform.position = new Vector3(location.x, location.y + 0.5f*stalkScale.y);
		//for debug
		//Debug.Log (location.ToString ("F3"));


		Vector3 stalkPosition = stalk.transform.position;	




		leafObjects = new List<GameObject>();

		var leafScale = new Vector2 (Random.Range(0.5f,1.0f), Random.Range(0.5f,0.75f));
		var leafLocation1 =
			new Vector3 (stalkPosition.x+0.5f*leafScale.x, stalkPosition.y+0.5f*stalkScale.y-leafScale.y);

		//add the first leaf
		leafObjects.Add(Instantiate(leaf));
		leafObjects[0].transform.position = leafLocation1;
		leafObjects[0].transform.localScale = leafScale;


		//add the second leaf
		var leafLocation2 =
			new Vector3 (location.x-0.5f*leafScale.x, stalkPosition.y+Random.Range(0.0f, 0.06f)*stalkScale.y-leafScale.y);
		leafObjects.Add(Instantiate(leaf));
		leafObjects[1].transform.position = leafLocation2;
		var leftScale = new Vector2((-1.0f)*leafScale.x, leafScale.y);
		leafObjects[1].transform.localScale = leftScale;


		//if there is a need in the future to recursively creating leaves
		// for (int i = 0; i < 2; i++) {
		//
		// 	leafObjects.Add(Instantiate(leaf));
		// 	leafLocation.x +=  1.0f;
		// 	leafLocation.y += 1.0f;
		// 	leafObjects[i].transform.position = leafLocation;
		// 	leafObjects[i].transform.localScale = leafScale;
		//
		// }


	}
}
