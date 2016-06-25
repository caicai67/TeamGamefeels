using UnityEngine;
using System.Collections;


// Team GameFeels
// Chris, Ambrose, KP, Justin, Caitlin, Charlie

public class PlayerMetrics : MonoBehaviour {
	private Keymapping keymap = new Keymapping();
	public Camera cam;
	public RaycastHit ground_raycast;
	public float ground_correction = 0.6f;
	public bool grounded = true;
	public bool airborne = false;
	public float angular_input = 0f;
	public float forward_input = 0f;
	public float input_magnitude = 0f;
	public Vector2 player_joystick;
	public Vector2 player_forward;
	public Vector2 camera_forward;
	public Vector2 phi_forward; // cam to player translation

	// Use this for initialization
	void Start () {
		this.player_joystick = new Vector2 (this.keymap.Player_HorizontalAxis (), this.keymap.Player_VerticalAxis ());

		float player_y_rotation = (float)transform.eulerAngles.y;
		this.player_forward = new Vector2 (Mathf.Sin ((player_y_rotation * Mathf.PI) / 180f), Mathf.Cos ((player_y_rotation * Mathf.PI) / 180f));

		float camera_y_rotation = (float)cam.transform.eulerAngles.y;
		this.camera_forward = new Vector2 (Mathf.Sin ((camera_y_rotation * Mathf.PI) / 180f), Mathf.Cos ((camera_y_rotation * Mathf.PI) / 180f));

		float phi_player_camera = CalculatePhi (this.player_forward, this.camera_forward);
		this.phi_forward = new Vector2 (Mathf.Sin (phi_player_camera), Mathf.Cos (phi_player_camera));
	}
	
	// Update is called once per frame
	void Update () {
		UpdateGroundHit ();
		UpdatePlayerJoystick ();
		UpdateDirectionVectors ();
		UpdateInputs ();
	}
	void UpdatePlayerJoystick(){
		this.player_joystick.x = this.keymap.Player_HorizontalAxis ();
		this.player_joystick.y = this.keymap.Player_VerticalAxis ();
	}
	void UpdateGroundHit(){
		Physics.Raycast (transform.position, -Vector3.up, out this.ground_raycast, 50.0f);
		this.ground_raycast.distance = this.ground_raycast.distance;
		if (this.ground_raycast.distance - this.ground_correction < 0.1f) {
			this.grounded = true;
		} else {
			this.grounded = false;
		}
	}

	// This calculates the relative values:
	// ...forward_input
	// ...angular_input
	void UpdateInputs(){
		this.input_magnitude = this.player_joystick.magnitude;
		if (this.input_magnitude > 0.9f)
			this.input_magnitude = 1.0f;
		// Order sort of matters here: first, forward input.  Then, angular input. 
		UpdateForwardInput();
		UpdateAngularInput();

	}
	void UpdateDirectionVectors(){
		float player_y_rotation = (float)transform.eulerAngles.y;
		this.player_forward.x = Mathf.Sin ((player_y_rotation * Mathf.PI) / 180f);
		this.player_forward.y = Mathf.Cos ((player_y_rotation * Mathf.PI) / 180f);

		float camera_y_rotation = (float)cam.transform.eulerAngles.y;
		this.camera_forward.x = Mathf.Sin ((camera_y_rotation * Mathf.PI) / 180f);
		this.camera_forward.y = Mathf.Cos ((camera_y_rotation * Mathf.PI) / 180f);

		float phi_player_camera = CalculatePhi (this.player_forward, this.camera_forward);
		this.phi_forward.x = Mathf.Sin (phi_player_camera);
		this.phi_forward.y = Mathf.Cos (phi_player_camera);
	}
	void UpdateAngularInput(){
		// Calculate angular offset (i.e., angular input, but you don't know which side of the 
		// circle it is on. 


		// This line, where I normalize (sort of a double check), is why order matters
		float angular_offset = Mathf.Acos (this.forward_input / this.player_joystick.magnitude); // should be 1.0
		// remove it if necessary. 

		if (float.IsNaN (angular_offset)) {
			this.angular_input = 0f;
			return;
		}
		float sign = AngleSign (this.phi_forward, this.player_joystick);
		this.angular_input = sign * angular_offset;

	}
	void UpdateForwardInput(){
		float fin = Vector2.Dot (this.phi_forward, this.player_joystick);
		if (float.IsNaN (fin)) {
			this.forward_input = 0f;
		} else {
			this.forward_input = fin;
		}
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
