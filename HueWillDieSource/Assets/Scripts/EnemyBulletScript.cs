using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(collision.gameObject);
			//respawn ();
        } else
        {
			if (this.gameObject.tag != "Enemy") {
				Destroy (this.gameObject);
			}
        }
    }

	void respawn()
	{
		SceneManager.LoadScene (PlayerPrefs.GetString ("worldToReturnTo"));
	}
}
