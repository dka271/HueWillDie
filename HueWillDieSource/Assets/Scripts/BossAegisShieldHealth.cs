using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAegisShieldHealth : MonoBehaviour {

	public int Health = 4;

	// Use this for initialization
	void Start () {
		
	}

	public void reduceHealth()
	{
		Health -= 1;
		if (Health <= 0)
		{
			Destroy(this.gameObject);
		}
	}
}
