using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementScript : MonoBehaviour
{

    public float EnemySpeed = 0.06f;

    private float moveAngle;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        if (Player)
        {
            float x1 = gameObject.transform.position.x, y1 = gameObject.transform.position.y, x2 = Player.gameObject.transform.position.x, y2 = Player.gameObject.transform.position.y;
            moveAngle = Mathf.Atan2(y2 - y1, x2 - x1);
            Vector3 pos = this.gameObject.transform.position;
            pos.x = pos.x + (Mathf.Cos(moveAngle) * EnemySpeed);
            pos.y = pos.y + (Mathf.Sin(moveAngle) * EnemySpeed);
            this.gameObject.transform.position = pos;
        }   
    }
}
