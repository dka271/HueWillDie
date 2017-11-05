using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour {

	public GameObject Bullet;
	public int ShootTime = 15;
	public int BulletsPerShot = 4;
	public float BulletSpeed = 3.0f;
	public float InitialAngle = 0.0f;
	public float AngularVelocity = 4.0f;
	public float BulletDespawnTime = 10.0f;

	private float angle;
	private int fireTime;

	// Use this for initialization
	void Start () {
		fireTime = ShootTime;
		angle = InitialAngle;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (fireTime <= 0) {
			fireTime = ShootTime;
			Shoot ();
		} else {
			fireTime--;
		}

		angle = (angle + AngularVelocity) % 360.0f;

		//Freeze the velocity
		this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
	}

	//Shoot Bullets
	void Shoot(){
		float tempAngle;
		float spread = 360.0f / BulletsPerShot;
		for (int i = 0; i < BulletsPerShot; i++) {
			tempAngle = (angle + (spread * i)) % 360.0f;
			GameObject temp = Instantiate (Bullet, transform.position, Quaternion.Euler (new Vector3 (0, 0, tempAngle - 90.0f))) as GameObject;
			temp.GetComponent<Rigidbody2D> ().velocity = new Vector2 (BulletSpeed * Mathf.Cos ((tempAngle) * Mathf.Deg2Rad), BulletSpeed * Mathf.Sin ((tempAngle) * Mathf.Deg2Rad));
			Destroy (temp, BulletDespawnTime);
		}
	}
}
