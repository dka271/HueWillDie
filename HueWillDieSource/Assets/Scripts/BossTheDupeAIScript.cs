using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTheDupeAIScript : MonoBehaviour {

    public GameObject RedBullet;
    public GameObject BlueBullet;
    public GameObject GreenBullet;
    public GameObject YellowBullet;
    public GameObject BossTheDupePrefab;
    public float BossSpeed = 2f;
    public int maxHealth = 50;
	public static int initialTimeToSplit = 180;
	public static int duplicatorCount = 1;
    public int splitTimeIncreaseBy = 60;
    public int numberOfBullets = 5;
    public float bulletSpeed = 2.5f;
    public int timeBetweenRandomShot = 30;
    public int timeBetweenTargetShot = 120;
    public int timeBeforeRandomMove = 90;

    //private int state = 0;
    private int timeTargetShot;
    private int timeRandomShot;
    private int timeRandomMove;
    private int timeToSplit;

    // Use this for initialization
    void Start () {
        Random.InitState((int)System.DateTime.Now.Ticks);
        timeTargetShot = timeBetweenTargetShot;
        timeRandomShot = timeBetweenRandomShot;
        timeRandomMove = timeBeforeRandomMove;
        timeToSplit = initialTimeToSplit;
}

    void FixedUpdate()
    {
        int rand = Random.Range(0, 4);
        GameObject curBullet;
        if (rand == 0)
        {
            curBullet = RedBullet;
        } else if (rand == 1)
        {
            curBullet = BlueBullet;
        }
        else if (rand == 2)
        {
            curBullet = GreenBullet;
        } else
        {
            curBullet = YellowBullet;
        }

        if (timeRandomShot <= 0)
        {
            randomShoot(curBullet);
            timeRandomShot = timeBetweenRandomShot;
        } else
        {
            timeRandomShot--;
        }

        if (timeRandomMove <= 0)
        {
            randomMove();
            timeRandomMove = timeBeforeRandomMove;
        } else
        {
            timeRandomMove--;
        }

        if (timeTargetShot <= 0)
        {
            TargetedShoot(curBullet, 5, 30.0f, 5.0f);
            timeTargetShot = timeBetweenTargetShot;
        } else
        {
            timeTargetShot--;
        }

        if (timeToSplit <= 0)
        {
            Split();
            initialTimeToSplit += splitTimeIncreaseBy;
            timeToSplit = initialTimeToSplit;
        } else
        {
            timeToSplit--;
        }
    }

	void OnDestroy(){
		duplicatorCount--;
		if (duplicatorCount == 0) {
			GameObject Player = GameObject.FindGameObjectWithTag ("Player");
			if (Player) {
				PlayerPrefs.SetInt ("theDupeKilled", 1);
			}
			//Debug.Log ("dup ded");
		}
	}

    void Split()
    {
		duplicatorCount++;
        float tempAngle = Random.Range(0f, 361f);
        GameObject temp = Instantiate(BossTheDupePrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, tempAngle - 90))) as GameObject;
        temp.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed * Mathf.Cos((tempAngle) * Mathf.Deg2Rad), bulletSpeed * Mathf.Sin((tempAngle) * Mathf.Deg2Rad));
		//temp.GetComponent<BossTheDupeAIScript>().i
		BossTheDupeAIScript.initialTimeToSplit += splitTimeIncreaseBy;
        
    }

    //Shoot towards player
    void TargetedShoot(GameObject Bullet, int BulletsPerShot, float Spread, float BulletSpeed)
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        if (Player)
        {
            Vector3 playerPos = Player.gameObject.transform.position;
            playerPos.x = playerPos.x - transform.position.x;
            playerPos.y = playerPos.y - transform.position.y;
            float angle = Mathf.Atan2(playerPos.y, playerPos.x) * Mathf.Rad2Deg;

            //float Spread = 360.0f / BulletsPerShot;

            float startingAngle;
            if (BulletsPerShot > 1)
            {
                startingAngle = angle - ((Spread * (BulletsPerShot - 1)) / 2);
            }
            else
            {
                startingAngle = angle;
            }

            for (int i = 0; i < BulletsPerShot; i++)
            {
                float tempAngle = startingAngle + (i * Spread);
                GameObject temp = Instantiate(Bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, tempAngle - 90))) as GameObject;
                temp.GetComponent<Rigidbody2D>().velocity = new Vector2(BulletSpeed * Mathf.Cos((tempAngle) * Mathf.Deg2Rad), BulletSpeed * Mathf.Sin((tempAngle) * Mathf.Deg2Rad));
                Destroy(temp, 10.0f);
            }
        }
    }

    void randomShoot(GameObject Bullet)
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        if (Player)
        {
            float tempAngle = Random.Range(0f, 361f);
            GameObject temp = Instantiate(Bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, tempAngle - 90))) as GameObject;
            temp.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed * Mathf.Cos((tempAngle) * Mathf.Deg2Rad), bulletSpeed * Mathf.Sin((tempAngle) * Mathf.Deg2Rad));
            Destroy(temp, 10.0f);
        }
    }

    void randomMove()
    {
        float tempAngle = Random.Range(0f, 361f);
        this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(BossSpeed * Mathf.Cos((tempAngle) * Mathf.Deg2Rad), BossSpeed * Mathf.Sin((tempAngle) * Mathf.Deg2Rad));
        //Vector3 pos = this.gameObject.transform.position;
        //pos.x = pos.x + (Mathf.Cos(tempAngle) * BossSpeed);
        //pos.y = pos.y + (Mathf.Sin(tempAngle) * BossSpeed);
        //this.gameObject.transform.position = pos;
    }
}
