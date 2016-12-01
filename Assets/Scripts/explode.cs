using UnityEngine;
using System.Collections;

public class explode : MonoBehaviour {

	
	//the script simply spins, grows and fades a sprite to create a little explosion effect

	SpriteRenderer sprt;	//access Sprite Renderer
	float a;				//opacity value of the image
	public float speed;		//speed of rotation
	Vector3 rotation;		//rotation vector

	void Start () {
		playSounds.playSound (1);							//as soon as the explosion is summoned, make the explosion sound
		rotation = new Vector3(0f,0f, speed);				//set rotation speed
		sprt = gameObject.GetComponent<SpriteRenderer> ();	//get sprite renderer
		a = 1f;												//set opacity value
	}

	void Update () {
		this.transform.localScale +=  new Vector3 (2f  * Time.deltaTime, 2f * Time.deltaTime, 0.0f);	//grow
		a-= 5f * Time.deltaTime;																		//fade
		sprt.color = new Color(1f,1f,1f,a);																//fade
		transform.Rotate (rotation * Time.deltaTime);													//rotate
		if (a < 0) {Destroy (gameObject);}																//when the object is no longer visible, destroy it
	}
}
