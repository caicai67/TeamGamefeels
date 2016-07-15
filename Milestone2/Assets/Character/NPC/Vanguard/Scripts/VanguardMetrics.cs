using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class VanguardMetrics : MonoBehaviour {
	public CapsuleCollider capsule;
	public RaycastHit raycastToGround;
	public Camera cam;
	public bool grounded = false; 
	public bool sliding = false; // enable later!
	public float actual_speed = 0f;
	public float forward_speed = 0f;
	public float acceleration_ability = 2.5f; //units per second^2
	public float deceleration_ability = 6f;
	public float backward_speed = 0f;
	public float backward_acceleration_ability = 1.5f;
	public float backward_deceleration_ability = 6f;
	public float max_backward_speed = 4f;
	public float max_forward_speed = 6.0f; //units per second.  This value is determined by animation!
	public float forwardInput = 0f; // the input from the controller pointing forward!
	public float inputMagnitude = 0f;
	public bool velocity_input_match;
	public Vector3 position;
	public Vector3 deltaX;
	public Vector2 player_joystick;
	public float angularInput;
	public float rightThrottle;
	public bool updateForwardInput = true;
	public bool update_speed = true;

	private MyKeymapping keymap = new MyKeymapping();

	// Use this for initialization
	void Start () {
		this.capsule = GetComponent<CapsuleCollider> ();
		UpdateGroundHit ();
		this.position = transform.position;
		this.velocity_input_match = false;
	}
	
	// Update is called once per frame
	void Update () {
		UpdateGroundHit ();
	}

	void FixedUpdate(){
		Vector3 newPosition = transform.position;
		this.deltaX = newPosition - this.position;
		this.actual_speed = deltaX.magnitude / Time.deltaTime;
		this.position = newPosition;
		UpdateControllerInput ();

		UpdateVelocityInputMatch ();

		if (this.update_speed) {
			if (this.forwardInput > 0f) {

				// Case: 180 pivot from backpedal
				if (this.velocity_input_match && this.backward_speed > 0f) {
					this.forward_speed = Mathf.Max (this.forward_speed, this.backward_speed);
					this.backward_speed = 0f;
				}


				// Case: Running Backwards
				// decelerate backwards speed
				if (this.backward_speed > 0f) {
					this.backward_speed = ForcedDeceleration (this.backward_speed, this.max_backward_speed, this.forwardInput, this.backward_deceleration_ability);
				}

				// Case: Stopped or Running forwards
				if (this.backward_speed <= 0f) {
					this.forward_speed = Acceleration (this.forward_speed, this.max_forward_speed, this.forwardInput, this.acceleration_ability);
				}
			}

			if (this.forwardInput == 0f) {
				// Case: Backpedaling
				if (this.backward_speed > 0f) {
					this.backward_speed = UnforcedDeceleration (this.backward_speed, this.max_backward_speed, this.forwardInput, this.backward_deceleration_ability);
				}

				// Case: Running
				if (this.forward_speed > 0f) {
					this.forward_speed = UnforcedDeceleration (this.forward_speed, this.max_forward_speed, this.forwardInput, (1.5f + this.acceleration_ability));
				}
			}

			if (this.forwardInput < 0f) {
				// Case: Running Forward
				if (this.forward_speed > 0f) {
					this.forward_speed = ForcedDeceleration (this.forward_speed, this.max_forward_speed, (-1 * this.forwardInput), this.deceleration_ability);
				}
				// Case: Backpedaling
				if (this.forward_speed <= 0f) {
					this.backward_speed = Acceleration (this.backward_speed, this.max_backward_speed, (-1f * this.forwardInput), this.backward_acceleration_ability);
				}
			}
		}
		// Set limit at zero
		if (this.forward_speed < 0f) this.forward_speed = 0f;
		if (this.backward_speed < 0f) this.backward_speed = 0f;
	}

	float Acceleration(float speed, float max_speed, float forward_input_var,float acceleration_ability){
		float proportional_forwardInput = forward_input_var - speed / max_speed;
		return Mathf.Min ((proportional_forwardInput + 0.3f) * this.acceleration_ability * Time.deltaTime + speed, max_speed);
	}
	float ForcedDeceleration(float speed, float max_speed, float forward_input_var, float deceleration_ability){
		return Mathf.Min (forward_input_var * deceleration_ability * (-Time.deltaTime) + speed, max_speed);
	}

	float UnforcedDeceleration(float speed, float max_speed, float forward_input_var,float deceleration_ability){
		float speed_over_max_ratio = speed / max_speed;
		return Mathf.Max (0f, (0.2f + speed_over_max_ratio) * deceleration_ability * (-Time.deltaTime) + speed);
	}

	void UpdateGroundHit(){
		Physics.Raycast (transform.position, -Vector3.up, out this.raycastToGround, 50.0f);
		this.raycastToGround.distance = this.raycastToGround.distance;
		if (this.raycastToGround.distance - 0.6f < 0.1f) {
			this.grounded = true;
		} else {
			this.grounded = false;
		}
	}
	void UpdateVelocityInputMatch(){
		Vector2 delta_xz = new Vector2 (this.deltaX.x, this.deltaX.z);
		if (Vector2.Dot (this.player_joystick, delta_xz) > 0f) {
			this.velocity_input_match = true;
		} else {
			this.velocity_input_match = false;
		}
	}
	void UpdateControllerInput(){

		//Throttles (R2 and L2 on the PS4 controller)
		// Setting base value to 0 and upper limit to 1 (originally ranges from -1 to 1)
		this.rightThrottle = this.keymap.RightThrottle();


		float camera_y_rotation = (float)cam.transform.eulerAngles.y;
		float player_y_rotation = (float)transform.eulerAngles.y;

		Vector2 player_unit_forward = new Vector2(Mathf.Sin((player_y_rotation*Mathf.PI)/180f),Mathf.Cos((player_y_rotation*Mathf.PI)/180f));
		Vector2 camera_unit_forward = new Vector2 (Mathf.Sin ((camera_y_rotation * Mathf.PI) / 180f), Mathf.Cos ((camera_y_rotation * Mathf.PI) / 180f));

		float phi_player_camera = CalculatePhi (player_unit_forward, camera_unit_forward);

		Vector2 phi_unit_forward = new Vector2 (Mathf.Sin (phi_player_camera), Mathf.Cos (phi_player_camera));

		this.player_joystick = new Vector2 (this.keymap.Player_HorizontalAxis (), this.keymap.Player_VerticalAxis ());
		this.inputMagnitude = this.player_joystick.magnitude;
		this.player_joystick.Normalize ();



		// Forward Input

		float forward_input = Vector2.Dot (phi_unit_forward, this.player_joystick);
		if (float.IsNaN(forwardInput)){
			forward_input = 0f;
		}
		if (this.updateForwardInput) {
			this.forwardInput = forward_input;
		}

		// Colculate Angular offset (phi = Acos(A dot B) / |A|)
		float angular_offset = Mathf.Acos(forward_input / this.player_joystick.magnitude); // should be 1.0...
		if (float.IsNaN (angular_offset)) {
			angular_offset = 0f;
		}

		float sign = AngleSign (phi_unit_forward, player_joystick);
		this.angularInput = sign*angular_offset;
		// for now, it is just using the axis information from the controller.
		//return this.keymap.Player_VerticalAxis();
	}
	float CalculatePhi(Vector2 n1,Vector2 n2){
		n1.Normalize ();
		n2.Normalize ();
		float half_angle = Mathf.Acos (Vector2.Dot (n1, n2));
		float angle_sign = AngleSign (n1, n2);
		return half_angle * angle_sign;
	}
	float AngleSign(Vector2 n1,Vector2 n2){
		return Mathf.Sign (n1.x * n2.y - n1.y * n2.x);
	}
}
