using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class demonUpdateSpeed : RAINAction
{
	private GameObject player;
	private Rigidbody player_rigidbody;
    public override void Start(RAIN.Core.AI ai)
    {
		this.player = ai.WorkingMemory.GetItem<GameObject> ("playerCharacter");
		this.player_rigidbody = this.player.GetComponent<Rigidbody> ();
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		
		float player_speed = this.player_rigidbody.velocity.magnitude;
		
		ai.WorkingMemory.SetItem<float>("playerSpeed",player_speed);

        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}