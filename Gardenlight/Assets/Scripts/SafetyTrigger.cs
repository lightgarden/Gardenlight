using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafetyTrigger : MonoBehaviour {

	void OnCollisionEnter2D (Collision2D other) {
		GameManager.GM.Lose ();
	}

}
