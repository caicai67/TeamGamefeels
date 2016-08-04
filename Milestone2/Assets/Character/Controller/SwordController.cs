using UnityEngine;
using System.Collections;

public class SwordController : MonoBehaviour {
	public GameObject host_character;
	private Animator animator;
	private AudioSource audio;
	private Light light_saber;
	private bool slashed = false;
	private bool light_flash = false;
	private float light_flash_timer = 0f;
	// Use this for initialization
	void Awake(){
		this.audio = GetComponent<AudioSource> ();
		this.light_saber = GetComponent<Light> ();
		this.animator = host_character.GetComponent<Animator> ();
	}
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!this.animator.GetCurrentAnimatorStateInfo (0).IsName ("Slash")) {
			this.slashed = false;
		}

		if (this.light_flash && this.light_flash_timer < 0.1f) {
			this.light_saber.intensity = 8f;
			this.light_saber.range = 100f;
			this.light_saber.bounceIntensity = 8f;
			this.light_flash_timer += Time.deltaTime;
		} else {
			this.light_flash = false;
			this.light_saber.intensity = 1f;
			this.light_saber.range = 10f;
			this.light_saber.bounceIntensity = 1f;
			this.light_flash_timer = 0f;
		}
	
	}
	void OnTriggerEnter(Collider other){
		GameObject other_object = other.gameObject;
		if (!(other_object == host_character) && other_object.layer == 9 && this.animator.GetCurrentAnimatorStateInfo(0).IsName("Slash") && other.GetType() == typeof(CharacterController)) {
			CharacterHealth damage_script = other_object.GetComponent<CharacterHealth>();
			if (!slashed) {
				this.light_flash = true;
				damage_script.SlashDamage ();
				this.slashed = true;
			}
		}
	}
}
