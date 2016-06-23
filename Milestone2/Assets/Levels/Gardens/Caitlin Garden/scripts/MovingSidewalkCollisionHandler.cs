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
			captured.gameObject.transform.parent = this.transform.parent.Find("Moving").Find("CharacterWrapper");
			Debug.Log ("entered trigger");
			Debug.Log ("captured object name is " + captured.name);
			Debug.Log ("new parent is " + captured.gameObject.transform.parent.name);
		}

	}

	void OnTriggerExit (Collider captured) {
		captured.gameObject.transform.parent = null;
	}
}
