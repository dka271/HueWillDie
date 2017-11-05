using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanMoveScript : MonoBehaviour {

    public bool canMove = true;

    // Use this for initialization
    void Start()
    {

    }

    public void makeCannotMove()
    {
        canMove = false;
    }

    public void makeCanMove()
    {
        canMove = true;
    }

    public bool getMove()
    {
        return canMove;
    }
}
