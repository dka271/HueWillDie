using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariableScript : MonoBehaviour {

	/* Variables in PlayerPrefs:
    haveBlueShield
    haveGreenShield
    haveYellowShield
    aegisKilled
    spikeKilled
    oAndSKilled
    boomerKilled
    elBomboKilled
    threeMusketeersKilled
    theDupeKilled
    quatroKilled
    cometKilled
    stardustKilled
    curCheckpoint
    curShieldActivated
	numDeaths
	gameMode: 0 = normal, 1 = boss rush, 2 = challenge
	bossesKilled
    */

    // Use this for initialization
    void Start () {
		
	}

	public static void ResetGlobalVariables(){
		PlayerPrefs.SetInt("haveBlueShield", 0);
		PlayerPrefs.SetInt("haveGreenShield", 0);
		PlayerPrefs.SetInt("haveYellowShield", 0);
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
		PlayerPrefs.SetInt ("numDeaths", 0);
		PlayerPrefs.SetInt ("gameMode", 0);
		PlayerPrefs.SetString("worldToReturnTo", "World00");
	}

	public static void StartBossRush(){
		PlayerPrefs.SetInt("haveBlueShield", 1);
		PlayerPrefs.SetInt("haveGreenShield", 0);
		PlayerPrefs.SetInt("haveYellowShield", 0);
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
		PlayerPrefs.SetInt ("numDeaths", 0);
		PlayerPrefs.SetInt ("gameMode", 1);
		PlayerPrefs.SetString("worldToReturnTo", "BossSpikeDude");
	}

	public static void StartChallengeMode(){
		PlayerPrefs.SetInt("haveBlueShield", 1);
		PlayerPrefs.SetInt("haveGreenShield", 1);
		PlayerPrefs.SetInt("haveYellowShield", 1);
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
		PlayerPrefs.SetInt ("numDeaths", 0);
		PlayerPrefs.SetInt ("gameMode", 2);
		PlayerPrefs.SetString("worldToReturnTo", "ChallengeOverworld");
	}
}
