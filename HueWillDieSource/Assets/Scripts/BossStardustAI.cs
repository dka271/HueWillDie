using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStardustAI : MonoBehaviour {

	public GameObject StardustBase;
	public GameObject StardustTurret;

	private bool noBase;
	private int waitTime;
	private int barrageCount;
	private int state;//0 = no movement, 1 = normal movement, 2 = wait before teleport, 3 = teleport, 4 = barrage after teleport, 5 = cooldown, 6 = pause after phase 1

	private float slowSpeed = 0.05f;
	private float fastSpeed = 0.09f;
	private int idleTime = 60;
	private int pauseTime = 240;
	private int barrageTime = 120;
	private int teleportTime = 80;
	private int shootingTime = 600;

	// Use this for initialization
	void Start () {
		noBase = false;
		state = 0;
		waitTime = idleTime;
		barrageCount = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//Handle death
		if (StardustBase.gameObject.GetComponent<BossStardustBaseAI> ().GetDead () && StardustTurret.gameObject.GetComponent<BossStardustTurretAI> ().GetDead ()) {
			Destroy (this.gameObject);
		} else if (StardustBase.gameObject.GetComponent<BossStardustBaseAI> ().GetDead () && !noBase) {
			noBase = true;
			waitTime = pauseTime;
			state = 6;
			StardustBase.gameObject.GetComponent<BossStardustBaseAI> ().EnableShooting(false);
		}

		//Handle the state
		waitTime--;
		if (waitTime <= 0) {
			if (state == 0) {
				state = 1;
				waitTime = shootingTime;
			} else if (state == 1) {
				state = 2;
				waitTime = idleTime;
			} else if (state == 2) {
				state = 3;
				waitTime = teleportTime;
				Color transparent = new Color (0.4f, 0.4f, 0.4f, 0.5f);
				if (!noBase) {
					StardustBase.gameObject.GetComponent<CircleCollider2D> ().enabled = false;
				}
				StardustBase.gameObject.GetComponent<BossStardustBaseAI> ().enabled = false;
				StardustBase.gameObject.GetComponent<SpriteRenderer> ().color = transparent;
				StardustTurret.gameObject.GetComponent<CircleCollider2D> ().enabled = false;
				StardustTurret.gameObject.GetComponent<BossStardustTurretAI> ().enabled = false;
				StardustTurret.gameObject.GetComponent<SpriteRenderer> ().color = transparent;
				GameObject Player = GameObject.FindGameObjectWithTag("Player");
				if (Player) {
					transform.position = Player.transform.position;
				}
			} else if (state == 3) {
				Color visible = new Color (1.0f, 1.0f, 1.0f, 1.0f);
				if (!noBase) {
					StardustBase.gameObject.GetComponent<CircleCollider2D> ().enabled = true;
				}
				StardustBase.gameObject.GetComponent<BossStardustBaseAI> ().enabled = true;
				StardustBase.gameObject.GetComponent<SpriteRenderer> ().color = visible;
				StardustTurret.gameObject.GetComponent<CircleCollider2D> ().enabled = true;
				StardustTurret.gameObject.GetComponent<BossStardustTurretAI> ().enabled = true;
				StardustTurret.gameObject.GetComponent<SpriteRenderer> ().color = visible;
				state = 4;
				waitTime = barrageTime;
			} else if (state == 4) {
				state = 5;
				waitTime = idleTime;
				barrageCount++;
				if (barrageCount >= 4) {
					barrageCount = 0;
				}
			} else if (state == 5) {
				state = 1;
				waitTime = shootingTime;
			} else if (state == 6) {
				state = 1;
				StardustBase.gameObject.GetComponent<BossStardustBaseAI> ().EnableShooting(true);
				waitTime = shootingTime;
			}
		} else {
			GameObject Player = GameObject.FindGameObjectWithTag ("Player");
			if (Player) {
				if (!noBase) {
					if (state == 1) {
						if (waitTime % 120 == 0) {
							StardustTurret.gameObject.GetComponent<BossStardustTurretAI> ().FirePattern1Red ();
						} else if (waitTime % 120 == 30) {
							StardustTurret.gameObject.GetComponent<BossStardustTurretAI> ().FirePattern1Blue ();
						} else if (waitTime % 120 == 60) {
							StardustTurret.gameObject.GetComponent<BossStardustTurretAI> ().FirePattern1Green ();
						} else if (waitTime % 120 == 90) {
							StardustTurret.gameObject.GetComponent<BossStardustTurretAI> ().FirePattern1Yellow ();
						}
					} else if (state == 4) {
						if (waitTime % 3 == 0) {
							if (barrageCount == 0) {
								StardustTurret.gameObject.GetComponent<BossStardustTurretAI> ().FirePattern1Red ();
							} else if (barrageCount == 1) {
								StardustTurret.gameObject.GetComponent<BossStardustTurretAI> ().FirePattern1Blue ();
							} else if (barrageCount == 2) {
								StardustTurret.gameObject.GetComponent<BossStardustTurretAI> ().FirePattern1Green ();
							} else if (barrageCount == 3) {
								StardustTurret.gameObject.GetComponent<BossStardustTurretAI> ().FirePattern1Yellow ();
							}
						}
					}
				} else {
					if (state == 1) {
						if (waitTime % 300 == 0) {
							StardustTurret.gameObject.GetComponent<BossStardustTurretAI> ().FirePattern2Red ();
						} else if (waitTime % 300 == 75) {
							StardustTurret.gameObject.GetComponent<BossStardustTurretAI> ().FirePattern2Blue ();
						} else if (waitTime % 300 == 150) {
							StardustTurret.gameObject.GetComponent<BossStardustTurretAI> ().FirePattern2Green ();
						} else if (waitTime % 300 == 225) {
							StardustTurret.gameObject.GetComponent<BossStardustTurretAI> ().FirePattern2Yellow ();
						}
					} else if (state == 4) {
						if (waitTime % 3 == 0) {
							if (barrageCount == 0) {
								StardustTurret.gameObject.GetComponent<BossStardustTurretAI> ().FirePattern2Red ();
							} else if (barrageCount == 1) {
								StardustTurret.gameObject.GetComponent<BossStardustTurretAI> ().FirePattern2Blue ();
							} else if (barrageCount == 2) {
								StardustTurret.gameObject.GetComponent<BossStardustTurretAI> ().FirePattern2Green ();
							} else if (barrageCount == 3) {
								StardustTurret.gameObject.GetComponent<BossStardustTurretAI> ().FirePattern2Yellow ();
							}
						}
					}
				}
			}
		}

		//Handle movement
		if (state == 1) {
			if (!noBase) {
				MoveTowardsPlayer (slowSpeed);
			} else {
				MoveTowardsPlayer (fastSpeed);
			}
		}
	}

	void OnDestroy(){
		GameObject Player = GameObject.FindGameObjectWithTag ("Player");
		if (Player) {
			PlayerPrefs.SetInt ("stardustKilled", 1);
		}
	}

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
