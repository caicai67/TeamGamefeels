﻿using UnityEngine;
using System.Collections;

// Team GameFeels
// Chris, Ambrose, KP, Justin, Caitlin, Charlie

[RequireComponent(typeof(SphereCollider))]
public class PlayerFootsteps : MonoBehaviour {
	public AudioClip metal_step;
	public AudioClip sneak_metal_step = null;
	public AudioClip concrete_step;
	public AudioClip sneak_concrete_step= null;
	public AudioClip dirt_step;
	public AudioClip sneak_dirt_step= null;
    public AudioClip wood_step;
	public AudioClip sneak_wood_step= null;
	public Rigidbody player;
	public AudioSource audio;
	public float time_offset = 0.1f;
	public PlayerFootsteps other_foot;
	public bool front_foot = true;
	public bool step_triggered = false;
	public bool sneak = false;
	private float offset_timer = 0f;
	// Use this for initialization
	void Start () {
		this.audio.volume = 0.2f;
	}
	
	// Update is called once per frame
	void Update () {
		if (other_foot.step_triggered && this.offset_timer > this.time_offset) {
			this.step_triggered = false;
			this.offset_timer = 0f;
		} else {
			this.offset_timer += Time.deltaTime;
		}
	}

	void OnTriggerEnter(Collider floor){
		if (player.velocity.magnitude > 0.4f) {
			if (floor.gameObject.layer == 10 && !this.step_triggered) {
				if (!this.sneak) {
					audio.clip = this.metal_step;
				} else {
					audio.clip = this.sneak_metal_step;
				}
				this.offset_timer = 0f;
				if (audio.clip != null) {
					audio.Play ();
				}
				this.step_triggered = true;
			} else if (floor.gameObject.layer == 11 && !this.step_triggered) {
				if (!this.sneak) {
					audio.clip = this.concrete_step;
				} else {
					audio.clip = this.sneak_concrete_step;
				}
				this.offset_timer = 0f;
				if (audio.clip != null) {
					audio.Play ();
				}
				this.step_triggered = true;
			} else if (floor.gameObject.layer == 13 && !this.step_triggered) {
				if (!this.sneak) {
					audio.clip = this.dirt_step;
				} else {
					audio.clip = this.sneak_dirt_step;
				}
				this.offset_timer = 0f;
				if (audio.clip != null) {
					audio.Play ();
				}
				this.step_triggered = true;
			} else if (floor.gameObject.layer == 15 && !this.step_triggered) {
				if (!this.sneak) {
					audio.clip = this.wood_step;
				} else {
					audio.clip = this.sneak_wood_step;
				}
				this.offset_timer = 0f;
				if (audio.clip != null) {
					audio.Play ();
				}
                this.step_triggered = true;
            }
    }
	}
}
