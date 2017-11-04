using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour {
	public float water = 100;
	public PlayerController player;
	public Transform plant;
	public GameObject plantPassed;
	private Vector3 currentLocation;

	public bool plantTimed;
	public bool waterTimed;
	public bool sunTimed;

	public float timer;

	public float playerHeight = 1; //this should be changed based on height of player avatar
	public int waterLevel = 10; //this is an arbitrary minimum water level to water plants; change as needed

	// Use this for initialization
	void Start () {
		player = this.GetComponent<PlayerController>();
		plantTimed = false;
		waterTimed = false;
		sunTimed = false;
	}

	// Update is called once per frame
	void Update () {
		//print (plantPassed);
		if (plantTimed || waterTimed || sunTimed)
		{
			if (plantTimed) StartCoroutine(planting());

			else if (waterTimed) StartCoroutine(watering());

			else if (sunTimed) StartCoroutine(sunning());
		}

		else
		{
			if (Input.GetKeyDown(KeyCode.P)) //press P to plant seed
			{
				startPlant();
				currentLocation = this.transform.position;
			}

			else if (Input.GetKeyDown(KeyCode.O) && plantPassed != null) //press O to water plant
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
					print ("Your water levels are too low!");
					//GUI.Label(new Rect(this.transform.position.x, this.transform.position.y + playerHeight / 2, 100, 20), "Your water levels are too low!");
				}
			}

			else if (Input.GetKeyDown(KeyCode.U) && plantPassed != null) //press U to use sun
			{
				startSun();
				currentLocation = this.transform.position;
			}
		}

	}

	IEnumerator planting()
	{
		while(plantTimed)
		{
			Debug.Log(timer);
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
		timer = 10; //this freezes for 1 second
	}

	void startWater()
	{
		player.canMove = false;
		waterTimed = true;
		player.runSpeed = 0;
		player.jumpForce = 0;
		timer = 10;
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
		//the following instantiates a seed prefab at your feet
		Instantiate(plant, new Vector3(this.transform.position.x, this.transform.position.y - playerHeight / 2), transform.rotation);

		//please add animation trigger stuff here

		plantTimed = false;
		player.runSpeed = 5;
		player.jumpForce = 500;
		player.canMove = true;
		this.transform.position = currentLocation;
	}

	void waterPlant()
	{
		//do watering animation trigger stuff here
		plantPassed.GetComponent<SpawnPlant>().Water();
		water -= 5; //lose 5 waters for each time you water a plant
		waterTimed = false;
		player.runSpeed = 5;
		player.jumpForce = 500;
		player.canMove = true;
		this.transform.position = currentLocation;
	}

	void sunPower()
	{
		//insert sun animations here
		plantPassed.GetComponent<SpawnPlant>().Sun();
		sunTimed = false;
		player.runSpeed = 5;
		player.jumpForce = 500;
		player.canMove = true;
		this.transform.position = currentLocation;
	}

	void OnTriggerEnter2D(Collider2D other){
		plantPassed = other.gameObject;

		//Temporary. Used to test water collection system
		if (other.gameObject.name.Substring(0,5).Equals("Water"))
		{
			Debug.Log (other.gameObject.name);
			water = Mathf.Min(water + 20, 100);
			Destroy (other.gameObject);
		}
	}

	void OnTriggerExit2D(Collider2D other){
		plantPassed = null;
	}
		
	public string returnSomething()
	{
		//Used in conjunction with Debug text to display any wanted information
		//Currently testing for water level
		string returnString = water.ToString ();
		return returnString;
	}




}
