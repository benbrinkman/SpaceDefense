using UnityEngine;
using System.Collections;

public class turretControl : MonoBehaviour {

	
	public GameObject explosion, bullet;	//Prefabs for explosion and bullet
	public Sprite[] damage;					//Sprits showing damage to the turrets
	private Transform target;				//position of target
	private SpriteRenderer sprt;			//Access to image renderer
	public static float radius;				//radius turret will check for enemies
	public float turnSpeed;					//turn speed of the turret
	public int health;						//healh of the turret
	private float timeOfLastShot;			//cooldown

	void Start () {
		playSounds.playSound(3);	//play place turret sound
		health = 3;
		radius = 6;
		turnSpeed = 200;
		sprt = gameObject.GetComponent<SpriteRenderer> ();
		timeOfLastShot = Time.timeSinceLevelLoad;
	}

	void Update () {

		sprt.sprite = damage[health -1];	//show correct damage sprite

		bool isEnemies = false;
		GameObject[] objs = GameObject.FindGameObjectsWithTag ("enemy");
		for (int i = 0; i < objs.Length; i++){isEnemies = true;}			//check if there are any enemies

		if (isEnemies) {
			target = GetClosestEnemy (GameObject.FindGameObjectsWithTag ("enemy"));												//find the closest enemy
			if(Vector2.Distance(transform.position, target.position) < radius){													//if closest enemy is within turret range
				Vector3 vectorToTarget = target.position - transform.position;													//get the vector between the two points
				float angle = Mathf.Atan2 (vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;									//get angle between turret and enemy
				Quaternion targetRotation = Quaternion.AngleAxis (angle - 90f, Vector3.forward);								//set the angle goal to facing the enemy  
				transform.rotation = Quaternion.RotateTowards (transform.rotation, targetRotation, Time.deltaTime * turnSpeed);	//rotate toward the enemy by on turn speed
				if ((Time.timeSinceLevelLoad - timeOfLastShot) > 0.8f) {	//if shooting cooldown runs out
					timeOfLastShot = Time.timeSinceLevelLoad;				//reset cooldown
					Quaternion turretRot = transform.rotation;				//get rotation for the bullet
					Instantiate (bullet, transform.position, turretRot);	//summon bullet
				}
			}
		}
	}
	
	Transform GetClosestEnemy(GameObject[] enemies){												
		Transform bestTarget = null;															//start with nothing selected
		float closestDistanceSqr = Mathf.Infinity;												//set to high number so every check is lower
		Vector3 currentPosition = transform.position;											
		foreach (GameObject potentialTarget in enemies) {										//for every enemy found
			Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
			float dSqrToTarget = directionToTarget.sqrMagnitude;								//get distance
			if (dSqrToTarget < closestDistanceSqr) {											//check if it is shortest distance
				closestDistanceSqr = dSqrToTarget;												//if it is, make it the new target
				bestTarget = potentialTarget.transform;
			}
		}
		return bestTarget;																		//return closest enemy
	}

	void OnTriggerEnter2D(Collider2D coll) {
		
		if (coll.gameObject.name == "enemy(Clone)") { 								//if enemy collides with turret
			Destroy (coll.gameObject);												//destroy the enemy
			health--;																//bring down turret health
			if (health == 0){												
				Instantiate (explosion, transform.position, Quaternion.identity);	//if turret is out of health, summon explosion and destroy object
				Destroy (gameObject);
			}
		}
	}
}
