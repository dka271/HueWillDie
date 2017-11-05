using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PausedScreenButtons : MonoBehaviour {
	
	GameObject pausedObject;

	// Use this for initialization
	void Start () {
		pausedObject = GameObject.FindGameObjectWithTag ("PausedScreen");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}

	public void LoadMainMenu(){
		Time.timeScale = 1;
		if (pausedObject) {
			pausedObject.SetActive (false);
		}
		SceneManager.LoadScene("MenuDuplicate");
	}
}
