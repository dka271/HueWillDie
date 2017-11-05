using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBoomerAI : MonoBehaviour {

    public int spawnRate = 180;
    public GameObject shotgunner_Red;
    public GameObject shotgunner_Blue;
    public GameObject shotgunner_Green;
    public GameObject gunner_Red;
    public GameObject gunner_Blue;
    public GameObject gunner_Green;
    public GameObject wavegunner_Red;
    public GameObject wavegunner_Blue;
    public GameObject wavegunner_Green;
    public GameObject enemy_Holder;

    private int waitTime;

	// Use this for initialization
	void Start () {
        waitTime = spawnRate;
        Random.InitState((int)System.DateTime.Now.Ticks);
    }

    void FixedUpdate()
    {
        if (waitTime <= 0)
        {
            waitTime = spawnRate;
			spawnRate -= 3;
			if (spawnRate <= 70) {
				spawnRate = 70;
			}
            spawnEnemies();
        }
        else
        {
            waitTime--;
		}

		//Freeze the velocity
		this.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
    }

	void OnDestroy(){
		GameObject Player = GameObject.FindGameObjectWithTag ("Player");
		if (Player) {
			PlayerPrefs.SetInt ("boomerKilled", 1);
		}
	}

    void spawnEnemies()
    {
        
        int rand1 = Random.Range(0, 10);
        
        GameObject genericEnemy1;
        GameObject genericEnemy2;
        if (rand1 == 0)
        {
            genericEnemy1 = gunner_Red;
        }
        else if (rand1 == 1)
        {
            genericEnemy1 = gunner_Blue;
        }
        else if (rand1 == 2)
        {
            genericEnemy1 = gunner_Green;
        }
        else if (rand1 == 3)
        {
            genericEnemy1 = shotgunner_Red;
        }
        else if (rand1 == 4)
        {
            genericEnemy1 = shotgunner_Blue;
        }
        else if (rand1 == 5)
        {
            genericEnemy1 = shotgunner_Green;
        }
        else if (rand1 == 6)
        {
            genericEnemy1 = wavegunner_Red;
        }
        else if (rand1 == 7)
        {
            genericEnemy1 = wavegunner_Blue;
        }
        else if (rand1 == 8)
        {
            genericEnemy1 = wavegunner_Green;
        }
        else
        {
            genericEnemy1 = enemy_Holder;
        }

        int rand2 = Random.Range(0, 10);

        if (rand2 == 0)
        {
            genericEnemy2 = gunner_Red;
        }
        else if (rand2 == 1)
        {
            genericEnemy2 = gunner_Blue;
        }
        else if (rand2 == 2)
        {
            genericEnemy2 = gunner_Green;
        }
        else if (rand2 == 3)
        {
            genericEnemy2 = shotgunner_Red;
        }
        else if (rand2 == 4)
        {
            genericEnemy2 = shotgunner_Blue;
        }
        else if (rand2 == 5)
        {
            genericEnemy2 = shotgunner_Green;
        }
        else if (rand2 == 6)
        {
            genericEnemy2 = wavegunner_Red;
        }
        else if (rand2 == 7)
        {
            genericEnemy2 = wavegunner_Blue;
        }
        else if (rand2 == 8)
        {
            genericEnemy2 = wavegunner_Green;
        }
        else
        {
            genericEnemy2 = enemy_Holder;
        }

        float angle = 0;
        Vector3 pos1 = this.GetComponent<Transform>().position;
        pos1.x -= 1;
        
        Vector3 pos2 = this.GetComponent<Transform>().position;
        pos2.x += 1;
        
        GameObject tempEnemy1 = Instantiate(genericEnemy1, pos1, Quaternion.Euler(new Vector3(0, 0, angle))) as GameObject;
		GameObject tempEnemy2 = Instantiate(genericEnemy2, pos2, Quaternion.Euler(new Vector3(0, 0, angle))) as GameObject;
		tempEnemy1.GetComponent<EnemyDocileEnable> ().enabled = false;
		tempEnemy2.GetComponent<EnemyDocileEnable> ().enabled = false;
		Destroy (tempEnemy1, 30.0f);
		Destroy (tempEnemy2, 30.0f);
    }
}
