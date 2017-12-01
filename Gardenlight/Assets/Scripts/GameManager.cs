using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	static public GameManager GM;

	public int state = 0;
	private int level = 1;

	// Background loading things
	public float levelHeight = 90;
	public float curHeight;
	public GameObject bg;
	public GameObject startBg;
	public GameObject lvl2;
	public GameObject lvl3;
	public GameObject lvl4;

	private GameObject prevLevel;
	private GameObject curLevel;
	private GameObject nextLevel;

	public GameObject player;

	public GameObject winText;
	public GameObject loseText;



	// Use this for initialization
	void Awake () {
		GM = this;
		//winText.SetActive (false);
		//loseText.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		curHeight = player.transform.position.y;
		if ((curHeight > 0) && (curHeight % 90 > 80) && nextLevel == null) {
			transitionLevel ();
		}
		if (curHeight > level * levelHeight) {
			changeLevel ();
		}
	}

	void transitionLevel() {
		print ("level up");
		if (level == 1) {
			nextLevel = Instantiate (lvl2);							// Load rainy background

		} else if (level == 2) {
			nextLevel = Instantiate (lvl3);							// Load kelp background

		} else if (level == 3) {
			nextLevel = Instantiate (lvl4);					// Load final background
		}	else {
			Win ();
			return;
		}
		nextLevel.transform.position = new Vector3 (0, levelHeight * (level + 0.5f), 1);
	}

	void changeLevel() {
		prevLevel = curLevel;
		curLevel = nextLevel;
		level++;
		nextLevel = null;

	}

	public int Level () {
		return level;
	}

	public GameObject currentLevel() {
		return curLevel;
	}

	void Win () {
		//winText.SetActive (true);
	}

	void Loose() {
		//loseText.SetActive (true);
	}
}
