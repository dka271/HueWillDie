using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float Speed = 0.17f;

	private float curSpeed;
	private float speedMultiplier = 0.4f;

	// Use this for initialization
	void Start () {
		
	}

	void FixedUpdate () {
		CanMoveScript canMoveScript = this.gameObject.GetComponent<CanMoveScript> ();
		if (canMoveScript.getMove ()) {
			//Get the current position
			Vector3 pos = this.gameObject.transform.position;
			//Update the position for the next frame
			curSpeed = Speed;
			if (Input.GetAxis ("Fire3") != 0) {
				curSpeed *= speedMultiplier;
			}
			pos.x = pos.x + (Input.GetAxis ("Horizontal") * curSpeed);
			pos.y = pos.y + (Input.GetAxis ("Vertical") * curSpeed);
			this.gameObject.transform.position = pos;
		}

		//Freeze the velocity
		this.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
	}
}
