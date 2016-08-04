using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PortalEnd : MonoBehaviour {

	public GameObject endScreen;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player")){
			
			Debug.Log ("End Screen Reached!");

			//Show end title card
			endScreen.SetActive(true);
		}
	}
}
