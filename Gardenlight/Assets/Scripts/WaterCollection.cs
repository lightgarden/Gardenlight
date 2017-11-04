using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCollection : MonoBehaviour {

	public PlayerActions player;

	// Use this for initialization
	void Start () {
		player = this.GetComponent<PlayerActions> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnParticleCollision(GameObject other)
	{
		Debug.Log ("works");
		player.incrementWater (3);
	}
	void OnTriggerEnter2D(Collider2D other)
	{ 
		if (other.gameObject.name.Substring(0,5).Equals("Water"))
		{
			Debug.Log (other.gameObject.name);
			player.incrementWater (20);
			Destroy (other.gameObject);
		}
	}
}
