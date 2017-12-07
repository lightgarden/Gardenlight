using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beespawner : MonoBehaviour {
	public GameObject bee;
	public GameObject player;
	public int screenWidth = 16;            //Horizontal size fo the screen 
	public int screenHeight = 10;

	void Start () {
		StartCoroutine (SpawnBees ());
	}
	// Update is called once per frame
	IEnumerator SpawnBees () {
		while (true) {
			yield return new WaitForSeconds (1);
			Debug.Log ("bees!");
			Vector2 newPosition = new Vector2 (Random.Range (0, screenWidth), transform.position.y + screenHeight);
			GameObject currBee = Instantiate (bee, newPosition, Quaternion.identity);
		}
	}
}
