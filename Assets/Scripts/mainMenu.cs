using UnityEngine;
using System.Collections;

public class mainMenu : MonoBehaviour {

	Camera cam;
	SpriteRenderer sprt;
	public TextMesh text;
	public Color color2;
	public float screenWidth, screenHeight, width, maxTime;
	float blinker;

	void Start () {
		sprt = gameObject.GetComponent<SpriteRenderer>();
		blinker = Time.time;
	}

	void Update () {
		if (Input.GetKey (KeyCode.Space)) {Application.LoadLevel("Tutorial");}
		if (Time.time - blinker > maxTime/2) {text.color = new Color(255, 255, 255);}	//this flashes the color to draw attention to the word [space]
		else {text.color = color2;}
		if (Time.time - blinker > maxTime) {blinker = Time.time;}
		cam = Camera.main;
		screenHeight = 2f * cam.orthographicSize;
		screenWidth = screenHeight * cam.aspect;

		//below is the code from the "UIbar" script. It causes the selected object to resize to the screen size

		width = sprt.bounds.size.x;
		bool end = false;
		if (sprt.bounds.size.x < screenWidth) {
			if (Mathf.Abs(sprt.bounds.size.x - screenWidth) > 0.1){
				while(!end){
					float currentx = gameObject.transform.localScale.x;
					float currenty = gameObject.transform.localScale.y;
					gameObject.transform.localScale  = new Vector3(currentx + 0.01f,currenty + 0.01f, 1.0f);
					if(sprt.bounds.size.x > screenWidth){end = true;}
				}
			}
		} else {
			if (Mathf.Abs(screenWidth - sprt.bounds.size.x) > 0.1){
				while(!end){
					float currentx = gameObject.transform.localScale.x;
					float currenty = gameObject.transform.localScale.y;
					gameObject.transform.localScale = new Vector3(currentx - 0.01f,currenty - 0.01f, 1.0f);
					if(sprt.bounds.size.x < screenWidth){end = true;}
				}
			}
		}

	}
}
