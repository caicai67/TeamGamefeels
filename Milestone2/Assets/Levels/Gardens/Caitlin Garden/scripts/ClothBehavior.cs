using UnityEngine;
using System.Collections;

public class ClothBehavior : MonoBehaviour {

	AudioSource audS; 
	// Use this for initialization
	void Start () {
		audS = GetComponent<AudioSource> ();
	}

	void OnCollisionEnter(Collision other)  {
		audS.Play();
		Debug.Log ("cloth rattled");
	}
	void OnCollisionExit(Collision other) {
		audS.Stop ();
	}
}
