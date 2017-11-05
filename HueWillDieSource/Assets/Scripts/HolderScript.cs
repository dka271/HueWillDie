using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolderScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Stops the player from moving
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CanMoveScript moveScript = collision.gameObject.GetComponent<CanMoveScript>();
            moveScript.makeCannotMove();
        }
    }

    //Allows the player to move again
    void OnDestroy()
    {
		GameObject Player = GameObject.FindGameObjectWithTag ("Player");
		if (Player) {
			CanMoveScript canMoveScript = Player.gameObject.GetComponent<CanMoveScript> ();
			canMoveScript.makeCanMove ();
		}
    }
}
