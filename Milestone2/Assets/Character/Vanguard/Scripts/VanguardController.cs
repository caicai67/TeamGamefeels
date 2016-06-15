using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(VanguardMetrics))]
[RequireComponent(typeof(CharacterController))]
public class VanguardController : MonoBehaviour {
	public Camera cam;
	private Animator animator;
	private MyKeymapping keymap = new MyKeymapping();
	private VanguardMetrics metrics;
	private CharacterController controller;
	private Rigidbody rigid;
	private float stopWalkingTimer;
	private bool rolling = false;
	private bool running_turn_180 = false;
	private bool exploration_mode = false;
	private bool turning = false;
	private float float_save_slot = 0f;
	// Use this for initialization
	void Awake() {
		this.rigid = GetComponent<Rigidbody> ();
		this.controller = GetComponent<CharacterController> ();
		this.animator = GetComponent<Animator> ();
		metrics = GetComponent<VanguardMetrics> ();
		this.stopWalkingTimer = 0f;
	}
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (this.keymap.Exit ()) {
			Application.Quit ();
		}
	}
	void FixedUpdate()
	{
		
		AnimatorStateInfo current_state = this.animator.GetCurrentAnimatorStateInfo (0);
		//var v = CrossPlatformInputManager.GetAxis("PS4ControllerLeftY");
		var forward = this.metrics.forwardInput;
		bool runSkill = this.keymap.RunSkill ();
		float normalized_angular_input = Mathf.Abs (this.metrics.angularInput) / Mathf.PI;
		//var v = this.metrics.relativeV

		// Controls
		this.animator.SetBool("RunSkill",this.keymap.RunSkill());
		this.animator.SetBool ("SecondarySkill", this.keymap.SecondarySkill ());
		this.animator.SetBool ("RollAction", this.keymap.RollAction ());

		// Throttles
		this.animator.SetFloat("RightThrottle",this.metrics.rightThrottle);

		if (current_state.IsName("falling_idle") || current_state.IsName("falling_to_landing")){
			this.animator.SetBool("Falling",true);
			Vector3 direction = new Vector3 (Mathf.Cos (this.transform.eulerAngles.y), 0.2f, Mathf.Sin (this.transform.eulerAngles.y));
			this.rigid.AddForce (10f * direction.normalized);
		} else{
			this.animator.SetBool("Falling",false);
		}
		// Environment Settings
		this.animator.SetFloat("VerticalVelocity",this.controller.velocity.y);
		this.animator.SetFloat("GroundDistance",this.metrics.raycastToGround.distance - 0.1f);
		this.animator.SetBool ("Grounded", this.metrics.grounded);
		this.animator.SetBool ("Sliding", this.metrics.sliding);

		// Input Magnitude
		this.animator.SetFloat("InputMagnitude",this.metrics.inputMagnitude);
		// Forward Input
		this.animator.SetFloat ("ForwardInput", forward, 0f, Time.deltaTime);
		if (forward == 0f) {
			this.animator.SetBool ("noForwardInput", true);
			this.stopWalkingTimer += Time.deltaTime;
		} else {
			this.animator.SetBool ("noForwardInput", false);
			this.stopWalkingTimer = 0f;
			this.animator.ResetTrigger ("StopWalkingTrigger");
		}
		if (this.stopWalkingTimer > 0.1) {
			this.animator.SetTrigger ("StopWalkingTrigger");
		}
		this.animator.SetFloat ("TotalSpeed", this.metrics.actual_speed);
		if (this.metrics.actual_speed < 0.2f) {
			this.animator.SetBool ("StandingStill", true);
		} else {
			this.animator.SetBool ("StandingStill", false);
		}

		// Speeds
		this.animator.SetFloat ("ForwardSpeed", this.metrics.forward_speed/this.metrics.max_forward_speed);
		this.animator.SetFloat ("BackwardSpeed", this.metrics.backward_speed/this.metrics.max_backward_speed);

		// Angular Input
		this.animator.SetFloat("AngularInput",this.metrics.angularInput);
		this.animator.SetFloat ("NormalizedAngularInput", normalized_angular_input);



		// Turning Logic
		if (current_state.IsName ("RightTurn") || current_state.IsName ("LeftTurn")) {
			if (!turning) {
				this.turning = true;
				this.float_save_slot = this.animator.speed;
			}
			if (this.metrics.updateForwardInput) this.metrics.updateForwardInput = false;
			this.metrics.forwardInput = this.metrics.inputMagnitude;
			// Increase the rate of turn proportional to normalized angular input!
			if (normalized_angular_input > 0.3f) {
				this.animator.speed = this.float_save_slot + normalized_angular_input * 2f;
			} else { 
				this.animator.speed = this.float_save_slot;
			}
		} else {
			if (turning){
				this.turning = false;
				this.animator.speed = this.float_save_slot;
			}
			if(!this.metrics.updateForwardInput) this.metrics.updateForwardInput = true;
		}
		if (current_state.IsName ("isolated_standing_right_turn") || current_state.IsName("isolated_standing_right_turn 0") || current_state.IsName("IdleWalkRun")) {
			this.animator.SetFloat ("StanceSide", 1.0f);
		} else if (current_state.IsName ("isolated_standing_left_turn") || current_state.IsName("isolated_standing_left_turn 0")) {
			this.animator.SetFloat ("StanceSide", 0f);
		}


		// Running turn 180 logic
		if (current_state.IsName ("running_turn_180")) {
			if (!this.running_turn_180)
				this.running_turn_180 = true;
			this.metrics.forward_speed = Mathf.Max (0.6f*this.metrics.max_forward_speed, this.metrics.forward_speed);
			this.animator.SetFloat ("ForwardSpeed", 0.6f);
			this.metrics.update_speed = false;
		} else {
			if (this.running_turn_180) {
				this.metrics.update_speed = true;
				this.running_turn_180 = false;
			}
		}

		// Rolling Logic

		if (current_state.IsName ("sprinting_forward_roll") && !this.rolling) {
			this.metrics.forward_speed = this.metrics.max_forward_speed;
			this.animator.SetFloat ("ForwardSpeed", 1f);
			this.metrics.update_speed = false;
			this.rolling = true;
			Vector3 player_rotation = new Vector3 (this.transform.eulerAngles.x, -(this.metrics.angularInput * 180f / Mathf.PI), this.transform.eulerAngles.z);
			if (player_rotation.y > 360f) player_rotation.y -= 360f;
			// Transform Player
			this.transform.Rotate (player_rotation);
			this.animator.speed = 1.3f*this.animator.speed;
		} else if (!current_state.IsName("sprinting_forward_roll")){
			if (this.rolling) {
				this.metrics.update_speed = true;
				this.rolling = false;
				this.animator.speed = this.animator.speed / 1.3f;
			}
		}

		// Exploration Mode Logic
		if (current_state.IsName ("walking") ||
		    current_state.IsName ("start_walking") ||
		    current_state.IsName ("basic_stop_walking") ||
		    current_state.IsName ("basic_walking_turn_180") ||
		    current_state.IsName ("Walking_Right_Turn") ||
		    current_state.IsName ("Walking_Left_Turn")) {
			this.metrics.update_speed = false;
			this.metrics.forward_speed = 0.1f;
			this.metrics.backward_speed = 0.1f;
			if (!this.exploration_mode) this.exploration_mode = true;
		} else {
			if (this.exploration_mode) {
				this.exploration_mode = false;
				this.metrics.update_speed = true;
			}
		}
	}
}
