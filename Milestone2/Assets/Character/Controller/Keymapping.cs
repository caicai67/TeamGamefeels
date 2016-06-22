using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class Keymapping : MonoBehaviour {
	
	public struct MovementToggle1
	{
		public KeyCode keyboard;
		public string ps4;
		public MovementToggle1(KeyCode key,string button)
		{
			this.keyboard = key;
			this.ps4 = button;
		}
	}
	public struct RollAction
	{
		public KeyCode keyboard;
		public string ps4;
		public RollAction(KeyCode key, string ps4button){
			this.keyboard = key;
			this.ps4 = ps4button;
		}
	}
	public struct Jump
	{
		public KeyCode keyboard;
		public string ps4;
		public Jump(KeyCode key, string ps4button){
			this.keyboard = key;
			this.ps4 = ps4button;
		}
	}

	public MovementToggle1 movement_toggle1 = new MovementToggle1(KeyCode.Slash,"PS4ControllerL3");
	public RollAction roll_action = new RollAction(KeyCode.Alpha0,"PS4ControllerCircle");
	public Jump jump = new Keymapping.Jump(KeyCode.Space,"PS4ControllerCross");

	private float MaxInput(float inputA,float inputB){
		if (Mathf.Abs (inputA) >= Mathf.Abs (inputB)) {
			return inputA;
		} 
		return inputB;
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
