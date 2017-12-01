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

	private GameObject prevLevel;
	private GameObject curLevel;
	private GameObject nextLevel;



	// Use this for initialization
	void Awake () {
		GM = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void transitionLevel() {
		
	}

	void changeLevel() {
		
	}

	public int Level () {
		return level;
	}
}
