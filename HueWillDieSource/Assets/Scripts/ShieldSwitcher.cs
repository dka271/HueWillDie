using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSwitcher : MonoBehaviour {

	public Sprite YellowShield;
	public Sprite GreenShield;
	public Sprite BlueShield;
	public Sprite RedShield;

	private bool canChangeShield = true;

	// Use this for initialization
	void Start () {
		this.GetComponent<SpriteRenderer> ().sprite = RedShield;
		this.gameObject.layer = LayerMask.NameToLayer ("Red");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetAxis ("ShieldRed") != 0) {
			this.GetComponent<SpriteRenderer> ().sprite = RedShield;
			this.gameObject.layer = LayerMask.NameToLayer ("Red");
		} else if (Input.GetAxis ("ShieldYellow") != 0 && PlayerPrefs.GetInt ("haveYellowShield") == 1) {
			this.GetComponent<SpriteRenderer> ().sprite = YellowShield;
			this.gameObject.layer = LayerMask.NameToLayer ("Yellow");
		} else if (Input.GetAxis ("ShieldGreen") != 0 && PlayerPrefs.GetInt ("haveGreenShield") == 1) {
			this.GetComponent<SpriteRenderer> ().sprite = GreenShield;
			this.gameObject.layer = LayerMask.NameToLayer ("Green");
		} else if (Input.GetAxis ("ShieldBlue") != 0 && PlayerPrefs.GetInt ("haveBlueShield") == 1) {
			this.GetComponent<SpriteRenderer> ().sprite = BlueShield;
			this.gameObject.layer = LayerMask.NameToLayer ("Blue");
		} else if (Input.GetButton ("Fire2") && canChangeShield) {
			//Set flag, change shield
			canChangeShield = false;
			if (this.gameObject.layer == LayerMask.NameToLayer ("Red")) {
				if (PlayerPrefs.GetInt ("haveBlueShield") == 1) {
					this.GetComponent<SpriteRenderer> ().sprite = BlueShield;
					this.gameObject.layer = LayerMask.NameToLayer ("Blue");
				}
			} else if (this.gameObject.layer == LayerMask.NameToLayer ("Blue")) {
				if (PlayerPrefs.GetInt ("haveGreenShield") == 1) {
					this.GetComponent<SpriteRenderer> ().sprite = GreenShield;
					this.gameObject.layer = LayerMask.NameToLayer ("Green");
				} else {
					this.GetComponent<SpriteRenderer> ().sprite = RedShield;
					this.gameObject.layer = LayerMask.NameToLayer ("Red");
				}
			} else if (this.gameObject.layer == LayerMask.NameToLayer ("Green")) {
				if (PlayerPrefs.GetInt ("haveYellowShield") == 1) {
					this.GetComponent<SpriteRenderer> ().sprite = YellowShield;
					this.gameObject.layer = LayerMask.NameToLayer ("Yellow");
				} else {
					this.GetComponent<SpriteRenderer> ().sprite = RedShield;
					this.gameObject.layer = LayerMask.NameToLayer ("Red");
				}
			} else if (this.gameObject.layer == LayerMask.NameToLayer ("Yellow")) {
				this.GetComponent<SpriteRenderer> ().sprite = RedShield;
				this.gameObject.layer = LayerMask.NameToLayer ("Red");
			}
		} else if (!Input.GetButton ("Fire2")) {
			//Clear flag
			canChangeShield = true;
		}
		/*else if (Input.GetButtonDown("Fire2") == true) {
			if (this.gameObject.layer == LayerMask.NameToLayer ("Red")) {
				if (PlayerPrefs.GetInt ("haveBlueShield") == 1) {
					this.GetComponent<SpriteRenderer> ().sprite = BlueShield;
					this.gameObject.layer = LayerMask.NameToLayer ("Blue");
				}
			} else if (this.gameObject.layer == LayerMask.NameToLayer ("Blue")) {
				if (PlayerPrefs.GetInt ("haveGreenShield") == 1) {
					this.GetComponent<SpriteRenderer> ().sprite = GreenShield;
					this.gameObject.layer = LayerMask.NameToLayer ("Green");
				} else {
					this.GetComponent<SpriteRenderer> ().sprite = RedShield;
					this.gameObject.layer = LayerMask.NameToLayer ("Red");
				}
			} else if (this.gameObject.layer == LayerMask.NameToLayer ("Green")) {
				if (PlayerPrefs.GetInt ("haveYellowShield") == 1) {
					this.GetComponent<SpriteRenderer> ().sprite = YellowShield;
					this.gameObject.layer = LayerMask.NameToLayer ("Yellow");
				} else {
					this.GetComponent<SpriteRenderer> ().sprite = RedShield;
					this.gameObject.layer = LayerMask.NameToLayer ("Red");
				}
			} else if (this.gameObject.layer == LayerMask.NameToLayer ("Yellow")) {
				this.GetComponent<SpriteRenderer> ().sprite = RedShield;
				this.gameObject.layer = LayerMask.NameToLayer ("Red");
			}
		}*/
	}
}
