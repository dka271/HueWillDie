using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            HealthScript hScript = collision.gameObject.GetComponent<HealthScript>();
			hScript.reduceHealth();
			Destroy(this.gameObject);
        } else
        {
            Destroy(this.gameObject);
        }
    }
}
