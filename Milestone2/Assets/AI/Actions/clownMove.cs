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
		Debug.Log (asps.Count);
		//step 1 - get sensory input and change mecanim param if something in sensor
		/*check the sensors
		if(sees player) { //move behind
			find furthest angle to back of player he can reach that is x units away
			teleport to that point
		} else { //move randomly
			pick a point at max distance away from self and move there
		*/
	}
}
