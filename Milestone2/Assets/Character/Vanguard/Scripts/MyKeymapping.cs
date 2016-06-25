using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class MyKeymapping : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	private float MaxInput(float inputA,float inputB){
		if (Mathf.Abs (inputA) >= Mathf.Abs (inputB)) {
			return inputA;
		} 
		return inputB;
	}
	public string Jump(){
		string jump_key = "PS4ControllerCross";
		return jump_key;
	}
	public float Camera_HorizontalAxis(){
		float ps4_input = Input.GetAxis ("PS4ControllerRightX");
		float keyboard_input = Input.GetAxis ("Cam_Horizontal");
		return MaxInput(ps4_input,keyboard_input);
	}
	public float Camera_VerticalAxis(){
		float ps4_input = Input.GetAxis ("PS4ControllerRightY");
		float keyboard_input = Input.GetAxis ("Cam_Vertical");
		return MaxInput (ps4_input, keyboard_input);
	}
	public float Player_HorizontalAxis(){
		float ps4_input = Input.GetAxis ("PS4ControllerLeftX");
		float keyboard_input = Input.GetAxis ("Horizontal");
		return MaxInput(ps4_input,keyboard_input);
	}
	public float Player_VerticalAxis(){
		float ps4_input = Input.GetAxis ("PS4ControllerLeftY");
		float keyboard_input = Input.GetAxis ("Vertical");
		return MaxInput(ps4_input,keyboard_input);
	}
	public bool RunSkill(){
		string runSkill_button = "PS4ControllerL1";
		return CrossPlatformInputManager.GetButton (runSkill_button);
	}
	public bool RollAction(){
		string rollAction_button = "PS4ControllerCircle";
		return CrossPlatformInputManager.GetButton (rollAction_button);
	}
	public bool SecondarySkill(){
		string secondarySkill_button = "PS4ControllerR1";
		return CrossPlatformInputManager.GetButton (secondarySkill_button);
	}
	public float RightThrottle(){
		string rightThrottle = "PS4ControllerR2Throttle";
		return CrossPlatformInputManager.GetAxis(rightThrottle);;
	}
	public bool Exit(){
		if (Input.GetKey (KeyCode.Q) || Input.GetKey (KeyCode.Escape)) {
			return true;
		} else {
			return false;
		}
	}
}
