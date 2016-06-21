using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerMetrics))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {
	public Camera cam;
	private Keymapping keymap = new Keymapping();
	private Rigidbody rigid_body;
	private Animator animator;
	private PlayerMetrics metrics;
	private CharacterController controller;
	private CapsuleCollider collider;
	private AnimationManager anim_manager;

	// Collider/Controller Defaults
	float controller_height = 1.7f;
	float collider_height = 1.7f;
	Vector3 controller_center = new Vector3 (0f, 0.85f, 0f);
	Vector3 collider_center = new Vector3 (0f, 0.85f, 0f);


	// In game variables
	private bool sneaking = false;
	private bool rolling = false;

	void Awake(){
		this.rigid_body = GetComponent<Rigidbody> ();
		this.animator = GetComponent<Animator> ();
		this.metrics = GetComponent<PlayerMetrics> ();
		this.controller = GetComponent<CharacterController> ();
		this.collider = GetComponent<CapsuleCollider> ();
		this.anim_manager = new AnimationManager ();
	}
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (this.keymap.Exit ()) {
			Application.Quit ();
		}
		AnimatorStateInfo current_state = this.animator.GetCurrentAnimatorStateInfo (0);

		// Environment Settings
		this.animator.SetBool("Grounded",this.metrics.grounded);

		// Inputs





		this.animator.SetFloat("ForwardInput",this.metrics.forward_input);
		this.animator.SetFloat ("AngularInput", this.metrics.angular_input);
		this.animator.SetFloat ("InputMagnitude", this.metrics.input_magnitude);



		// Sections below: may not need them; leaving them for notes
		// Movement Modes




		// SNEAK MODE
		// ...for some reason, the sneak mode is not "triggering;" so janky boolean local variable instead. 
		// ...maybe it has something to do with the animation transitions

		//if (!current_state.IsName ("Sneak Mode")) {
			//this.animator.ResetTrigger ("sneakTrigger");
		//}
		if (Input.GetKeyDown(this.keymap.movement_toggle1.keyboard)||Input.GetButtonDown(this.keymap.movement_toggle1.ps4)){
			this.sneaking = !(this.sneaking);
			this.animator.SetBool("sneak",this.sneaking);
			//this.animator.SetTrigger ("sneakTrigger");
		}




		// Button Controls
		if (!current_state.IsName("Roll")){
			this.animator.ResetTrigger ("roll");
		}
		if (Input.GetKeyDown (this.keymap.roll_action.keyboard) || Input.GetButtonDown (this.keymap.roll_action.ps4)) {
			this.anim_manager.roll_timer = 0f;
			this.animator.SetTrigger ("roll");
		}
		// Throttles

		// Input Magnitude

		// Forward Input

		// Speeds


		// Angular Input
	}
	void FixedUpdate(){
		UpdateColliderController ();
	}
	void UpdateColliderController(){
		if (animator.enabled){
			if (animator.GetCurrentAnimatorStateInfo (0).IsName(anim_manager.roll.ac_name)) {
				this.anim_manager.roll_timer += Time.deltaTime;
				float c_height = this.anim_manager.roll.collider_height.Evaluate (this.anim_manager.roll_timer);
				this.collider.height = c_height;
				this.controller.height = c_height;

			}
		}
	}
}
