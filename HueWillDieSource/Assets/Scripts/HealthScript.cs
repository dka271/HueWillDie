using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour {

    public int Health = 10;
	public bool DestroyOnHealth0 = true;

	// Use this for initialization
	void Start () {
		
	}

    public void reduceHealth()
    {
        Health -= 1;
		if (Health <= 0 && DestroyOnHealth0)
        {
            Destroy(this.gameObject);
        }
    }

    public void reduceHealthByAmount(int damageAmount)
    {
        Health -= damageAmount;
		if (Health <= 0 && DestroyOnHealth0)
        {
            Destroy(this.gameObject);
        }
    }

    public void increaseHealth()
    {
        Health += 1;
    }

    public int getHealth()
	{
		return Health;
	}

	public void setHealth(int num){
		Health = num;
	}
}
