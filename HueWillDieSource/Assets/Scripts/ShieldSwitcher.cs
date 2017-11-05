using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSwitcher : MonoBehaviour
{

    public Sprite YellowShield;
    public Sprite GreenShield;
    public Sprite BlueShield;
    public Sprite RedShield;

    private Sprite[] shields;
    private string[] shieldColorNames;
    private string[] haveShieldColor;
    private int curShield;
    private bool allowShieldUpdate;

    // Use this for initialization
    void Start()
    {
        this.GetComponent<SpriteRenderer>().sprite = RedShield;
        this.gameObject.layer = LayerMask.NameToLayer("Red");
        curShield = 0;

        shields = new Sprite[] { RedShield, BlueShield, GreenShield, YellowShield };
        shieldColorNames = new string[] { "Red", "Blue", "Green", "Yellow" };
        haveShieldColor = new string[] { "haveBlueShield", "haveGreenShield", "haveYellowShield" };
        allowShieldUpdate = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetAxis("ShieldRed") != 0)
        {
            this.GetComponent<SpriteRenderer>().sprite = RedShield;
            this.gameObject.layer = LayerMask.NameToLayer("Red");
            curShield = 0;
        }
        else if (Input.GetAxis("ShieldBlue") != 0 && PlayerPrefs.GetInt("haveBlueShield") == 1)
        {
            this.GetComponent<SpriteRenderer>().sprite = BlueShield;
            this.gameObject.layer = LayerMask.NameToLayer("Blue");
            curShield = 1;
        }
        else if (Input.GetAxis("ShieldGreen") != 0 && PlayerPrefs.GetInt("haveGreenShield") == 1)
        {
            this.GetComponent<SpriteRenderer>().sprite = GreenShield;
            this.gameObject.layer = LayerMask.NameToLayer("Green");
            curShield = 2;
        }
        else if (Input.GetAxis("ShieldYellow") != 0 && PlayerPrefs.GetInt("haveYellowShield") == 1)
        {
            this.GetComponent<SpriteRenderer>().sprite = YellowShield;
            this.gameObject.layer = LayerMask.NameToLayer("Yellow");
        }
        else if ((Input.GetAxis("ControllerShieldSwitchLeftTrig") > 0) && allowShieldUpdate)
        {
            curShield--;
            if (curShield < 0) { curShield = 3; }
            checkIfShieldAvailable("left");
            this.GetComponent<SpriteRenderer>().sprite = shields[curShield];
            this.gameObject.layer = LayerMask.NameToLayer(shieldColorNames[curShield]);
            allowShieldUpdate = false;
        }
        else if ((Input.GetButton("Fire2") == true || Input.GetAxis("ControllerShieldSwitchRightTrig") > 0) && allowShieldUpdate)
        {
            curShield++;
            if (curShield > 3) { curShield = 0; }
            checkIfShieldAvailable("right");
            this.GetComponent<SpriteRenderer>().sprite = shields[curShield];
            this.gameObject.layer = LayerMask.NameToLayer(shieldColorNames[curShield]);
            allowShieldUpdate = false;
        }
        else if (Input.GetAxis("ControllerShieldSwitchLeftTrig") == 0 && Input.GetAxis("ControllerShieldSwitchRightTrig") == 0 && Input.GetButton("Fire2") == false)
        {
            allowShieldUpdate = true;
        }
    }

    void checkIfShieldAvailable(string direction)
    {
        if (curShield == 0)
        {
            //Do nothing because we have this shield, but player prefs doesn't check for red shield
        }
        else if (PlayerPrefs.GetInt(haveShieldColor[curShield - 1]) == 1)
        {
            //Do nothing because we have this shield
        }
        else if (direction == "right")
        {
            curShield++;
            if (curShield > 3) { curShield = 0; }
            checkIfShieldAvailable("right");
        }
        else
        {
            curShield--;
            if (curShield < 0) { curShield = 3; }
            checkIfShieldAvailable("left");
        }
    }
}
