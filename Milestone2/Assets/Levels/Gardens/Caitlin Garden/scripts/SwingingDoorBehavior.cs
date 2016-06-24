using UnityEngine;
using System.Collections;

public class SwingingDoorBehavior : MonoBehaviour {

	float prevRotation;
	AudioSource doorSound;

	// Use this for initialization
	void Start () {
		doorSound = GetComponent <AudioSource> ();
		prevRotation = transform.rotation.y;
	}
	
	// Update is called once per frame
	void Update () {
		if (Mathf.Abs(transform.rotation.y - prevRotation) > 0.005 && !doorSound.isPlaying) {
			doorSound.Play ();
		}
		prevRotation = transform.rotation.y;
	}
}
