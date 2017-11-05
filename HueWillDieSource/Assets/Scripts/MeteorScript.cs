using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorScript : MonoBehaviour {

	public GameObject StarBullet;
	public float SpawnBulletLifetime = 10.0f;
	public int NumStars = 36;

	// Use this for initialization
	void Start () {
		
	}

	void OnCollisionEnter2D(Collision2D col){
		Shoot (StarBullet, NumStars, 0.0f, 2.0f);
		/*int i = Random.Range (1, 3);
		Shoot (StarBullet, 9, Mathf.Pow (-1, i) * 0.0f, 2.0f);
		Shoot (StarBullet, 9, Mathf.Pow (-1, i) * 2.0f, 1.8f);
		Shoot (StarBullet, 9, Mathf.Pow (-1, i) * 4.0f, 1.6f);
		Shoot (StarBullet, 9, Mathf.Pow (-1, i) * 6.0f, 1.4f);*/
	}

	//Shoot Bullets
	void Shoot(GameObject Bullet, int BulletsPerShot, float angle, float BulletSpeed){
		float tempAngle;
		float spread = 360.0f / BulletsPerShot;
		for (int i = 0; i < BulletsPerShot; i++){
			tempAngle = (angle + (spread * i)) % 360.0f;
			GameObject temp = Instantiate(Bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, tempAngle - 90.0f))) as GameObject;
			temp.GetComponent<Rigidbody2D>().velocity = new Vector2(BulletSpeed * Mathf.Cos((tempAngle) * Mathf.Deg2Rad), BulletSpeed * Mathf.Sin((tempAngle) * Mathf.Deg2Rad));
			Destroy(temp, SpawnBulletLifetime);
		}
	}
}
