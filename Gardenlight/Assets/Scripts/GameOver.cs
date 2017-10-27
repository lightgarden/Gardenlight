using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

	public float cameraHeight;
	private float characterHeight;
	private float maxDist;

	private GameObject cam;

	// Use this for initialization
	void Start () {

		if (GetComponent<SpriteRenderer> () != null) {
			SpriteRenderer img = GetComponent<SpriteRenderer> ();
			characterHeight = img.size.y;
			Debug.Log (characterHeight);
		} else {
			characterHeight = transform.localScale.y;
		}

		maxDist = (cameraHeight + characterHeight) / 2f;

		cam = GameObject.FindGameObjectWithTag ("MainCamera");
		
	}
	
	// Update is called once per frame
	void Update () {

		if (this.transform.position.y + maxDist < cam.transform.position.y) {
			SceneManager.LoadScene("main"); //replace this with whatever happens when game ends
		}
	}
}
