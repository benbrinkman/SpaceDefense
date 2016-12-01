using UnityEngine;
using System.Collections;

public class HQControl : MonoBehaviour {
	//this just rotates the Space Station parts. A negative speed rotates it backward


	public static Vector2 hqPos;	//station position, for acessing from other scripts
	public float speed;				//rotation speed
	Vector3 rotationSpeed;			//rotation vector

	void Start () {
		hqPos = transform.position;
		rotationSpeed = new Vector3(0.0f ,0.0f ,speed);
	}

	void Update () {
		transform.Rotate (rotationSpeed * Time.deltaTime);	//rotate station
	}



}
