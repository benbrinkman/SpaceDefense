using UnityEngine;
using System.Collections;

public class SelfDestructOnDeath : MonoBehaviour {

	//anything with this script will delete itself when the game is over
	void Update () {
		if (PauseGame.dead) {Destroy (gameObject);}
	}
}
