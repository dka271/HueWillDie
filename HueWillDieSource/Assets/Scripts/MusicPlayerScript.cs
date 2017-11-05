using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayerScript : MonoBehaviour {

    public AudioSource menuSong;
    public AudioSource tutorialSong;
	public AudioSource overworldSong1;
	public AudioSource overworldSong2;
	public AudioSource overworldSong3;
	public AudioSource bossSong1;
	public AudioSource bossSong2;
	public AudioSource bossSong3;
    public AudioSource finalSong;

    // Use this for initialization
    void Start () {
        menuSong.Stop();
        tutorialSong.Stop();
		overworldSong1.Stop();
		overworldSong2.Stop();
		overworldSong3.Stop();
		bossSong1.Stop();
		bossSong2.Stop();
		bossSong3.Stop();
        finalSong.Stop();
        menuSong.loop = true;
        tutorialSong.loop = true;
		overworldSong1.loop = true;
		overworldSong2.loop = true;
		overworldSong3.loop = true;
		bossSong1.loop = true;
		bossSong2.loop = true;
		bossSong3.loop = true;
        finalSong.loop = true;

		if (SceneManager.GetActiveScene ().name.Equals ("MenuDuplicate")) {
			//print(SceneManager.GetActiveScene().name);
			menuSong.Play ();
		} else if (SceneManager.GetActiveScene ().name.Equals ("World00")) {
			tutorialSong.Play ();
		} else if (SceneManager.GetActiveScene ().name.Equals ("BossStardust") || SceneManager.GetActiveScene ().name.Equals ("BossStardustChallenge")) {
			finalSong.Play ();
		} else if (SceneManager.GetActiveScene ().name.Equals ("world01")) {
			overworldSong1.Play ();
		} else if (SceneManager.GetActiveScene ().name.Equals ("Area2")) {
			overworldSong2.Play ();
		} else if (SceneManager.GetActiveScene ().name.Equals ("Final Map")) {
			overworldSong3.Play ();
		} else if (SceneManager.GetActiveScene ().name.Equals ("ChallengeOverworld")) {
			Random.InitState((int)System.DateTime.Now.Ticks);
			int rand1 = Random.Range(0, 3);
			if (rand1 == 0) {
				overworldSong1.Play ();
			} else if (rand1 == 1) {
				overworldSong2.Play ();
			} else {
				overworldSong3.Play ();
			}
		} else {
			string[] bosses1 = {
				"BossAegis",
				"BossO&S",
				"BossSpikeDude",
				"BossAegisChallenge",
				"BossO&SChallenge",
				"BossSpikeDudeChallenge"
			};
			for (int i = 0; i < 6; i++) {
				if (SceneManager.GetActiveScene ().name.Equals (bosses1 [i])) {
					bossSong1.Play ();
				}
			}
			string[] bosses2 = {
				"Boss3Musketeers",
				"BossBoomer",
				"BossElBombo",
				"Boss3MusketeersChallenge",
				"BossBoomerChallenge",
				"BossElBomboChallenge"
			};
			for (int i = 0; i < 6; i++) {
				if (SceneManager.GetActiveScene ().name.Equals (bosses2 [i])) {
					bossSong2.Play ();
				}
			}
			string[] bosses3 = {
				"BossDuplicator",
				"BossQuatro",
				"BossComet",
				"BossDuplicatorChallenge",
				"BossQuatroChallenge",
				"BossCometChallenge"
			};
			for (int i = 0; i < 6; i++) {
				if (SceneManager.GetActiveScene ().name.Equals (bosses3 [i])) {
					bossSong3.Play ();
				}
			}
			/*string[] overworlds = { "world01", "Area2", "Final Map" };
			for (int i = 0; i < 3; i++) {
				if (SceneManager.GetActiveScene ().name.Equals (overworlds [i])) {
					overworldSong1.Play ();
				}
			}
			string[] bosses = {
				"Boss3Musketeers",
				"BossAegis",
				"BossBoomer",
				"BossDuplicator",
				"BossElBombo",
				"BossO&S",
				"BossSpikeDude",
				"BossQuatro",
				"BossComet"
			};
			for (int i = 0; i < 9; i++) {
				if (SceneManager.GetActiveScene ().name.Equals (bosses [i])) {
					bossSong1.Play ();
				}
			}*/
		}


    }
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}
}
