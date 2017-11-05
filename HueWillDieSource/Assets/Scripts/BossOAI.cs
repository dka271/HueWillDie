using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOAI : MonoBehaviour {

	public GameObject RedBullet;
	public GameObject BlueBullet;
	public GameObject BossS;
	public float BossSpeed = 0.06f;

	private int fireTimeR, fireTimeB;
	private int timeR = 25;
	private int timeB = 120;
	private float moveAngle;

	// Use this for initialization
	void Start () {
		fireTimeR = timeR;
		fireTimeB = timeB;
	}

	// Update is called once per frame
	void FixedUpdate () {
		GameObject Player = GameObject.FindGameObjectWithTag ("Player");
		if (Player) {
			float x1 = gameObject.transform.position.x, y1 = gameObject.transform.position.y, x2 = Player.gameObject.transform.position.x, y2 = Player.gameObject.transform.position.y;
			moveAngle = Mathf.Atan2 (y2 - y1, x2 - x1);
			Vector3 pos = this.gameObject.transform.position;
			pos.x = pos.x + (Mathf.Cos(moveAngle) * BossSpeed);
			pos.y = pos.y + (Mathf.Sin(moveAngle) * BossSpeed);
			this.gameObject.transform.position = pos;
		}

		fireTimeB--;
		fireTimeR--;
		if (fireTimeB == 0) {
			Shoot (BlueBullet, 8, 0.0f, 3.0f);
			Shoot (RedBullet, 8, 22.5f, 3.0f);
			//Shoot (BlueBullet, 8, 22.5f, 2.0f);
			//Shoot (RedBullet, 8, 0.0f, 2.0f);
			fireTimeB = timeB;
		}
		if (fireTimeR == 0) {
			TargetedShoot (BlueBullet, 2, 30.0f, 5.0f);
			TargetedShoot (RedBullet, 1, 0.0f, 5.0f);
			fireTimeR = timeR;
		}

		//Freeze the velocity
		this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
	}

	void OnDestroy(){
		if (!BossS) {
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
