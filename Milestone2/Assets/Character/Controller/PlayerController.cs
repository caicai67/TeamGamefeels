using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerMetrics))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour {
	public Camera cam;
	private Keymapping keymap = new Keymapping();
	private Rigidbody rigid_body;
	private Animator animator;
	private PlayerMetrics metrics;

	// In game variables
	private bool sneaking;

	void Awake(){
		this.rigid_body = GetComponent<Rigidbody> ();
		this.animator = GetComponent<Animator> ();
		this.metrics = GetComponent<PlayerMetrics> ();
		this.sneaking = false;
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
		if (Input.GetKeyDown(this.keymap.movement_toggle1.keyboard)||Input.GetButtonDown(this.keymap.movement_toggle1.ps4)){
			this.sneaking = !(this.sneaking);
			this.animator.SetBool("sneak",this.sneaking);
		}
		// Throttles

		// Input Magnitude

		// Forward Input

		// Speeds


		// Angular Input
	}
	void FixedUpdate(){
	}
}
