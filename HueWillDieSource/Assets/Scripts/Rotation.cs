using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour {

	public float AngularVelocity = 1.0f;
	public float AngularAcceleration = 0.0f;

	private float angle = 0.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		OrientTowardsAngle (angle);
		angle = (angle + AngularVelocity) % 360.0f;
		AngularVelocity = (AngularVelocity + AngularAcceleration) % 360.0f;
	}

	void OrientTowardsAngle (float angle) {
		angle -= 90;
		transform.rotation = Quaternion.Euler (new Vector3 (0, 0, angle));
	}
}
