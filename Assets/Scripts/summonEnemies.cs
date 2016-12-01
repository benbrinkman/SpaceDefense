using UnityEngine;
using System.Collections;

public class summonEnemies : MonoBehaviour {
	//this script summons enemies and the orbs

	//orb variables
	public GameObject orb;						//orb prefab
	public static float lowerBound, upperBound;	//The bounds of the orb's spawn time
	public static bool active;					//if an orb is spawned
	public static Vector2 orbPos;				//orb position
	float randomTime, previousTime;				//orb spawn times

	//enemy variables
	public GameObject enemy, formation;							//enemy prefabs
	public static int playerKillCount;							//this varible is for the first stage
	public static int enemyType;								//determines the type of enemy that will spawn
	float lastEnemy, enemySpawnTime, stageTime, nextStageTime;	//time based variables for spawning enemies
	bool form;													//if the enemy will spawn as a formation
	int stage, upperBoundEnemy, upperBoundForm;					//stage of gameplay progression, max type of enemy spawning, chance of formations spawning


	void Start () {
		
		lowerBound = 5.0f;
		upperBound = 15.0f;
		active = false;
		previousTime = Time.timeSinceLevelLoad;
		randomTime = Random.Range (lowerBound, upperBound);
		
		playerKillCount = 0;
		lastEnemy = Time.time;
		nextStageTime = 15;
		stage = 1;
		upperBoundForm = 20;
	}

	void Update () {
		
		if (Random.Range (0, upperBoundForm) == 0) {form = true;} //chance of a formation spawning instead of a single enemy
		else {form = false;}

		//below is the game progression, the part causing the game to get harder over time
		//the game is divided into stages, each one progressivly harder, changing the spawn time between enemies, highest enemy type, and chance of formations
		if (stage == 1) {
			enemySpawnTime = 1.5f;
			upperBoundEnemy = 1;
			form = false;
		}else if (stage == 2) {
			upperBoundEnemy = 2;
		}else if (stage == 3) {
			enemySpawnTime = 1.0f;
			upperBoundForm = 10;
		}else if (stage == 4) {
			upperBoundEnemy = 3;
			upperBoundForm = 5;
		}else if (stage == 5) {
			upperBoundEnemy = 4;
			enemySpawnTime = 0.5f;
		}else if (stage == 6) {
			upperBoundForm = 5;
			enemySpawnTime = 0.4f;
		}else if (stage == 7) {
			enemySpawnTime = 0.3f;
		}else if (stage == 8) {
			upperBoundForm = 5;
			enemySpawnTime = 0.2f;
		}else if (stage == 9) {
			upperBoundForm = 3;
			enemySpawnTime = 0.2f;
		}

		if (playerKillCount == 12) {	//the first stage goes up when player has killed 12 enemies
			stage = 2;
			stageTime = Time.time;
		}
		if (stage > 1 && Time.time - stageTime > nextStageTime && stage !=9) {	//this is where time causes the stages to change
			stage++;													//go to next stage
			stageTime = Time.time;										//reset time
			if (stage == 4){nextStageTime += 25;}						//add time to next stage
			else if (stage == 5 || stage == 6){nextStageTime += 30;}
			else if (stage == 7){nextStageTime += 35;}
			else if (stage == 8){nextStageTime += 70;}
			else {nextStageTime +=10;}
		}

		if (Time.time - lastEnemy > enemySpawnTime) {		//spawning the enemies based on the current spawn time determined by the stage
			lastEnemy = Time.time;
			float x, y;
			x = Random.Range (15f, -15f);														//pick random x
			if (x < 13 && x > -13){y = Random.Range (10f, 12f) * (Random.Range (0,2)*2-1);}		//if the x is on the screen, make the y off screen
			else{y = Random.Range (-12f, 12f);}													//else draw y in random spot

			enemy.GetComponent<enemyBehavior>().enemyType = Random.Range (0,upperBoundEnemy);	//choose random enemy type
			if (form){																			//if formation
				enemyType = Random.Range (0,upperBoundEnemy);									//choose enemy type for formation
				Instantiate(formation, new Vector3(x, y, 0.0f), Quaternion.identity);			//summon formation
			}else{Instantiate(enemy, new Vector3(x, y, 0.0f), Quaternion.identity);}			//summon enemy

		}

		if (!active) {																				//if orb is not active
			if ((Time.timeSinceLevelLoad - previousTime) > randomTime) {							//if the orb spawn time is chosen
				previousTime = Time.timeSinceLevelLoad;												//reset spawn time
				randomTime = Random.Range (lowerBound, upperBound);									//get new spawn time
				orbPos = new Vector2 (Random.Range (10.0f, -12.0f), Random.Range (9.5f, -9.5f));	//set orb position
				Instantiate (orb, orbPos, Quaternion.identity);										//summon orb
				active = true;																		//activate orb
			}
		} else {
			previousTime = Time.timeSinceLevelLoad;													//check previous time
			randomTime = Random.Range (lowerBound, upperBound);										//set random summon time
		}
		if (playerShip.dead || playerShip.powerUp) {active = true;}									//make sure orb doesn't spawn while the player is dead or already has a power up


	}
}
