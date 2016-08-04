using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InputDeviceHUD : MonoBehaviour {

	Text controllerInfo;
	public PlayerController playerController = null;
	// Use this for initialization
	void Start () {
		controllerInfo = GetComponent<Text> ();

		if (this.playerController == null) {
			playerController = GetComponent<PlayerController> ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (playerController.isControllerEnabled) {
			controllerInfo.text = "Controller";
		} else {
			controllerInfo.text = "Keyboard";
		}
	}
}
