using UnityEngine;
using System.Collections;
using InControl;

public class InputDeviceController : MonoBehaviour {

	public InputDevice activeController;
	// Use this for initialization
	void Start () {
		activeController = InputManager.ActiveDevice;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
