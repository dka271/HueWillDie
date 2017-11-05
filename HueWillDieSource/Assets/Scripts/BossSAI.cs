using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSAI : MonoBehaviour {

	public GameObject GreenBullet;
	public GameObject RedBullet;
	public GameObject BlueBullet;
	public GameObject BossO;

	private int mainShootTime = 151;
	private int directShootTime = 81;
	private int idleTime = 30;
	private int state;//0 = nondirectional shoot, 1 = directional shoot, 2 and 3 are idle states
	private int waitTime;
	private float angle;
	private float angularVelocity;

	// Use this for initialization
	void Start () {
		waitTime = mainShootTime;
		angle = 0.0f;
		angularVelocity = 0.4f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//Update the angle
		angle = (angle + angularVelocity) % 360.0f;

		//Shoot stuff
		if (waitTime <= 0) {
			if (state == 0) {
				waitTime = idleTime;
				state = 2;
			} else if (state == 1) {
				waitTime = idleTime;
				state = 3;
			} else if (state == 2) {
				waitTime = directShootTime;
				state = 1;
			} else if (state == 3) {
				waitTime = mainShootTime;
				state = 0;
				angularVelocity *= -1.0f;
			}
		} else {
			waitTime--;
			if (state == 0) {
				if ((waitTime % 10) == 0) {
					Shoot (GreenBullet, 4, angle, 2.0f);
				}
			} else if (state == 1) {
				if ((waitTime % 4) == 0) {
					TargetedShoot (GreenBullet, 5, 30.0f, 4.0f);
					//TargetedShoot (GreenBullet, 2, 60.0f, 4.0f);
					//TargetedShoot (RedBullet, 2, 30.0f, 4.5f);
					//TargetedShoot (BlueBullet, 1, 0.0f, 5.0f);
				}
			}
		}

		//Freeze the velocity
		this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
	}

	void OnDestroy(){
		if (!BossO) {
			GameObject Player = GameObject.FindGameObjectWithTag ("Player");
			if (Player) {
				PlayerPrefs.SetInt ("oAndSKilled", 1);
			}
		}
	}

	//Shoot Bullets
	void Shoot(GameObject Bullet, int BulletsPerShot, float angle, float BulletSpeed){
		float tempAngle;
		float spread = 360.0f / BulletsPerShot;
		for (int i = 0; i < BulletsPerShot; i++) {
			tempAngle = (angle + (spread * i)) % 360.0f;
			GameObject temp = Instantiate (Bullet, transform.position, Quaternion.Euler (new Vector3 (0, 0, tempAngle - 90.0f))) as GameObject;
			temp.GetComponent<Rigidbody2D> ().velocity = new Vector2 (BulletSpeed * Mathf.Cos ((tempAngle) * Mathf.Deg2Rad), BulletSpeed * Mathf.Sin ((tempAngle) * Mathf.Deg2Rad));
			Destroy (temp, 10.0f);
		}
	}

	//Shoot towards player
	void TargetedShoot(GameObject Bullet, int BulletsPerShot, float Spread, float BulletSpeed){
		GameObject Player = GameObject.FindGameObjectWithTag ("Player");
		if (Player) {
			Vector3 playerPos = Player.gameObject.transform.position;
			playerPos.x = playerPos.x - transform.position.x;
			playerPos.y = playerPos.y - transform.position.y;
			float angle = Mathf.Atan2 (playerPos.y, playerPos.x) * Mathf.Rad2Deg;

			//float Spread = 360.0f / BulletsPerShot;

			float startingAngle;
			if (BulletsPerShot > 1) {
				startingAngle = angle - ((Spread * (BulletsPerShot - 1)) / 2);
			} else {
				startingAngle = angle;
			}

			for (int i = 0; i < BulletsPerShot; i++) {
				float tempAngle = startingAngle + (i * Spread);
				GameObject temp = Instantiate (Bullet, transform.position, Quaternion.Euler (new Vector3 (0, 0, tempAngle - 90))) as GameObject;
				temp.GetComponent<Rigidbody2D> ().velocity = new Vector2 (BulletSpeed * Mathf.Cos ((tempAngle) * Mathf.Deg2Rad), BulletSpeed * Mathf.Sin ((tempAngle) * Mathf.Deg2Rad));
				Destroy (temp, 10.0f);
			}
		}
	}
}
