using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuControl : MonoBehaviour {

    public GameObject mainMenu;
    public GameObject helpMenu;
    public GameObject creditsMenu;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void clickedPlay()
    {
        //SceneManager.LoadScene("VerticalLevel");
		GlobalVariableScript.ResetGlobalVariables();

        SceneManager.LoadScene("World00");

    }

	public void clickedBossRush(){
		GlobalVariableScript.StartBossRush();

		SceneManager.LoadScene("BossSpikeDude");
	}

	public void clickedChallengeMode(){
		GlobalVariableScript.StartChallengeMode ();

		SceneManager.LoadScene("ChallengeOverworld");
	}

    public void clickedHelp()
    {
        mainMenu.SetActive(false);
        helpMenu.SetActive(true);
    }

    public void clickedCredits()
    {
        mainMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }

    public void clickedQuit()
    {
        Application.Quit();
    }

    public void clickedBack()
    {
        creditsMenu.SetActive(false);
        helpMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

}
