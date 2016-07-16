using UnityEngine;
using System.Collections;

public class DemonStateManager : MonoBehaviour {
	public Animator animator;
	private bool spell_casted = false;
	private float cooldown = 10f;
	private float cooldown_timer = 0f;
	private bool spell_isCasting = false;
	public GameObject dreyar;
	private PlayerController player_controller;
	private float spell_casting_timer = 0f;
	// Use this for initialization
	void Start () {
		this.player_controller = dreyar.GetComponent<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
		bool state = animator.GetCurrentAnimatorStateInfo (0).IsName ("Spell");
		if (state && this.cooldown_timer < 10f) {
			if (this.animator.GetBool ("CanCast"))
				this.animator.SetBool ("CanCast", false);
			this.cooldown_timer += Time.deltaTime;
		} else if (this.cooldown_timer < 10f) {
			this.cooldown_timer += Time.deltaTime;
		} else {
			this.cooldown_timer = 0f;
			if (!this.animator.GetBool ("CanCast"))
				this.animator.SetBool ("CanCast", true);
		}
	}
	void FixedUpdate(){
		bool original_spell_state = this.spell_isCasting;
		this.spell_isCasting = animator.GetCurrentAnimatorStateInfo (0).IsName ("Spell");

		bool spell_started = !original_spell_state && (original_spell_state != spell_isCasting);
		bool spell_ended = original_spell_state && (original_spell_state != spell_isCasting);


		if (spell_isCasting) {
			this.spell_casting_timer += Time.deltaTime;
		} else {
			this.spell_casting_timer = 0f;
		}

		// Hit "calculation" (can be replaced later I guess)
		if (spell_isCasting && this.spell_casting_timer >= 1.45f && this.spell_casting_timer < 2f) {
			this.player_controller.demon_spell_hit = true;
			this.spell_casting_timer += 5f;
		}

		if (!original_spell_state && (original_spell_state != spell_isCasting)) { //spell just started!

			// dreyar_position
			Vector3 dreyar_position = dreyar.transform.position;

			// vector between demon and dreyar
			Vector3 difference_vector;
			GetDifferenceVector (dreyar_position,transform.position,out difference_vector);
			difference_vector.Normalize ();

			// demon 2D direction vector
			float demon_orientation_Y = transform.eulerAngles.y * Mathf.Deg2Rad;
			Vector3 demon_direction = new Vector3 (Mathf.Cos (demon_orientation_Y), 0f, Mathf.Sin (demon_orientation_Y));
			// make sure everything is what it is supposed to be
			demon_direction.Normalize ();

			// calculate theta
			float theta;
			GetTheta (demon_direction, difference_vector, out theta);

			transform.Rotate (new Vector3 (0f, 90f, 0f)); // theta - 50f, 0f));
		}
	}

	void GetDifferenceVector(Vector3 dreyar_position,Vector3 demon_position,out Vector3 difference_vector){
		difference_vector.x = dreyar_position.x - demon_position.x;
		difference_vector.y = 0f;
		difference_vector.z = dreyar_position.z - demon_position.z;
	}
	void GetTheta(Vector3 demon_direction,Vector3 difference_vector, out float theta){
		float dot_product = 0f;
		GetDotProduct(demon_direction,difference_vector,out dot_product);

		float theta_prime = Mathf.Acos (dot_product) * Mathf.Rad2Deg;
		float determinant = demon_direction.z * difference_vector.x - difference_vector.z * demon_direction.x;
		theta = -1* Mathf.Sign (determinant) * theta_prime;
	}
	void GetDotProduct(Vector3 demon_direction,Vector3 difference_vector, out float dot_product){
		dot_product = 0f;
		dot_product += demon_direction.x * difference_vector.x;
		dot_product += demon_direction.y * difference_vector.y;
		dot_product += demon_direction.z * difference_vector.z;
	}
}
