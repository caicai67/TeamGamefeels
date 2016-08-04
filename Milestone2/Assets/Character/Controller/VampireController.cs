using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class VampireController : MonoBehaviour {
	public GameObject player_character;
	private Animator anim;
	public PlayerController dreyar_controller;
    // Audio
    public AudioSource audio;
    public AudioClip die;
    //public AudioClip breathing;
    public AudioClip kickGrunt;
	private bool start_attack = false;
	private float roundhouse_timer = 0f;
	private bool damage_triggered = false;
    // Use this for initialization
    void Start () {
		this.anim = this.GetComponent<Animator> ();
        this.audio = this.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 self = this.transform.position;
		Vector3 dreyar = this.player_character.transform.position;

		float distance = Vector3.Distance (self, dreyar);
		this.anim.SetFloat ("DreyarDistance", distance);
		if (this.anim.GetCurrentAnimatorStateInfo (0).IsName ("FastPursuitMode") && !(this.anim.IsInTransition(0))) {
			this.anim.ResetTrigger("Attack");
			this.roundhouse_timer = 0f;
			this.damage_triggered = false;
		}

		bool in_transition = this.anim.IsInTransition(0);
		bool is_roundhouse = this.anim.GetCurrentAnimatorStateInfo (0).IsName ("roundhouse_kick");

		if (this.start_attack && !(in_transition) && !(is_roundhouse)) {
			this.anim.SetTrigger ("Attack");
        }
		if (this.anim.GetCurrentAnimatorStateInfo (0).IsName ("roundhouse_kick")) {
			this.roundhouse_timer += Time.deltaTime;
		}
		if (this.roundhouse_timer > 0.9f && !this.damage_triggered) {
			this.dreyar_controller.takeVampireKickDamage ();
			this.damage_triggered = true;
			this.start_attack = false;
		}
	}

	void OnTriggerEnter(Collider other){
		GameObject game_object = other.gameObject;
		if (game_object.name == "Dreyar") {
			this.start_attack = true;
		}
	}
    void VampireKickGrunt()
    {
        this.audio.clip = this.kickGrunt;
        this.audio.Play();
    }
}
