using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMusketeerGreenAI : MonoBehaviour {

	public GameObject GreenBullet;
	public GameObject YellowBullet;
	public GameObject RedMusketeer;
	public GameObject BlueMusketeer;
	public int Musketeers = 3;

	private float moveAngle;
	private int fireTime = 75;
	private int fireTimeRemaining;

	// Use this for initialization
	void Start () {
		fireTimeRemaining = fireTime;
	}

	// Update is called once per frame
	void FixedUpdate () {
		//Change to yellow if this is the last Musketeer
		if (Musketeers == 1) {
			GreenBullet = YellowBullet;
		}

		//Do things when this dies
		GameObject Player = GameObject.FindGameObjectWithTag ("Player");
		if (gameObject.GetComponent<HealthScript> ().getHealth () <= 0) {
			if (Musketeers != 1) {
				Shoot (YellowBullet, 18, 0.0f, 3.0f);
				Shoot (YellowBullet, 18, 10.0f, 3.2f);
			} else {
				if (Player) {
					PlayerPrefs.SetInt ("threeMusketeersKilled", 1);
				}
			}
			if (BlueMusketeer) {
				BlueMusketeer.GetComponent<BossMusketeerBlueAI>().Musketeers--;
			}
			if (RedMusketeer) {
				RedMusketeer.GetComponent<BossMusketeerRedAI>().Musketeers--;
			}
			Destroy (this.gameObject);
		}

		//Shoot at player
		fireTimeRemaining--;
		if (fireTimeRemaining <= 0) {
			fireTimeRemaining = fireTime;
			TargetedShoot (GreenBullet, 1, 2.0f, 6.5f);
			TargetedShoot (GreenBullet, 2, 4.0f, 6.0f);
			TargetedShoot (GreenBullet, 2, 8.0f, 5.5f);
			TargetedShoot (GreenBullet, 2, 12.0f, 5.0f);
			Shoot(GreenBullet, 30, 0.0f, 3.0f);
		}

		//Freeze the velocity
		this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
	}

	//On death, do stuff
	void OnDestroy(){
		/*if (Musketeers != 1) {
			Shoot (YellowBullet, 18, 0.0f, 3.0f);
			Shoot (YellowBullet, 18, 10.0f, 3.2f);
		} else {
			GameObject Player = GameObject.FindGameObjectWithTag ("Player");
			if (Player) {
				PlayerPrefs.SetInt ("threeMusketeersKilled", 1);
			}
		}
		if (BlueMusketeer) {
			BlueMusketeer.GetComponent<BossMusketeerBlueAI>().Musketeers--;
		}
		if (RedMusketeer) {
			RedMusketeer.GetComponent<BossMusketeerRedAI>().Musketeers--;
		}*/
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
