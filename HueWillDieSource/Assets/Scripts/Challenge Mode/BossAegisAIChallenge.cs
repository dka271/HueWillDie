using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAegisAIChallenge : MonoBehaviour {

	public GameObject RedBullet;
	public GameObject BlueBullet;
	public GameObject GreenBullet;
	public GameObject YellowBullet;
	public GameObject RedStar;
	public GameObject BlueStar;
	public GameObject GreenStar;
	public GameObject YellowStar;
	public GameObject Shield1;
	public GameObject Shield2;
	public GameObject Shield3;
	public GameObject Shield4;
	public GameObject ShieldMid1;
	public GameObject ShieldMid2;
	public GameObject ShieldMid3;
	public GameObject ShieldMid4;
	public GameObject ShieldFar1;
	public GameObject ShieldFar2;
	public GameObject ShieldFar3;
	public GameObject ShieldFar4;

	private bool enableShoot = false;
	private bool enableTargetedShoot = false;
	private bool enableTrappingShoot = false;
	private float angularVelocity = 0.5f;
	private float angle1;
	private float angle2;
	//private float spreadModifier;
	private int maxTimeToShoot = 4;
	private int maxTimeToTrappingShoot = 4;
	private int maxTimeToTargetedShoot = 60;
	private int timeToShoot;
	private int timeToTrappingShoot;
	private int timeToTargetedShoot;
	private int curBullet;

	// Use this for initialization
	void Start () {
		angle1 = 0.0f;
		angle2 = 0.0f;
		//spreadModifier = 0.0f;
		timeToShoot = maxTimeToShoot;
		timeToTargetedShoot = maxTimeToTargetedShoot;
		timeToTrappingShoot = maxTimeToTrappingShoot;
		curBullet = 0;
	}

	// Update is called once per frame
	void FixedUpdate () {
		//Handle angle updates
		angle1 = (angle1 + angularVelocity) % 360.0f;
		angle2 = (angle2 - angularVelocity) % 360.0f;
		//spreadModifier += 0.05f;

		//Check which shields are down
		if (!(Shield1 || Shield2 || Shield3 || Shield4)) {
			enableTargetedShoot = true;
		}
		if (!(ShieldMid1 || ShieldMid2 || ShieldMid3 || ShieldMid4)) {
			enableTrappingShoot = true;
		}
		if (!(ShieldFar1 || ShieldFar2 || ShieldFar3 || ShieldFar4)) {
			enableShoot = true;
		}

		//Handle shooting
		if (enableShoot) {
			timeToShoot--;
			if (timeToShoot <= 0) {
				timeToShoot = maxTimeToShoot;
				Shoot (RedStar, 2, angle1, 2.0f, transform.position);
				Shoot (BlueStar, 2, angle1 + 90.0f, 2.0f, transform.position);
				Shoot (GreenStar, 2, angle2, 2.0f, transform.position);
				Shoot (YellowStar, 2, angle2 - 90.0f, 2.0f, transform.position);
			}
		}
		if (enableTrappingShoot) {
			timeToTrappingShoot--;
			if (timeToTrappingShoot <= 0) {
				timeToTrappingShoot = maxTimeToTrappingShoot;
				TargetedShoot (RedStar, 2, 150.0f + (60 * Mathf.Sin (angle1 * Mathf.Deg2Rad * 3.0f)), 4.0f, transform.position);
				TargetedShoot (BlueStar, 2, 150.0f + (60 * Mathf.Cos (angle1 * Mathf.Deg2Rad * 3.0f)), 4.0f, transform.position);
				TargetedShoot (GreenStar, 2, 150.0f - (60 * Mathf.Sin (angle1 * Mathf.Deg2Rad * 3.0f)), 4.0f, transform.position);
				TargetedShoot (YellowStar, 2, 150.0f - (60 * Mathf.Cos (angle1 * Mathf.Deg2Rad * 3.0f)), 4.0f, transform.position);
			}
		}
		if (enableTargetedShoot) {
			timeToTargetedShoot--;
			if (timeToTargetedShoot <= 0) {
				timeToTargetedShoot = maxTimeToTargetedShoot;
				if (curBullet == 0) {
					TargetedShoot (RedBullet, 11, 5, 3.0f, transform.position);
					curBullet++;
				} else if (curBullet == 1) {
					TargetedShoot (BlueBullet, 11, 5, 3.0f, transform.position);
					curBullet++;
				} else if (curBullet == 2) {
					TargetedShoot (GreenBullet, 11, 5, 3.0f, transform.position);
					curBullet++;
				} else {
					TargetedShoot (YellowBullet, 11, 5, 3.0f, transform.position);
					curBullet = 0;
				}
			}
		}

	}

	void OnDestroy(){
		GameObject Player = GameObject.FindGameObjectWithTag ("Player");
		if (Player) {
			PlayerPrefs.SetInt ("aegisKilled", 1);
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
				Destroy (temp, 10.0f);
			}
		}
	}
}
