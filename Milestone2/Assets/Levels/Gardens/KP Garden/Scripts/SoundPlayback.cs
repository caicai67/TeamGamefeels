using UnityEngine;
using System.Collections;

public class SoundPlayback : MonoBehaviour {

	AudioSource barrelClang;

	void Awake() {
		barrelClang = GetComponent<AudioSource> ();
	}

	void OnCollisionStay(Collision col) {
		if(!barrelClang.isPlaying && col.relativeVelocity.magnitude >= 2) {

			barrelClang.volume = col.relativeVelocity.magnitude;
			Debug.Log (col.relativeVelocity.magnitude);
			barrelClang.Play();
		}
	}
}
