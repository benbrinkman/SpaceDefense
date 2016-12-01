using UnityEngine;
using System.Collections;

public class playerShip : MonoBehaviour {

	//load prefabs
	public GameObject bullet, turret;
	//public GameObject turret;

	//get needed componants
	private BoxCollider2D boxCol;
	private Rigidbody2D playerRB;

	//these are the positions for where to initaite the bullet prefabs
	Transform rGun, lGun;

	//variabls that need to be accessed by other scripts
	public static bool dead, powerUp;
	public static float deathTime;

	//speed variables, editable in the inspector for ease of finding the best ratio
	public float turnSpeed, playerSpeed;

	//cooldown variables
	float timeOfLastShot, turretCooldown;

	void Start () {
		dead = powerUp = false;
		timeOfLastShot = turretCooldown = Time.timeSinceLevelLoad;
		boxCol = gameObject.GetComponent<BoxCollider2D> ();
		rGun =  this.gameObject.transform.GetChild(0);
		lGun =  this.gameObject.transform.GetChild(1);
	}

	void Update () {
		
		playerRB = GetComponent<Rigidbody2D>();

		if (!dead) {	//while player is alive
			if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow) && !PauseGame.pause) {		//turn player right
				playerRB.rotation += turnSpeed * Time.deltaTime;
			}

			if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)&& !PauseGame.pause) {		//turn player left
				playerRB.rotation -= turnSpeed * Time.deltaTime;
			}

			if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)&& !PauseGame.pause) {
				playerRB.AddForce (transform.up * Time.deltaTime * playerSpeed);						//move forward
			} else if (Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow)) {
				playerRB.AddForce (-1 * transform.up * Time.deltaTime * playerSpeed);					//move backward
			}


			//shooting
			if (Input.GetKey (KeyCode.Space) && (Time.timeSinceLevelLoad - timeOfLastShot) > 0.2f && boxCol.isTrigger == false && !PauseGame.pause) {
				timeOfLastShot = Time.timeSinceLevelLoad;					//bullet cooldown
				Quaternion playerRot = playerRB.transform.rotation;			//rotate bullet to player's rotation
				Instantiate (bullet, rGun.transform.position, playerRot);	//shoot right gun
				Instantiate (bullet, lGun.transform.position, playerRot);	//shoot left gun
				if (powerUp){Instantiate (bullet, transform.position, playerRot);}	//if powered up, shoot middle gun

			}

			//placing turrets
			if (Input.GetKey (KeyCode.E)&& (Time.timeSinceLevelLoad - turretCooldown) > 1.0f && TurretUI.numTurrets > 0 && boxCol.isTrigger == false && !PauseGame.pause) {
				turretCooldown = Time.timeSinceLevelLoad;						//turret cooldown
				TurretUI.numTurrets--;											//decrease number of available turrets
				Instantiate (turret, transform.position, Quaternion.identity);	//place turret
			}
			/***************************IF DEAD*******************************/
		} else {
			GameObject[] objs = GameObject.FindGameObjectsWithTag ("orb");		//delete all instances of orbs. There only should be one, but this finds more anyway just to be sure. You never know
			foreach (GameObject orb in objs) {Destroy (orb);}
			transform.position = new Vector2 (-100, -100);			//move player off screen until respawned
			if ((Time.timeSinceLevelLoad - deathTime) > 1.5f){		//if player has been dead for more than 1.5 seconds, respawn
				dead = false;										//set player back to active state
				boxCol.isTrigger = true;							//allow player to spawn on the Space Station
				transform.position = new Vector2 (0, -0.5f);		//spawn player on top of the Space Station
				playerRB.rotation = 0;								//reset rotation
				powerUp = false;									//deactivate powerup
				summonEnemies.active = false;						//allow for orbs to spawn again
			}
		}
		if (PauseGame.dead) {Destroy(gameObject);}						//when game is over, destroy player so nothing else can be effected
	}

	void OnTriggerExit2D(Collider2D coll) {
		if (coll.gameObject.name == "HQ") {boxCol.isTrigger = false;}	//when player leaves the Space Station after respawning, it regains it's collider so it cannot go back in
	}
}
