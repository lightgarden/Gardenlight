using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour {
    public float water = 100;
    public PlayerController player;
    public Transform plant;

    public bool plantTimed = false;
    public bool waterTimed = false;
    public bool sunTimed = false;

    public float timer = 0;

    public int playerHeight = 10; //this should be changed based on height of player avatar
    public int waterLevel = 10; //this is an arbitrary minimum water level to water plants; change as needed

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if (plantTimed || waterTimed || sunTimed)
        {
            while (plantTimed)
            {
                if (timer > 0) timer -= Time.deltaTime;

                else
                {
                    plantSeed();
                }
            }

            while (waterTimed)
            {
                if (timer > 0) timer -= Time.deltaTime;

                else
                {
                    waterPlant();
                }
            }

            while (sunTimed)
            {
                if (timer > 0) timer -= Time.deltaTime;

                else
                {
                    sunPower();
                }
            }
        }

        else
        {
            if (Input.GetKeyDown(KeyCode.P)) //press P to plant seed
            {
                startPlant();
            }

            else if (Input.GetKeyDown(KeyCode.O)) //press O to water plant
            {
                if (water >= waterLevel) //if water levels are high enough
                {
                    startWater();
                }

                else
                {
                    //display message "Water Levels aren't High Enough" above player head
                    //can add another argument for GUIStyle (background, color, etc.) if needed
                    GUI.Label(new Rect(this.transform.position.x, this.transform.position.y + playerHeight / 2, 100, 20), "Your water levels are too low!");
                }
            }

            else if (Input.GetKeyDown(KeyCode.U)) //press U to use sun
            {
                startSun();
            }
        }

	}

    void startPlant()
    {
        player.runSpeed = 0;
        player.jumpForce = 0;
        player.canMove = false;

        plantTimed = true;
        timer = 1;
    }

    void startWater()
    {
        player.runSpeed = 0;
        player.jumpForce = 0;
        player.canMove = false;

        waterTimed = true;
        timer = 1;
    }

    void startSun()
    {
        player.runSpeed = 0;
        player.jumpForce = 0;
        player.canMove = false;

        sunTimed = true;
        timer = 1;
    }

    void plantSeed()
    {
        //the following instantiates a seed prefab at your feet
        Instantiate(plant, new Vector3(this.transform.position.x, this.transform.position.y-playerHeight), this.transform.rotation);

        //please add animation trigger stuff here

        plantTimed = false;
        player.canMove = true;
    }

    void waterPlant()
    {
        //do watering animation trigger stuff here
        water -= 5; //lose 5 waters for each time you water a plant
        waterTimed = false;
        player.canMove = true;
    }

    void sunPower()
    {
        //insert sun animations here
        //when sun spawns, use plant script to interact with sun trigger
        //and start plant growth function
        sunTimed = false;
        player.canMove = true;
    }



}
