using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAegisGeneratorDeath : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	void OnDestroy () {
		GameObject Aegis = GameObject.FindGameObjectWithTag ("AegisShield");
		if (Aegis) {
			BossAegisShieldHealth health = Aegis.gameObject.GetComponent<BossAegisShieldHealth> ();
			health.reduceHealth ();
		}
	}
}
