using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElBomboBullet : MonoBehaviour {

    public GameObject RedBullet;
    public GameObject BlueBullet;
    public GameObject GreenBullet;
    public GameObject YellowBullet;
    public int numBullet;
    public int timeToExplosion;
    //0 = red explosion
    //1 = blue explosion
    //2 = green explosion
    //3 = yellow explosion
    public int typeOfBullet;
	public float BulletLifetime = 10.0f;

    private int state = 0;
    private int waitTime;

    // Use this for initialization
    void Start () {
        state = typeOfBullet;
        waitTime = timeToExplosion;
	}

    void FixedUpdate()
    {
        if (waitTime <= 0) {
            if (state == 0)
            {
                this.Shoot(RedBullet, numBullet, 0, 2.25f);
                Destroy(this.gameObject);
            } else if (state == 1)
            {
                this.Shoot(BlueBullet, numBullet, 0, 2.25f);
                Destroy(this.gameObject);
            } else if (state == 2)
            {
                this.Shoot(GreenBullet, numBullet, 0, 2.25f);
                Destroy(this.gameObject);
            } else
            {
                this.Shoot(YellowBullet, numBullet, 0, 2.25f);
                Destroy(this.gameObject);
            }
        } else
        {
            waitTime--;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        waitTime = 0;
    }

    void Shoot(GameObject Bullet, int BulletsPerShot, float angle, float BulletSpeed)
    {
        float tempAngle;
        float spread = 360.0f / BulletsPerShot;
        for (int i = 0; i < BulletsPerShot; i++)
        {
            tempAngle = (angle + (spread * i)) % 360.0f;
            GameObject temp = Instantiate(Bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, tempAngle - 90.0f))) as GameObject;
            temp.GetComponent<Rigidbody2D>().velocity = new Vector2(BulletSpeed * Mathf.Cos((tempAngle) * Mathf.Deg2Rad), BulletSpeed * Mathf.Sin((tempAngle) * Mathf.Deg2Rad));
            Destroy(temp, BulletLifetime);
        }
    }
}
