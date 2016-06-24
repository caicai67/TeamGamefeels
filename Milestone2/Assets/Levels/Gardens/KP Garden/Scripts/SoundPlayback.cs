using UnityEngine;
using System.Collections;

public class SoundPlayback : MonoBehaviour {

	void OnCollisionStay(Collision col) {
		if() {
			audio.volume = col.relativeVelocity.magnitude/20;
			audio.Play();
		}
	}
}
