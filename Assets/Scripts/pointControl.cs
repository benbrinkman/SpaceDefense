using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class pointControl : MonoBehaviour {

	public static int score, totalScore;
	public float timeMultiplier;
	public TextMesh scoreText;
	int timeScore;

	void Start () {
		score = 0;
		scoreText.text = "SCORE: " + score.ToString ();

	}

	void Update () {
		timeScore = Mathf.RoundToInt (Time.timeSinceLevelLoad * timeMultiplier);	//get score based on time
		totalScore = timeScore + score;												//add time score and score from killing enemies
		if (!PauseGame.dead) {scoreText.text = "SCORE: " + totalScore.ToString ();}	//write score to screen
	}

}
