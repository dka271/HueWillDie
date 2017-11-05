using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStardustBaseAI : MonoBehaviour {

	public GameObject StarYellow;
	public GameObject StarBlue;
	public GameObject StarGreen;
	public GameObject StarRed;
	public Sprite DeadSprite;

	private float forwardAngle;
	private float angularVelocity;
	private float angularAcceleration;
	private int time;
	private bool dead;
	private bool canShoot;

	private float maxAngularVelocity = 2.0f;
	private float maxAngularAcceleration = 0.05f;

	// Use this for initialization
	void Start () {
		forwardAngle = 0.0f;
		angularVelocity = 0;
		angularAcceleration = maxAngularAcceleration;
		time = 0;
		dead = false;
		canShoot = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		time++;
		HandleAngle ();

		//Handle death
		if (!dead && GetDead()) {
			dead = true;
			gameObject.GetComponent<SpriteRenderer> ().sprite = DeadSprite;
			gameObject.GetComponent<CircleCollider2D> ().enabled = false;
			Vector3 redPos = new Vector3(), yellowPos = new Vector3(), greenPos = new Vector3(), bluePos = new Vector3();
			float dist = 1.4f;
			redPos.x = transform.position.x + (dist * Mathf.Cos ((forwardAngle + 0.0f) * Mathf.Deg2Rad));
			redPos.y = transform.position.y + (dist * Mathf.Sin ((forwardAngle + 0.0f) * Mathf.Deg2Rad));
			yellowPos.x = transform.position.x + (dist * Mathf.Cos ((forwardAngle + 90.0f) * Mathf.Deg2Rad));
			yellowPos.y = transform.position.y + (dist * Mathf.Sin ((forwardAngle + 90.0f) * Mathf.Deg2Rad));
			greenPos.x = transform.position.x + (dist * Mathf.Cos ((forwardAngle + 180.0f) * Mathf.Deg2Rad));
			greenPos.y = transform.position.y + (dist * Mathf.Sin ((forwardAngle + 180.0f) * Mathf.Deg2Rad));
			bluePos.x = transform.position.x + (dist * Mathf.Cos ((forwardAngle + 270.0f) * Mathf.Deg2Rad));
			bluePos.y = transform.position.y + (dist * Mathf.Sin ((forwardAngle + 270.0f) * Mathf.Deg2Rad));
			Shoot (StarRed, 30, (time % 360) * 1.0f, 2.5f, redPos);
			Shoot (StarYellow, 30, (time % 360) * 1.0f, 2.5f, yellowPos);
			Shoot (StarGreen, 30, (time % 360) * 1.0f, 2.5f, greenPos);
			Shoot (StarBlue, 30, (time % 360) * 1.0f, 2.5f, bluePos);
			maxAngularVelocity /= 3.0f;
			angularAcceleration /= 10.0f;
		}

		//Handle shooting
		if (canShoot) {
			if (dead && time % 5 == 0) {
				Vector3 redPos = new Vector3 (), yellowPos = new Vector3 (), greenPos = new Vector3 (), bluePos = new Vector3 ();
				float dist = 0.9f;
				redPos.x = transform.position.x + (dist * Mathf.Cos ((forwardAngle + 0.0f) * Mathf.Deg2Rad));
				redPos.y = transform.position.y + (dist * Mathf.Sin ((forwardAngle + 0.0f) * Mathf.Deg2Rad));
				yellowPos.x = transform.position.x + (dist * Mathf.Cos ((forwardAngle + 90.0f) * Mathf.Deg2Rad));
				yellowPos.y = transform.position.y + (dist * Mathf.Sin ((forwardAngle + 90.0f) * Mathf.Deg2Rad));
				greenPos.x = transform.position.x + (dist * Mathf.Cos ((forwardAngle + 180.0f) * Mathf.Deg2Rad));
				greenPos.y = transform.position.y + (dist * Mathf.Sin ((forwardAngle + 180.0f) * Mathf.Deg2Rad));
				bluePos.x = transform.position.x + (dist * Mathf.Cos ((forwardAngle + 270.0f) * Mathf.Deg2Rad));
				bluePos.y = transform.position.y + (dist * Mathf.Sin ((forwardAngle + 270.0f) * Mathf.Deg2Rad));
				TargetedShootAtAngle (StarRed, 3, 5.0f, 2.2f, redPos, 0.0f + forwardAngle);
				TargetedShootAtAngle (StarYellow, 3, 5.0f, 2.2f, yellowPos, 90.0f + forwardAngle);
				TargetedShootAtAngle (StarGreen, 3, 5.0f, 2.2f, greenPos, 180.0f + forwardAngle);
				TargetedShootAtAngle (StarBlue, 3, 5.0f, 2.2f, bluePos, 270.0f + forwardAngle);
			} else if (!dead && time % 10 == 0) {
				Vector3 redPos = new Vector3 (), yellowPos = new Vector3 (), greenPos = new Vector3 (), bluePos = new Vector3 ();
				float dist = 1.4f;
				redPos.x = transform.position.x + (dist * Mathf.Cos ((forwardAngle + 0.0f) * Mathf.Deg2Rad));
				redPos.y = transform.position.y + (dist * Mathf.Sin ((forwardAngle + 0.0f) * Mathf.Deg2Rad));
				yellowPos.x = transform.position.x + (dist * Mathf.Cos ((forwardAngle + 90.0f) * Mathf.Deg2Rad));
				yellowPos.y = transform.position.y + (dist * Mathf.Sin ((forwardAngle + 90.0f) * Mathf.Deg2Rad));
				greenPos.x = transform.position.x + (dist * Mathf.Cos ((forwardAngle + 180.0f) * Mathf.Deg2Rad));
				greenPos.y = transform.position.y + (dist * Mathf.Sin ((forwardAngle + 180.0f) * Mathf.Deg2Rad));
				bluePos.x = transform.position.x + (dist * Mathf.Cos ((forwardAngle + 270.0f) * Mathf.Deg2Rad));
				bluePos.y = transform.position.y + (dist * Mathf.Sin ((forwardAngle + 270.0f) * Mathf.Deg2Rad));
				Shoot (StarRed, 3, (time % 360) * 1.0f, 1.5f, redPos);
				Shoot (StarYellow, 3, (time % 360) * 1.0f, 1.5f, yellowPos);
				Shoot (StarGreen, 3, (time % 360) * 1.0f, 1.5f, greenPos);
				Shoot (StarBlue, 3, (time % 360) * 1.0f, 1.5f, bluePos);
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
	void HandleAngle (){
		/*if (forwardAngle > 0) {
			angularAcceleration -= 0.001f;
		} else if (forwardAngle < 0) {
			angularAcceleration += 0.001f;
		}
		if (angularAcceleration > maxAngularAcceleration) {
			angularAcceleration = maxAngularAcceleration;
		} else if (angularAcceleration < -1*maxAngularAcceleration) {
			angularAcceleration = -1*maxAngularAcceleration;
		}*/
		if (time % 1000 < 500) {
			angularVelocity += angularAcceleration;
		} else {
			angularVelocity -= angularAcceleration;
		}
		if (angularVelocity > maxAngularVelocity) {
			angularVelocity = maxAngularVelocity;
		} else if (angularVelocity < -1*maxAngularVelocity) {
			angularVelocity = -1*maxAngularVelocity;
		}
		forwardAngle += angularVelocity;
	}

	void OrientTowardsAngle (float angle) {
		angle -= 90.0f;
		angle %= 360.0f;
		transform.rotation = Quaternion.Euler (new Vector3 (0, 0, angle));
	}

	public bool GetDead(){
		return gameObject.GetComponent<HealthScript> ().getHealth () <= 0;
	}

	public void EnableShooting(bool enable){
		canShoot = enable;
	}
}
