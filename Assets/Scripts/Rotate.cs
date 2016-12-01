using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {
	//this gives an object a random rotation
	public Vector3 cubeRotation;

	void Start () {cubeRotation = new Vector3(0,0, Random.Range(-30.0f, 30.0f));} 	//set speed

	void Update () {transform.Rotate (cubeRotation * Time.deltaTime);}				//spin at speed
}
