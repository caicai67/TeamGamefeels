using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]

public class CombatSounds : MonoBehaviour {

	public PlayerController player_controller = null;

	void Start() {
		if (this.player_controller == null) {
		
			this.player_controller = GetComponent<PlayerController> ();
		}
	}

	void Update(){

		if (player_controller.swingedSword) {
			player_controller.swingedSword = false;

			GetComponent<AudioSource> ().PlayDelayed (0.55f);
		}
	}
}