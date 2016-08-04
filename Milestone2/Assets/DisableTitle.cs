using UnityEngine;
using System.Collections;

public class DisableTitle : MonoBehaviour {

	public GameObject titleScreen;

	// Use this for initialization
	void Start () {
		titleScreen = GetComponent<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void disableTitleScreen() {
		titleScreen.SetActive (false);
	}
}
