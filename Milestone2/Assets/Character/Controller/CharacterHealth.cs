using UnityEngine;
using System.Collections;

public class CharacterHealth : MonoBehaviour {

	private Animator animator;
	private Rigidbody rigid_body;
	private CharacterController controller;
	public int health = 100;
	void Awake(){
		this.animator = GetComponent<Animator> ();
		this.rigid_body = GetComponent<Rigidbody> ();
		this.controller = GetComponent<CharacterController> ();
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (this.health <= 0) {
			Die ();
		}

	}
	public void Die(){
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
