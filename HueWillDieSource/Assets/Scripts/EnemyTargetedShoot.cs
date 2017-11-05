using UnityEngine;
using System.Collections;

public class EnemyTargetedShoot : MonoBehaviour {

	public GameObject Bullet;
	public bool HasSpread = true;
	public float Spread = 10.0f;
	public int BulletsPerShot = 3;
	public int FireRate = 25;
	public float BulletSpeed = 2.5f;
	public float BulletDespawnTime = 10.0f;

	private float angle;
	private int fireTime;

	// Use this for initialization
	void Start () {
		fireTime = FireRate;
	}

	void FixedUpdate () {
		if (fireTime <= 0) {
			fireTime = FireRate;
			TargetedShoot ();
		} else {
			fireTime--;
		}

		//Freeze the velocity
		this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
	}

	void TargetedShoot(){
		GameObject Player = GameObject.FindGameObjectWithTag ("Player");
		if (Player) {
			Vector3 playerPos = Player.gameObject.transform.position;
			playerPos.x = playerPos.x - transform.position.x;
			playerPos.y = playerPos.y - transform.position.y;
			angle = Mathf.Atan2 (playerPos.y, playerPos.x) * Mathf.Rad2Deg;

			if (!HasSpread) {
				Spread = 360.0f / BulletsPerShot;
			}

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
				Destroy (temp, BulletDespawnTime);
			}
		}
	}
}
