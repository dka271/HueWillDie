using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos;
		GameObject Player = GameObject.FindGameObjectWithTag ("Player");
		if (Player) {
			pos.x = Player.transform.position.x;
			pos.y = Player.transform.position.y;
			pos.z = this.transform.position.z;
			this.gameObject.transform.position = pos;
		}
	}
}
