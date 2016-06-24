using UnityEngine;
using System.Collections;

public class TableBehavior : MonoBehaviour {
	
	public AudioClip slide;
	public AudioClip crash;

	AudioSource audS;
	Transform prevTransform;

	void Start () {
		audS = this.gameObject.AddComponent <AudioSource>();;
		prevTransform = transform;
	}
	void Update() {
		if(Mathf.Abs (transform.position.y - prevTransform.position.y) > 0f) {
			Debug.Log ("audio played");
			audS.clip = crash;
			audS.Play ();
		} else if((Mathf.Abs (transform.position.x - prevTransform.position.x) > 0f 
			|| Mathf.Abs (transform.position.z - prevTransform.position.z) > 0f )) 
		{
			Debug.Log ("audio played");
			audS.clip = slide;
			audS.Play ();
		}

		prevTransform = transform;
	}
}
