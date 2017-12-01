using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour {

//	public GameObject loadingImage;

	public void LoadIt(string s)
	{
//		loadingImage.SetActive(true);
		SceneManager.LoadScene(s);
	}
}
