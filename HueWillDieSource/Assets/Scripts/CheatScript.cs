using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatScript : MonoBehaviour {

	//This script is only meant to be used during playtesting
	//It messes with PlayerPrefs

	//Set all of these to false when not playtesting
	public bool EnableAllShields = false;
	public bool SetAllBossesDead = false;
	public bool SetAllBossesAlive = false;
	public bool SetInvincible = false;

	// Use this for initialization
	void Start () {
		if (EnableAllShields == true) {
			PlayerPrefs.SetInt("haveBlueShield", 1);
			PlayerPrefs.SetInt("haveGreenShield", 1);
			PlayerPrefs.SetInt("haveYellowShield", 1);
		}
		if (SetAllBossesDead) {
			PlayerPrefs.SetInt("aegisKilled", 1);
			PlayerPrefs.SetInt("spikeKilled", 1);
			PlayerPrefs.SetInt("oAndSKilled", 1);
			PlayerPrefs.SetInt("boomerKilled", 1);
			PlayerPrefs.SetInt("elBomboKilled", 1);
			PlayerPrefs.SetInt("threeMusketeersKilled", 1);
			PlayerPrefs.SetInt("theDupeKilled", 1);
			PlayerPrefs.SetInt("quatroKilled", 1);
			PlayerPrefs.SetInt("cometKilled", 1);
			PlayerPrefs.SetInt("stardustKilled", 1);
		} else if (SetAllBossesAlive) {
			PlayerPrefs.SetInt("aegisKilled", 0);
			PlayerPrefs.SetInt("spikeKilled", 0);
			PlayerPrefs.SetInt("oAndSKilled", 0);
			PlayerPrefs.SetInt("boomerKilled", 0);
			PlayerPrefs.SetInt("elBomboKilled", 0);
			PlayerPrefs.SetInt("threeMusketeersKilled", 0);
			PlayerPrefs.SetInt("theDupeKilled", 0);
			PlayerPrefs.SetInt("quatroKilled", 0);
			PlayerPrefs.SetInt("cometKilled", 0);
			PlayerPrefs.SetInt("stardustKilled", 0);
		}
		if (SetInvincible) {
			this.gameObject.layer = 21;
		}
	}
}
