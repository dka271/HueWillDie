using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStardustTurretAI : MonoBehaviour {

	public GameObject YellowMeteor;//0
	public GameObject RedMeteor;//1
	public GameObject GreenMeteor;//2
	public GameObject BlueMeteor;//3
	public GameObject YellowBullet;//4
	public GameObject RedBullet;//5
	public GameObject GreenBullet;//6
	public GameObject BlueBullet;//7

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}

	//Shoot Bullets from position
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

	public bool GetDead(){
		return gameObject.GetComponent<HealthScript> ().getHealth () <= 0;
	}

	//Public shoot functions
	//Call from the main AI script
	public void FirePattern1Red(){
		Vector3 turretPos = new Vector3 ();
		float dist = 1.1f;
		turretPos.x = transform.position.x + (dist * Mathf.Cos ((FindAngleTowardsPlayer(transform.position) + 0.0f) * Mathf.Deg2Rad));
		turretPos.y = transform.position.y + (dist * Mathf.Sin ((FindAngleTowardsPlayer(transform.position) + 0.0f) * Mathf.Deg2Rad));
		TargetedShootAtAngle (RedBullet, 1, 0.0f, 4.5f, turretPos, FindAngleTowardsPlayer (transform.position));
		TargetedShootAtAngle (RedBullet, 2, 4.0f, 4.2f, turretPos, FindAngleTowardsPlayer (transform.position));
		TargetedShootAtAngle (RedBullet, 2, 8.0f, 3.9f, turretPos, FindAngleTowardsPlayer (transform.position));
		TargetedShootAtAngle (RedBullet, 2, 12.0f, 3.6f, turretPos, FindAngleTowardsPlayer (transform.position));
	}

	public void FirePattern1Blue(){
		Vector3 turretPos = new Vector3 ();
		float dist = 1.1f;
		turretPos.x = transform.position.x + (dist * Mathf.Cos ((FindAngleTowardsPlayer(transform.position) + 0.0f) * Mathf.Deg2Rad));
		turretPos.y = transform.position.y + (dist * Mathf.Sin ((FindAngleTowardsPlayer(transform.position) + 0.0f) * Mathf.Deg2Rad));
		TargetedShootAtAngle (BlueBullet, 1, 0.0f, 4.5f, turretPos, FindAngleTowardsPlayer (transform.position));
		TargetedShootAtAngle (BlueBullet, 2, 4.0f, 4.2f, turretPos, FindAngleTowardsPlayer (transform.position));
		TargetedShootAtAngle (BlueBullet, 2, 8.0f, 3.9f, turretPos, FindAngleTowardsPlayer (transform.position));
		TargetedShootAtAngle (BlueBullet, 2, 12.0f, 3.6f, turretPos, FindAngleTowardsPlayer (transform.position));
	}

	public void FirePattern1Green(){
		Vector3 turretPos = new Vector3 ();
		float dist = 1.1f;
		turretPos.x = transform.position.x + (dist * Mathf.Cos ((FindAngleTowardsPlayer(transform.position) + 0.0f) * Mathf.Deg2Rad));
		turretPos.y = transform.position.y + (dist * Mathf.Sin ((FindAngleTowardsPlayer(transform.position) + 0.0f) * Mathf.Deg2Rad));
		TargetedShootAtAngle (GreenBullet, 1, 0.0f, 4.5f, turretPos, FindAngleTowardsPlayer (transform.position));
		TargetedShootAtAngle (GreenBullet, 2, 4.0f, 4.2f, turretPos, FindAngleTowardsPlayer (transform.position));
		TargetedShootAtAngle (GreenBullet, 2, 8.0f, 3.9f, turretPos, FindAngleTowardsPlayer (transform.position));
		TargetedShootAtAngle (GreenBullet, 2, 12.0f, 3.6f, turretPos, FindAngleTowardsPlayer (transform.position));
	}

	public void FirePattern1Yellow(){
		Vector3 turretPos = new Vector3 ();
		float dist = 1.1f;
		turretPos.x = transform.position.x + (dist * Mathf.Cos ((FindAngleTowardsPlayer(transform.position) + 0.0f) * Mathf.Deg2Rad));
		turretPos.y = transform.position.y + (dist * Mathf.Sin ((FindAngleTowardsPlayer(transform.position) + 0.0f) * Mathf.Deg2Rad));
		TargetedShootAtAngle (YellowBullet, 1, 0.0f, 4.5f, turretPos, FindAngleTowardsPlayer (transform.position));
		TargetedShootAtAngle (YellowBullet, 2, 4.0f, 4.2f, turretPos, FindAngleTowardsPlayer (transform.position));
		TargetedShootAtAngle (YellowBullet, 2, 8.0f, 3.9f, turretPos, FindAngleTowardsPlayer (transform.position));
		TargetedShootAtAngle (YellowBullet, 2, 12.0f, 3.6f, turretPos, FindAngleTowardsPlayer (transform.position));
	}

	public void FirePattern2Red(){
		Vector3 turretPos = new Vector3 ();
		float dist = 1.1f;
		turretPos.x = transform.position.x + (dist * Mathf.Cos ((FindAngleTowardsPlayer(transform.position) + 0.0f) * Mathf.Deg2Rad));
		turretPos.y = transform.position.y + (dist * Mathf.Sin ((FindAngleTowardsPlayer(transform.position) + 0.0f) * Mathf.Deg2Rad));
		TargetedShootAtAngle (RedMeteor, 1, 0.0f, 6.5f, turretPos, FindAngleTowardsPlayer (transform.position));
		TargetedShootAtAngle (RedBullet, 2, 4.0f, 6.2f, turretPos, FindAngleTowardsPlayer (transform.position));
		TargetedShootAtAngle (RedBullet, 2, 8.0f, 5.9f, turretPos, FindAngleTowardsPlayer (transform.position));
		TargetedShootAtAngle (RedBullet, 2, 12.0f, 5.6f, turretPos, FindAngleTowardsPlayer (transform.position));
	}

	public void FirePattern2Blue(){
		Vector3 turretPos = new Vector3 ();
		float dist = 1.1f;
		turretPos.x = transform.position.x + (dist * Mathf.Cos ((FindAngleTowardsPlayer(transform.position) + 0.0f) * Mathf.Deg2Rad));
		turretPos.y = transform.position.y + (dist * Mathf.Sin ((FindAngleTowardsPlayer(transform.position) + 0.0f) * Mathf.Deg2Rad));
		TargetedShootAtAngle (BlueMeteor, 1, 0.0f, 6.5f, turretPos, FindAngleTowardsPlayer (transform.position));
		TargetedShootAtAngle (BlueBullet, 2, 4.0f, 6.2f, turretPos, FindAngleTowardsPlayer (transform.position));
		TargetedShootAtAngle (BlueBullet, 2, 8.0f, 5.9f, turretPos, FindAngleTowardsPlayer (transform.position));
		TargetedShootAtAngle (BlueBullet, 2, 12.0f, 5.6f, turretPos, FindAngleTowardsPlayer (transform.position));
	}

	public void FirePattern2Green(){
		Vector3 turretPos = new Vector3 ();
		float dist = 1.1f;
		turretPos.x = transform.position.x + (dist * Mathf.Cos ((FindAngleTowardsPlayer(transform.position) + 0.0f) * Mathf.Deg2Rad));
		turretPos.y = transform.position.y + (dist * Mathf.Sin ((FindAngleTowardsPlayer(transform.position) + 0.0f) * Mathf.Deg2Rad));
		TargetedShootAtAngle (GreenMeteor, 1, 0.0f, 6.5f, turretPos, FindAngleTowardsPlayer (transform.position));
		TargetedShootAtAngle (GreenBullet, 2, 4.0f, 6.2f, turretPos, FindAngleTowardsPlayer (transform.position));
		TargetedShootAtAngle (GreenBullet, 2, 8.0f, 5.9f, turretPos, FindAngleTowardsPlayer (transform.position));
		TargetedShootAtAngle (GreenBullet, 2, 12.0f, 5.6f, turretPos, FindAngleTowardsPlayer (transform.position));
	}

	public void FirePattern2Yellow(){
		Vector3 turretPos = new Vector3 ();
		float dist = 1.1f;
		turretPos.x = transform.position.x + (dist * Mathf.Cos ((FindAngleTowardsPlayer(transform.position) + 0.0f) * Mathf.Deg2Rad));
		turretPos.y = transform.position.y + (dist * Mathf.Sin ((FindAngleTowardsPlayer(transform.position) + 0.0f) * Mathf.Deg2Rad));
		TargetedShootAtAngle (YellowMeteor, 1, 0.0f, 6.5f, turretPos, FindAngleTowardsPlayer (transform.position));
		TargetedShootAtAngle (YellowBullet, 2, 4.0f, 6.2f, turretPos, FindAngleTowardsPlayer (transform.position));
		TargetedShootAtAngle (YellowBullet, 2, 8.0f, 5.9f, turretPos, FindAngleTowardsPlayer (transform.position));
		TargetedShootAtAngle (YellowBullet, 2, 12.0f, 5.6f, turretPos, FindAngleTowardsPlayer (transform.position));
	}

}
