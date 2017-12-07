using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlant : MonoBehaviour {

	// This program will spawn leaf prefabs a certain height above the plant seedling

	private bool grown = false;
	public int plantType;
	public GameObject leaf;
	public GameObject stalk;
	public GameObject MushroomTop;
	bool grow_status = false;
	List<GameObject> leafObjects;
	public bool water_status = false;



	// Use this for initialization
	void Start () 
	{
		plantType = GetComponent<PlantType> ().getPlantType ();

	}

	// Update is called once per frame
	public void spawnPlant() {
		//for testing only
		if (plantType == 0 && !grown) {
			grown = true;
			growPlantBeanStalk();
		} else if (plantType == 2 && !grown) {
			grown = true;
			growPlant ();
			//Grow plant is plant with leaves only on top
		} 
	}

	public void water()
	{
		water_status = true;
	}


	public void sun()
	{
		if (water_status == true && grow_status == false) {
            growPlant();
			grow_status =true;
		}
	}



	void growPlant(){
		Vector3 location = transform.position;
		Debug.Log (location.ToString ("F3"));
		//Vector2 size_x = transform.localScale.x;

		//seting the scale of the stalk
		//stalk.transform.localScale = new Vector2(transform.localScale.x, Mathf.Abs(leafLocation1.y)+4.0f*Mathf.Abs(leafScale.y));
		stalk.transform.localScale = new Vector3(transform.localScale.x, Random.Range(4.0f,4.5f), 1);

		Vector2 stalkScale = stalk.transform.localScale;
		//setting the location of the stalk (the same as the seed/plant)
		stalk.transform.position = new Vector2(location.x, location.y);
//		//for debug

		Vector3 stalkPosition = stalk.transform.position;	

		Instantiate(stalk);



		leafObjects = new List<GameObject>();

		var leafScale = new Vector2 (Random.Range(0.5f,1.0f), Random.Range(0.5f,0.75f));
		var leafLocation1 =
			new Vector2 (stalkPosition.x-2f*leafScale.x, stalkPosition.y+stalkScale.y-2f*leafScale.y-1f);
		//add the first leaf
		leafObjects.Add(Instantiate(leaf));
		leafObjects[0].transform.position = leafLocation1;
		leafObjects[0].transform.localScale = leafScale;
		//		Debug.Log (stalk.transform.position.ToString ("F3"));


		//add the second leaf
		var leafLocation2 =
			new Vector3 (location.x+2f*leafScale.x, stalkPosition.y+stalkScale.y-2f*leafScale.y-1f);
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

	//Done by Andrew Cao
	//Testing new method to grow plant
	void growPlantBeanStalk()
	{
		//Plant height
		int counter = Random.Range (60, 80);

		//Chace a leaf will spawn
		int leafSpawnRate = 10;
		stalk = Instantiate (stalk,new Vector3(transform.position.x, transform.position.y, 0.0f), Quaternion.identity) as GameObject;
		StartCoroutine(growingStalks(counter, leafSpawnRate));
	}

	IEnumerator growingStalks (int counter, int leafSpawnRate)
	{
		yield return new WaitForSeconds(0.1f);
		counter--;

		//Old Stalk creation. Used multiple stalk
		/*if (counter >= 0) {
			Instantiate (stalk, new Vector3 (PrevPos.x, PrevPos.y, 0.0f), Quaternion.identity);
			int leafDistribution = Random.Range (1, 5);
			if (leafDistribution >= 4) {
				Instantiate (leaf, new Vector3 (PrevPos.x - 1.0f, PrevPos.y + Random.Range (-1.0f, 1.0f), 0.0f), Quaternion.identity);
				Debug.Log ("left leaf");
			} 

			//right leaf
			else if (leafDistribution >= 2) {
				Instantiate (leaf, new Vector3 (PrevPos.x + 1.0f, PrevPos.y + Random.Range (-1.0f, 1.0f), 0.0f), Quaternion.Euler (0.0f, 180.0f, 0.0f));
				Debug.Log ("right leaf");
			} 

			//both leaf
			else {
				Instantiate (leaf, new Vector3 (PrevPos.x - 1.0f, PrevPos.y + Random.Range (-1.0f, 1.0f), 0.0f), Quaternion.identity);
				Instantiate (leaf, new Vector3 (PrevPos.x + 1.0f, PrevPos.y + Random.Range (-1.0f, 1.0f), 0.0f), Quaternion.Euler (0.0f, 180.0f, 0.0f));
				Debug.Log ("both leaf");
			}
			StartCoroutine (growingStalks (counter, PrevPos));
		} */

		if (counter >= 0) 
		{
			Debug.Log ("growing stalk");
			stalk.transform.localScale = new Vector2 (stalk.transform.localScale.x, stalk.transform.localScale.y + 0.25f);
			stalk.transform.position = new Vector2 (stalk.transform.position.x, stalk.transform.position.y + 0.125f);
			StartCoroutine (growingStalks (counter, leafSpawnRate));
			if (Random.Range (1, 100) <= leafSpawnRate) {
				leafSpawnRate = 10;
				Vector3 leafPos = new Vector3 (stalk.transform.position.x + Mathf.Sign (Random.Range (-1.0f, 1.0f)), stalk.transform.position.y + stalk.transform.localScale.y / 2);
				leaf = Instantiate (leaf, leafPos, Quaternion.identity) as GameObject;
				if (leaf.transform.position.x > stalk.transform.position.x)
					leaf.transform.rotation = Quaternion.Euler (0.0f, 180.0f, 0.0f);
			} 
			else
				leafSpawnRate += 10;
		}

		else {
			Destroy (this.gameObject);
		}
	}
}
