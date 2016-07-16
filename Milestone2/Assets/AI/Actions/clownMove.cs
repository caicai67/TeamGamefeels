using UnityEngine;
using System.Collections;
using RAIN.Serialization;
using RAIN.Core;
using RAIN.Entities.Aspects;
using System.Collections.Generic;

[RAINSerializableClass]
public class clownMove : CustomAIElement {
	
	public override void Act() 
	{
		IList<RAINAspect> asps = AI.Senses.SenseAll ();

		//Debug.Log ("white clown sensed objects: " + asps.Count);
		Animator anim = AI.Body.GetComponent<Animator> ();
		if (asps.Count > 0) {
			anim.SetBool ("slow", true);
		} else {
			anim.SetBool ("slow", false);
		}
		foreach (RAINAspect detected in asps) {
			GameObject character = detected.Entity.Form;
			Debug.Log (character);
			if (character.CompareTag("Player")) {
				Debug.Log ("dreyar Forward: " + character.transform.forward);
				target = character.transform.position - (5 * character.transform.forward);
			}

		}
		//:) step 1 - get sensory input
		//:) 2 - change mecanim param if something in sensor
		/*check the sensors
		if(sees player) { //move behind
			find furthest angle to back of player he can reach that is x units away
			teleport to that point
		} else { //move randomly
			pick a point at max distance away from self and move there
		*/
	}
}
