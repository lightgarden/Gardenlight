using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour {

	public GameObject backgroundBase;
	public GameObject backgroundTile;
	public GameObject backgroundTop;
	public Vector3 startPosition;
	public float bgScrollSpeed = 0f;
	public int maxTile = 3;

	private Camera camera;
	private BackgroundScroller scroller;
	private float baseHeight;
	private float tileHeight;
	private float topHeight;
	private float camOrthSize;
	private int tileCount = 0;

	// Use this for initialization
	void Start () {
			camera = GameObject.Find("Main Camera").GetComponent<Camera>();
			scroller =  GameObject.Find("Main Camera")
													  .GetComponent<BackgroundScroller>();
			baseHeight = backgroundBase.GetComponent<SpriteRenderer>().bounds.size.y;
			tileHeight = backgroundTile.GetComponent<SpriteRenderer>().bounds.size.y;
			topHeight = backgroundTop.GetComponent<SpriteRenderer>().bounds.size.y;

			Debug.Log(baseHeight.ToString());

			backgroundBase.transform.position = startPosition;
			backgroundTile.transform.position =
				new Vector3(backgroundBase.transform.position.x,
										backgroundBase.transform.position.y + baseHeight / 2
										+ tileHeight / 2,
										backgroundBase.transform.position.z + 20);

			scroller.tile = false;
			scroller.maxTile = maxTile;
			scroller.bgScrollSpeed = bgScrollSpeed;
			scroller.background = backgroundTile;
			scroller.otherObjects = new GameObject[2];
			scroller.otherObjects[0] =
				Object.Instantiate(backgroundBase, backgroundBase.transform.position,
													 Quaternion.identity);
			scroller.otherObjects[1] =
				Object.Instantiate(backgroundTop, backgroundTop.transform.position,
													 Quaternion.identity);
			backgroundBase.SetActive(false);
		  backgroundTop.SetActive(false);
			scroller.otherObjects[1].SetActive(false);

			backgroundBase = scroller.otherObjects[0];
			backgroundTop = scroller.otherObjects[1];
			camOrthSize = camera.orthographicSize;
	}

	// Update is called once per frame
	void Update () {

		//determine whether tiling should start
		if (backgroundBase.transform.position.y + baseHeight / 2 <
				camera.transform.position.y + camOrthSize &&
				tileCount == 0)
		{
				scroller.tile = true;
		}

		else if (tileCount == maxTile && !backgroundTop.activeSelf)
		{
				backgroundTop.SetActive(true);
				backgroundTop.transform.position = scroller.EndTile() +
					Vector3.up * topHeight/2;
				//signal prepare new level;
		}

		tileCount = scroller.GetTileCount();
		Debug.Log(tileCount.ToString());
	}
}
