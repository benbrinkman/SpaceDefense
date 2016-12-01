using UnityEngine;
using System.Collections;

public class enemyBehavior : MonoBehaviour {
	//the enemy script, used on all types of enemies. Their type is defined within

	public GameObject explosion;			//get explosion prefab
	public Sprite[] enemySprite;			//get different enemy sprites
	private SpriteRenderer spriteRender;	//access the sprite renderer
	public int enemyType; 					//determine type of enemy
	float speed, extraSpeed;				//the speed of the enemy, and extra speed if it is a faster type
	Vector2 target;							//the position the enemies are moving towards

	void Start () {
		extraSpeed = 0;
		target = HQControl.hqPos; 	//enemies start by targeting the Space Station
		speed = 1.0f;
		spriteRender = gameObject.GetComponent<SpriteRenderer> ();
	}


	void Update () {
		target = HQControl.hqPos;

		bool isTurret = false;
		GameObject[] objs = GameObject.FindGameObjectsWithTag ("turret"); 						//check for any turrets in game
		for (int i = 0; i < objs.Length; i++){isTurret = true;}
		if (isTurret){
			Transform turret = GetClosestTurret (GameObject.FindGameObjectsWithTag ("turret")); //finds closest turret
			if(Vector2.Distance(transform.position, turret.position) < turretControl.radius){	//if within radius, go toward turret instead of base
				target = turret.position;
			}
		}
		if (Vector2.Distance (transform.position, HQControl.hqPos) < 6) {	//if no turret, go for the space station (hq)
			target = HQControl.hqPos;
		}

		spriteRender.sprite = enemySprite [enemyType];	//load the correct enemy color
		if (enemyType == 2 || enemyType == 3) {
			extraSpeed = speed / 2;						//make the fast enemies fast
		} else {
			extraSpeed = 0;
		}
		
		if (gameObject.name != "enemy_f") {				//this disables the movement of enemies in the formation, so they follow the formation movement. see "enemyFormation" script
			transform.position = Vector2.MoveTowards (gameObject.transform.position, target, Time.deltaTime * (speed + extraSpeed));
		}

	}



	void OnTriggerEnter2D(Collider2D coll) {
	
		if (coll.gameObject.name == "playerBullet(Clone)") {
			Destroy (coll.gameObject);
			if (enemyType == 1 || enemyType == 3){ 		//this is where the special enemies have 2 health, they turn into the 1 health enemies when hit
				enemyType--;
				pointControl.score += 500;				//player gets half points for damaging one
				Instantiate(explosion, gameObject.transform.position, Quaternion.identity); //summon explosion
			}else{
				pointControl.score += 1000;				//give player full points
				if (TurretUI.numTurrets < 4){
					TurretUI.progress ++;				//gain progress toward more turrets
				}
				summonEnemies.playerKillCount++;		//this variable is for getting past the first stage
				Instantiate(explosion, gameObject.transform.position, Quaternion.identity);	//more 'splosions
				Destroy (gameObject);
			}
		}
		if (coll.gameObject.name == "turretBullet(Clone)") {	//this is all similar to plaer, but half the amount of points for the player
			Destroy (coll.gameObject);
			if (enemyType == 1 || enemyType == 3){
				enemyType--;
				pointControl.score += 250;
				Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
			}
			else{
				pointControl.score += 500;
				Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
				Destroy (gameObject);
			}
		}
		if (coll.gameObject.name == "HQ") {		//this is where the health goes down for the space station when it is hit
			playSounds.playSound(4);
			HealthBar.health--;
			Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
			Destroy (gameObject);
		}
		if (coll.gameObject.tag == "Player") {	//kill the player if it gets hit
			playerShip.dead = true;
			playSounds.playSound(6);			//play the player explosion sound
			playerShip.deathTime = Time.timeSinceLevelLoad;
			Instantiate(explosion, coll.transform.position, Quaternion.identity);
			Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
			Destroy (gameObject);
		}

	}

	Transform GetClosestTurret(GameObject[] turrets){
		Transform bestTarget = null;
		float closestDistanceSqr = Mathf.Infinity; 			//set to highest so we can have somthing to compare the first one too
		Vector3 currentPosition = transform.position;		
		foreach (GameObject potentialTarget in turrets) {	//check each turret in play
			Vector3 directionToTarget = potentialTarget.transform.position - currentPosition; 
			float dSqrToTarget = directionToTarget.sqrMagnitude;	//get distance between enemy and turret
			if (dSqrToTarget < closestDistanceSqr) {
				closestDistanceSqr = dSqrToTarget;					//if lowest turret so far, make best target
				bestTarget = potentialTarget.transform;
			}
		}
		
		return bestTarget;
	}


}
