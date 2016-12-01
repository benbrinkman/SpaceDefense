using UnityEngine;
using System.Collections;

public class enemyFormation : MonoBehaviour {
	//this script moves the enemies who are in formation. They are initiated in a prefab under one parent, which this script applies to

	float speed, extraSpeed; //Speed of formation, and extra speed if it is a faster enemy type

	void Start () {
		speed = 1.0f;

		if (summonEnemies.enemyType == 2 || summonEnemies.enemyType == 3) {extraSpeed = speed / 2;}	//add extra speed to fast enemies
		else {extraSpeed = 0;}

		Vector3 vectorToTarget = new Vector3(HQControl.hqPos.x, HQControl.hqPos.y, 0) - transform.position;	
		transform.LookAt (transform.position + new Vector3 (0, 0, 1), vectorToTarget);						//rotate to face the space station

		enemyBehavior[] newScript = gameObject.GetComponentsInChildren<enemyBehavior> ();
		for (int i = 0; i < transform.childCount; i++) {newScript [i].enemyType = summonEnemies.enemyType;}	//set enemies to whichever enemy type it inputs
	}

	void Update () {
		if (transform.childCount > 0) {transform.position = Vector2.MoveTowards (gameObject.transform.position, HQControl.hqPos, Time.deltaTime * (1.0f + extraSpeed));}//move towards space station 
		else {Destroy (gameObject);}
	}
}
