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

//    public string DataString()
//    {
//        string dispString = name + " x" + count.ToString();
//        return dispString;
//    }
}

//pull data from player actions about which seed is the active one [framework done]
//modify player actions function to change the count on the relevant seed [framework done]
//add visual elements to UI

public class Inventory : MonoBehaviour
{
    public Seed seed1;
    public Seed seed2;
    public Text nameCount1;
    public Text nameCount2;
    public Image image1;
    public Image image2;
    public int currentSeedID;
    public GameObject playerController;
    public Canvas inventoryCanvas;
    public GameObject highlightPanel;

	// Use this for initialization
	void Start ()
    {
        seed1 = new Seed("seed1", 10, 1);
        seed2 = new Seed("seed2", 10, 2);
	}

	// Update is called once per frame
	void Update ()
    {
//        DispNameCount(nameCount1, seed1);
//        DispNameCount(nameCount2, seed2);
        MoveHighlight(highlightPanel);
	}

//    void DispNameCount(Text nameCount, Seed seed) //Displays the name and count in the label
//    {
//        nameCount.text = seed.DataString();
//    }

    void MoveHighlight(GameObject panel) //This moves the highlight panel to the correct position
    {
        // playerController = GameObject.Find("player"); I don't think this line is necessary since I declared it at the beginning of the class and it looks like we can drag it in, but the forums say to have this, so idk
        PlayerActions playerActions = playerController.GetComponent<PlayerActions>();
        currentSeedID = playerActions.plantSelected;
        if (currentSeedID == 1)
        {
			panel.transform.localPosition = image1.transform.localPosition; // - new Vector3(10, 10, 0);
        }
        else
        {
			panel.transform.localPosition = image2.transform.localPosition; // - new Vector3(10, 10, 0);
        }
    }

}
