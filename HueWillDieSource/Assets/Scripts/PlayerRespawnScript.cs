using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerRespawnScript : MonoBehaviour {
	
	// Use this for initialization
	void Start () {

    }

	void FixedUpdate(){
		if (Input.GetAxis ("Respawn") > 0.2f) {
			//Reset the scene
			GameObject Player = GameObject.FindGameObjectWithTag ("Player");
			if (!Player) {
				PlayerPrefs.SetInt("numDeaths", PlayerPrefs.GetInt("numDeaths") + 1);
				if (PlayerPrefs.GetInt ("gameMode") == 1) {
					ResetBossRushKills ();
					respawn ();
				} else {
					resetScene ();
					//Reset Duplicator stuff
					BossTheDupeAIScript.initialTimeToSplit = 180;
					BossTheDupeAIScript.duplicatorCount = 1;
					BossDuplicatorAIChallenge.initialTimeToSplit = 180;
					BossDuplicatorAIChallenge.duplicatorCount = 1;
				}
			}
		} else if (Input.GetAxis ("Respawn") < -0.2f) {
			//Respawn the player
			GameObject Player = GameObject.FindGameObjectWithTag ("Player");
			if (!Player) {
				PlayerPrefs.SetInt("numDeaths", PlayerPrefs.GetInt("numDeaths") + 1);
				if (PlayerPrefs.GetInt ("gameMode") == 1) {
					ResetBossRushKills ();
				}
				respawn ();
			}
		}
	}

	//Respawns at a checkpoint
    void respawn(){
		SceneManager.LoadScene (PlayerPrefs.GetString ("worldToReturnTo"));
	}

	//Respawns at the current scene
	void resetScene(){
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}

	//Handles boss rush checkpoints
	void ResetBossRushKills(){
		string spawn = PlayerPrefs.GetString ("worldToReturnTo");
		if (spawn == "BossSpikeDude") {
			PlayerPrefs.SetInt("aegisKilled", 0);
			PlayerPrefs.SetInt("spikeKilled", 0);
			PlayerPrefs.SetInt("oAndSKilled", 0);
		} else if (spawn == "BossElBombo") {
			PlayerPrefs.SetInt("boomerKilled", 0);
			PlayerPrefs.SetInt("elBomboKilled", 0);
			PlayerPrefs.SetInt("threeMusketeersKilled", 0);
		} else if (spawn == "BossComet") {
			PlayerPrefs.SetInt("theDupeKilled", 0);
			PlayerPrefs.SetInt("quatroKilled", 0);
			PlayerPrefs.SetInt("cometKilled", 0);
		} else if (spawn == "BossStardust") {
			PlayerPrefs.SetInt("stardustKilled", 0);
		}
	}
}
