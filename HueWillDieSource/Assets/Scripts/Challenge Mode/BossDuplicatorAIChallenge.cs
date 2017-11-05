using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDuplicatorAIChallenge : MonoBehaviour {

	public GameObject RedBullet;
	public GameObject BlueBullet;
	public GameObject GreenBullet;
	public GameObject YellowBullet;
	public GameObject RedStar;
	public GameObject BlueStar;
	public GameObject GreenStar;
	public GameObject YellowStar;
	public GameObject DupeRB;
	public GameObject DupeBG;
	public GameObject DupeGY;
	public GameObject DupeYR;

	public int DuplicatorType = 1;//0 = RB, 1 = BG, 2 = GY, 3 = YR

	private float BossSpeed = 6f;
	public static int initialTimeToSplit = 180;
	public static int duplicatorCount = 1;
	private int splitTimeIncreaseBy = 60;
	private int timeBetweenRandomShot = 10;
	private int timeBetweenTargetShot = 120;
	private int timeBeforeRandomMove = 90;

	//private int state = 0;
	private int timeTargetShot;
	private int timeRandomShot;
	private int timeRandomMove;
	private int timeToSplit;

	// Use this for initialization
	void Start () {
		Random.InitState((int)System.DateTime.Now.Ticks);
		timeToSplit = initialTimeToSplit;
		//Set things for different dupes
		if (DuplicatorType == 0) {
			timeToSplit /= 2;
			BossSpeed = 8f;
			timeBeforeRandomMove = 60;
		} else if (DuplicatorType == 1) {
			timeBetweenTargetShot = 100;
		} else if (DuplicatorType == 2) {
			timeBetweenRandomShot = 10;
			BossSpeed = 5f;
		} else if (DuplicatorType == 3) {
			timeBetweenTargetShot = 40;
			BossSpeed = 7f;
		}
		timeTargetShot = timeBetweenTargetShot;
		timeRandomShot = timeBetweenRandomShot;
		timeRandomMove = timeBeforeRandomMove;
	}

	void FixedUpdate()
	{
		//Handle this being the first duplicator
		if (DuplicatorType == -1) {
			GameObject BossTheDupePrefab;
			int dupe = (int) Random.Range(0, 4);
			if (dupe == 0) {
				BossTheDupePrefab = DupeRB;
			} else if (dupe == 1) {
				BossTheDupePrefab = DupeBG;
			} else if (dupe == 2) {
				BossTheDupePrefab = DupeGY;
			} else {
				BossTheDupePrefab = DupeYR;
			}
			GameObject temp = Instantiate(BossTheDupePrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
			duplicatorCount = 2;
			Destroy (this.gameObject);
		}

		//Pick a random color
		int rand = Random.Range(0, 4);
		GameObject curBullet, curStar;
		if (rand == 0) {
			curBullet = RedBullet;
			curStar = RedStar;
		} else if (rand == 1) {
			curBullet = BlueBullet;
			curStar = BlueStar;
		} else if (rand == 2) {
			curBullet = GreenBullet;
			curStar = GreenStar;
		} else {
			curBullet = YellowBullet;
			curStar = YellowStar;
		}

		//Handle different dupe AIs
		if (DuplicatorType == 0) {
			if (timeRandomMove <= 0) {
				randomMove ();
				timeRandomMove = timeBeforeRandomMove;
			} else {
				timeRandomMove--;
			}
			if (timeToSplit <= 0) {
				Split ();
				initialTimeToSplit += splitTimeIncreaseBy;
				timeToSplit = initialTimeToSplit/2;
			} else {
				timeToSplit--;
			}
		} else if (DuplicatorType == 1) {
			if (timeRandomMove <= 0) {
				randomMove ();
				timeRandomMove = timeBeforeRandomMove;
			} else {
				timeRandomMove--;
			}
			if (timeTargetShot <= 0) {
				TargetedShoot (curBullet, 5, 30.0f, 5.0f);
				timeTargetShot = timeBetweenTargetShot;
			} else {
				timeTargetShot--;
			}
			if (timeToSplit <= 0) {
				Split ();
				initialTimeToSplit += splitTimeIncreaseBy;
				timeToSplit = initialTimeToSplit;
			} else {
				timeToSplit--;
			}
		} else if (DuplicatorType == 2) {
			if (timeRandomShot <= 0) {
				randomShoot (curStar, 2.5f);
				timeRandomShot = timeBetweenRandomShot;
			} else {
				timeRandomShot--;
			}
			if (timeRandomMove <= 0) {
				randomMove ();
				timeRandomMove = timeBeforeRandomMove;
			} else {
				timeRandomMove--;
			}
			if (timeToSplit <= 0) {
				Split ();
				initialTimeToSplit += splitTimeIncreaseBy;
				timeToSplit = initialTimeToSplit;
			} else {
				timeToSplit--;
			}
		} else {
			if (timeRandomMove <= 0) {
				randomMove ();
				timeRandomMove = timeBeforeRandomMove;
			} else {
				timeRandomMove--;
			}
			if (timeTargetShot <= 0) {
				TargetedShoot (curBullet, 2, 10.0f, 6.0f);
				TargetedShoot (curBullet, 1, 10.0f, 6.5f);
				timeTargetShot = timeBetweenTargetShot;
			} else {
				timeTargetShot--;
			}
			if (timeToSplit <= 0) {
				Split ();
				initialTimeToSplit += splitTimeIncreaseBy;
				timeToSplit = initialTimeToSplit;
			} else {
				timeToSplit--;
			}
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
		if (duplicatorCount >= 20) {
			//Prevent this from getting out of hand
			return;
		}

		GameObject BossTheDupePrefab;
		int dupe = DuplicatorType;
		while (dupe == DuplicatorType) {
			dupe = (int)Random.Range (0, 4);
		}
		if (dupe == 0) {
			BossTheDupePrefab = DupeRB;
		} else if (dupe == 1) {
			BossTheDupePrefab = DupeBG;
		} else if (dupe == 2) {
			BossTheDupePrefab = DupeGY;
		} else {
			BossTheDupePrefab = DupeYR;
		}

		duplicatorCount++;
		float tempAngle = Random.Range(0f, 361f);
		GameObject temp = Instantiate(BossTheDupePrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, tempAngle - 90))) as GameObject;
		temp.GetComponent<Rigidbody2D>().velocity = new Vector2(BossSpeed * Mathf.Cos((tempAngle) * Mathf.Deg2Rad), BossSpeed * Mathf.Sin((tempAngle) * Mathf.Deg2Rad));
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
			if (BulletsPerShot > 1) {
				startingAngle = angle - ((Spread * (BulletsPerShot - 1)) / 2);
			} else {
				startingAngle = angle;
			}

			for (int i = 0; i < BulletsPerShot; i++) {
				float tempAngle = startingAngle + (i * Spread);
				GameObject temp = Instantiate (Bullet, transform.position, Quaternion.Euler (new Vector3 (0, 0, tempAngle - 90))) as GameObject;
				temp.GetComponent<Rigidbody2D> ().velocity = new Vector2 (BulletSpeed * Mathf.Cos ((tempAngle) * Mathf.Deg2Rad), BulletSpeed * Mathf.Sin ((tempAngle) * Mathf.Deg2Rad));
				Destroy (temp, 50.0f);
			}
		}
	}

	void randomShoot(GameObject Bullet, float BulletSpeed)
	{
		GameObject Player = GameObject.FindGameObjectWithTag("Player");
		if (Player)
		{
			float tempAngle = Random.Range(0f, 361f);
			GameObject temp = Instantiate(Bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, tempAngle - 90))) as GameObject;
			temp.GetComponent<Rigidbody2D>().velocity = new Vector2(BulletSpeed * Mathf.Cos((tempAngle) * Mathf.Deg2Rad), BulletSpeed * Mathf.Sin((tempAngle) * Mathf.Deg2Rad));
			Destroy(temp, 50.0f);
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
