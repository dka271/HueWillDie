using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathTextScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.GetInt ("numDeaths") == 0) {
			this.gameObject.GetComponent<Text> ().text = "You didn't die at all!";
		} else if (PlayerPrefs.GetInt ("numDeaths") == 1) {
			this.gameObject.GetComponent<Text> ().text = "You only died one time!";
		} else {
			this.gameObject.GetComponent<Text> ().text = "You only died " + PlayerPrefs.GetInt ("numDeaths").ToString () + " times!";
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}
}
