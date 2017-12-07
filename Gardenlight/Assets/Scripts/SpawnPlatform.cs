using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script for spawning in platforms, basically stolen wholesale from this tutorial: https://unity3d.com/learn/tutorials/topics/2d-game-creation/creating-basic-platformer-game
// -10/06/2017 - script created -Arjuna
// -10/13/2017 - made some changes to Spawn() -Arjuna

public class SpawnPlatform : MonoBehaviour
{

    public GameObject player;
    public GameObject platform;             //the platform that will be repeatedly spawned in, currently is generated in the editor and dragged into a field here
	public GameObject rainCloud;
	public int gameOverHeight = 100;        //If the earliest created platform is this distance from the player, it will be deleted
    public int screenWidth = 16;            //Horizontal size fo the screen 
    public float minHorizontalScale;   		//The minimum size of the platform
    public float maxHorizontalScale;  		//The maximum size of the platform
    public float verticalScale = 1f;        //The vertical size of the platform
    public float verticalMin = 3f;          //verticalMin and verticalMax determine the maximum vertical displacement from current platforms that the new platforms will be spwaned at
    public float verticalMax = 10f;
    public float movespeed = 3f;            //Determines how fast the platforms will wiggle
    public int maxPlatforms = 10;
    public int onScreenPlatforms = 2;
    private Queue<GameObject> platformQueue;
    //camera fov is 10 units tall

    private Vector2 originPosition;     //the position of the "current" platform
    private Vector2 topPosition;

    // Use this for initialization
    void Start()
    {
        originPosition = transform.position;
        platformQueue = new Queue<GameObject>(maxPlatforms);
        Spawn(maxPlatforms, originPosition);
    }

    void Spawn(int numPlatforms, Vector2 startPosition)
    {
        for (int i = 0; i < numPlatforms; i++)
        {

            Vector2 newPlatformScale = new Vector2(Random.Range(minHorizontalScale, maxHorizontalScale), verticalScale);    //Determine horizontal size
			float horizontalDisplacement;
			if (GameManager.GM.Level () == 1)
				horizontalDisplacement = Random.Range (newPlatformScale.x / 2, screenWidth - newPlatformScale.x / 2) - 8;      //Determine horizontal position
			else if (GameManager.GM.Level () == 2)
				horizontalDisplacement = Random.Range (newPlatformScale.x / 2, 3 * screenWidth / 2 - newPlatformScale.x / 2) - 6;
			else
				horizontalDisplacement = Random.Range (newPlatformScale.x / 2, screenWidth / 2 - newPlatformScale.x / 2) - 4;

			//As level goes up clouds can move more distance, possible spawn is more centered to avoid a lot of clouds moving offscreen
			float verticalDisplacement = startPosition.y + Random.Range(verticalMin + GameManager.GM.Level (), verticalMax + 2f * GameManager.GM.Level ());                                //Determine vertical position
            Vector2 newPosition = new Vector2(horizontalDisplacement, verticalDisplacement);

			GameObject iPlatform;
			if (Random.value > 0.9) 
			{
				iPlatform = Instantiate(rainCloud, newPosition, Quaternion.identity) as GameObject;
			} else {
				iPlatform = Instantiate(platform, newPosition, Quaternion.identity);
			}
			//print (iPlatform.transform.localScale);
            iPlatform.transform.localScale = newPlatformScale;
			//print (iPlatform.transform.localScale);
            startPosition = newPosition;
            platformQueue.Enqueue(iPlatform);
            topPosition = new Vector2(iPlatform.transform.position.x, iPlatform.transform.position.y);
        }
    }

    void CreatePlatform()
    {
        Spawn(1, topPosition);
    }

    void DestroyPlatform()
    {
        GameObject temp = platformQueue.Dequeue();
        Destroy(temp);
    }

    // Update is called once per frame
    void Update()
    {
        //Check for the number of on screen platforms, spawn some in if there are not enough
        //Delete the bottom most cloud and add a new cloud
        //Check for which platforms which have plants on them, move the platforms that do not
        //Remove platforms below max distance
		        float playerHeight = player.transform.position.y;
        float platformHeight = platformQueue.Peek().transform.position.y;
        if (playerHeight - platformHeight >= 10)
        {
            DestroyPlatform();
            CreatePlatform();
        }
    }
}
