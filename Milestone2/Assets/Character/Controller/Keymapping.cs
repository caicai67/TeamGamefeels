using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class Keymapping : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public string Jump(){
		string jump_key = "PS4ControllerCross";
		return jump_key;
	}
	public float Camera_HorizontalAxis(){
		string camera_h = "PS4ControllerRightX";
		return CrossPlatformInputManager.GetAxis(camera_h);
	}
	public float Camera_VerticalAxis(){
		string camera_v = "PS4ControllerRightY";
		return CrossPlatformInputManager.GetAxis(camera_v);
	}
	public float Player_HorizontalAxis(){
		string player_h = "PS4ControllerLeftX";
		return CrossPlatformInputManager.GetAxis (player_h);
	}
	public float Player_VerticalAxis(){
		string player_v = "PS4ControllerLeftY";
		return CrossPlatformInputManager.GetAxis (player_v);
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
