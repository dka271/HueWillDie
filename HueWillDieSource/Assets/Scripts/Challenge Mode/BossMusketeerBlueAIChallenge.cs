using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMusketeerBlueAIChallenge : MonoBehaviour {

	public GameObject BlueBullet;
	public GameObject BlueBomboBullet;
	public GameObject YellowMeteor;
	public GameObject GreenBullet;
	public GameObject RedBullet;
	public GameObject RedMusketeer;
	public GameObject GreenMusketeer;
	public GameObject YellowMusketeer;
	public float BossSpeed = 0.06f;
	public int Musketeers = 4;

	private float moveAngle;
	private int fireTime = 60;
	private int fireTimeRemaining;
	private int fireTimeRed = 20;
	private int fireTimeRemainingRed;
	private int fireTimeGreen = 75;
	private int fireTimeRemainingGreen;
	private int fireTimeYellow = 180;
	private int fireTimeRemainingYellow;
	private int bomboTime = 80;
	private int bomboTimeRemaining;

	// Use this for initialization
	void Start () {
		bomboTimeRemaining = bomboTime;
		fireTimeRemaining = fireTime;
		fireTimeRemainingRed = fireTimeRed;
		fireTimeRemainingGreen = fireTimeGreen;
		fireTimeRemainingYellow = fireTimeYellow;
	}

	// Update is called once per frame
	void FixedUpdate () {
		//Do things when this dies
		GameObject Player = GameObject.FindGameObjectWithTag ("Player");
		if (gameObject.GetComponent<HealthScript> ().getHealth () <= 0) {
			if (Musketeers != 1) {
				Shoot (BlueBullet, 18, 0.0f, 3.0f);
				Shoot (BlueBullet, 18, 10.0f, 3.2f);
			} else {
				if (Player) {
					PlayerPrefs.SetInt ("threeMusketeersKilled", 1);
				}
			}
			if (RedMusketeer) {
				RedMusketeer.GetComponent<BossMusketeerRedAIChallenge>().Musketeers--;
			}
			if (GreenMusketeer) {
				GreenMusketeer.GetComponent<BossMusketeerGreenAIChallenge>().Musketeers--;
			}
			if (YellowMusketeer) {
				YellowMusketeer.GetComponent<BossMusketeerYellowAIChallenge>().Musketeers--;
			}
			Destroy (this.gameObject);
		}

		//GameObject Player = GameObject.FindGameObjectWithTag ("Player");
		if (Player) {
			float x1 = gameObject.transform.position.x, y1 = gameObject.transform.position.y, x2 = Player.gameObject.transform.position.x, y2 = Player.gameObject.transform.position.y;
			moveAngle = Mathf.Atan2 (y2 - y1, x2 - x1);
			Vector3 pos = this.gameObject.transform.position;
			pos.x = pos.x + (Mathf.Cos(moveAngle) * BossSpeed);
			pos.y = pos.y + (Mathf.Sin(moveAngle) * BossSpeed);
			this.gameObject.transform.position = pos;
		}

		//Shoot at player
		fireTimeRemaining--;
		if (fireTimeRemaining <= 0) {
			fireTimeRemaining = fireTime;
			TargetedShoot (BlueBullet, 4, 30.0f, 3.0f);
		}
		if (!GreenMusketeer) {
			//Do green patterned stuff
			fireTimeRemainingGreen--;
			if (fireTimeRemainingGreen <= 0) {
				Shoot(GreenBullet, 14, 0.0f, 3.0f);
				fireTimeRemainingGreen = fireTimeGreen;
			}
		}
		if (!RedMusketeer) {
			//Do red patterned stuff
			fireTimeRemainingRed--;
			if (fireTimeRemainingRed <= 0) {
				TargetedShoot (RedBullet, 3, 30.0f, 3.8f);
				fireTimeRemainingRed = fireTimeRed;
			}
		}
		if (!YellowMusketeer) {
			//Do yellow patterned stuff
			fireTimeRemainingYellow--;
			if (fireTimeRemainingYellow <= 0) {
				TargetedShoot (YellowMeteor, 2, 310.0f, 8.0f);
				fireTimeRemainingYellow = fireTimeYellow;
			}
		}

		//Drop bombos
		bomboTimeRemaining--;
		if (bomboTimeRemaining <= 0) {
			bomboTimeRemaining = bomboTime;
			TargetedShoot (BlueBomboBullet, 1, 0.0f, 0.0f);
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
