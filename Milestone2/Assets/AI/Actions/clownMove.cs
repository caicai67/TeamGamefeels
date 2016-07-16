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
		//glowing light to show where spell is being cast
		Light spellGlow = AI.Body.transform.FindChild("spellCast").GetComponent<Light>();

		//check to see if clown can see player
		if (AI.WorkingMemory.GetItem<GameObject>("detectedPlayer") != null) {
			
			//find point behind player
			GameObject character = AI.WorkingMemory.GetItem<GameObject> ("detectedPlayer");
			AI.WorkingMemory.SetItem ("targetSpot", character.transform.position - (5 * character.transform.forward));

			//check if clown is close enough to affect player
			bool inRange = Vector3.Magnitude (AI.Body.transform.position - character.transform.position) < 8f;
			AI.WorkingMemory.SetItem ("playerInRange", inRange);

			if (inRange) {
				//turn on glow and place it on player
				spellGlow.gameObject.transform.position = character.transform.position;
				spellGlow.intensity = 1;
			} else {
				//turn off glow
				spellGlow.intensity = 0;
			}

		} else {
			AI.WorkingMemory.SetItem ("playerInRange", false);
			//turn off glow
			spellGlow.intensity = 0;
		}
	}
}
