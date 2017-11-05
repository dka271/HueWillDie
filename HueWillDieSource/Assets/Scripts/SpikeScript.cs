using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SpikeScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            HealthScript hScript = collision.gameObject.GetComponent<HealthScript>();
            hScript.reduceHealthByAmount(25);
        }
        else if (collision.gameObject.tag == "Player")
        {
			Destroy(collision.gameObject);
			//respawn ();
        }
	}

	void respawn()
	{
		SceneManager.LoadScene (PlayerPrefs.GetString ("worldToReturnTo"));
	}
}
