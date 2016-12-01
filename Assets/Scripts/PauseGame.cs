using UnityEngine;
using System.Collections;

public class PauseGame : MonoBehaviour {
	//this controls not only pause game functions, but also end game functions


	private SpriteRenderer sprt;				//Access sprite renderer
	public TextMesh scoreText;					//text displaying score
	public Sprite[] deadScreen, pauseScreen;	//sprites for the pause and death screens
	public static bool dead, pause;				//the bools that check for end of game and pause
	public static int finalScore;				//the score sent to the final score text at the game over screen
	public float cooldownTime;					//min cooldown
	bool cont;									//if the player continues; if they have selected this option
	float cooldown;								//cooldown time


	void Start () {
		finalScore = 0;
		sprt = gameObject.GetComponent<SpriteRenderer> ();
		cooldown = Time.realtimeSinceStartup;
		dead = false;
		pause = false;
		cont = true;
	}

	void Update () {
		if (Input.GetKey (KeyCode.P) || (Input.GetKey (KeyCode.Escape) && !dead)) {pause = true;}	//pause game
		if (pause || dead) {
			if (dead){scoreText.text = "FINAL SCORE: " + finalScore.ToString ();}					//display final score on death screen
			bool select = false;
			Time.timeScale = 0.0f;																	//this pauses time and stops all game functions
			if ((Time.realtimeSinceStartup - cooldown) > cooldownTime)
				if ((Input.GetKey (KeyCode.Space)&& pause) || Input.GetKey (KeyCode.Return)) {		//the select option for both screens
					select = true;
					cooldown = Time.realtimeSinceStartup;
				}else if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.S)) { //the switch selection option
					cooldown = Time.realtimeSinceStartup;
				if (cont) {cont = false;} 
				else {cont = true;}
			}

			//below determines what to do when somthing has been selected
			if (cont){
				if (pause){
					sprt.sprite = pauseScreen[0];
					if (select){pause = false;}
				}
				else if (dead){
					sprt.sprite = deadScreen[0];
					if (select){Application.LoadLevel("Main");}
				}
			}else{
				if (pause){
					sprt.sprite = pauseScreen[1];
					if (select){Application.Quit ();}
				}
				else if (dead){
						sprt.sprite = deadScreen[1];
						if (select){Application.Quit ();}
				}
			}
		}else{
			Time.timeScale = 1.0f;
			sprt.sprite = null;
			cont = true;
		}

	}
}
