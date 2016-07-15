using UnityEngine;
using System.Collections;

public class DemonStateManager : MonoBehaviour {
	public Animator animator;
	private bool spell_casted = false;
	private float cooldown = 10f;
	private float cooldown_timer = 0f;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (!animator.GetCurrentAnimatorStateInfo (0).IsName ("Spell") && this.spell_casted) {
			if (this.cooldown_timer >= 10f) {
				this.spell_casted = false;
				this.animator.ResetTrigger ("CastSpell");
				this.cooldown_timer = 0f;
			} else {
				this.cooldown_timer += Time.deltaTime;
			}
		}
	}
}
