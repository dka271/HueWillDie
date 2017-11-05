using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCometAI : MonoBehaviour {

	public GameObject YellowMeteor;//0
	public GameObject RedMeteor;//1
	public GameObject GreenMeteor;//2
	public GameObject BlueMeteor;//3
	public GameObject YellowBullet;//4
	public GameObject RedBullet;//5
	public GameObject GreenBullet;//6
	public GameObject BlueBullet;//7

	private int state;//0 = idleBeforeCharge, 1 = idleBeforeTarget, 2 = charging, 3 = shooting, 4 = cooldown
	private int chargesRemaining;
	private int waitTime;
	private float forwardAngle;
	private GameObject currentBullet;

	private int chargesPerPhase = 1;
	private int timeBetweenCharges = 20;
	private int chargingTime = 60;
	private int chargingTimeFinal = 100;
	private int cooldownTime = 100;
	private int idleTime = 60;
	private int shootingTime = 360;
	private float chargingSpeed = 0.1f;
	private float followSpeed = 0.1f;

	// Use this for initialization
	void Start () {
		state = 0;
		waitTime = cooldownTime;
		chargesRemaining = chargesPerPhase;
		forwardAngle = -90.0f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (waitTime <= 0) {
			if (state == 0) {
				if (chargesRemaining == 1) {
					//forwardAngle = FindAngleTowardsPlayer (gameObject.transform.position);
					waitTime = chargingTimeFinal;
				} else {
					//forwardAngle = Random.Range (0.0f, 360.0f);
					waitTime = chargingTime;
				}
				chargesRemaining--;
				state = 2;
			} else if (state == 1) {
				waitTime = shootingTime;
				state = 3;
			} else if (state == 2) {
				if (chargesRemaining > 0) {
					waitTime = timeBetweenCharges;
					state = 0;
				} else {
					waitTime = idleTime;
					state = 1;
				}
			} else if (state == 3) {
				waitTime = cooldownTime;
				state = 4;//uncomment this to enable all AI
			} else if (state == 4) {
				chargesRemaining = chargesPerPhase;
				state = 0;
			}
		} else {
			if (state == 2) {
				MoveTowardsAngle (forwardAngle, chargingSpeed);
				if (waitTime % 20 == 0) {
					forwardAngle = FindAngleTowardsPlayer (gameObject.transform.position);
					currentBullet = getNextBullet (4, 7);
					TargetedShootAtAngle (currentBullet, 15, 5, 3.5f, gameObject.transform.position, forwardAngle);
					TargetedShootAtAngle (currentBullet, 15, 5, 3.5f, gameObject.transform.position, (forwardAngle + 120.0f) % 360.0f);
					TargetedShootAtAngle (currentBullet, 15, 5, 3.5f, gameObject.transform.position, (forwardAngle - 120.0f) % 360.0f);
				}
			} else if (state == 3) {
				forwardAngle = FindAngleTowardsPlayer (gameObject.transform.position);
				MoveTowardsPlayer (followSpeed);
				if (waitTime % 45 == 0) {
					Vector3 leftPos = new Vector3(), rightPos = new Vector3();
					leftPos.x = transform.position.x + (0.8f * Mathf.Cos ((forwardAngle + 90.0f) * Mathf.Deg2Rad));
					leftPos.y = transform.position.y + (0.8f * Mathf.Sin ((forwardAngle + 90.0f) * Mathf.Deg2Rad));
					rightPos.x = transform.position.x + (-0.8f * Mathf.Cos ((forwardAngle + 90.0f) * Mathf.Deg2Rad));
					rightPos.y = transform.position.y + (-0.8f * Mathf.Sin ((forwardAngle + 90.0f) * Mathf.Deg2Rad));
					currentBullet = getNextBullet (0, 3);
					TargetedShootAtAngle (currentBullet, 3, 10, 8.0f, leftPos, FindAngleTowardsPlayer(leftPos));
					TargetedShootAtAngle (currentBullet, 3, 10, 8.0f, rightPos, FindAngleTowardsPlayer(rightPos));
				}
			}

			waitTime--;
		}
		//Orient Comet
		//OrientTowardsAngle(forwardAngle);
	}

	void OnDestroy(){
		GameObject Player = GameObject.FindGameObjectWithTag ("Player");
		if (Player) {
			PlayerPrefs.SetInt ("cometKilled", 1);
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
			Destroy (temp, 10.0f);
		}
	}

	//Shoot towards player
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
		transform.rotation = Quaternion.Euler (new Vector3 (0, 0, angle));
	}

	void MoveTowardsAngle (float angle, float speed){
		Vector3 pos = this.gameObject.transform.position;
		pos.x = pos.x + (Mathf.Cos(angle) * speed);
		pos.y = pos.y + (Mathf.Sin(angle) * speed);
		this.gameObject.transform.position = pos;
	}

	void MoveTowardsPlayer (float speed){
		GameObject Player = GameObject.FindGameObjectWithTag("Player");
		if (Player)
		{
			float x1 = gameObject.transform.position.x, y1 = gameObject.transform.position.y, x2 = Player.gameObject.transform.position.x, y2 = Player.gameObject.transform.position.y;
			float moveAngle = Mathf.Atan2(y2 - y1, x2 - x1);
			Vector3 pos = this.gameObject.transform.position;
			pos.x = pos.x + (Mathf.Cos(moveAngle) * speed);
			pos.y = pos.y + (Mathf.Sin(moveAngle) * speed);
			this.gameObject.transform.position = pos;
		}
	}

	GameObject getNextBullet(int min, int max){
		int i = Random.Range (min, max+1);
		if (i == 0) {
			return YellowMeteor;
		}else if (i == 1) {
			return RedMeteor;
		}else if (i == 2) {
			return GreenMeteor;
		}else if (i == 3) {
			return BlueMeteor;
		}else if (i == 4) {
			return YellowBullet;
		}else if (i == 5) {
			return RedBullet;
		}else if (i == 6) {
			return GreenBullet;
		}else{
			return BlueBullet;
		}
	}
}
