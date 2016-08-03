using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using InControl;

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
	public bool demon_spell_hit = false;
	private bool demon_spell_impact_animation_playing = false;

    //Audio Clips
    public AudioSource audio;
    public AudioClip die;
    public AudioClip swordDraw;
    public AudioClip swordSheath;
    public AudioClip swordSwing;
    public AudioClip characterRollGrunt;
    public AudioClip characterJumpGrunt;
    public AudioClip characterCrouch;


    //InControl's InputDevice variable
    public InputDevice activeController;
	public float player_horizontal_axis = 0f;
	public float player_vertical_axis = 0f;

	public float camera_horizontal_axis = 0f;
	public float camera_vertical_axis = 0f;

	//Sword Combat related variables
	public bool swingedSword = false;

	// Collider/Controller Defaults
	float controller_height;
	float collider_height;
	Vector3 controller_center;
	Vector3 collider_center;
	private Vector3 player_origin;

	// In game variables
	private bool sneaking = false;
	private bool fighting = false;
	private bool player_dead = false;
	public bool using_character_controller = false;
	private bool isControllerEnabled = false;
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

		//Intialize 
	}
	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {

		//Get the connected controller through InControl's InputManager
		activeController = InputManager.ActiveDevice;

		//Get input from analog sticks
		///////////////// 

		this.player_horizontal_axis = activeController.LeftStickX.Value;
		this.player_vertical_axis = activeController.LeftStickY.Value;

		/////////////////

		this.camera_horizontal_axis = activeController.RightStickX.Value;
		this.camera_vertical_axis = activeController.RightStickY.Value;

		/////////////////
		/// Switch between controller and keyboard using I key on the keyboard
		if (Input.GetKeyDown (KeyCode.I)) {
			isControllerEnabled = !isControllerEnabled;
		}

		if (this.keymap.Exit ()) {
			Application.Quit ();
		}

		//Note here that inControl treats the Options key on a PS4 controller as the Select key for some reason
		if (Input.GetKey (this.keymap.respawn.keyboard) || activeController.GetControl(InputControlType.Select).WasPressed) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		}
		AnimatorStateInfo current_state = this.animator.GetCurrentAnimatorStateInfo (0);

		// Environment Settings (these should be set first!)
		this.animator.SetBool("Grounded",this.metrics.grounded);
		this.animator.SetBool ("RunSkill", (Input.GetKey (this.keymap.run_skill.keyboard) || activeController.LeftBumper.IsPressed));
		// Inputs


		////////////////

		this.animator.SetFloat("ForwardInput",this.metrics.forward_input);
		this.animator.SetFloat ("AngularInput", this.metrics.angular_input);
		this.animator.SetFloat ("InputMagnitude", this.metrics.input_magnitude);



		// Sections below: may not need them; leaving them for notes
		// Spell Impacts
		demon_spell_impact_animation_playing = animator.GetCurrentAnimatorStateInfo(0).IsName("Demon Spell Impact");

		if (demon_spell_impact_animation_playing) {
			if (animator.GetBool("CanBeHit"))
				animator.SetBool ("CanBeHit", false);
			if (animator.GetBool ("DemonSpellHit"))
				animator.SetBool ("DemonSpellHit", false);
			if (this.demon_spell_hit)
				this.demon_spell_hit = false;
		}

		if (!demon_spell_impact_animation_playing) {
			if (!animator.GetBool ("CanBeHit"))
				animator.SetBool ("CanBeHit", true);
		}

		if (this.demon_spell_hit)
			animator.SetBool ("DemonSpellHit", true);




		// SNEAK MODE
		// ...for some reason, the sneak mode is not "triggering;" so janky boolean local variable instead. 
		// ...maybe it has something to do with the animation transitions

		//Code to change the collider height and Y value if in sneak mode
		if (this.sneaking == true) {

			//Make collider smaller in sneak mode
			//this.collider_.height = 0.7f;
		//	this.collider_. = new Vector3 (0.0f, 0.35f, 0.0f);

		} else {
			//Make collider larger in walking/idle/run mode
			//this.animator.SetFloat ("ColliderHeight", 1.7f);
			//this.animator.SetFloat ("ColliderY", 0.85f);

		}

		if (Input.GetKeyDown(this.keymap.movement_toggle1.keyboard)||activeController.LeftStickButton.WasPressed){
			
			this.sneaking = !(this.sneaking);
			this.animator.SetBool("sneak",this.sneaking);
            this.audio.clip = this.characterCrouch;
            this.audio.Play();
        }




		// Button Controls
		if (!current_state.IsName("Roll")){
			this.animator.ResetTrigger ("roll");
		}
		if (Input.GetKeyDown (this.keymap.roll_action.keyboard) || activeController.Action2.WasPressed) {
			this.animator.SetTrigger ("roll");
            this.audio.clip = this.characterRollGrunt;
            this.audio.Play();
        }

		//Jumping Code
		if (Input.GetKeyDown(this.keymap.jump.keyboard) || activeController.Action1.WasPressed) {
			
			//Disable sneaking if it was active.
			if (this.sneaking == true) {
				this.sneaking = false;

				//Let animator know that sneak mode is over
				this.animator.SetBool ("sneak", this.sneaking);

			} else {
				
				//Set jump trigger to jump
				this.animator.SetTrigger ("Jump");
                this.audio.clip = this.characterJumpGrunt;
                this.audio.Play();
            }

		}

		//Sword Draw and Sheath Code
		if (Input.GetKeyDown (this.keymap.fight.keyboard) || activeController.RightBumper.WasPressed || activeController.DPadUp.WasPressed) {

			//Disable sneaking if it was active.
			if (this.sneaking == true) {
				this.sneaking = false;

				//Let animator know that sneak mode is over
				this.animator.SetBool ("sneak", this.sneaking);

			} else {

				if (!this.fighting) {
					this.fighting = true;
					this.animator.SetTrigger ("DrawSword");
                } else {
					this.fighting = false;
					this.animator.SetTrigger ("SheathSword");
                }
			}

		}

		//Sword Fight Code
		if (Input.GetKeyDown (this.keymap.fight.keyboard) || activeController.RightTrigger.WasPressed) {

			//Disable sneaking if it was active.
			if (this.sneaking == true) {
				this.sneaking = false;

				//Let animator know that sneak mode is over
				this.animator.SetBool ("sneak", this.sneaking);

			} else {

				if (!this.fighting) {
					this.fighting = true;
					this.animator.SetTrigger ("DrawSword");
                } else {

					swingedSword = true;
					this.animator.SetTrigger ("SlashSword");
                }
			}

		}

        //Hanging Code
		if (canInteract && (Input.GetKeyDown(this.keymap.interaction.keyboard) || activeController.Action3.WasPressed))
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
                //this.rigid_body.useGravity = true;
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

	void DrawSword(){

		Transform katana = GameObject.FindGameObjectWithTag ("Sword").transform;
		katana.parent = GameObject.FindGameObjectWithTag ("RightHand").transform;

		//Values for KP's scene
		//katana.localPosition = new Vector3 (-0.081f, 0.113f, -0.039f);
		//katana.localEulerAngles = new Vector3 (9.292282f, 39.94241f, 304.3139f);

		//Values for Temp2 scene
		katana.localPosition = new Vector3 (-0.103f, 0.018f, 0.043f);
		katana.localEulerAngles = new Vector3 (3.971051f, 134.7626f, 142f);


        this.audio.clip = this.swordDraw;
        this.audio.Play();
	}

	void SheathSword(){

		Transform katana = GameObject.FindGameObjectWithTag ("Sword").transform;
		katana.parent = GameObject.FindGameObjectWithTag ("SwordHolster").transform;

		katana.localPosition = new Vector3 (0.123f, -0.06f, 0.01f);
		katana.rotation = katana.parent.rotation;

        this.audio.clip = this.swordSheath;
        this.audio.Play();
    }

    void SwordAttack()
    {
        this.audio.clip = this.swordSwing;
        this.audio.Play();
    }



	//Getter method to be used in CombatSounds.cs
	public bool isFighting(){
		return this.fighting;
	}

	public float getHorizontalMovement(){
		
		if(isControllerEnabled){
			return activeController.LeftStickX.Value;
		} else {
			return Input.GetAxis("Horizontal");
		}
	}
	public float getVerticalMovement(){

		if(isControllerEnabled){
			return activeController.LeftStickY.Value;
		} else {
			return Input.GetAxis("Vertical");
		}
	}

	public float getHorizontalCameraMovement(){

		if(isControllerEnabled){
			return activeController.RightStickX.Value;
		} else {
			return Input.GetAxis("Cam_Horizontal");
		}
	}
	public float getVerticalCameraMovement(){

		if(isControllerEnabled){
			return activeController.RightStickY.Value;
		} else {
			return Input.GetAxis("Cam_Vertical");
		}
	}
}
