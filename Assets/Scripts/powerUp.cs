using UnityEngine;
using System.Collections;

public class powerUp : MonoBehaviour {

	float pulseSize;
	public float lifeEnd;

	void Start () {
		lifeEnd = Time.time + Random.Range(6, 20);	//assign a random time that the orb will dissapear at
		pulseSize = 0.1f;							//set size for orb
	}

	void Update () {
		if (Time.time > lifeEnd) {			//if the orb has been alive longer than it's life time
			summonEnemies.active = false;	//tell summoning script 
			Destroy (gameObject);			//destroy the orb
		}

		transform.localScale += new Vector3(pulseSize, pulseSize, pulseSize) * Time.deltaTime * 2;	//incease/decrease orb size
		if (transform.localScale.y > 1f || transform.localScale.y < 0.7f) {pulseSize*=-1f;}			//set limits of size
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Player") {	//if player picks up orb
			playerShip.powerUp = true;			//upgrade the ship
			playSounds.playSound(2);			//play upgrade sound
			Destroy (gameObject);				//get rid of orb
		}
		if (coll.gameObject.name == "HQ") {transform.position = new Vector2 (Random.Range (12.0f, -12.0f), Random.Range (9.5f, -9.5f));}	//if the orb is placed on the Space Station, give it a new position

	
	}
}
