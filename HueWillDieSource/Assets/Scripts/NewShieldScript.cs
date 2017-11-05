using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewShieldScript : MonoBehaviour {

	public int ShieldNumber = 1;//1 = blue, 2 = green, 3 = yellow

	// Use this for initialization
	void Start () {
		//Do not respawn these when the scene reloads if the player already has them
		if (ShieldNumber == 1 && PlayerPrefs.GetInt("haveBlueShield") != 0) {
			Destroy(this.gameObject);
		} else if (ShieldNumber == 2 && PlayerPrefs.GetInt("haveGreenShield") != 0) {
			Destroy(this.gameObject);
		} else if (ShieldNumber == 3 && PlayerPrefs.GetInt("haveYellowShield") != 0) {
			Destroy(this.gameObject);
		}
	}
	
	private void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Player") {
			if (ShieldNumber == 1) {
				//Give player the blue shield
				PlayerPrefs.SetInt("haveBlueShield", 1);
				Destroy(this.gameObject);
			} else if (ShieldNumber == 2) {
				//Give player the green shield
				PlayerPrefs.SetInt("haveGreenShield", 1);
				Destroy(this.gameObject);
			} else if (ShieldNumber == 3) {
				//Give player the yellow shield
				PlayerPrefs.SetInt("haveYellowShield", 1);
				Destroy(this.gameObject);
			}
		}
	}
}
