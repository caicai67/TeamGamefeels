using UnityEngine;
using System.Collections;
// TeamGameFeels
public class DemonStateManager : MonoBehaviour {
	public Animator animator;
	private bool spell_casted = false;
	private float cooldown = 10f;
	private float cooldown_timer = 0f;
	private bool spell_isCasting = false;
	public GameObject dreyar;
	private PlayerController player_controller;
	private float spell_casting_timer = 0f;
	public GameObject demon_ai_unity_object;
	private RAIN.Core.AI demon_ai;
	private RAIN.Core.AIRig demon_ai_rig;

	private float player_distance = Mathf.Infinity;
	// Use this forinitialization
	void Start () {

		this.demon_ai_rig = demon_ai_unity_object.GetComponent<RAIN.Core.AIRig> ();
		this.player_controller = dreyar.GetComponent<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {


		// Hack to prevent the demon from smacking the player from across the map. 
		// realistically, demon needs to be refactored.
		Vector3 player_position = this.player_controller.transform.position;
		Vector3 demon_position = this.transform.position;
		this.player_distance = Vector3.Distance (player_position, demon_position);



		bool state = animator.GetCurrentAnimatorStateInfo (0).IsName ("Spell");
		if (state && this.cooldown_timer < 10f) {
			if (this.animator.GetBool ("CanCast")) {
				this.animator.SetBool ("CanCast", false);
				this.demon_ai_rig.AI.WorkingMemory.SetItem<bool> ("canCast", false);
			}
			this.cooldown_timer += Time.deltaTime;
		} else if (this.cooldown_timer < 10f) {
			this.cooldown_timer += Time.deltaTime;
		} else {
			this.cooldown_timer = 0f;
			if (!this.animator.GetBool ("CanCast")){
				this.animator.SetBool ("CanCast", true);
				this.demon_ai_rig.AI.WorkingMemory.SetItem<bool> ("canCast", true);
			}
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
			if (this.player_distance < 30f) {
				this.player_controller.demon_spell_hit = true;
			}
			this.spell_casting_timer += 5f;
		}

		if (!original_spell_state && (original_spell_state != spell_isCasting)) { //spell just started!
			transform.Rotate (new Vector3 (0f,85f, 0f)); 
		}
	}
}
