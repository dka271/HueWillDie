using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

/*Portal Number Warp Destinations:
1:	world01
2:	BossSpikeDude
3:	BossAegis
4:	BossO&S
5:	world01
6:	Area2
7:	BossBoomer
8:	BossElBombo
9:	Area2
10:	Final Map
12:	Boss3Musketeers
13:	BossDuplicator
13:	BossQuatro
14:	HighScore
15: BossComet
16: BossStardust
*/

public class PortalScript : MonoBehaviour {

    public int portalIdentifier;

    // Use this for initialization
    void Start () {
		//Delete boss portals if the boss has already been defeated
		if (portalIdentifier == 2 && PlayerPrefs.GetInt("spikeKilled") != 0) // Level 1 -> Spike
		{
			Destroy (this.gameObject);
		}
		else if (portalIdentifier == 3 && PlayerPrefs.GetInt("aegisKilled") != 0)// Level 1 -> Aegis
		{
			Destroy (this.gameObject);
		}
		else if (portalIdentifier == 4 && PlayerPrefs.GetInt("oAndSKilled") != 0) // Level 1 -> O&S
		{
			Destroy (this.gameObject);
		}
		else if (portalIdentifier == 7 && PlayerPrefs.GetInt("boomerKilled") != 0) // Level 2 -> Boomer
		{
			Destroy (this.gameObject);
		}
		else if (portalIdentifier == 8 && PlayerPrefs.GetInt("elBomboKilled") != 0) // Level 2 -> El Bombo
		{
			Destroy (this.gameObject);
		}
		else if (portalIdentifier == 11 && PlayerPrefs.GetInt("threeMusketeersKilled") != 0) // Level 2 -> 3 Musketeers
		{
			Destroy (this.gameObject);
		}
		else if (portalIdentifier == 12 && PlayerPrefs.GetInt("theDupeKilled") != 0) // Level 3 -> The Dupe
		{
			Destroy (this.gameObject);
		}
		else if (portalIdentifier == 13 && PlayerPrefs.GetInt("quatroKilled") != 0) // Level 3 -> Quatro
		{
			Destroy (this.gameObject);
		}
		else if (portalIdentifier == 15 && PlayerPrefs.GetInt("cometKilled") != 0) // Level 3 -> Comet
		{
			Destroy (this.gameObject);
		}
		else if (portalIdentifier == 16 && PlayerPrefs.GetInt("stardustKilled") != 0) // Level 3 -> Stardust
		{
			Destroy (this.gameObject);
		}
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        //GlobalVariableScript globalScript = GameObject.FindGameObjectWithTag("MainCamera").gameObject.GetComponent<GlobalVariableScript>();

		if (collision.gameObject.tag == "Player") {
			if (PlayerPrefs.GetInt("gameMode") == 1) {
				//Handle boss rush mode
				LoadNextBoss();
			} else if (PlayerPrefs.GetInt("gameMode") == 2) {
				//Handle challenge mode
				LoadNextChallengeRoom();
			} else if (portalIdentifier == 1) { // Tutorial -> Level 1 (checkpoint)
				PlayerPrefs.SetString("worldToReturnTo", "world01");
				SceneManager.LoadScene ("world01");
				//GlobalVariableScript.curCheckpoint++;
			} else if (portalIdentifier == 2 && PlayerPrefs.GetInt ("spikeKilled") == 0) { // Level 1 -> Spike
				SceneManager.LoadScene ("BossSpikeDude");
			} else if (portalIdentifier == 3 && PlayerPrefs.GetInt ("aegisKilled") == 0) {// Level 1 -> Aegis
				SceneManager.LoadScene ("BossAegis");
			} else if (portalIdentifier == 4 && PlayerPrefs.GetInt ("oAndSKilled") == 0) { // Level 1 -> O&S
				SceneManager.LoadScene ("BossO&S");
			} else if (portalIdentifier == 5) { //Boss -> level1
				SceneManager.LoadScene ("world01");
			} else if (portalIdentifier == 6) { // Level 1 -> Level 2 (checkpoint)
				PlayerPrefs.SetString("worldToReturnTo", "Area2");
				SceneManager.LoadScene ("Area2");
				//GlobalVariableScript.curCheckpoint++;
			} else if (portalIdentifier == 7 && PlayerPrefs.GetInt ("boomerKilled") == 0) { // Level 2 -> Boomer
				SceneManager.LoadScene ("BossBoomer");
			} else if (portalIdentifier == 8 && PlayerPrefs.GetInt ("elBomboKilled") == 0) { // Level 2 -> El Bombo
				SceneManager.LoadScene ("BossElBombo");
			} else if (portalIdentifier == 9) { //Boss -> Level 2
				SceneManager.LoadScene ("Area2");
			} else if (portalIdentifier == 10) { // Level 2 -> Level 3 (checkpoint)
				PlayerPrefs.SetString("worldToReturnTo", "Final Map");
				SceneManager.LoadScene ("Final Map");
				//GlobalVariableScript.curCheckpoint++;
			} else if (portalIdentifier == 11 && PlayerPrefs.GetInt ("threeMusketeersKilled") == 0) { // Level 2 -> 3 Musketeers
				SceneManager.LoadScene ("Boss3Musketeers");
			} else if (portalIdentifier == 12 && PlayerPrefs.GetInt ("theDupeKilled") == 0) { // Level 3 -> The Dupe
				BossTheDupeAIScript.initialTimeToSplit = 180;
				BossTheDupeAIScript.duplicatorCount = 1;
				SceneManager.LoadScene ("BossDuplicator");
			} else if (portalIdentifier == 13 && PlayerPrefs.GetInt ("quatroKilled") == 0) { // Level 3 -> Quatro
				SceneManager.LoadScene ("BossQuatro");
			} else if (portalIdentifier == 14) { // Stardust -> Credits Screen
				SceneManager.LoadScene ("HighScore");
			} else if (portalIdentifier == 15 && PlayerPrefs.GetInt ("cometKilled") == 0) { // Level 3 -> Comet
				SceneManager.LoadScene ("BossComet");
			} else if (portalIdentifier == 16 && PlayerPrefs.GetInt ("stardustKilled") == 0) { // Level 3 -> Stardust
				SceneManager.LoadScene ("BossStardust");
			}
		}
    }

	//Handle boss rush mode
	void LoadNextBoss(){
		int killed = PlayerPrefs.GetInt ("spikeKilled");
		killed += PlayerPrefs.GetInt ("aegisKilled");
		killed += PlayerPrefs.GetInt ("oAndSKilled");
		killed += PlayerPrefs.GetInt ("elBomboKilled");
		killed += PlayerPrefs.GetInt ("boomerKilled");
		killed += PlayerPrefs.GetInt ("threeMusketeersKilled");
		killed += PlayerPrefs.GetInt ("cometKilled");
		killed += PlayerPrefs.GetInt ("theDupeKilled");
		killed += PlayerPrefs.GetInt ("quatroKilled");
		killed += PlayerPrefs.GetInt ("stardustKilled");
		if (killed == 1) {
			SceneManager.LoadScene ("BossAegis");
			//PlayerPrefs.SetString ("worldToReturnTo", "BossAegis");
		} else if (killed == 2) {
			SceneManager.LoadScene ("BossO&S");
			//PlayerPrefs.SetString ("worldToReturnTo", "BossO&S");
		} else if (killed == 3) {
			PlayerPrefs.SetInt ("haveGreenShield", 1);
			SceneManager.LoadScene ("BossElBombo");
			PlayerPrefs.SetString ("worldToReturnTo", "BossElBombo");
		} else if (killed == 4) {
			SceneManager.LoadScene ("BossBoomer");
			//PlayerPrefs.SetString ("worldToReturnTo", "BossBoomer");
		} else if (killed == 5) {
			SceneManager.LoadScene ("Boss3Musketeers");
			//PlayerPrefs.SetString ("worldToReturnTo", "Boss3Musketeers");
		} else if (killed == 6) {
			PlayerPrefs.SetInt ("haveYellowShield", 1);
			SceneManager.LoadScene ("BossComet");
			PlayerPrefs.SetString ("worldToReturnTo", "BossComet");
		} else if (killed == 7) {
			BossTheDupeAIScript.initialTimeToSplit = 180;
			BossTheDupeAIScript.duplicatorCount = 1;
			SceneManager.LoadScene ("BossDuplicator");
			//PlayerPrefs.SetString ("worldToReturnTo", "BossDuplicator");
		} else if (killed == 8) {
			SceneManager.LoadScene ("BossQuatro");
			//PlayerPrefs.SetString ("worldToReturnTo", "BossQuatro");
		} else if (killed == 9) {
			SceneManager.LoadScene ("BossStardust");
			PlayerPrefs.SetString ("worldToReturnTo", "BossStardust");
		} else {
			SceneManager.LoadScene ("HighScore");
		}
	}

	//Handle challenge mode
	void LoadNextChallengeRoom(){
		if (portalIdentifier == 2 && PlayerPrefs.GetInt ("spikeKilled") == 0) {
			SceneManager.LoadScene ("BossSpikeDudeChallenge");
		} else if (portalIdentifier == 3 && PlayerPrefs.GetInt ("aegisKilled") == 0) {
			SceneManager.LoadScene ("BossAegisChallenge");
		} else if (portalIdentifier == 4 && PlayerPrefs.GetInt ("oAndSKilled") == 0) {
			SceneManager.LoadScene ("BossO&SChallenge");
		} else if (portalIdentifier == 7 && PlayerPrefs.GetInt ("boomerKilled") == 0) {
			SceneManager.LoadScene ("BossBoomerChallenge");
		} else if (portalIdentifier == 8 && PlayerPrefs.GetInt ("elBomboKilled") == 0) {
			SceneManager.LoadScene ("BossElBomboChallenge");
		} else if (portalIdentifier == 11 && PlayerPrefs.GetInt ("threeMusketeersKilled") == 0) {
			SceneManager.LoadScene ("Boss3MusketeersChallenge");
		} else if (portalIdentifier == 12 && PlayerPrefs.GetInt ("theDupeKilled") == 0) {
			BossDuplicatorAIChallenge.initialTimeToSplit = 180;
			BossDuplicatorAIChallenge.duplicatorCount = 1;
			SceneManager.LoadScene ("BossDuplicatorChallenge");
		} else if (portalIdentifier == 13 && PlayerPrefs.GetInt ("quatroKilled") == 0) {
			SceneManager.LoadScene ("BossQuatroChallenge");
		} else if (portalIdentifier == 14) {
			SceneManager.LoadScene ("HighScore");
		} else if (portalIdentifier == 15 && PlayerPrefs.GetInt ("cometKilled") == 0) {
			SceneManager.LoadScene ("BossCometChallenge");
		} else if (portalIdentifier == 16 && PlayerPrefs.GetInt ("stardustKilled") == 0) {
			SceneManager.LoadScene ("BossStardustChallenge");
		} else {
			SceneManager.LoadScene ("ChallengeOverworld");
		}
	}
}
