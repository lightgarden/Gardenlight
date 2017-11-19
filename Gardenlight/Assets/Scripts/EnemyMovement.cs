using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
	
	private float distance;
	public int speed;
	private float searchRadius;
	private float followRadius;
	private float start;
	private bool aggro;



	private EnemyCharacteristics attributes;
	public GameObject player;

	void Start () 
	{
		attributes = GetComponent<EnemyCharacteristics> ();
		distance = 10;
		speed = Random.Range (3, 5);
		searchRadius = 5;
		followRadius = 7;
		start = transform.position.x;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void LateUpdate()
	{
		if (attributes.movementPattern == 1) 
		{
			if (Mathf.Abs (transform.position.x - start) <= distance) 
			{
				transform.Translate (Vector2.right * speed * Time.deltaTime);
			} 
			else 
			{
				speed = speed * -1;
				start = transform.position.x;
			}
		} 
		else if (attributes.movementPattern == 2) 
		{
			if (Vector2.Distance (player.GetComponent<Transform> ().position, transform.position) <= searchRadius) 
			{
				Debug.Log ("aggro true");
				aggro = true;
			}

			if (Vector2.Distance (player.GetComponent<Transform> ().position, transform.position) >= followRadius) 
			{
				Debug.Log ("aggro false");
				aggro = false;
			}

			if (aggro) 
			{
				Debug.Log ("moving");
				transform.position = Vector2.MoveTowards (transform.position, player.GetComponent<Transform> ().position, speed / 150f);
			} 
				
		}
	}

}
