//A component of "MovingSidewalk"

using UnityEngine;
using System.Collections;

public class MovingSidewalkCollisionHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	//capture an object when it reaches the moving sidewalk
	void OnTriggerEnter (Collider captured) {
		if (captured.CompareTag ("Player")) {
			Transform wrapper = captured.transform.parent; //Dreyar_Character
			wrapper.parent = this.transform.parent.Find("Moving").Find("CapturedObjs");
			Debug.Log ("entered trigger");
			Debug.Log ("captured object name is " + captured.name);
			Debug.Log ("new parent is " + wrapper.gameObject.transform.parent.name);
		}

	}

	void OnTriggerExit (Collider captured) {
		if (captured.CompareTag ("Player")) {
			Transform wrapper = captured.transform.parent;
			wrapper.transform.parent = null;
		}
	}

}
