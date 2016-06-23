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
	void OnTriggerEnter(Collider other) {
		anim.SetBool ("open", true);
	}
	void OnTriggerExit(Collider other) {
		anim.SetBool ("open", false);
	}
}
