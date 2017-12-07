using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicChanger : MonoBehaviour {
	public AudioClip farm;
	public AudioClip rain;
	public AudioClip kelp;
	public AudioClip stars;
	
	// Update is called once per frame
	void changeMusic () {
		AudioSource audio = this.GetComponent<AudioSource>();
		if (GameManager.GM.Level () == 1) {
			audio.clip = farm;
		} else if (GameManager.GM.Level () == 2) {
			audio.clip = rain;
		} else if (GameManager.GM.Level () == 3) {
			audio.clip = kelp;
		} else {
			audio.clip = stars;
		}
	}
}
