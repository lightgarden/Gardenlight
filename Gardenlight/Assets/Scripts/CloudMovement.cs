using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour {
	public float speed;
	private float distance;
	float start;
	public bool canMove = true;
	// Use this for initialization
	void Start () 
	{
		distance = Random.Range(3.0f + 1.0f * GameManager.GM.Level (), 6.0f + 2f * GameManager.GM.Level ());
		speed = Mathf.Sign(Random.Range(-1.0f,1.0f)) * Random.Range (distance / 2, (GameManager.GM.Level () - 1) * distance / 2); 
		start = transform.position.x;
	}

	// Update is called once per frame
	void Update () 
	{
		if (canMove & GameManager.GM.Level () > 1) 
		{
			if (Mathf.Abs (transform.position.x - start) <= distance) {
				transform.Translate (Vector3.right * speed * Time.deltaTime);
			} else {
				speed = speed * -1;
				start = transform.position.x;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		plantedOn ();
	}

	void OnCollisionStay2D(Collision2D coll)
	{
		
		if (coll.gameObject.CompareTag ("Player")) 
		{
			GameObject player = coll.gameObject;
			if (canMove & GameManager.GM.Level () > 1)
				player.transform.Translate (Vector3.right * speed * Time.deltaTime);
		}
	}
		

	public void plantedOn() 
	{
		canMove = false;
	}
}
