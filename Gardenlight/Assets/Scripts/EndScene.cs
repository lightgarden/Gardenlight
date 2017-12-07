using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour {

	public Text txt;
	private int count = 0;
	private string[] dialogue = new string[] {
	"I've been watching your journey, and you've been working hard.",
	"The sun seems to have taken a shine to you!",
		"I'm glad everything's alright! Come back and play with us sometime!"};
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Debug.Log ("wow");
			if (count < 3) {
//				txt = GetComponent<Text>();
				txt.text = dialogue [count];
				count++;
			} else {
				SceneManager.LoadScene("Assets/_scene/win.unity");
			}
		}
	}
}
