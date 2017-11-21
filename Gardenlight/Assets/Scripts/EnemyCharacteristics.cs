using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyCharacteristics : MonoBehaviour {

	public int health = 1;
	public int movementPattern;
	//0 = still
	//1 = moves back and forth
	//2 = chases player

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (health <= 0)
			Destroy(gameObject);
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		GameObject player = other.gameObject;

		if (other.otherCollider.gameObject.name.Equals ("EnemyHitbox")) 
		{
			//player bounces up
			player.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 500);
			health--;
		} 
		else
		{
			Vector2 reaction = new Vector2 (Mathf.Sign(other.relativeVelocity.x) * -10, 10);
			//Coroutine: Player is knocked back
			StartCoroutine(player.GetComponent<PlayerController> ().knockBack(reaction, 1.0f));
		}

	}
		

}
