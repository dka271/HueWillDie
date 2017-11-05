using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreenScript : MonoBehaviour {

	private GameObject deathScreen;

	// Use this for initialization
	void Start () {
		deathScreen = GameObject.FindGameObjectWithTag ("DeathScreen");
		deathScreen.SetActive (false);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		/*GameObject Player = GameObject.FindGameObjectWithTag ("Player");
		if (!Player) {
			this.gameObject.SetActive (true);
		}*/
	}

	void OnDestroy(){
		if (deathScreen) {
			deathScreen.SetActive (true);
		}
	}
}
