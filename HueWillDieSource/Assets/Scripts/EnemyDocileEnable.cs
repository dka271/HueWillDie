using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDocileEnable : MonoBehaviour {

	public float SightRange = 15.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		GameObject Player = GameObject.FindGameObjectWithTag ("Player");
		if (Player) {
			float x1 = gameObject.transform.position.x, y1 = gameObject.transform.position.y, x2 = Player.gameObject.transform.position.x, y2 = Player.gameObject.transform.position.y;
			float distance = Mathf.Sqrt (Mathf.Pow ((y2 - y1), 2) + Mathf.Pow ((x2 - x1), 2));
			if (distance <= SightRange) {
				gameObject.GetComponent<HealthScript> ().enabled = true;
				if (gameObject.GetComponent<EnemyMovementScript> ()) {
					gameObject.GetComponent<EnemyMovementScript> ().enabled = true;
				}
				if (gameObject.GetComponent<EnemyShoot> ()) {
					gameObject.GetComponent<EnemyShoot> ().enabled = true;
				}
				if (gameObject.GetComponent<EnemyTargetedShoot> ()) {
					gameObject.GetComponent<EnemyTargetedShoot> ().enabled = true;
				}
			} else {
				gameObject.GetComponent<HealthScript> ().enabled = false;
				if (gameObject.GetComponent<EnemyMovementScript> ()) {
					gameObject.GetComponent<EnemyMovementScript> ().enabled = false;
				}
				if (gameObject.GetComponent<EnemyShoot> ()) {
					gameObject.GetComponent<EnemyShoot> ().enabled = false;
				}
				if (gameObject.GetComponent<EnemyTargetedShoot> ()) {
					gameObject.GetComponent<EnemyTargetedShoot> ().enabled = false;
				}
			}
		}
	}
}
