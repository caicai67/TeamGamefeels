 using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;


// Team GameFeels
// Chris, Ambrose, KP, Justin, Caitlin, Charlie
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
    public struct Interaction
    {
        public KeyCode keyboard;
        public string ps4;
        public Interaction(KeyCode key, string ps4button)
        {
            this.keyboard = key;
            this.ps4 = ps4button;
        }
    }
	public struct RunSkill
	{
		public KeyCode keyboard;
		public string ps4;
		public RunSkill(KeyCode key,string ps4button){
			this.keyboard = key;
			this.ps4 = ps4button;
		}
	}
	public struct Control
	{
		public KeyCode keyboard;
		public string ps4;
		public Control(KeyCode key,string ps4button){
			this.keyboard = key;
			this.ps4 = ps4button;
		}
	}
	public Control movement_toggle1 = new Control(KeyCode.C,"PS4ControllerL3");
	public Control roll_action = new Control(KeyCode.R,"PS4ControllerCircle");
	public Control jump = new Control(KeyCode.Space,"PS4ControllerCross");
    public Control interaction = new Control(KeyCode.E, "PS4ControllerSquare");
	public Control run_skill = new Control(KeyCode.LeftShift,"PS4ControllerR1");
	public Control respawn = new Control(KeyCode.Backspace,"PS4ControllerOptions");

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
	public bool SecondarySkill(){
		string secondarySkill_button = "PS4ControllerR1";
		return Input.GetButton (secondarySkill_button);
	}
    public bool InteractionSkill()
    {
        string interactionSkill_button = "PS4ControllerSquare";
        return CrossPlatformInputManager.GetButton(interactionSkill_button);
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
