using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAegisAI : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}

	void OnDestroy(){
		GameObject Player = GameObject.FindGameObjectWithTag ("Player");
		if (Player) {
			PlayerPrefs.SetInt ("aegisKilled", 1);
		}
	}
}
