using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour {

	public string TargetTag = "Enemy";
	public float GravitationalConstant = 5.0f;
	public int GravityDuration = 600;//How long the gravity is applied
	public int GravityDelay = 30;//How long until gravity starts being applied
	public float MaxVelocity = 8.0f;
	public float MaxForce = 2.0f;

	private GameObject target;
	//private float m1;
	//private float m2;

	// Use this for initialization
	void Start () {
		target = GameObject.FindGameObjectWithTag (TargetTag);
		GravityDuration += GravityDelay;
		//m1 = this.GetComponent<Rigidbody2D> ().mass;
		//m2 = target.GetComponent<Rigidbody2D> ().mass;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//F = G*m1*m2/r^2
		GravityDuration--;
		GravityDelay--;
		if (!(GravityDelay > 0 || GravityDuration <= 0)) {
			float angle = FindAngleTowardsTarget ();
			float distance = FindDistanceToTarget ();
			//float force = (GravitationalConstant * m1 * m2) / Mathf.Pow (distance, 2);
			float force = (GravitationalConstant) / Mathf.Pow (distance, 2);

			//Cap the force
			if (force > MaxForce) {
				force = MaxForce;
			}

			float velX = Mathf.Cos (angle) * force;
			float velY = Mathf.Sin (angle) * force;
			//this.GetComponent<Rigidbody2D> ().velocity = new Vector2 (velX, velY) + this.GetComponent<Rigidbody2D> ().velocity;
			this.GetComponent<Rigidbody2D> ().AddForce(new Vector2(velX, velY));

			//Cap the velocity
			/*if (Mathf.Sqrt (Mathf.Pow (this.GetComponent<Rigidbody2D> ().velocity.x, 2) + Mathf.Pow (this.GetComponent<Rigidbody2D> ().velocity.y, 2)) > MaxVelocity) {
				velX = Mathf.Cos (angle) * MaxVelocity;
				velY = Mathf.Sin (angle) * MaxVelocity;
				this.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
				this.GetComponent<Rigidbody2D> ().AddForce(new Vector2(velX, velY));
			}*/

			//Orient the object
			Vector2 vel = this.GetComponent<Rigidbody2D> ().velocity;
			angle = Mathf.Atan2 (vel.y, vel.x) * Mathf.Rad2Deg;
			OrientTowardsAngle(angle);
		}
	}

	void OrientTowardsAngle (float angle) {
		angle -= 90.0f;
		this.gameObject.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, angle));
	}

	//Finds the angle to the target object
	float FindAngleTowardsTarget(){
		if (target) {
			Vector3 pos = target.gameObject.transform.position;
			pos.x = pos.x - transform.position.x;
			pos.y = pos.y - transform.position.y;
			//pos = target.transform.InverseTransformDirection (pos);
			float angle = Mathf.Atan2 (pos.y, pos.x);// * Mathf.Rad2Deg;
			return angle;
		} else {
			return 0.0f;
		}
	}

	//Finds the distance to the target
	float FindDistanceToTarget(){
		if (target) {
			Vector3 pos = target.gameObject.transform.position;
			float distance = Mathf.Sqrt(Mathf.Pow(pos.x - transform.position.x, 2) + Mathf.Pow(pos.y - transform.position.y,2));
			return distance;
		} else {
			return 1000000.0f;
		}
	}

	//Set the duration of the gravity
	public void SetGravityDuration(int duration){
		GravityDuration = duration;
	}
}
