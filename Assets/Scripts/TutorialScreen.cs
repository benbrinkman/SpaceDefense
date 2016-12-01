using UnityEngine;
using System.Collections;

public class TutorialScreen : MonoBehaviour {
	public TextMesh startText;
	private int startTime;

	void Start () {
		startTime = 5;
		startText.text = "Game Will Start In " + startTime.ToString ();
	}

	void Update () {
		startTime = 6 - Mathf.RoundToInt(Time.timeSinceLevelLoad);
			startText.text = "Game Will Start In " + startTime.ToString ();	//show time until game starts
		if (startTime < 1) {
			Application.LoadLevel("Main");	//when time runs out, start game
		}
	}
}
