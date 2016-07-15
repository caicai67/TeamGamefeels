using UnityEngine;
using System.Collections;

// List of functions I need to implement
// - HandleGroundMovement
// - HandleAirborneMovement
// - UpdateAnimator !!!!!!!!!!!!!!! (this is the 'good stuff')


// List of functions I am not implementing
// - ScaleCapsuleForCrouching  -- I am not implementing crouch movement
// - PreventStandingInLowHeadroom -- Low headroom blocks me...because No Crouch! Crouching Sucks!



[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Animator))]
public class MyThirdPersonCharacter : MonoBehaviour {

	[SerializeField] float movingTurnSpeed = 360;
	[SerializeField] float stationaryTurnSpeed =180;

	// I think this should depend on speed, map to L2, and the variance in L2 + speed should determine which jump animation!
	[SerializeField] float jumpPower = 12f;

	[Range(1f,4f)][SerializeField] float gravityMultiplier = 2f;

	//[SerializeField] float runCycleLegOffset = 0.2f;
	[SerializeField] float moveSpeedMultiplier = 1f;
	[SerializeField] float animSpeedMultiplier = 1f;
	[SerializeField] float groundCheckDistance = 0.1f;

	Rigidbody rigidbody;
	Animator animator;
	bool isGrounded;
	//const float k_Half = 0.5f; // I don't know what this does yet...
	float turnAmount;
	float forwardAmount;
	Vector3 groundNormal;
	Vector3 down;
	CapsuleCollider capsule;
	RaycastHit groundHit;
	//bool crouching; // I don't like crouching. Crouching and crawling are not fun. 


	// Use this for initialization
	void Start () {
		// Initialize class members
		this.animator = GetComponent<Animator>();
		this.rigidbody = GetComponent<Rigidbody> ();
		this.capsule = GetComponent<CapsuleCollider> ();
		Physics.Raycast (transform.position, -Vector3.up, out this.groundHit, 50.0f);

		UpdateGroundHit ();
		// I don't know what this does yet. 
		//this.rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
	}
	// Update is called once per frame
	void Update () {
		// These functions are called from the "MyThirdPersonUserControl.cs" file
	}




	void UpdateGroundHit(){
		Physics.Raycast (transform.position, -Vector3.up, out this.groundHit, 50.0f);
		this.groundHit.distance = this.groundHit.distance - (this.capsule.height / 2f);
	}

	public void Move(Vector3 move, bool Jump){ // Add more user input state modifiers here
		if (move.magnitude > 1f) move.Normalize();
		move = transform.InverseTransformDirection (move);
		this.turnAmount = Mathf.Atan2 (move.x, move.z);
		this.forwardAmount = move.z;
		ApplyExtraTurnRotation();

		// Test To see if is grounded 
		UpdateGroundHit();
		this.isGrounded = (this.groundHit.distance < 0.1);
		// done

		if (this.isGrounded) {
			HandleGroundedMovement (false); 
		} else {
			HandleAirborneMovement (this.groundHit);
		}
		UpdateAnimator (move);
	}
	void ApplyExtraTurnRotation()
	{
		float turnSpeed = Mathf.Lerp (this.stationaryTurnSpeed, this.movingTurnSpeed, this.forwardAmount);
		transform.Rotate (0, this.turnAmount * turnSpeed * Time.deltaTime, 0);
	}
	void HandleAirborneMovement(RaycastHit hit){
		Vector3 extraGravityForce = (Physics.gravity * this.gravityMultiplier) - Physics.gravity;
		this.rigidbody.AddForce (extraGravityForce);

		// Do stuff with hit!
		// Landing *much simpler
		//    - basic landing *low (0 - 1 meters)
		//    - heavy landing *med (1 - 2 meters)
		//    - hard landing  (damage) *high (2 - 6 meters; unrealistic, but large enough for user to notice) 
		//    - hard landing with roll (no damage, user input) *highest (2-12 meters)
		//    - hard landing to splat (or roll--overlap here) *very high (6 - 15)
		//    - out of control fall to splat *extremely high (15 - infinity)
	}
	void HandleGroundedMovement(bool jump){
		// leaving blank for now.  just walking at first. 


		// Jump *This could get complicated*
		//    - stationary jump
		//    - running/sprinting jump
		//    - jump over zombie
		//    - jump up to low wall/block
		//    - jump up to medium wall/block
		//    - jump up to hang

		// Walk 
		// Run
		// Sprint
		// Slide
	}
	void UpdateAnimator(Vector3 move){
		// Starting with walking forward
		this.animator.SetFloat("Forward",this.forwardAmount,0f,Time.deltaTime);
		//if (


		// This is where I do all the good stuff!!


		// Jump *This could get complicated*
		//    - stationary jump
		//    - running/sprinting jump
		//    - jump over zombie
		//    - jump up to low wall/block
		//    - jump up to medium wall/block
		//    - jump up to hang

		// Do stuff with hit!
		// Landing *much simpler
		//    - basic landing *low (0 - 1 meters)
		//    - heavy landing *med (1 - 2 meters)
		//    - hard landing  (damage) *high (2 - 6 meters; unrealistic, but large enough for user to notice) 
		//    - hard landing with roll (no damage, user input) *highest (2-12 meters)
		//    - hard landing to splat (or roll--overlap here) *very high (6 - 15)
		//    - out of control fall to splat *extremely high (15 - infinity)

		// Walk 
		// Run
		// Sprint
		// Slide

		// Roll!!
		// Wall run
		// Climb
		// Swing
	}
	//public void OnAnimatorMove()
	//{
	//	Vector3 newPosition = transform.position;

		/* //Unity Implementation:

		 * if (this.isGrounded && Time.deltaTime > 0) {
			Vector3 v = (this.animator.deltaPosition * this.moveSpeedMultiplier) / Time.deltaTime;
			v.y = this.rigidbody.velocity.y;
			this.rigidbody.velocity = v;
		}*/ 
	//}
}

