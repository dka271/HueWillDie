using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStardustBaseAIChallenge : MonoBehaviour {

	public GameObject StarYellow;
	public GameObject StarBlue;
	public GameObject StarGreen;
	public GameObject StarRed;
	public GameObject BulletYellow;
	public GameObject BulletBlue;
	public GameObject BulletGreen;
	public GameObject BulletRed;
	public GameObject HomingYellow;
	public GameObject HomingBlue;
	public GameObject HomingGreen;
	public GameObject HomingRed;
	public Sprite DeadSprite;

	private float forwardAngle;
	private float angularVelocity;
	private float angularAcceleration;
	private int time;
	private int spinDirection;
	private int attackPattern;//0 = nothing
	private int attackTime;
	private bool dead;

	private float maxAngularVelocity = 2.0f;
	private float maxAngularAcceleration = 0.05f;

	// Use this for initialization
	void Start () {
		forwardAngle = 0.0f;
		angularVelocity = 0;
		angularAcceleration = maxAngularAcceleration;
		time = 0;
		spinDirection = 1;
		attackPattern = 0;
		attackTime = 0;
		dead = false;
	}

	// Update is called once per frame
	void FixedUpdate () {
		time++;
		attackTime++;

		//Handle death
		if (!dead && GetDead()) {
			gameObject.GetComponent<SpriteRenderer> ().sprite = DeadSprite;
			gameObject.GetComponent<CircleCollider2D> ().enabled = false;
//			Shoot (StarRed, 30, (time % 360) * 1.0f, 2.5f, GetPositionOfTurret(0));
//			Shoot (StarBlue, 30, (time % 360) * 1.0f, 2.5f, GetPositionOfTurret(1));
//			Shoot (StarGreen, 30, (time % 360) * 1.0f, 2.5f, GetPositionOfTurret(2));
//			Shoot (StarYellow, 30, (time % 360) * 1.0f, 2.5f, GetPositionOfTurret(3));
			maxAngularVelocity /= 3.0f;
			angularAcceleration /= 10.0f;
			dead = true;
		}

		//Handle shooting
		HandleAngle (attackPattern);
		if (attackPattern == 1) {
			if (attackTime % 6 == 1) {
				TargetedShootAtAngle (StarRed, 7, 12.0f, 2.5f, GetPositionOfTurret (0), GetAngleOfTurret (0));
				TargetedShootAtAngle (StarBlue, 7, 12.0f, 2.5f, GetPositionOfTurret (1), GetAngleOfTurret (1));
				TargetedShootAtAngle (StarGreen, 7, 12.0f, 2.5f, GetPositionOfTurret (2), GetAngleOfTurret (2));
				TargetedShootAtAngle (StarYellow, 7, 12.0f, 2.5f, GetPositionOfTurret (3), GetAngleOfTurret (3));
			}
		} else if (attackPattern == 2) {
			if (attackTime % 5 == 0) {
				TargetedShootAtAngle (StarRed, 5, 30.0f, 3.5f, GetPositionOfTurret (0), FindAngleTowardsPlayer (GetPositionOfTurret (0)));
				TargetedShootAtAngle (StarBlue, 5, 30.0f, 3.5f, GetPositionOfTurret (1), FindAngleTowardsPlayer (GetPositionOfTurret (1)));
				TargetedShootAtAngle (StarGreen, 5, 30.0f, 3.5f, GetPositionOfTurret (2), FindAngleTowardsPlayer (GetPositionOfTurret (2)));
				TargetedShootAtAngle (StarYellow, 5, 30.0f, 3.5f, GetPositionOfTurret (3), FindAngleTowardsPlayer (GetPositionOfTurret (3)));
			}
		} else if (attackPattern == 3) {
			if (attackTime % 20 == 0) {
				Shoot (StarRed, 5, (time % 360) * 1.0f, 1.5f, GetPositionOfTurret (0));
			} else if (attackTime % 20 == 5) {
				Shoot (StarBlue, 5, (time % 360) * 1.0f, 1.5f, GetPositionOfTurret (1));
			} else if (attackTime % 20 == 10) {
				Shoot (StarGreen, 5, (time % 360) * 1.0f, 1.5f, GetPositionOfTurret (2));
			} else if (attackTime % 20 == 15) {
				Shoot (StarYellow, 5, (time % 360) * 1.0f, 1.5f, GetPositionOfTurret (3));
			}
		} else if (attackPattern == 4) {
			if (attackTime % 120 == 1) {
				Shoot (StarRed, 20, (time % 360) * 1.0f, 3.0f, GetPositionOfTurret (0));
				Shoot (StarRed, 20, (time % 360) * 1.0f, 2.5f, GetPositionOfTurret (0));
				Shoot (StarRed, 20, (time % 360) * 1.0f, 2.0f, GetPositionOfTurret (0));
				Shoot (StarRed, 20, (time % 360) * 1.0f, 1.5f, GetPositionOfTurret (0));
			} else if (attackTime % 120 == 31) {
				Shoot (StarBlue, 20, (time % 360) * 1.0f, 3.0f, GetPositionOfTurret (1));
				Shoot (StarBlue, 20, (time % 360) * 1.0f, 2.5f, GetPositionOfTurret (1));
				Shoot (StarBlue, 20, (time % 360) * 1.0f, 2.0f, GetPositionOfTurret (1));
				Shoot (StarBlue, 20, (time % 360) * 1.0f, 1.5f, GetPositionOfTurret (1));
			} else if (attackTime % 120 == 61) {
				Shoot (StarGreen, 20, (time % 360) * 1.0f, 3.0f, GetPositionOfTurret (2));
				Shoot (StarGreen, 20, (time % 360) * 1.0f, 2.5f, GetPositionOfTurret (2));
				Shoot (StarGreen, 20, (time % 360) * 1.0f, 2.0f, GetPositionOfTurret (2));
				Shoot (StarGreen, 20, (time % 360) * 1.0f, 1.5f, GetPositionOfTurret (2));
			} else if (attackTime % 120 == 91) {
				Shoot (StarYellow, 20, (time % 360) * 1.0f, 3.0f, GetPositionOfTurret (3));
				Shoot (StarYellow, 20, (time % 360) * 1.0f, 2.5f, GetPositionOfTurret (3));
				Shoot (StarYellow, 20, (time % 360) * 1.0f, 2.0f, GetPositionOfTurret (3));
				Shoot (StarYellow, 20, (time % 360) * 1.0f, 1.5f, GetPositionOfTurret (3));
			}
		} else if (attackPattern == 5) {
			if (attackTime % 5 == 0) {
				//TargetedShootAtAngle (HomingRed, 1, 12.0f, 4.5f, GetPositionOfTurret (0), GetAngleOfTurret (0));
				//TargetedShootAtAngle (HomingBlue, 1, 12.0f, 4.5f, GetPositionOfTurret (1), GetAngleOfTurret (1));
				//TargetedShootAtAngle (HomingGreen, 1, 12.0f, 4.5f, GetPositionOfTurret (2), GetAngleOfTurret (2));
				//TargetedShootAtAngle (HomingYellow, 1, 12.0f, 4.5f, GetPositionOfTurret (3), GetAngleOfTurret (3));
				TargetedShootAtAngle (StarRed, 2, 30.0f, 2.5f, GetPositionOfTurret (0), GetAngleOfTurret (0));
				TargetedShootAtAngle (StarBlue, 2, 30.0f, 2.5f, GetPositionOfTurret (1), GetAngleOfTurret (1));
				TargetedShootAtAngle (StarGreen, 2, 30.0f, 2.5f, GetPositionOfTurret (2), GetAngleOfTurret (2));
				TargetedShootAtAngle (StarYellow, 2, 30.0f, 2.5f, GetPositionOfTurret (3), GetAngleOfTurret (3));
			}
		} else if (attackPattern == 6) {
			if (attackTime % 5 == 0) {
				TargetedShootAtAngle (StarRed, 10, 9.0f, 3.5f, GetPositionOfTurret (0), GetAngleOfTurret (0));
				TargetedShootAtAngle (StarBlue, 10, 9.0f, 3.5f, GetPositionOfTurret (1), GetAngleOfTurret (1));
				TargetedShootAtAngle (StarGreen, 10, 9.0f, 3.5f, GetPositionOfTurret (2), GetAngleOfTurret (2));
				TargetedShootAtAngle (StarYellow, 10, 9.0f, 3.5f, GetPositionOfTurret (3), GetAngleOfTurret (3));
			}
		}

		//Orient the base
		OrientTowardsAngle(forwardAngle);
	}

	//Shoot Bullets from position
	void Shoot(GameObject Bullet, int BulletsPerShot, float angle, float BulletSpeed, Vector3 position){
		float tempAngle;
		float spread = 360.0f / BulletsPerShot;
		for (int i = 0; i < BulletsPerShot; i++) {
			tempAngle = (angle + (spread * i)) % 360.0f;
			GameObject temp = Instantiate (Bullet, position, Quaternion.Euler (new Vector3 (0, 0, tempAngle - 90.0f))) as GameObject;
			temp.GetComponent<Rigidbody2D> ().velocity = new Vector2 (BulletSpeed * Mathf.Cos ((tempAngle) * Mathf.Deg2Rad), BulletSpeed * Mathf.Sin ((tempAngle) * Mathf.Deg2Rad));
			Destroy (temp, 20.0f);
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
			Destroy (temp, 20.0f);
		}
	}

	//Update the angle
	void HandleAngle (int pattern){
		if (pattern == 1) {
			if (attackTime % 500 < 250) {
				angularVelocity += angularAcceleration;
			} else {
				angularVelocity -= angularAcceleration;
			}
			if (angularVelocity > maxAngularVelocity) {
				angularVelocity = maxAngularVelocity;
			} else if (angularVelocity < -1 * maxAngularVelocity) {
				angularVelocity = -1 * maxAngularVelocity;
			}
			forwardAngle += angularVelocity;
		} else if (pattern == 2) {
			forwardAngle += maxAngularVelocity;
		} else if (pattern == 3) {
			if (attackTime % 1000 < 500) {
				angularVelocity += angularAcceleration;
			} else {
				angularVelocity -= angularAcceleration;
			}
			if (angularVelocity > maxAngularVelocity) {
				angularVelocity = maxAngularVelocity;
			} else if (angularVelocity < -1 * maxAngularVelocity) {
				angularVelocity = -1 * maxAngularVelocity;
			}
			forwardAngle += angularVelocity;
		} else if (pattern == 4) {
			forwardAngle += (maxAngularVelocity * -1.0f);
		} else if (pattern == 5) {
			if (attackTime % 500 < 250) {
				angularVelocity += 4 * angularAcceleration;
			} else {
				angularVelocity -= 4 * angularAcceleration;
			}
			if (angularVelocity > 4 * maxAngularVelocity) {
				angularVelocity = 4 * maxAngularVelocity;
			} else if (angularVelocity < -4 * maxAngularVelocity) {
				angularVelocity = -4 * maxAngularVelocity;
			}
			forwardAngle += angularVelocity;
		} else if (pattern == 6) {
			if (attackTime == 1) {
				spinDirection *= -1;
			}
			forwardAngle += 5 * maxAngularVelocity * spinDirection;
		}
	}

	//Returns the position of a shooter of a specified color
	//0 is red, 1 is blue, 2 is green, 3 is yellow
	Vector3 GetPositionOfTurret(int color){
		Vector3 pos = new Vector3();
		float dist;
		if (dead) {
			dist = 0.9f;
		} else {
			dist = 1.4f;
		}
		if (color == 0) {
			//Red
			pos.x = transform.position.x + (dist * Mathf.Cos ((forwardAngle + 0.0f) * Mathf.Deg2Rad));
			pos.y = transform.position.y + (dist * Mathf.Sin ((forwardAngle + 0.0f) * Mathf.Deg2Rad));
		} else if (color == 1) {
			//Blue
			pos.x = transform.position.x + (dist * Mathf.Cos ((forwardAngle + 270.0f) * Mathf.Deg2Rad));
			pos.y = transform.position.y + (dist * Mathf.Sin ((forwardAngle + 270.0f) * Mathf.Deg2Rad));
		} else if (color == 2) {
			//Green
			pos.x = transform.position.x + (dist * Mathf.Cos ((forwardAngle + 180.0f) * Mathf.Deg2Rad));
			pos.y = transform.position.y + (dist * Mathf.Sin ((forwardAngle + 180.0f) * Mathf.Deg2Rad));
		} else {
			//Yellow
			pos.x = transform.position.x + (dist * Mathf.Cos ((forwardAngle + 90.0f) * Mathf.Deg2Rad));
			pos.y = transform.position.y + (dist * Mathf.Sin ((forwardAngle + 90.0f) * Mathf.Deg2Rad));
		}
		return pos;
	}

	//Returns the angle of a shooter of a specified color
	//0 is red, 1 is blue, 2 is green, 3 is yellow
	float GetAngleOfTurret(int color){
		Vector3 pos = GetPositionOfTurret (color);
		float angle = Mathf.Atan2 (pos.y - transform.position.y, pos.x - transform.position.x) * Mathf.Rad2Deg;
		return angle;
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

	void OrientTowardsAngle (float angle) {
		angle -= 90.0f;
		angle %= 360.0f;
		transform.rotation = Quaternion.Euler (new Vector3 (0, 0, angle));
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
