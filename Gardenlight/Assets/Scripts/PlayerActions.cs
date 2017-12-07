
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour {
	public float water = 100;
	public PlayerController player;
	public Transform plant;
	public Transform plantPassed;
	private Vector3 currentLocation;

	public bool plantTimed;
	public bool waterTimed;
	public bool sunTimed;

	public float timer;

	public float jumpForce;
	public float moveSpeed;

	public float playerHeight = 1; //this should be changed based on height of player avatar
	public int waterLevel = 10; //this is an arbitrary minimum water level to water plants; change as needed

	public float plantDistance = 2;
	public float seedDistance = 1;
	private bool plantContact;

	public int plantSelected = 1;
	//1 is beanstalk
	//2 is mushroom plant

	// Use this for initialization
	void Start () 
	{
		player = this.GetComponent<PlayerController>();
		plantTimed = false;
		waterTimed = false;
		sunTimed = false;
		jumpForce = player.jumpForce;
		moveSpeed = player.runSpeed;
		plantContact = false;
		//plantDistance = playerHeight/2;
	}

	// Update is called once per frame
	void Update () 
	{
		checkPassed ();

		if (!player.isMoving && (plantTimed || waterTimed || sunTimed))
		{
			if (plantTimed) StartCoroutine(planting());

			else if (waterTimed) StartCoroutine(watering());

			else if (sunTimed) StartCoroutine(sunning());
		}

		else
		{
			if (!player.isMoving && Input.GetKeyDown(KeyCode.P)  && player.OnGround () == true) //press P to plant seed
			{
				startPlant();
				currentLocation = this.transform.position;
			}

			else if (!player.isMoving && Input.GetKeyDown(KeyCode.O) && plantPassed != null && plantContact) //press O to water plant
			{
				if (water >= waterLevel) //if water levels are high enough
				{
					startWater();
					currentLocation = this.transform.position;
				}

				else
				{
					//display message "Water Levels aren't High Enough" above player head
					//can add another argument for GUIStyle (background, color, etc.) if needed


					//GUI.Label(new Rect(this.transform.position.x, this.transform.position.y + playerHeight / 2, 100, 20), "Your water levels are too low!");
                    print("could not water");
                }
			}

			else if (!player.isMoving && Input.GetKeyDown(KeyCode.U) && plantPassed != null && plantContact) //press U to use sun
			{
				startSun();
				currentLocation = this.transform.position;
			}
			else if (Input.GetKeyDown(KeyCode.Y))
			{
				plantSelected++;
				if (plantSelected >= 3) 
				{
					plantSelected = 1;
				}
				Debug.Log ("Plant " + plantSelected + " is selected");
			}
		}

	}

	IEnumerator planting()
	{
		while(plantTimed)
		{
			//Debug.Log(timer);
			if (timer > 0) timer -= Time.deltaTime;
			else
			{
				plantSeed();
			}
		}
		yield return new WaitForSeconds(0);  //does nothing but yield a return value
	}

	IEnumerator sunning()
	{

		while (sunTimed)
		{
			//Debug.Log(timer);

			if (timer > 0) timer -= Time.deltaTime;

			else
			{
				sunPower();
			}
		}
		yield return new WaitForSeconds(0);

	}

	IEnumerator watering()
	{

		while (waterTimed)
		{
			//Debug.Log(timer);

			if (timer > 0) timer -= Time.deltaTime;

			else
			{
				waterPlant();
			}
		}
		yield return new WaitForSeconds(0);
	}

	void startPlant()
	{
		player.canMove = false;
		plantTimed = true;
		player.runSpeed = 0;
		player.jumpForce = 0;
		timer = 1; //this freezes for 1 second
	}

	void startWater()
	{
		player.canMove = false;
		waterTimed = true;
		player.runSpeed = 0;
		player.jumpForce = 0;
		timer = 1;
	}

	void startSun()
	{
		player.canMove = false;
		sunTimed = true;
		player.runSpeed = 0;
		player.jumpForce = 0;
		timer = 10;
	}

	void plantSeed()
	{
		//the following instantiates a seed prefab at your feet slightly offset
		if(player.facingRight) //player is facing right
			Instantiate(plant, new Vector3(this.transform.position.x + seedDistance, this.transform.position.y - playerHeight / 2), transform.rotation);
		else //player is facing left
			Instantiate(plant, new Vector3(this.transform.position.x - seedDistance, this.transform.position.y - playerHeight / 2), transform.rotation);
		//please add animation trigger stuff here

		plantTimed = false;
		player.runSpeed = moveSpeed;
		player.jumpForce = jumpForce;
		player.canMove = true;
		this.transform.position = currentLocation;
	}

	void waterPlant()
	{
		//do watering animation trigger stuff here
		plantPassed.GetComponent<SpawnPlant>().water();
		plantPassed.GetComponent<SpawnPlant> ().spawnPlant ();
		water -= 5; //lose 5 waters for each time you water a plant
		waterTimed = false;
		player.runSpeed = moveSpeed;
		player.jumpForce = jumpForce;
		player.canMove = true;
		this.transform.position = currentLocation;
	}

	void sunPower()
	{
		//insert sun animations here
		plantPassed.GetComponent<SpawnPlant>().sun();
		sunTimed = false;
		player.runSpeed = moveSpeed;
		player.jumpForce = jumpForce;
		player.canMove = true;
		this.transform.position = currentLocation;
	}

	void OnTriggerEnter2D(Collider2D other){
		plantPassed = other.transform;
        print("plant is passed");
		plantContact = true;
	}

	void OnTriggerExit2D(Collider2D other){
        plantPassed = null;
		plantContact = false;
    }

	public void incrementWater(float value)
	{
		water = Mathf.Min (100, water + value);
	}

	void checkPassed() {
		float temp = plantDistance;
		 
		if (plantPassed != null) {
			float plantx = plantPassed.transform.position.x;
			if (player.facingRight &&  plantx < this.transform.position.x) {
				plantContact = false;
			} else if (!player.facingRight && plantx > this.transform.position.x) {
				plantContact = false;
			} else {
				plantContact = true;
			}


		}
//		if(plantPassed != null && (Mathf.Abs(plantPassed.transform.position.x - this.transform.position.x) > plantDistance //plant is too far away
//			|| plantPassed.transform.position.x < this.transform.position.x - 0.5)){ //plant is behind player
//			//print("plant is now null");
//			//print(Mathf.Abs(plantPassed.transform.position.x - this.transform.position.x));
//			//print(plantDistance);
//			//if (Mathf.Abs(plantPassed.transform.position.x - this.transform.position.x) > plantDistance) print("first condition");
//			//if(plantPassed.transform.position.x < this.transform.position.x) print("second condition");
//			plantPassed = null;
//		}
	}

}
