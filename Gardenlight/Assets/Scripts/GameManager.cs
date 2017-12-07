using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	static public GameManager GM;

	public int state = 0;
	private int level = 1;

	// Background loading things
	public float levelHeight = 90;
	public float curHeight;
	public Vector3 startPos;
	public GameObject[] lvl1BGs;
	public GameObject[] lvl2BGs;
	public GameObject[] lvl3BGs;
	public GameObject[] lvl4BGs;
	//public BackgroundManager bg;

	private GameObject prevLevel;
	private GameObject curLevel;
	private GameObject nextLevel;

	public GameObject player;

	public GameObject winText;
	public GameObject loseText;



	// Use this for initialization
	void Awake () {
		GM = this;
		/*bg = GameObject.Find("Background Manager").GetComponent<BackgroundManager>();
		foreach (GameObject bg in lvl2BGs)
		{
			bg.SetActive(false);
		}
		foreach (GameObject bg in lvl3BGs)
		{
			bg.SetActive(false);
		}
		foreach (GameObject bg in lvl4BGs)
		{
			bg.SetActive(false);
		}*/

		//winText.SetActive (false);
		//loseText.SetActive (false);
	}

	/*void Start()
	{
		float height = lvl1BGs[0].GetComponent<SpriteRenderer>().bounds.size.y;
		Vector3 position = new Vector3(0, lvl1BGs[0].transform.position.y, 20);
		bg.SetUpBackgrounds(lvl1BGs[0], lvl1BGs[1], lvl1BGs[2], position);
	}*/

	// Update is called once per frame
	void Update () {
		/*curHeight = player.transform.position.y;
		if ((curHeight > 0) && (curHeight % 90 > 80) && nextLevel == null) {
			transitionLevel ();
		}
		if (curHeight > level * levelHeight) {
			changeLevel ();
		}*/
	}

	public void transitionLevel() {
		print ("level up");
		/*if (level == 1)
		{
			foreach(GameObject bg in lvl1BGs)
				bg.SetActive(false);

			foreach (GameObject bg in lvl2BGs)
				bg.SetActive(true);

			float height = lvl2BGs[0].GetComponent<SpriteRenderer>().bounds.size.y;
			Vector3 position = new Vector3(0, bg.GetTop().y + height / 2, 20);
			bg.SetUpBackgrounds(lvl2BGs[0], lvl2BGs[1], lvl2BGs[2], position);
			changeLevel();

		} else if (level == 2)
		{
			foreach(GameObject bg in lvl2BGs)
				bg.SetActive(false);

			foreach (GameObject bg in lvl3BGs)
				bg.SetActive(true);

			float height = lvl3BGs[0].GetComponent<SpriteRenderer>().bounds.size.y;
			Vector3 position = new Vector3(0, bg.GetTop().y + height / 2, 20);
			bg.SetUpBackgrounds(lvl3BGs[0], lvl3BGs[1], lvl3BGs[2], position);
			changeLevel();

		} else if (level == 3)
		{
			foreach(GameObject bg in lvl3BGs)
				bg.SetActive(false);

			foreach (GameObject bg in lvl4BGs)
				bg.SetActive(true);

			float height = lvl4BGs[0].GetComponent<SpriteRenderer>().bounds.size.y;
			Vector3 position = new Vector3(0, bg.GetTop().y + height / 2, 20);
			bg.SetUpBackgrounds(lvl4BGs[0], lvl4BGs[1], lvl4BGs[2], position);
			changeLevel();
		}	else {
			Win ();
			return;
		}
		nextLevel.transform.position = new Vector3 (0, levelHeight * (level + 0.5f), 1);*/
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

	public void Win () {
		SceneManager.LoadScene("Assets/_scene/win.unity");
	}

	public void Lose() {
		SceneManager.LoadScene("Assets/_scene/lose.unity");
	}
}
