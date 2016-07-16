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
		//IList<RAINAspect> asps = AI.Senses.SenseAll ();
		Animator anim = AI.Body.GetComponent<Animator> ();
		//foreach (RAINAspect detected in asps) {
			
		GameObject character = AI.WorkingMemory.GetItem("detectedPlayer");
			if (character != null) {
				AI.WorkingMemory.SetItem ("targetSpot", character.transform.position - (5 * character.transform.forward));
				AI.WorkingMemory.SetItem ("playerInRange", Vector3.Magnitude (AI.Body.transform.position - character.transform.position) < 5f);
			} else {
				AI.WorkingMemory.SetItem ("playerInRange", false);
			}

		//}
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
