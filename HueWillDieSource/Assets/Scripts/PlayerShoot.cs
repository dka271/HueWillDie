using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour
{

    public int FireRate = 10;
    public float BulletSpeed = 15.0f;
    public float BulletLifetime = 2.0f;
    public GameObject bullet;

    private int FireTime;

    // Use this for initialization
    void Start()
    {
        FireTime = 0;
        if (PlayerPrefs.GetInt("gameMode") == 2)
        {
            //Increase bullet lifetime while in challenge mode
            BulletLifetime *= 5.0f;
        }
    }

    void FixedUpdate()
    {
        if (Input.GetAxis("Fire1") > 0)
        {
            if (FireTime <= 0)
            {
                FireTime = FireRate;
                float angle = GetAngle();
                Vector3 pos = this.GetComponent<Transform>().position;
                pos.x = pos.x + (Mathf.Cos((angle + 90) * Mathf.Deg2Rad) * 0.4f);
                pos.y = pos.y + (Mathf.Sin((angle + 90) * Mathf.Deg2Rad) * 0.4f);
                GameObject temp = Instantiate(bullet, pos, Quaternion.Euler(new Vector3(0, 0, angle))) as GameObject;
                temp.GetComponent<Rigidbody2D>().velocity = new Vector2(BulletSpeed * Mathf.Cos((angle + 90) * Mathf.Deg2Rad), BulletSpeed * Mathf.Sin((angle + 90) * Mathf.Deg2Rad));
                Destroy(temp, BulletLifetime);
            }
            else
            {
                FireTime--;
            }
        }
        else if (Input.GetAxis("xControllerShoot") > 0.19 || Input.GetAxis("yControllerShoot") > 0.19 || Input.GetAxis("xControllerShoot") < -0.19 || Input.GetAxis("yControllerShoot") < -0.19)
        {
            if (FireTime <= 0)
            {
                FireTime = FireRate;
                float angle = GetControllerAngle();
                Vector3 pos = this.GetComponent<Transform>().position;
                pos.x = pos.x + (Mathf.Cos((angle + 90) * Mathf.Deg2Rad) * 0.4f);
                pos.y = pos.y + (Mathf.Sin((angle + 90) * Mathf.Deg2Rad) * 0.4f);
                GameObject temp = Instantiate(bullet, pos, Quaternion.Euler(new Vector3(0, 0, angle))) as GameObject;
                temp.GetComponent<Rigidbody2D>().velocity = new Vector2(BulletSpeed * Mathf.Cos((angle + 90) * Mathf.Deg2Rad), BulletSpeed * Mathf.Sin((angle + 90) * Mathf.Deg2Rad));
                Destroy(temp, BulletLifetime);
            }
            else
            {
                FireTime--;
            }
        }
    }

    float GetAngle()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 cameraPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - cameraPos.x;
        mousePos.y = mousePos.y - cameraPos.y;
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        angle -= 90;// + Random.Range(-1.0f, 1.0f);
        return angle;
    }

    float GetControllerAngle()
    {
        float angle = Mathf.Atan2(-1 * Input.GetAxis("yControllerShoot"), Input.GetAxis("xControllerShoot")) * Mathf.Rad2Deg;
        angle -= 90;// + Random.Range(-1.0f, 1.0f);
        return angle;
    }
}
