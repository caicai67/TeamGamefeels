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
