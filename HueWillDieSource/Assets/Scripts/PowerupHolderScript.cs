using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupHolderScript : MonoBehaviour {

    public GameObject[] walls;
    public int size;
    public string[] playerPrefBossNames;

	// Use this for initialization
	void Start () {
		//PlayerPrefs.SetInt ("oAndSKilled", 1);
		//PlayerPrefs.SetInt ("spikeKilled", 1);
		//PlayerPrefs.SetInt ("aegisKilled", 1);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //print(PlayerPrefs.GetInt("oAndSKilled"));
        int numKilled = PlayerPrefs.GetInt(playerPrefBossNames[0]) + PlayerPrefs.GetInt(playerPrefBossNames[1]);
        if (size == 3)
            numKilled = numKilled + PlayerPrefs.GetInt(playerPrefBossNames[2]);
		//Debug.Log (numKilled);

        if (numKilled >= 1)
            walls[0].SetActive(false);
        if (numKilled >= 2)
            walls[1].SetActive(false);
        if (size == 3 && numKilled >= 3)
            walls[2].SetActive(false);
    }
}
