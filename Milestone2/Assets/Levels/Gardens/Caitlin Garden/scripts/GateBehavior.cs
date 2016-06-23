using UnityEngine;
using System.Collections;

public class GateBehavior : MonoBehaviour {
	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter() {
		anim.SetBool ("open", true);
		Debug.Log ("gate trigger entered");
	}
	void OnTriggerExit() {
		anim.SetBool ("open", false);
	}
}
