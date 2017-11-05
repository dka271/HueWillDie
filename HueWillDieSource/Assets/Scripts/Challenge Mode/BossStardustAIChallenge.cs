using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStardustAIChallenge : MonoBehaviour {

	public GameObject StardustBase;
	public GameObject StardustTurret;

	private bool noBase;
	private bool isMoving;
	private bool isTeleporting;
	private int waitTime;
	private int attackPattern;
	private int attackTime;

	private float slowSpeed = 0.05f;
	private float fastSpeed = 0.09f;
	private int timeBetweenAttacks = 65;
	private int teleportTime = 80;
	private int startingBaseHealth = 600;//600
	private int startingTurretHealth = 300;//300

	// Use this for initialization
	void Start () {
		noBase = false;
		isMoving = false;
		isTeleporting = false;
		waitTime = timeBetweenAttacks;
		attackPattern = 1;
		attackTime = 0;
		StardustBase.gameObject.GetComponent<HealthScript> ().setHealth(startingBaseHealth);
		StardustTurret.gameObject.GetComponent<HealthScript> ().setHealth(startingTurretHealth);
	}

	// Update is called once per frame
	void FixedUpdate () {
		//Handle death
		if (StardustBase.gameObject.GetComponent<BossStardustBaseAIChallenge> ().GetDead () && StardustTurret.gameObject.GetComponent<BossStardustTurretAIChallenge> ().GetDead ()) {
			Destroy (this.gameObject);
		} else if (StardustBase.gameObject.GetComponent<BossStardustBaseAIChallenge> ().GetDead () && !noBase) {
			noBase = true;
		}

		//Handle the attack patterns
		waitTime--;
		attackTime++;
		if (waitTime == 0) {
			//End a teleport
			if (isTeleporting) {
				FinishTeleport ();
			}
			//Handle attack pattern switch
			if (attackPattern == 5) {
				isMoving = true;
			}
			attackTime = 0;
			StardustBase.gameObject.GetComponent<BossStardustBaseAIChallenge> ().SetAttackPattern (attackPattern);
			StardustTurret.gameObject.GetComponent<BossStardustTurretAIChallenge> ().SetAttackPattern (attackPattern);
		} else {
			if (StardustBase.gameObject.GetComponent<HealthScript> ().getHealth () <= 450 && attackPattern == 1) {
				//Enable attack 2
				attackPattern = 2;
				waitTime = timeBetweenAttacks;
				isMoving = false;
				StardustBase.gameObject.GetComponent<BossStardustBaseAIChallenge> ().SetAttackPattern (0);
				StardustTurret.gameObject.GetComponent<BossStardustTurretAIChallenge> ().SetAttackPattern (0);
			}else if (StardustBase.gameObject.GetComponent<HealthScript> ().getHealth () <= 300 && attackPattern == 2) {
				//Enable attack 3
				attackPattern = 3;
				waitTime = timeBetweenAttacks;
				isMoving = false;
				StardustBase.gameObject.GetComponent<BossStardustBaseAIChallenge> ().SetAttackPattern (0);
				StardustTurret.gameObject.GetComponent<BossStardustTurretAIChallenge> ().SetAttackPattern (0);
			}else if (StardustBase.gameObject.GetComponent<HealthScript> ().getHealth () <= 150 && attackPattern == 3) {
				//Enable attack 4
				attackPattern = 4;
				waitTime = timeBetweenAttacks;
				isMoving = false;
				StardustBase.gameObject.GetComponent<BossStardustBaseAIChallenge> ().SetAttackPattern (0);
				StardustTurret.gameObject.GetComponent<BossStardustTurretAIChallenge> ().SetAttackPattern (0);
			}else if (StardustBase.gameObject.GetComponent<HealthScript> ().getHealth () <= 0 && attackPattern == 4) {
				//Enable attack 5
				attackPattern = 5;
				waitTime = timeBetweenAttacks + 40;
				isMoving = false;
				StardustBase.gameObject.GetComponent<BossStardustBaseAIChallenge> ().SetAttackPattern (0);
				StardustTurret.gameObject.GetComponent<BossStardustTurretAIChallenge> ().SetAttackPattern (0);
			}else if (StardustTurret.gameObject.GetComponent<HealthScript> ().getHealth () <= 150 && attackPattern == 5) {
				//Enable attack 6
				attackPattern = 6;
				waitTime = timeBetweenAttacks;
				isMoving = false;
				StardustBase.gameObject.GetComponent<BossStardustBaseAIChallenge> ().SetAttackPattern (0);
				StardustTurret.gameObject.GetComponent<BossStardustTurretAIChallenge> ().SetAttackPattern (0);
			}
		}

		//Handle movement
		if (attackPattern == 3) {
			if (attackTime == 60) {
				isMoving = true;
			}
			if (attackTime == 600) {
				StartTeleport ();
				isMoving = false;
				waitTime = teleportTime;
				StardustBase.gameObject.GetComponent<BossStardustBaseAIChallenge> ().SetAttackPattern (0);
				StardustTurret.gameObject.GetComponent<BossStardustTurretAIChallenge> ().SetAttackPattern (0);
			}
		} else if (attackPattern == 4) {
			if (attackTime == 120) {
				StartTeleport ();
				waitTime = teleportTime;
				StardustBase.gameObject.GetComponent<BossStardustBaseAIChallenge> ().SetAttackPattern (0);
				StardustTurret.gameObject.GetComponent<BossStardustTurretAIChallenge> ().SetAttackPattern (0);
			}
		} else if (attackPattern == 6) {
			if (attackTime == 80) {
				StartTeleport ();
				waitTime = teleportTime;
				StardustBase.gameObject.GetComponent<BossStardustBaseAIChallenge> ().SetAttackPattern (0);
				StardustTurret.gameObject.GetComponent<BossStardustTurretAIChallenge> ().SetAttackPattern (0);
			}
		}
		if (isMoving) {
			if (!noBase) {
				MoveTowardsPlayer (slowSpeed);
			} else {
				MoveTowardsPlayer (fastSpeed);
			}
		}
	}

	//Teleport and phase out
	void StartTeleport(){
		Color transparent = new Color (0.4f, 0.4f, 0.4f, 0.5f);
		if (!noBase) {
			StardustBase.gameObject.GetComponent<CircleCollider2D> ().enabled = false;
		}
		StardustBase.gameObject.GetComponent<BossStardustBaseAIChallenge> ().enabled = false;
		StardustBase.gameObject.GetComponent<SpriteRenderer> ().color = transparent;
		StardustTurret.gameObject.GetComponent<CircleCollider2D> ().enabled = false;
		StardustTurret.gameObject.GetComponent<BossStardustTurretAIChallenge> ().enabled = false;
		StardustTurret.gameObject.GetComponent<SpriteRenderer> ().color = transparent;
		GameObject Player = GameObject.FindGameObjectWithTag("Player");
		if (Player) {
			transform.position = Player.transform.position;
		}
		isTeleporting = true;
	}

	//Phase in after a teleport
	void FinishTeleport(){
		Color visible = new Color (1.0f, 1.0f, 1.0f, 1.0f);
		if (!noBase) {
			StardustBase.gameObject.GetComponent<CircleCollider2D> ().enabled = true;
		}
		StardustBase.gameObject.GetComponent<BossStardustBaseAIChallenge> ().enabled = true;
		StardustBase.gameObject.GetComponent<SpriteRenderer> ().color = visible;
		StardustTurret.gameObject.GetComponent<CircleCollider2D> ().enabled = true;
		StardustTurret.gameObject.GetComponent<BossStardustTurretAIChallenge> ().enabled = true;
		StardustTurret.gameObject.GetComponent<SpriteRenderer> ().color = visible;
		isTeleporting = false;
	}

	//Update stuff when this dies
	void OnDestroy(){
		GameObject Player = GameObject.FindGameObjectWithTag ("Player");
		if (Player) {
			PlayerPrefs.SetInt ("stardustKilled", 1);
		}
	}

	//Move towards the player
	void MoveTowardsPlayer(float speed){
		GameObject Player = GameObject.FindGameObjectWithTag ("Player");
		if (Player) {
			float x1 = gameObject.transform.position.x, y1 = gameObject.transform.position.y, x2 = Player.gameObject.transform.position.x, y2 = Player.gameObject.transform.position.y;
			float moveAngle = Mathf.Atan2 (y2 - y1, x2 - x1);
			Vector3 pos = this.gameObject.transform.position;
			pos.x = pos.x + (Mathf.Cos (moveAngle) * speed);
			pos.y = pos.y + (Mathf.Sin (moveAngle) * speed);
			this.gameObject.transform.position = pos;
		}  
	}
}
