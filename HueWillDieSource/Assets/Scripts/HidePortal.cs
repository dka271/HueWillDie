using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidePortal : MonoBehaviour {

	public string BossKilledString;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (BossIsDead ()) {
			gameObject.GetComponent<CircleCollider2D> ().enabled = true;
			Color visible = new Color (1.0f, 1.0f, 1.0f, 1.0f);
			gameObject.GetComponent<SpriteRenderer> ().color = visible;
		} else {
			gameObject.GetComponent<CircleCollider2D> ().enabled = false;
			Color transparent = new Color (0.4f, 0.4f, 0.4f, 0.0f);
			gameObject.GetComponent<SpriteRenderer> ().color = transparent;
		}
	}

	private bool BossIsDead(){
		if (PlayerPrefs.GetInt (BossKilledString) != 0) {
			return true;
		} else {
			return false;
		}
	}
}
