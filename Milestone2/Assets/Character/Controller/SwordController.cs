using UnityEngine;
using System.Collections;

public class SwordController : MonoBehaviour {
	public GameObject host_character;
	private Animator animator;
	private bool slashed = false;
	// Use this for initialization
	void Awake(){
		this.animator = host_character.GetComponent<Animator> ();
	}
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!this.animator.GetCurrentAnimatorStateInfo (0).IsName ("Slash")) {
			this.slashed = false;
		}
	
	}
	void OnTriggerEnter(Collider other){
		GameObject other_object = other.gameObject;
		if (!(other_object == host_character) && other_object.layer == 9 && this.animator.GetCurrentAnimatorStateInfo(0).IsName("Slash") && other.GetType() == typeof(CharacterController)) {
			CharacterHealth damage_script = other_object.GetComponent<CharacterHealth>();
			if (!slashed) {
				damage_script.SlashDamage ();
				this.slashed = true;
			}
		}
	}
}
