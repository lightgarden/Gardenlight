﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour {
	public float speed;
	private float distance;
	float start;
	// Use this for initialization
	void Start () 
	{
		distance = Random.Range (3, 7);
		if (speed != 0.0f )
		{
			speed = Random.Range(distance/4, 3*distance/4); 
		}
		start = transform.position.x;
	}

	// Update is called once per frame
	void Update () 
	{
		if (Mathf.Abs(transform.position.x - start) <= distance) 
		{
			transform.Translate (Vector3.right * speed * Time.deltaTime);
		}  
		else 
		{
			speed = speed * -1;
			start = transform.position.x;
		}
	}
}
