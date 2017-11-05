using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBoomerChallengeAI : MonoBehaviour
{

    public int spawnRate = 180;
    public int timeToTeleport = 600;
    public int timeToFade = 20; ////(one-tenth of timeToTeleport)
    public GameObject shotgunner_Red;
    public GameObject shotgunner_Blue;
    public GameObject shotgunner_Green;
    public GameObject shotgunner_Yellow;
    public GameObject gunner_Red;
    public GameObject gunner_Blue;
    public GameObject gunner_Green;
    public GameObject gunner_Yellow;
    public GameObject wavegunner_Red;
    public GameObject wavegunner_Blue;
    public GameObject wavegunner_Green;
    public GameObject wavegunner_Yellow;
	public GameObject enemy_Holder;
	public GameObject RedBullet;
	public GameObject BlueBullet;
	public GameObject GreenBullet;
	public GameObject YellowBullet;

	private Vector3 startingPosition;
	private int teleportRange = 17;

    private int waitTime;
    private int teleportTime;
    private int fadeTime;

    // Use this for initialization
    void Start()
    {
        waitTime = spawnRate;
        teleportTime = timeToTeleport;
        fadeTime = timeToFade;
        Random.InitState((int)System.DateTime.Now.Ticks);
		startingPosition = this.gameObject.transform.position;
    }

    void FixedUpdate()
    {
		if (waitTime <= 0) {
			waitTime = spawnRate;
			/*spawnRate -= 3;
			if (spawnRate <= 70) {
				spawnRate = 70;
			}*/
			spawnEnemies ();
		} else {
			waitTime--;
		}


		if (fadeTime <= 0) {
			fadeTime = timeToFade;
			fade ();
		} else {
			fadeTime--;
		}

		if (teleportTime <= 0) {
			teleportTime = timeToTeleport;
			teleport ();
		} else {
			teleportTime--;
		}


        //Freeze the velocity
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }

    void teleport()
	{
		int randx = Random.Range(-1*teleportRange, teleportRange);
		int randy = Random.Range(-1*teleportRange, teleportRange);
        Vector2 bossPos = this.GetComponent<Transform>().position;
		bossPos.x = randx + startingPosition.x;
		bossPos.y = randy + startingPosition.y;

		Shoot (RedBullet, 16, 0.0f, 3.0f);
		Shoot (YellowBullet, 16, 10.0f, 3.5f);

		this.gameObject.transform.position = bossPos;

		Shoot (BlueBullet, 16, 0.0f, 4.5f);
		Shoot (GreenBullet, 16, 10.0f, 4.0f);

		Color renderColor = this.GetComponent<SpriteRenderer> ().color;
		renderColor.a = 1.0f;
		this.GetComponent<SpriteRenderer> ().color = renderColor;
    }

    void fade()
    {
		Color renderColor = this.GetComponent<SpriteRenderer> ().color;
		renderColor.a -= (timeToTeleport / timeToFade) / (timeToTeleport * 2.5f);
		this.GetComponent<SpriteRenderer> ().color = renderColor;
        //Color testColor = new Color(0.1f, 0.1f, 0.1f, 0.1f);
        //Color renderColor = this.GetComponent<SpriteRenderer>().color;
        //renderColor.a -= .1f;
        //this.GetComponent<SpriteRenderer>().color = renderColor;
        //this.gameObject.GetComponent<SpriteRenderer>().color = arrayOfColors[curColor];
        //this.gameObject.GetComponent<SpriteRenderer>().color.a
    }

    void OnDestroy()
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
		if (Player) {
			PlayerPrefs.SetInt ("boomerKilled", 1);
		}
    }

    void spawnEnemies()
    {

        int rand1 = Random.Range(0, 13);

        GameObject genericEnemy1;
		if (rand1 == 0) {
			genericEnemy1 = gunner_Red;
		} else if (rand1 == 1) {
			genericEnemy1 = gunner_Blue;
		} else if (rand1 == 2) {
			genericEnemy1 = gunner_Green;
		} else if (rand1 == 3) {
			genericEnemy1 = gunner_Yellow;
		} else if (rand1 == 4) {
			genericEnemy1 = shotgunner_Red;
		} else if (rand1 == 5) {
			genericEnemy1 = shotgunner_Blue;
		} else if (rand1 == 6) {
			genericEnemy1 = shotgunner_Green;
		} else if (rand1 == 7) {
			genericEnemy1 = shotgunner_Yellow;
		} else if (rand1 == 8) {
			genericEnemy1 = wavegunner_Red;
		} else if (rand1 == 9) {
			genericEnemy1 = wavegunner_Blue;
		} else if (rand1 == 10) {
			genericEnemy1 = wavegunner_Green;
		} else if (rand1 == 11) {
			genericEnemy1 = wavegunner_Yellow;
		} else {
			genericEnemy1 = enemy_Holder;
		}

        /*int rand2 = Random.Range(0, 13);

		if (rand2 == 0) {
			genericEnemy2 = gunner_Red;
		} else if (rand2 == 1) {
			genericEnemy2 = gunner_Blue;
		} else if (rand2 == 2) {
			genericEnemy2 = gunner_Green;
		} else if (rand2 == 3) {
			genericEnemy2 = gunner_Yellow;
		} else if (rand2 == 4) {
			genericEnemy2 = shotgunner_Red;
		} else if (rand2 == 5) {
			genericEnemy2 = shotgunner_Blue;
		} else if (rand2 == 6) {
			genericEnemy2 = shotgunner_Green;
		} else if (rand2 == 7) {
			genericEnemy2 = shotgunner_Yellow;
		} else if (rand2 == 8) {
			genericEnemy2 = wavegunner_Red;
		} else if (rand2 == 9) {
			genericEnemy2 = wavegunner_Blue;
		} else if (rand2 == 10) {
			genericEnemy2 = wavegunner_Green;
		} else if (rand2 == 11) {
			genericEnemy2 = wavegunner_Yellow;
		} else {
			genericEnemy2 = enemy_Holder;
		}

        int rand3 = Random.Range(0, 13);

		if (rand3 == 0) {
			genericEnemy3 = gunner_Red;
		} else if (rand3 == 1) {
			genericEnemy3 = gunner_Blue;
		} else if (rand3 == 2) {
			genericEnemy3 = gunner_Green;
		} else if (rand3 == 3) {
			genericEnemy3 = gunner_Yellow;
		} else if (rand3 == 4) {
			genericEnemy3 = shotgunner_Red;
		} else if (rand3 == 5) {
			genericEnemy3 = shotgunner_Blue;
		} else if (rand3 == 6) {
			genericEnemy3 = shotgunner_Green;
		} else if (rand3 == 7) {
			genericEnemy3 = shotgunner_Yellow;
		} else if (rand3 == 8) {
			genericEnemy3 = wavegunner_Red;
		} else if (rand3 == 9) {
			genericEnemy3 = wavegunner_Blue;
		} else if (rand3 == 10) {
			genericEnemy3 = wavegunner_Green;
		} else if (rand3 == 11) {
			genericEnemy3 = wavegunner_Yellow;
		} else {
			genericEnemy3 = enemy_Holder;
		}

        int rand4 = Random.Range(0, 13);

		if (rand4 == 0) {
			genericEnemy4 = gunner_Red;
		} else if (rand4 == 1) {
			genericEnemy4 = gunner_Blue;
		} else if (rand4 == 2) {
			genericEnemy4 = gunner_Green;
		} else if (rand4 == 3) {
			genericEnemy4 = gunner_Yellow;
		} else if (rand4 == 4) {
			genericEnemy4 = shotgunner_Red;
		} else if (rand4 == 5) {
			genericEnemy4 = shotgunner_Blue;
		} else if (rand4 == 6) {
			genericEnemy4 = shotgunner_Green;
		} else if (rand4 == 7) {
			genericEnemy4 = shotgunner_Yellow;
		} else if (rand4 == 8) {
			genericEnemy4 = wavegunner_Red;
		} else if (rand4 == 9) {
			genericEnemy4 = wavegunner_Blue;
		} else if (rand4 == 10) {
			genericEnemy4 = wavegunner_Green;
		} else if (rand4 == 11) {
			genericEnemy4 = wavegunner_Yellow;
		} else {
			genericEnemy4 = enemy_Holder;
		}*/

        float angle = 0;
        Vector3 pos1 = this.GetComponent<Transform>().position;
        //pos1.x -= 1;
        //pos1.y -= 1;

        /*Vector3 pos2 = this.GetComponent<Transform>().position;
        pos2.x += 1;
        pos2.y += 1;

        Vector3 pos3 = this.GetComponent<Transform>().position;
        pos3.x -= 1;
        pos3.y += 1;

        Vector3 pos4 = this.GetComponent<Transform>().position;
        pos4.x += 1;
        pos4.y -= 1;*/

        GameObject tempEnemy1 = Instantiate(genericEnemy1, pos1, Quaternion.Euler(new Vector3(0, 0, angle))) as GameObject;
        //GameObject tempEnemy2 = Instantiate(genericEnemy2, pos2, Quaternion.Euler(new Vector3(0, 0, angle))) as GameObject;
        //GameObject tempEnemy3 = Instantiate(genericEnemy3, pos3, Quaternion.Euler(new Vector3(0, 0, angle))) as GameObject;
        //GameObject tempEnemy4 = Instantiate(genericEnemy4, pos4, Quaternion.Euler(new Vector3(0, 0, angle))) as GameObject;
        tempEnemy1.GetComponent<EnemyDocileEnable>().enabled = false;
        //tempEnemy2.GetComponent<EnemyDocileEnable>().enabled = false;
        //tempEnemy3.GetComponent<EnemyDocileEnable>().enabled = false;
        //tempEnemy4.GetComponent<EnemyDocileEnable>().enabled = false;
        Destroy(tempEnemy1, 30.0f);
        //Destroy(tempEnemy2, 30.0f);
        //Destroy(tempEnemy3, 30.0f);
        //Destroy(tempEnemy4, 30.0f);
        
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
