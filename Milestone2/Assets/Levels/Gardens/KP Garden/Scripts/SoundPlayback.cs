//Script created by Karan Pratap Singh(KP) for TeamGameFeels class project
//TeamGameFeels team members:
//Caitlin Morris - cmorris40@gatech.edu - cmorris40
//Ambrose Cheung - acheung30@gatech.edu - acheung30
//Chris Donlan - chris.donlan87@gmail.com - cdonlan3
//Karan Pratap Singh - kps@gatech.edu - ksingh75
//Justin Thornburgh - jthornburgh3@gatech.edu - jthornburgh3
//Charlie -  - charlie.jolman@gmail.com

using UnityEngine;
using System.Collections;

public class SoundPlayback : MonoBehaviour {

	AudioSource audioClip;

	void Awake() {
		audioClip = GetComponent<AudioSource> ();
	}

	void OnCollisionStay(Collision col) {
		if(!audioClip.isPlaying && col.relativeVelocity.magnitude >= 2) {

			//audioClip.volume = col.relativeVelocity.magnitude/10;
			Debug.Log (col.relativeVelocity.magnitude);
			audioClip.Play();
		}
	}
		
}
