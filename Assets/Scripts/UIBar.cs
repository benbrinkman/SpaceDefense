using UnityEngine;
using System.Collections;

public class UIBar : MonoBehaviour {

	Camera cam;
	public float screenWidth;
	public float screenHeight;
	public float width;
	SpriteRenderer sprt;
	void Start () {
		sprt = gameObject.GetComponent<SpriteRenderer>();
	}

	void Update () {
		cam = Camera.main;
		screenHeight = 2f * cam.orthographicSize;	//get height and width so we know where to put the ui bar
		screenWidth = screenHeight * cam.aspect;

		width = sprt.bounds.size.x;		//get the width of the current object
		bool end = false;
		if (sprt.bounds.size.x < screenWidth) {		//this grows the ui bar until it fits the width of the screen
			if (Mathf.Abs(sprt.bounds.size.x - screenWidth) > 0.1){
				while(!end){
					float currentx = gameObject.transform.localScale.x;
					float currenty = gameObject.transform.localScale.y;
					gameObject.transform.localScale  = new Vector3(currentx + 0.01f,currenty + 0.01f, 1.0f);
					if(sprt.bounds.size.x > screenWidth){
						end = true;
					}
				}
			}
		} else {									//this shrinks the ui bar until it fits the width of the screen
			if (Mathf.Abs(screenWidth - sprt.bounds.size.x) > 0.1){
				while(!end){
					
					float currentx = gameObject.transform.localScale.x;
					float currenty = gameObject.transform.localScale.y;
					gameObject.transform.localScale = new Vector3(currentx - 0.01f,currenty - 0.01f, 1.0f);
					if(sprt.bounds.size.x < screenWidth){
						end = true;
					}
				}
			}
		}

		float positionY = screenHeight / sprt.bounds.size.y /2; //determine height

		gameObject.transform.position =  new Vector3(0.0f, positionY + sprt.bounds.size.y, 0.0f);



	}
}
