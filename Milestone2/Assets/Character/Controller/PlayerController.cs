using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


// Team GameFeels
// Chris, Ambrose, KP, Justin, Caitlin, Charlie

//[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerMetrics))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {

    public static Vector3 interactionPosition = new Vector3(0, 0, 0);
    public static Vector3 interactionDirection = new Vector3(0, 0, 0);
    public static bool canInteract = false;

    public Camera cam;
	private Keymapping keymap = new Keymapping();
	private Rigidbody rigid_body;
	private Animator animator;
	private PlayerMetrics metrics;
	private CharacterController controller;
	private CapsuleCollider collider_;
	private SphereCollider trigger;
	public AudioSource audio;
	public AudioClip die;
	// Collider/Controller Defaults
	float controller_height;
	float collider_height;
	Vector3 controller_center;
	Vector3 collider_center;
	private Vector3 player_origin;

	// In game variables
	private bool sneaking = false;
	private bool player_dead = false;
	public bool using_character_controller = false;
	//private bool rolling = false;
    

	void Awake(){
		//this.rigid_body = GetComponent<Rigidbody> ();
		this.player_origin = transform.position;
		this.animator = GetComponent<Animator> ();
		this.metrics = GetComponent<PlayerMetrics> ();
		this.controller = GetComponent<CharacterController> ();
		this.trigger = GetComponent<SphereCollider> ();
		this.collider_ = GetComponent<CapsuleCollider> ();
		this.controller_height = this.controller.height;
		this.collider_height = this.collider_.height;
		this.controller_center = this.controller.center;
		this.collider_center = this.collider_.center;

		//No longer needed as I have set rig's layer(i.e. Ragdoll) to not 
		//interact with Character Model's layer(aka Character) in the Physics settings

		//make the ragdoll kinematic for now
		//makeRagdollKinematic(true);
	}
	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		if (this.keymap.Exit ()) {
			Application.Quit ();
		}
		if (Input.GetKey (this.keymap.respawn.keyboard) || Input.GetButtonDown (this.keymap.respawn.ps4)) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		}
		AnimatorStateInfo current_state = this.animator.GetCurrentAnimatorStateInfo (0);

		// Environment Settings (these should be set first!)
		this.animator.SetBool("Grounded",this.metrics.grounded);
		this.animator.SetBool ("RunSkill", (Input.GetKey (this.keymap.run_skill.keyboard) || Input.GetButton (this.keymap.run_skill.ps4)));
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
			this.animator.SetTrigger ("roll");
		}

		//Jumping Code
		if (Input.GetKeyDown(this.keymap.jump.keyboard) || Input.GetButtonDown (this.keymap.jump.ps4)) {
			this.animator.SetTrigger ("Jump");
		}
        //Hanging Code
        if (canInteract && (Input.GetKeyDown(this.keymap.interaction.keyboard) || Input.GetButtonDown(this.keymap.interaction.ps4)))
        //if (canInteract && Input.GetButtonDown("E"))
        {
            if(this.animator.GetInteger("CurrentInteraction") == 0)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, interactionPosition.z);
                transform.LookAt(new Vector3(transform.position.x, transform.position.y, interactionDirection.z));
                this.animator.SetInteger("CurrentInteraction", 1);
                this.rigid_body.useGravity = false;
                canInteract = false;
            }
            else
            {
                this.animator.SetInteger("CurrentInteraction", 0);
                this.rigid_body.useGravity = true;
                canInteract = false;
            }
        }
	

        // Throttles

        // Input Magnitude

        // Forward Input

        // Speeds


        // Angular Input

        //Level Switch
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Application.LoadLevel("Chris");
        } else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Application.LoadLevel("KP");
        } else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Application.LoadLevel("Caitlin");
        } else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Application.LoadLevel("Ambrose");
        } else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Application.LoadLevel("Charlie");
        } else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Application.LoadLevel("Justin");
        }
    }


	void FixedUpdate(){
	}

	void LateUpdate(){
		if (!animator.IsInTransition (0)) {

			UpdateController ();

		}
		if (animator.GetCurrentAnimatorStateInfo (0).IsName ("jump_over") && this.using_character_controller) {
			if (this.controller.enabled) {
				this.controller.enabled = false;
			}
		} else if (this.using_character_controller && !this.player_dead) {
			this.controller.enabled = true;
		}
	}

	void UpdateController(){
		float ch = animator.GetFloat ("ColliderHeight");
		float cy = animator.GetFloat ("ColliderY");
		if (!(ch == 0f && cy == 0f)) {
			this.controller.height = ch;
			this.controller.center = new Vector3 (0f, cy, 0f);

			this.collider_.height = ch;
			this.collider_.center = new Vector3 (0f, cy, 0f);

		} 

		else {


			this.collider_.height = this.collider_height;
			this.collider_.center = this.collider_center;
			this.controller.height = this.controller_height;
			this.controller.center = this.controller_center;

		}
	}

	void OnCollisionEnter(Collision other){
		if (other.gameObject.layer == 12 && this.controller.enabled) {
			if (!(this.audio == null) && !(this.die == null)) {
				this.audio.clip = this.die;
				this.audio.Play ();
			}
			this.player_dead = true;
			this.controller.enabled = false;
			this.animator.enabled = false;
		}
		if (other.gameObject.layer == 14 && this.using_character_controller) {
			this.controller.enabled = false;
		}
	}
	//void OnTriggerExit(Collider other){
	//	if (other.gameObject.layer == 12 && !this.controller.enabled) {
	//		this.controller.enabled = true;
	//	}
	//}
	void makeRagdollKinematic(bool setKinematic) {

		if (setKinematic) {
			foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>()) {
				rb.isKinematic = true;
			}

			rigid_body.isKinematic = false;
		} else {
			foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>()) {
				rb.isKinematic = false;
			}
			rigid_body.isKinematic = true;
		}
	}

    void checkInteractions()
    {

    }
}
