using UnityEngine;
using System.Collections;

public class playSounds : MonoBehaviour {
	//this plays all of the sounds in the game. All other objects simply call the function and the preloaded sounds here will play
	//this was done just to simplify sounds creation and have all the sounds in one place with an easy way to play them from anywhere in the scripts
	
	public static AudioSource[] sounds;
	//sound 0 = player fire
	//sound 1 = explosion
	//sound 2 = power up
	//sound 3 = turret place
	//sound 4 = space station damage
	//sound 5 = space station boom
	//sound 6 = player boom

	void Start () {sounds = gameObject.GetComponents<AudioSource> ();}
	public static void playSound(int x){sounds[x].Play();} //this function can be called by any script
}
