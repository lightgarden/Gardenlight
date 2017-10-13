using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour {
    public float water = 100;
    public PlayerController player;
    public Transform seed;
    public Transform plant;
    public Transform sun;
    public Transform waterFall; //the water that comes out of the watering can
    public Transform wateringCan; //the watering can itself

    private GameObject tempSun; //required for sunPower function
    private GameObject tempSeed; //required for planting function

    public int playerHeight = 10; //this should be changed based on height of player avatar
    public int waterLevel = 10; //this is an arbitrary minimum water level to water plants; change as needed

	// Use this for initialization
	void Start () {
        wateringCan.GetComponent<Renderer>().enabled = false; //hide watering can
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.P)) //press P to plant seed
        {
            plantSeed();
        }

        else if(Input.GetKey(KeyCode.O)) //press O to water plant
        {
            if(water >= waterLevel) //if water levels are high enough
            {
                waterPlant();
            }

            else
            {
                //display message "Water Levels aren't High Enough" above player head
                //can add another argument for GUIStyle (background, color, etc.) if needed
                GUI.Label(new Rect(this.transform.position.x, this.transform.position.y + playerHeight/2, 100, 20), "Your water levels are too low!");
            }
        }

        else if (Input.GetKey(KeyCode.U)) //press U to use sun
        {
            sunPower();
        }

	}

    IEnumerator timeDelay()
    {
        yield return new WaitForSeconds(1);
    }

    void plantSeed()
    {
        player.runSpeed = 0;
        player.jumpForce = 0;
        player.canMove = false;

        //the following instantiates a seed prefab at your feet
        tempSeed = Instantiate(seed, new Vector3(this.transform.position.x, this.transform.position.y-playerHeight), this.transform.rotation).gameObject;

        //to implement time delay, please add animation trigger stuff here
        //so that basically canMove becomes true again only after animation is done
        //right now I just added a 1 second time delay but it'll look awkward
        StartCoroutine(timeDelay());

        player.canMove = true;
    }

    void waterPlant()
    {
        //do watering animation trigger stuff here

        player.runSpeed = 0;
        player.jumpForce = 0;
        player.canMove = false;

        wateringCan.GetComponent<Renderer>().enabled = true; //show watering can
        Instantiate(waterFall, wateringCan.transform.position, wateringCan.transform.rotation); //water appears

        //replaces seed with plant prefab
        Destroy(tempSeed);
        Instantiate(plant, new Vector3(this.transform.position.x, this.transform.position.y - playerHeight), this.transform.rotation);

        //I added a "water" tag to water prefab to implement in OnTriggerEnter2D plant functions
        //Perhaps add a 'watered' bool to plant and set it to true when water touches plant

        water -= 5; //lose 5 waters for each time you water a plant
        wateringCan.GetComponent<Renderer>().enabled = false; //hide watering can

        StartCoroutine(timeDelay());
        player.canMove = true;
    }

    void sunPower()
    {
        player.runSpeed = 0;
        player.jumpForce = 0;
        player.canMove = false;

        if (transform.localScale.x > 0){ // if facing right spawns sun to the right of player
            tempSun = Instantiate(sun, new Vector3(transform.position.x + 10, transform.position.y), transform.rotation).gameObject;
        }

        else //if facing left spawns sun to the left of player
        {
            tempSun = Instantiate(sun, new Vector3(transform.position.x - 10, transform.position.y), transform.rotation).gameObject;
        }

        //insert sun animations here
        //when sun spawns, use plant script to interact with sun trigger
        //and start plant growth function

        StartCoroutine(timeDelay());
        Destroy(tempSun); //destroys sun once its functions are over

        player.canMove = true;
    }



}
