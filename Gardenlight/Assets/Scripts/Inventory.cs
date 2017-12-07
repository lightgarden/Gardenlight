using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Seed
{
    public int count, ID;
    public string name;

    public Seed(string s1, int i1, int i2)
    {
        name = s1;
        count = i1;
        ID = i2;
    }

    public void Increment()
    {
        count += 1;
    }

    public void Decrement()
    {
        count -= 1;
    }
}

//pull data from player actions about which seed is the active one [framework done]
//modify player actions function to change the count on the relevant seed [framework done]
//add visual elements to UI

public class Inventory : MonoBehaviour
{
    public Seed seed1 = new Seed("seed1", 10, 1);
    public Seed seed2 = new Seed("seed2", 10, 2);
    public int currentSeedID;
	// Use this for initialization
	void Start ()
    {
        GameObject playerController = GameObject.Find("player");
        PlayerActions playerActions = playerController.GetComponent<PlayerActions>();
        currentSeedID = playerActions.selectedPlant;
	}

	// Update is called once per frame
	void Update ()
    {

	}

}
