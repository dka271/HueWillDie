using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossElBomboAIChallenge : MonoBehaviour {

	public GameObject RedBombo;
	public GameObject BlueBombo;
	public GameObject GreenBombo;
	public GameObject YellowBombo;

	private int state;//0 = waiting, 1 = targeted shooting, 2 = waiting, 3 = upward shooting
	private int waitTime;
	private int shootingTime;

	private int idleTime = 60;
	private int idleTime2 = 180;
	private int targetedShootingtime = 100;

	// Use this for initialization
	void Start () {
		state = 0;
		shootingTime = 0;
		waitTime = idleTime;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//Handle the different states for attacking
		waitTime--;
		if (waitTime <= 0) {
			if (state == 0) {
				state = 1;
				waitTime = targetedShootingtime;
			} else if (state == 1) {
				state = 2;
				waitTime = idleTime;
			} else if (state == 2) {
				state = 3;
				shootingTime = 0;
			}
		}

		//Handle shooting
		if (state == 1) {
			float speed = 3.0f;// + (3.5f - (gameObject.GetComponent<HealthScript> ().getHealth () / 100));
			if (waitTime == 100) {
				TargetedShootAtAngle (RedBombo, 1, 0.0f, speed, transform.position, FindAngleTowardsPlayer (transform.position));
			} else if (waitTime == 75) {
				TargetedShootAtAngle (BlueBombo, 2, 30.0f, speed, transform.position, FindAngleTowardsPlayer (transform.position));
			} else if (waitTime == 50) {
				TargetedShootAtAngle (GreenBombo, 3, 30.0f, speed, transform.position, FindAngleTowardsPlayer (transform.position));
			} else if (waitTime == 25) {
				TargetedShootAtAngle (YellowBombo, 4, 30.0f, speed, transform.position, FindAngleTowardsPlayer (transform.position));
			}
		} else if (state == 3) {
			if (shootingTime % 25 == 0) {
				if (shootingTime > 0 && shootingTime <= 225) {
					//Shoot red
					TargetedShootAtAngle(RedBombo, 2, (360.0f - shootingTime), 3.5f, transform.position, 270.0f);
				}
				if (shootingTime > 100 && shootingTime <= 325) {
					//Shoot blue
					TargetedShootAtAngle(BlueBombo, 2, (360.0f - (shootingTime - 100)), 3.5f, transform.position, 270.0f);
				}
				if (shootingTime > 200 && shootingTime <= 425) {
					//Shoot green
					TargetedShootAtAngle(GreenBombo, 2, (360.0f - (shootingTime - 200)), 3.5f, transform.position, 270.0f);
				}
				if (shootingTime > 300 && shootingTime <= 525) {
					//Shoot yellow
					TargetedShootAtAngle(YellowBombo, 2, (360.0f - (shootingTime - 300)), 3.5f, transform.position, 270.0f);
				}
			}
			shootingTime++;
			if (shootingTime > 525) {
				state = 0;
				waitTime = idleTime2;
			}
		}
	}

	//Update the boss killed variable when this dies
	void OnDestroy(){
		GameObject Player = GameObject.FindGameObjectWithTag ("Player");
		if (Player) {
			PlayerPrefs.SetInt ("elBomboKilled", 1);
		}
	}

	//Shoot Bullets
	void Shoot(GameObject Bullet, int BulletsPerShot, float angle, float BulletSpeed, Vector3 position){
		float tempAngle;
		float spread = 360.0f / BulletsPerShot;
		for (int i = 0; i < BulletsPerShot; i++) {
			tempAngle = (angle + (spread * i)) % 360.0f;
			GameObject temp = Instantiate (Bullet, position, Quaternion.Euler (new Vector3 (0, 0, tempAngle - 90.0f))) as GameObject;
			temp.GetComponent<Rigidbody2D> ().velocity = new Vector2 (BulletSpeed * Mathf.Cos ((tempAngle) * Mathf.Deg2Rad), BulletSpeed * Mathf.Sin ((tempAngle) * Mathf.Deg2Rad));
			Destroy (temp, 30.0f);
		}
	}

	//Shoot towards an angle
	void TargetedShootAtAngle(GameObject Bullet, int BulletsPerShot, float Spread, float BulletSpeed, Vector3 position, float angle){
		float startingAngle;
		if (BulletsPerShot > 1) {
			startingAngle = angle - ((Spread * (BulletsPerShot - 1)) / 2);
		} else {
			startingAngle = angle;
		}

		for (int i = 0; i < BulletsPerShot; i++) {
			float tempAngle = startingAngle + (i * Spread);
			GameObject temp = Instantiate (Bullet, position, Quaternion.Euler (new Vector3 (0, 0, tempAngle - 90))) as GameObject;
			temp.GetComponent<Rigidbody2D> ().velocity = new Vector2 (BulletSpeed * Mathf.Cos ((tempAngle) * Mathf.Deg2Rad), BulletSpeed * Mathf.Sin ((tempAngle) * Mathf.Deg2Rad));
			Destroy (temp, 30.0f);
		}
	}

	float FindAngleTowardsPlayer(Vector3 position){
		GameObject Player = GameObject.FindGameObjectWithTag ("Player");
		if (Player) {
			Vector3 playerPos = Player.gameObject.transform.position;
			playerPos.x = playerPos.x - position.x;
			playerPos.y = playerPos.y - position.y;
			float angle = Mathf.Atan2 (playerPos.y, playerPos.x) * Mathf.Rad2Deg;
			return angle;
		} else {
			return 0.0f;
		}
	}
}
