using UnityEngine;
using System.Collections;

public class OrientTowardsPlayer : MonoBehaviour {

	private bool playerInRange;
	private Vector3 playerPos;
	private float angle;
	private GameObject Player;

	// Use this for initialization
	void Start () {
		Player = GameObject.FindGameObjectWithTag ("Player");
	}

	void FixedUpdate () {
		Player = GameObject.FindGameObjectWithTag ("Player");
		if (Player) {
			playerPos = Player.transform.position;
			playerPos.x = playerPos.x - transform.position.x;
			playerPos.y = playerPos.y - transform.position.y;
			angle = Mathf.Atan2 (playerPos.y, playerPos.x) * Mathf.Rad2Deg;
			angle -= 90;
			transform.rotation = Quaternion.Euler (new Vector3 (0, 0, angle));
		}
	}
}
