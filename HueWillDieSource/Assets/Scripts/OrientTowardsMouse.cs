using UnityEngine;
using System.Collections;

public class OrientTowardsMouse : MonoBehaviour
{

    private Vector3 mousePos;
    private Vector3 cameraPos;
    private float angle;

    // Use this for initialization
    void Start()
    {

    }

    void FixedUpdate()
    {
        if (Input.GetAxis("xControllerShoot") > 0.19 || Input.GetAxis("yControllerShoot") > 0.19 || Input.GetAxis("xControllerShoot") < -0.19 || Input.GetAxis("yControllerShoot") < -0.19)
        {
            angle = Mathf.Atan2(-1 * Input.GetAxis("yControllerShoot"), Input.GetAxis("xControllerShoot")) * Mathf.Rad2Deg;
            angle -= 90;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
        else
        {
            mousePos = Input.mousePosition;
            cameraPos = Camera.main.WorldToScreenPoint(transform.position);
            mousePos.x = mousePos.x - cameraPos.x;
            mousePos.y = mousePos.y - cameraPos.y;
            angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            angle -= 90;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }
}
