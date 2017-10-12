using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script for spawning in platforms, basically stolen wholesale from this tutorial: https://unity3d.com/learn/tutorials/topics/2d-game-creation/creating-basic-platformer-game
// -10/06/2017 - script created -Arjuna

public class SpawnPlatform : MonoBehaviour {

    public int maxPlatforms = 20;       //adjusts the number of platforms that will be spawned at start
    public GameObject platform;         //the platform that will be repeatedly spawned in, currently is generated in the editor and dragged into a field here
    public float horizontalMin = -5f;   //horizontalMin and horizontalMax determine the maximum horizontal displacement that new platforms have relative to old platforms
    public float horizontalMax = 5f;
    public float verticalMin = 3;       //verticalMin and vertical determine the maximum vertical displacement that new platforms have relative to existing platforms
    public float verticalMax = 8;
    public float horizontalScaleSize = 1f; //maximum change in horizontal size
    public float maxHorizontalSize = 15;   //maximum size of the platform

    private Vector2 originPosition;     //the position of the "current" platform
    private Vector2 originScale;        //the scale of the "current" platform

	// Use this for initialization
	void Start () {
        originPosition = transform.position;
        originScale = transform.localScale;
        Spawn();
	}

    void Spawn()
    {
        for (int i=0; i< maxPlatforms; i++)
        {
            Vector2 randomPosition = originPosition + new Vector2(Random.Range(horizontalMin, horizontalMax), Random.Range(verticalMin, verticalMax)); //determines the displacement of the new platform from the old platform
            Vector2 randomScale = originScale + new Vector2(Random.Range(-horizontalScaleSize,horizontalScaleSize),0);                                //determines the scale size of the new platform
            if (randomScale.x > maxHorizontalSize)      //making sure horizontal size of the platforms is not to large
            {
                randomScale.x = Random.Range(maxHorizontalSize / 4, (3 / 4) * maxHorizontalSize);
            }

            var iPlatform = Instantiate(platform, randomPosition, Quaternion.identity);
            iPlatform.transform.localScale = randomScale;
            originPosition = randomPosition;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
