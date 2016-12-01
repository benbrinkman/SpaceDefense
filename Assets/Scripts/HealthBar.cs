using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {
	//this script controls the healthbar withint the hud, as well as the space station's health, sending a message when it dies


	SpriteRenderer sprt;		//access the sprite renderer
	public Sprite[] current;	//get array of sprites
	public static int health;	//Space Station health

	void Start () {
		sprt = gameObject.GetComponent<SpriteRenderer> (); 	//access the sprite renderer
		sprt.sprite = current[0];							//load 10 sprites with the current variable, all the health bar with decreaseing health
		health = 10;										//set starting Space Station health
	}

	void Update () {
		int arrayInput;

		if (health < 0 && PauseGame.dead == false) {
			arrayInput = 0;
			PauseGame.dead = true;									//the "PauseGame" script deals with pause and death screen. This instance means the game is over, activating the end game screen
			PauseGame.finalScore = pointControl.totalScore;			//get a score for the end game to display
			playSounds.playSound (5);								//play station death noise
		} 
		else {arrayInput = health;}									//determines which sprite is being used, giving the losing health effect
		if (health > -1){sprt.sprite = current [10 - arrayInput];}	
	}
}
