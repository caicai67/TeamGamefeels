using UnityEngine;
using System.Collections;

public class CharacterHealth : MonoBehaviour {

	private Animator animator;
	private Rigidbody rigid_body;
	private CharacterController controller;
	public AudioClip death_gurgle = null;
	private AudioSource audio;
	private bool died = false;
	public int health = 100;
	void Awake(){
		this.audio = GetComponent<AudioSource> ();
		this.animator = GetComponent<Animator> ();
		this.rigid_body = GetComponent<Rigidbody> ();
		this.controller = GetComponent<CharacterController> ();
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (this.health <= 0 && !this.died) {
			Die ();
			this.died = true;
		}

	}
	public void Die(){
		if (this.death_gurgle != null) {
			this.audio.clip = this.death_gurgle;
			this.audio.Play ();
		}
		this.rigid_body.isKinematic = true;
		makeRagdollSolid ();
		this.animator.enabled = false;
		this.controller.enabled = false;
	}
	public void SlashDamage(){
		this.health -= 100;
	}

	void makeRagdollSolid() {
		foreach (CapsuleCollider capsule_collider in GetComponentsInChildren<CapsuleCollider>()) {
			capsule_collider.isTrigger = false;
		}
	}

}
