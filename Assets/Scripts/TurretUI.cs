using UnityEngine;
using System.Collections;

public class TurretUI : MonoBehaviour {

	public SpriteRenderer[] turrets;			//access the renderers for the images
	public Sprite[] turretProgress;				//load turret sprites
	public static int progress, numTurrets;		//the progress toward next turret, and the number of turrets the player has
	private int nextTurret;						//the goal for the next turret


	void Start () {
		progress = 0;
		nextTurret = 5;
		numTurrets = 0;
	}

	void Update () {
		if (progress == nextTurret) {	//if player gets enough kills for another turret
			numTurrets++;				//give player an extra turret
			nextTurret *= 2;			//make next turret twice as hard to get
			progress = 0;				//reset turret progress
		}

		if (!PauseGame.dead) {
			for (int i = 0; i < 4; i++) {
				if (i > numTurrets) {turrets [i].sprite = turretProgress [0];} 														//if the player has gotten the turret, use colored sprites
				else if (i < numTurrets) {turrets [i].sprite = turretProgress [11];} 												//if the player has not gotten the turret yet, color it grey
				else if (i == numTurrets) {turrets [i].sprite = turretProgress [Mathf.RoundToInt (11.0f * progress/nextTurret)];}	//if it is the one the player is currently working towards, give progress image
			}
		}
		else{Destroy(gameObject);} //destroy on death, so that it does not show up on the death screen
	}
}
