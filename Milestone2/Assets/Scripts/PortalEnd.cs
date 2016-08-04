using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PortalEnd : MonoBehaviour {

	public Canvas canvas;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player")){
			Animation anim = canvas.GetComponent<Animation> ();
			anim.Play ();
		}
	}
}
