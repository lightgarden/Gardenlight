using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Rigidbody2D rb;

	public float runSpeed;
	public float jumpForce;
    public bool canMove; //checks if player is allowed to move
	public bool isMoving;

	// Use this for initialization
	void Start () {

		rb = GetComponent<Rigidbody2D> ();
        canMove = true;
		isMoving = false;
	}

	// Update is called once per frame
	void Update () {

        isMoving = false;
		//horizontal movement:
		float velo = 0f;

		if (Input.GetKey(KeyCode.A) && canMove){
			velo -= runSpeed;
			isMoving = true;
		}
		if (Input.GetKey(KeyCode.D) && canMove){
			velo += runSpeed;
			isMoving = true;
		}

		if (canMove)
			rb.velocity = new Vector2 (velo, rb.velocity.y);

		//jumping:
		if (Input.GetKeyDown(KeyCode.W) && OnGround() && canMove){
			rb.AddForce (Vector2.up * jumpForce);
			isMoving = true;
		}

	}

	bool OnGround () {

		//find width and height of character
		BoxCollider2D coll = GetComponent<BoxCollider2D> ();
		Vector2 pos = transform.position;
		float width = coll.bounds.size.x;
		float height = coll.bounds.size.y;

		//the ground check draws a line right underneath the player
		//if there is a collider on that line, the player is on something
		//and therefore can jump
		//p1 and p2 are the ends of that line
		Vector2 p1 = new Vector2 (pos.x - width / 2f + 0.01f, pos.y - height / 2f - 0.02f);
		Vector2 p2 = new Vector2 (pos.x + width / 2f - 0.01f, pos.y - height / 2f - 0.02f);

		return Physics2D.Linecast (p1, p2);

	}


	//v is veloctiy player is knocked
	//s is time for inability to move
	public IEnumerator knockBack(Vector2 v, float s)
	{
		rb.velocity = new Vector2 (0, 0);
		canMove = false;
		rb.velocity = v;
		Debug.Log (rb.velocity);

		yield return new WaitForSeconds (s);
		canMove = true;
	}
}
