using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PortalEnd : MonoBehaviour {

	public Canvas canvas;
	public GameObject endScreen;

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

			//Show end title card
			endScreen.SetActive(true);
		}
	}
}
