using UnityEngine;
using System.Collections;

public class TableBehavior : MonoBehaviour {
	
//	public AudioClip slide;
	public AudioClip crash;

	AudioSource audS;
	Rigidbody cube;

	void Start () {
		audS = this.gameObject.AddComponent <AudioSource>();;

		cube = GetComponent<Rigidbody> ();
	}

	void Update(){
//		Debug.Log (cube.velocity);
//		if (!audS.isPlaying && Mathf.Abs(cube.velocity.y) > 0.4f) {
//			Debug.Log ("audio played");
//			audS.clip = crash;
//			audS.Play ();
//		} else if (!audS.isPlaying && (Mathf.Abs(cube.velocity.x) > 0.4f
//			|| Mathf.Abs(cube.velocity.z) > 0.4f)) {
//			Debug.Log ("audio played");
//			audS.clip = slide;
//			audS.Play ();
//		} else if (cube.velocity.magnitude < 0.4f){
//			audS.Stop();
//		}
	}

	void OnCollisionEnter(Collision other) {
		if (!audS.isPlaying && !other.gameObject.CompareTag("Player")) {
			audS.clip = crash;
			audS.volume = 0.5f;
			audS.Play ();
		}
	}
//	void OnCollisionStay(Collision other) {
//		if (!other.gameObject.CompareTag("Player") && !audS.isPlaying && cube.velocity.magnitude > 0.2) {
//			audS.clip = slide;
//			audS.Play ();
//		}
//	}
	void OnCollisionExit(Collision other) {
		if (!other.gameObject.CompareTag ("Player")) {
			audS.Stop ();
		}
	}
}
