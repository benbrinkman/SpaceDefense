using UnityEngine;
using System.Collections;

public class bulletScript : MonoBehaviour {

	public float bulletSpeed;
	Rigidbody2D bulletRB;


	void Start () {
		playSounds.playSound(0); 				//play shooting sound whenever a bullet is fired
		bulletRB = GetComponent<Rigidbody2D>();	//get access to the rigidbody of the bullet
	}

	void Update () {

		bulletRB.AddForce (transform.up * Time.deltaTime * bulletSpeed); //move bullet

		//destroy bullet if it goes offscreen
		if (gameObject.transform.position.x < -13 || gameObject.transform.position.x > 13 || gameObject.transform.position.y < -10 || gameObject.transform.position.y > 10) {Destroy (gameObject);}

	}
	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.name == "HQ") {
			Destroy (gameObject);			//causes the player to not be able to shoot through the space station
		}
	}

}
