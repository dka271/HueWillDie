using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpikeDudeAIChallenge : MonoBehaviour {

	public GameObject RedBullet;
	public GameObject BlueBullet;
	public GameObject GreenBullet;
	public GameObject YellowBullet;
	public float BossSpeed = 0.26f;

	private int waitTime;
	private int chargesRemaining;
	private int idleTime = 150;
	private int betweenChargeTime = 20;
	private int charges = 4;
	private int state;//0 = idle, 1 = moving, 2 = shooting, 3 = charge cooldown
	private float moveAngle;

	// Use this for initialization
	void Start () {
		waitTime = idleTime;
		chargesRemaining = charges;
		state = 0;
		moveAngle = 0.0f;
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (waitTime <= 0) {
			if (state == 0) {
				waitTime = 81;
				state = 2;
			} else if (state == 1) {
				chargesRemaining--;
				if (chargesRemaining == 3) {
					waitTime = betweenChargeTime;
					state = 3;
					Shoot (BlueBullet, 30, 0.0f, 2.35f);
					Shoot (RedBullet, 30, 6.0f, 2.25f);
				} else if (chargesRemaining == 2) {
					waitTime = betweenChargeTime;
					state = 3;
					Shoot (GreenBullet, 30, 0.0f, 2.35f);
					Shoot (BlueBullet, 30, 6.0f, 2.25f);
				} else if (chargesRemaining == 1) {
					waitTime = betweenChargeTime;
					state = 3;
					Shoot (YellowBullet, 30, 0.0f, 2.35f);
					Shoot (GreenBullet, 30, 6.0f, 2.25f);
				} else {
					waitTime = idleTime;
					state = 0;
					Shoot (RedBullet, 30, 0.0f, 2.35f);
					Shoot (YellowBullet, 30, 6.0f, 2.25f);
				}
			} else if (state == 2) {
				GameObject Player = GameObject.FindGameObjectWithTag ("Player");
				if (Player) {
					float x1 = gameObject.transform.position.x, y1 = gameObject.transform.position.y, x2 = Player.gameObject.transform.position.x, y2 = Player.gameObject.transform.position.y;
					float distance = Mathf.Sqrt (Mathf.Pow ((y2 - y1), 2) + Mathf.Pow ((x2 - x1), 2)) + 5.0f;
					moveAngle = Mathf.Atan2 (y2 - y1, x2 - x1);
					waitTime = (int)(distance / BossSpeed);
					state = 1;
					chargesRemaining = charges;
				}
			} else if (state == 3) {
				GameObject Player = GameObject.FindGameObjectWithTag ("Player");
				if (Player) {
					float x1 = gameObject.transform.position.x, y1 = gameObject.transform.position.y, x2 = Player.gameObject.transform.position.x, y2 = Player.gameObject.transform.position.y;
					float distance = Mathf.Sqrt (Mathf.Pow ((y2 - y1), 2) + Mathf.Pow ((x2 - x1), 2)) + 5.0f;
					moveAngle = Mathf.Atan2 (y2 - y1, x2 - x1);
					waitTime = (int)(distance / BossSpeed);
					state = 1;
				}
			}
		} else {
			waitTime--;
			if (state == 1) {
				Vector3 pos = this.gameObject.transform.position;
				pos.x = pos.x + (Mathf.Cos(moveAngle) * BossSpeed);
				pos.y = pos.y + (Mathf.Sin(moveAngle) * BossSpeed);
				this.gameObject.transform.position = pos;
			} else if (state == 2) {
				if (waitTime == 80) {
					Shoot (RedBullet, 50, 0.0f, 4.5f);
				} else if (waitTime == 55) {
					Shoot (BlueBullet, 45, 0.0f, 4.0f);
				} else if (waitTime == 30) {
					Shoot (GreenBullet, 40, 0.0f, 3.5f);
				} else if (waitTime == 5) {
					Shoot (YellowBullet, 40, 0.0f, 3.0f);
				}
			}
		}

		//Freeze the velocity
		this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

		//Set orientation
		//gameObject.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, (moveAngle - 90.0f)));
	}

	void OnDestroy(){
		GameObject Player = GameObject.FindGameObjectWithTag ("Player");
		if (Player) {
			PlayerPrefs.SetInt ("spikeKilled", 1);
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
			Destroy (temp, 20.0f);
		}
	}
}
