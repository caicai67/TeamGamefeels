using UnityEngine;
using System.Collections;

public class GateBehavior : MonoBehaviour {
	Animator anim;
	public AudioClip openSound;
	public AudioClip closeSound;
	AudioSource audS;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		audS = this.gameObject.AddComponent <AudioSource>();
		audS.volume = .2f;
	}

	void OnTriggerEnter(Collider other) {
		anim.SetBool ("open", true);
	}

	void OnTriggerExit(Collider other) {
		anim.SetBool ("open", false);
	}

	void playOpen(){
		audS.clip = openSound;
		audS.Play ();
	}

	void playClose(){
		audS.clip = closeSound;
		audS.Play ();
	}
}
