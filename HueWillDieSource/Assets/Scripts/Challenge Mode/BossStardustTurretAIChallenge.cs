using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStardustTurretAIChallenge : MonoBehaviour {

	public GameObject YellowMeteor;//0
	public GameObject RedMeteor;//1
	public GameObject GreenMeteor;//2
	public GameObject BlueMeteor;//3
	public GameObject YellowBullet;//4
	public GameObject RedBullet;//5
	public GameObject GreenBullet;//6
	public GameObject BlueBullet;//7
	public GameObject YellowStar;//8
	public GameObject RedStar;//9
	public GameObject GreenStar;//10
	public GameObject BlueStar;//11

	private int time;
	private int attackPattern;//0 = nothing
	private int attackTime;

	// Use this for initialization
	void Start () {
		time = 0;
		attackPattern = 0;
		attackTime = 0;
	}

	// Update is called once per frame
	void FixedUpdate () {
		time++;
		attackTime++;

		//Handle shooting
		if (attackPattern == 1) {
			if (attackTime % 360 == 0) {
				Vector3 pos = GetPositionOfTurret ();
				//TargetedShootAtAngle (RedBullet, 7, 10.0f, 5.0f, pos, FindAngleTowardsPlayer (pos));
				TargetedShootAtAngle (RedBullet, 7, 10.0f, 4.5f, pos, FindAngleTowardsPlayer (pos));
				TargetedShootAtAngle (RedBullet, 7, 10.0f, 4.0f, pos, FindAngleTowardsPlayer (pos));
				TargetedShootAtAngle (RedBullet, 7, 10.0f, 3.5f, pos, FindAngleTowardsPlayer (pos));
				TargetedShootAtAngle (RedBullet, 7, 10.0f, 3.0f, pos, FindAngleTowardsPlayer (pos));
				//TargetedShootAtAngle (RedBullet, 7, 10.0f, 2.5f, pos, FindAngleTowardsPlayer (pos));
			}else if (attackTime % 360 == 90) {
				Vector3 pos = GetPositionOfTurret ();
				//TargetedShootAtAngle (BlueBullet, 7, 10.0f, 5.0f, pos, FindAngleTowardsPlayer (pos));
				TargetedShootAtAngle (BlueBullet, 7, 10.0f, 4.5f, pos, FindAngleTowardsPlayer (pos));
				TargetedShootAtAngle (BlueBullet, 7, 10.0f, 4.0f, pos, FindAngleTowardsPlayer (pos));
				TargetedShootAtAngle (BlueBullet, 7, 10.0f, 3.5f, pos, FindAngleTowardsPlayer (pos));
				TargetedShootAtAngle (BlueBullet, 7, 10.0f, 3.0f, pos, FindAngleTowardsPlayer (pos));
				//TargetedShootAtAngle (BlueBullet, 7, 10.0f, 2.5f, pos, FindAngleTowardsPlayer (pos));
			}else if (attackTime % 360 == 180) {
				Vector3 pos = GetPositionOfTurret ();
				//TargetedShootAtAngle (GreenBullet, 7, 10.0f, 5.0f, pos, FindAngleTowardsPlayer (pos));
				TargetedShootAtAngle (GreenBullet, 7, 10.0f, 4.5f, pos, FindAngleTowardsPlayer (pos));
				TargetedShootAtAngle (GreenBullet, 7, 10.0f, 4.0f, pos, FindAngleTowardsPlayer (pos));
				TargetedShootAtAngle (GreenBullet, 7, 10.0f, 3.5f, pos, FindAngleTowardsPlayer (pos));
				TargetedShootAtAngle (GreenBullet, 7, 10.0f, 3.0f, pos, FindAngleTowardsPlayer (pos));
				//TargetedShootAtAngle (GreenBullet, 7, 10.0f, 2.5f, pos, FindAngleTowardsPlayer (pos));
			}else if (attackTime % 360 == 270) {
				Vector3 pos = GetPositionOfTurret ();
				//TargetedShootAtAngle (YellowBullet, 7, 10.0f, 5.0f, pos, FindAngleTowardsPlayer (pos));
				TargetedShootAtAngle (YellowBullet, 7, 10.0f, 4.5f, pos, FindAngleTowardsPlayer (pos));
				TargetedShootAtAngle (YellowBullet, 7, 10.0f, 4.0f, pos, FindAngleTowardsPlayer (pos));
				TargetedShootAtAngle (YellowBullet, 7, 10.0f, 3.5f, pos, FindAngleTowardsPlayer (pos));
				TargetedShootAtAngle (YellowBullet, 7, 10.0f, 3.0f, pos, FindAngleTowardsPlayer (pos));
				//TargetedShootAtAngle (YellowBullet, 7, 10.0f, 2.5f, pos, FindAngleTowardsPlayer (pos));
			}
		}else if (attackPattern == 2) {
			//behind
			if (attackTime % 7 == 0) {
				TargetedShootAtAngle (RedBullet, 2, 150.0f, 4.0f, transform.position, FindAngleTowardsPlayer (transform.position) + 180.0f);
				TargetedShootAtAngle (BlueBullet, 2, 150.0f, 7.0f, transform.position, FindAngleTowardsPlayer (transform.position) + 180.0f);
				TargetedShootAtAngle (GreenBullet, 2, 150.0f, 6.0f, transform.position, FindAngleTowardsPlayer (transform.position) + 180.0f);
				TargetedShootAtAngle (YellowBullet, 2, 150.0f, 5.0f, transform.position, FindAngleTowardsPlayer (transform.position) + 180.0f);
			}
			//infront
			if (attackTime % 240 == 0) {
				Vector3 pos = GetPositionOfTurret ();
				TargetedShootAtAngle (RedBullet, 10, 5.0f, 4.0f, pos, FindAngleTowardsPlayer (pos));
			} else if (attackTime % 240 == 60) {
				Vector3 pos = GetPositionOfTurret ();
				TargetedShootAtAngle (BlueBullet, 10, 5.0f, 4.0f, pos, FindAngleTowardsPlayer (pos));
			} else if (attackTime % 240 == 120) {
				Vector3 pos = GetPositionOfTurret ();
				TargetedShootAtAngle (GreenBullet, 10, 5.0f, 4.0f, pos, FindAngleTowardsPlayer (pos));
			} else if (attackTime % 240 == 180) {
				Vector3 pos = GetPositionOfTurret ();
				TargetedShootAtAngle (YellowBullet, 10, 5.0f, 4.0f, pos, FindAngleTowardsPlayer (pos));
			}
		}else if (attackPattern == 3) {
			if (attackTime % 240 == 0) {
				Vector3 pos = GetPositionOfTurret ();
				TargetedShootAtAngle (RedMeteor, 2, 50.0f, 7.0f, pos, FindAngleTowardsPlayer (pos));
			}else if (attackTime % 240 == 60) {
				Vector3 pos = GetPositionOfTurret ();
				TargetedShootAtAngle (BlueMeteor, 2, 50.0f, 7.0f, pos, FindAngleTowardsPlayer (pos));
			}else if (attackTime % 240 == 120) {
				Vector3 pos = GetPositionOfTurret ();
				TargetedShootAtAngle (GreenMeteor, 2, 50.0f, 7.0f, pos, FindAngleTowardsPlayer (pos));
			}else if (attackTime % 240 == 180) {
				Vector3 pos = GetPositionOfTurret ();
				TargetedShootAtAngle (YellowMeteor, 2, 50.0f, 7.0f, pos, FindAngleTowardsPlayer (pos));
			}
		}else if (attackPattern == 5) {
			if (attackTime % 7 == 0) {
				TargetedShootAtAngle (RedBullet, 2, 11.0f, 8.0f, transform.position, FindAngleTowardsPlayer (transform.position));
				TargetedShootAtAngle (BlueBullet, 2, 8.0f, 11.0f, transform.position, FindAngleTowardsPlayer (transform.position));
				TargetedShootAtAngle (GreenBullet, 2, 9.0f, 10.0f, transform.position, FindAngleTowardsPlayer (transform.position));
				TargetedShootAtAngle (YellowBullet, 2, 10.0f, 9.0f, transform.position, FindAngleTowardsPlayer (transform.position));
			}
		}else if (attackPattern == 6) {
			if (attackTime % 7 == 0) {
				TargetedShootAtAngle (RedBullet, 3, 8.0f, 8.0f, transform.position, FindAngleTowardsPlayer (transform.position));
				TargetedShootAtAngle (BlueBullet, 3, 5.0f, 11.0f, transform.position, FindAngleTowardsPlayer (transform.position));
				TargetedShootAtAngle (GreenBullet, 3, 6.0f, 10.0f, transform.position, FindAngleTowardsPlayer (transform.position));
				TargetedShootAtAngle (YellowBullet, 3, 7.0f, 9.0f, transform.position, FindAngleTowardsPlayer (transform.position));
			}
		}

	}

	//Shoot Bullets from position
	void Shoot(GameObject Bullet, int BulletsPerShot, float angle, float BulletSpeed, Vector3 position){
		float tempAngle;
		float spread = 360.0f / BulletsPerShot;
		for (int i = 0; i < BulletsPerShot; i++) {
			tempAngle = (angle + (spread * i)) % 360.0f;
			GameObject temp = Instantiate (Bullet, position, Quaternion.Euler (new Vector3 (0, 0, tempAngle - 90.0f))) as GameObject;
			temp.GetComponent<Rigidbody2D> ().velocity = new Vector2 (BulletSpeed * Mathf.Cos ((tempAngle) * Mathf.Deg2Rad), BulletSpeed * Mathf.Sin ((tempAngle) * Mathf.Deg2Rad));
			Destroy (temp, 10.0f);
		}
	}

	//Shoot towards angle from position
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
			Destroy (temp, 10.0f);
		}
	}

	//Finds the angle between this object and the player
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

	//Moves this object towards the player
	void MoveTowardsPlayer (float speed){
		GameObject Player = GameObject.FindGameObjectWithTag("Player");
		if (Player) {
			float x1 = gameObject.transform.position.x, y1 = gameObject.transform.position.y, x2 = Player.gameObject.transform.position.x, y2 = Player.gameObject.transform.position.y;
			float moveAngle = Mathf.Atan2(y2 - y1, x2 - x1);
			Vector3 pos = this.gameObject.transform.position;
			pos.x = pos.x + (Mathf.Cos(moveAngle) * speed);
			pos.y = pos.y + (Mathf.Sin(moveAngle) * speed);
			this.gameObject.transform.position = pos;
		}
	}

	//Returns the position of the shooter
	Vector3 GetPositionOfTurret(){
		Vector3 pos = new Vector3();
		float dist = 0.7f;
		pos.x = transform.position.x + (dist * Mathf.Cos ((FindAngleTowardsPlayer(transform.position)) * Mathf.Deg2Rad));
		pos.y = transform.position.y + (dist * Mathf.Sin ((FindAngleTowardsPlayer(transform.position)) * Mathf.Deg2Rad));
		return pos;
	}

	//Returns true if this game object is dead
	public bool GetDead(){
		return gameObject.GetComponent<HealthScript> ().getHealth () <= 0;
	}

	//Set the attack pattern of this part of Stardust
	public void SetAttackPattern(int att){
		attackPattern = att;
		attackTime = 0;
	}
}
