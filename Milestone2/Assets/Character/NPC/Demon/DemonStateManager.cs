using UnityEngine;
using System.Collections;

public class DemonStateManager : MonoBehaviour {
	public Animator animator;
	private bool spell_casted = false;
	private float cooldown = 10f;
	private float cooldown_timer = 0f;
	private bool spell_state = false;
	public GameObject dreyar;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		bool state = animator.GetCurrentAnimatorStateInfo (0).IsName ("Spell");
		if (state && this.cooldown_timer < 5000f) {
			if (this.animator.GetBool ("CanCast"))
				this.animator.SetBool ("CanCast", false);
			this.cooldown_timer += Time.deltaTime;
		} else if (this.cooldown_timer < 5000f) {
			this.cooldown_timer += Time.deltaTime;
		} else {
			this.cooldown_timer = 0f;
			if (!this.animator.GetBool ("CanCast"))
				this.animator.SetBool ("CanCast", true);
		}
	}
	void FixedUpdate(){
		bool original_spell_state = this.spell_state;
		this.spell_state = animator.GetCurrentAnimatorStateInfo (0).IsName ("Spell");
		if (!original_spell_state && (original_spell_state != spell_state)) { //spell just started!

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

			transform.Rotate (new Vector3 (0f, theta + 80f, 0f));
		}
	}

	void GetDifferenceVector(Vector3 dreyar_position,Vector3 demon_position,out Vector3 difference_vector){
		difference_vector.x = dreyar_position.x - demon_position.x;
		difference_vector.y = dreyar_position.y - demon_position.y;
		difference_vector.z = dreyar_position.z - demon_position.z;
	}
	void GetTheta(Vector3 demon_direction,Vector3 difference_vector, out float theta){
		float dot_product = 0f;
		GetDotProduct(demon_direction,difference_vector,out dot_product);

		float theta_prime = Mathf.Acos (dot_product) * Mathf.Rad2Deg;
		theta = Mathf.Sign (demon_direction.x * difference_vector.y - difference_vector.x * demon_direction.y) * theta_prime;
	}
	void GetDotProduct(Vector3 demon_direction,Vector3 difference_vector, out float dot_product){
		dot_product = 0f;
		dot_product += demon_direction.x * difference_vector.x;
		dot_product += demon_direction.y * difference_vector.y;
		dot_product += demon_direction.z * difference_vector.z;
	}
}
