using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMusketeerRedAIChallenge : MonoBehaviour {

	public GameObject RedBullet;
	public GameObject YellowMeteor;
	public GameObject GreenBullet;
	public GameObject BlueBomboBullet;
	public GameObject BlueMusketeer;
	public GameObject GreenMusketeer;
	public GameObject YellowMusketeer;
	public float BossSpeed = 0.2f;
	public int Musketeers = 4;

	private int waitTime;
	private int idleTime = 150;
	private int state;//0 = idle, 1 = moving, 2 = shooting
	private float moveAngle;
	private int fireTime = 20;
	private int fireTimeRemaining;
	private int fireTimeGreen = 75;
	private int fireTimeRemainingGreen;

	// Use this for initialization
	void Start () {
		waitTime = idleTime;
		state = 0;
		fireTimeRemaining = fireTime;
		fireTimeRemainingGreen = fireTimeGreen;
	}

	// Update is called once per frame
	void FixedUpdate () {

		//Do things when this dies
		GameObject Player = GameObject.FindGameObjectWithTag ("Player");
		if (gameObject.GetComponent<HealthScript> ().getHealth () <= 0) {
			if (Musketeers != 1) {
				Shoot (RedBullet, 18, 0.0f, 3.0f);
				Shoot (RedBullet, 18, 10.0f, 3.2f);
			} else {
				if (Player) {
					PlayerPrefs.SetInt ("threeMusketeersKilled", 1);
				}
			}
			if (BlueMusketeer) {
				BlueMusketeer.GetComponent<BossMusketeerBlueAIChallenge>().Musketeers--;
			}
			if (GreenMusketeer) {
				GreenMusketeer.GetComponent<BossMusketeerGreenAIChallenge>().Musketeers--;
			}
			if (YellowMusketeer) {
				YellowMusketeer.GetComponent<BossMusketeerYellowAIChallenge>().Musketeers--;
			}
			Destroy (this.gameObject);
		}

		if (waitTime <= 0) {
			if (state == 0) {
				waitTime = 21;
				state = 2;
			} else if (state == 1) {
				waitTime = idleTime;
				state = 0;
				//Shoot (RedBullet, 40, 0.0f, 2.25f);
			} else if (state == 2) {
				//GameObject Player = GameObject.FindGameObjectWithTag ("Player");
				if (Player) {
					float x1 = gameObject.transform.position.x, y1 = gameObject.transform.position.y, x2 = Player.gameObject.transform.position.x, y2 = Player.gameObject.transform.position.y;
					float distance = Mathf.Sqrt (Mathf.Pow ((y2 - y1), 2) + Mathf.Pow ((x2 - x1), 2)) + 8.0f;
					moveAngle = Mathf.Atan2 (y2 - y1, x2 - x1);
					waitTime = (int)(distance / BossSpeed);
					state = 1;
				}
				if (!BlueMusketeer) {
					//Do blue patterned stuff
					TargetedShoot (BlueBomboBullet, 1, 0.0f, 0.0f);
				}
				if (!YellowMusketeer) {
					//Do yellow patterned stuff
					TargetedShoot (YellowMeteor, 2, 6.0f, 8.0f);
				}
			}
		} else {
			waitTime--;
			if (state == 1) {
				Vector3 pos = this.gameObject.transform.position;
				pos.x = pos.x + (Mathf.Cos(moveAngle) * BossSpeed);
				pos.y = pos.y + (Mathf.Sin(moveAngle) * BossSpeed);
				this.gameObject.transform.position = pos;
			}
		}

		//Shoot at player
		fireTimeRemaining--;
		if (fireTimeRemaining <= 0) {
			fireTimeRemaining = fireTime;
			TargetedShoot (RedBullet, 3, 30.0f, 3.8f);
		}
		if (!GreenMusketeer) {
			//Do green patterned stuff
			fireTimeRemainingGreen--;
			if (fireTimeRemainingGreen <= 0) {
				TargetedShoot (GreenBullet, 1, 2.0f, 6.5f);
				TargetedShoot (GreenBullet, 2, 4.0f, 6.0f);
				TargetedShoot (GreenBullet, 2, 8.0f, 5.5f);
				TargetedShoot (GreenBullet, 2, 12.0f, 5.0f);
				fireTimeRemainingGreen = fireTimeGreen;
			}
		}

		//Freeze the velocity
		this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
	}

	//On death, do stuff
	void OnDestroy(){
		
	}

	//Shoot Bullets
	void Shoot(GameObject Bullet, int BulletsPerShot, float angle, float BulletSpeed){
		float tempAngle;
		float spread = 360.0f / BulletsPerShot;
		for (int i = 0; i < BulletsPerShot; i++) {
			tempAngle = (angle + (spread * i)) % 360.0f;
			GameObject temp = Instantiate (Bullet, transform.position, Quaternion.Euler (new Vector3 (0, 0, tempAngle - 90.0f))) as GameObject;
			temp.GetComponent<Rigidbody2D> ().velocity = new Vector2 (BulletSpeed * Mathf.Cos ((tempAngle) * Mathf.Deg2Rad), BulletSpeed * Mathf.Sin ((tempAngle) * Mathf.Deg2Rad));
			Destroy (temp, 40.0f);
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
				Destroy (temp, 40.0f);
			}
		}
	}
}
