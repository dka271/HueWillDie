using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossQuatroAIChallenge : MonoBehaviour {

	public GameObject RedBullet;
	public GameObject BlueBullet;
	public GameObject GreenBullet;
	public GameObject YellowBullet;
	public GameObject RedBomboBullet;
	public GameObject BlueBomboBullet;
	public GameObject GreenBomboBullet;
	public GameObject YellowBomboBullet;

	private float bulletDuration = 40.0f;

	private int waitTime;
	private int idleTime = 100;
	private int shootingTime = 66;
	private int teleportTime = 80;
	private int state;//0 = idle1, 1 = directional shooting, 2 = idle2, 3 = teleporting
	private float forwardDirection;
	private float angularVelocity;
	private bool teleported;

	// Use this for initialization
	void Start () {
		forwardDirection = 0.0f;
		waitTime = 75;
		state = 0;
		angularVelocity = 4.0f;
		teleported = false;
	}

	// Update is called once per frame
	void FixedUpdate () {
		GameObject Player = GameObject.FindGameObjectWithTag ("Player");
		if (waitTime <= 0) {
			if (state == 0) {
				waitTime = shootingTime;
				angularVelocity *= -1.0f;
				state = 1;
			} else if (state == 1) {
				waitTime = idleTime;
				state = 2;
			} else if (state == 2) {
				//Drop Bombos
				Vector3 redPos = new Vector3(), yellowPos = new Vector3(), greenPos = new Vector3(), bluePos = new Vector3();
				redPos.x = transform.position.x + (0.8f * Mathf.Cos ((forwardDirection + 45.0f) * Mathf.Deg2Rad));
				redPos.y = transform.position.y + (0.8f * Mathf.Sin ((forwardDirection + 45.0f) * Mathf.Deg2Rad));
				yellowPos.x = transform.position.x + (0.8f * Mathf.Cos ((forwardDirection + 225.0f) * Mathf.Deg2Rad));
				yellowPos.y = transform.position.y + (0.8f * Mathf.Sin ((forwardDirection + 225.0f) * Mathf.Deg2Rad));
				greenPos.x = transform.position.x + (0.8f * Mathf.Cos ((forwardDirection + 315.0f) * Mathf.Deg2Rad));
				greenPos.y = transform.position.y + (0.8f * Mathf.Sin ((forwardDirection + 315.0f) * Mathf.Deg2Rad));
				bluePos.x = transform.position.x + (0.8f * Mathf.Cos ((forwardDirection + 135.0f) * Mathf.Deg2Rad));
				bluePos.y = transform.position.y + (0.8f * Mathf.Sin ((forwardDirection + 135.0f) * Mathf.Deg2Rad));
				TargetedShoot (RedBomboBullet, 1, 20, 0.0f, redPos);
				TargetedShoot (YellowBomboBullet, 1, 20, 0.0f, yellowPos);
				TargetedShoot (GreenBomboBullet, 1, 20, 0.0f, greenPos);
				TargetedShoot (BlueBomboBullet, 1, 20, 0.0f, bluePos);
				//Teleport
				gameObject.GetComponent<CircleCollider2D> ().enabled = false;
				Color transparent = new Color (0.4f, 0.4f, 0.4f, 0.5f);
				gameObject.GetComponent<SpriteRenderer> ().color = transparent;
				if (Player) {
					transform.position = Player.transform.position;
				}
				waitTime = teleportTime;
				state = 3;
			} else if (state == 3) {
				gameObject.GetComponent<CircleCollider2D> ().enabled = true;
				Color visible = new Color (1.0f, 1.0f, 1.0f, 1.0f);
				gameObject.GetComponent<SpriteRenderer> ().color = visible;
				teleported = true;
				waitTime = idleTime;
				state = 0;
			}
		} else {
			waitTime--;
			if (state == 1) {
				forwardDirection = (forwardDirection + angularVelocity) % 360.0f;
				Vector3 redPos = new Vector3(), yellowPos = new Vector3(), greenPos = new Vector3(), bluePos = new Vector3();
				redPos.x = transform.position.x + (0.8f * Mathf.Cos ((forwardDirection + 45.0f) * Mathf.Deg2Rad));
				redPos.y = transform.position.y + (0.8f * Mathf.Sin ((forwardDirection + 45.0f) * Mathf.Deg2Rad));
				yellowPos.x = transform.position.x + (0.8f * Mathf.Cos ((forwardDirection + 225.0f) * Mathf.Deg2Rad));
				yellowPos.y = transform.position.y + (0.8f * Mathf.Sin ((forwardDirection + 225.0f) * Mathf.Deg2Rad));
				greenPos.x = transform.position.x + (0.8f * Mathf.Cos ((forwardDirection + 315.0f) * Mathf.Deg2Rad));
				greenPos.y = transform.position.y + (0.8f * Mathf.Sin ((forwardDirection + 315.0f) * Mathf.Deg2Rad));
				bluePos.x = transform.position.x + (0.8f * Mathf.Cos ((forwardDirection + 135.0f) * Mathf.Deg2Rad));
				bluePos.y = transform.position.y + (0.8f * Mathf.Sin ((forwardDirection + 135.0f) * Mathf.Deg2Rad));
				if (waitTime % 8 == 0) {
					TargetedShoot (RedBullet, 2, 3.0f, 8.0f, redPos);
				} else if (waitTime % 8 == 2) {
					TargetedShoot (YellowBullet, 2, 3.0f, 8.0f, yellowPos);
				} else if (waitTime % 8 == 4) {
					TargetedShoot (GreenBullet, 2, 3.0f, 8.0f, greenPos);
				} else if (waitTime % 8 == 6) {
					TargetedShoot (BlueBullet, 2, 3.0f, 8.0f, bluePos);
				}
				if (waitTime % 3 == 0){
					TargetedShootAtAngle(RedBullet, 3, 10.0f, 4.0f - (waitTime / 120.0f), redPos, forwardDirection + 45.0f);
					TargetedShootAtAngle(BlueBullet, 3, 10.0f, 4.0f - (waitTime / 120.0f), bluePos, forwardDirection + 135.0f);
					TargetedShootAtAngle(GreenBullet, 3, 10.0f, 4.0f - (waitTime / 120.0f), greenPos, forwardDirection + 315.0f);
					TargetedShootAtAngle(YellowBullet, 3, 10.0f, 4.0f - (waitTime / 120.0f), yellowPos, forwardDirection + 225.0f);
				}
			} else if (state == 0) {
				if (teleported) {
					teleported = false;
					Shoot (RedBullet, 72, forwardDirection, 2.2f, transform.position);
					Shoot (YellowBullet, 72, forwardDirection, 3.2f, transform.position);
					Shoot (GreenBullet, 72, forwardDirection, 4.2f, transform.position);
					Shoot (BlueBullet, 72, forwardDirection, 5.2f, transform.position);
				}
			}
		}

		//Orient Quatro
		OrientTowardsAngle(forwardDirection);

		//Freeze the velocity
		this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
	}

	void OnDestroy(){
		GameObject Player = GameObject.FindGameObjectWithTag ("Player");
		if (Player) {
			PlayerPrefs.SetInt ("quatroKilled", 1);
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
			Destroy (temp, bulletDuration);
		}
	}

	//Shoot towards player
	void TargetedShoot(GameObject Bullet, int BulletsPerShot, float Spread, float BulletSpeed, Vector3 position){
		GameObject Player = GameObject.FindGameObjectWithTag ("Player");
		if (Player) {
			Vector3 playerPos = Player.gameObject.transform.position;
			playerPos.x = playerPos.x - position.x;
			playerPos.y = playerPos.y - position.y;
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
				GameObject temp = Instantiate (Bullet, position, Quaternion.Euler (new Vector3 (0, 0, tempAngle - 90))) as GameObject;
				temp.GetComponent<Rigidbody2D> ().velocity = new Vector2 (BulletSpeed * Mathf.Cos ((tempAngle) * Mathf.Deg2Rad), BulletSpeed * Mathf.Sin ((tempAngle) * Mathf.Deg2Rad));
				Destroy (temp, bulletDuration);
			}
		}
	}

	void OrientTowardsAngle (float angle) {
		angle -= 90;
		transform.rotation = Quaternion.Euler (new Vector3 (0, 0, angle));
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
