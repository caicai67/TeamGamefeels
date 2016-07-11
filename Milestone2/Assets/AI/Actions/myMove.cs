using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class myMove : RAINAction
{
	private float inputMagnitude = 0f;
	private float angularInput = 0f;

	public NavMesh navMesh;
	public RAIN.Navigation.Targets.NavigationTarget navTarget_1 = null;
	public RAIN.Navigation.Targets.NavigationTarget navTarget_2 = null;
	public RAIN.Navigation.Targets.NavigationTarget navTarget_3 = null;

	private RAIN.Navigation.Targets.NavigationTarget[] navTargetList;
	private bool[] activeTarget = new bool[]{false,false,false};

    public override void Start(RAIN.Core.AI ai)
    {
		this.navTargetList = new RAIN.Navigation.Targets.NavigationTarget[]{ navTarget_1, navTarget_2, navTarget_3 };
		UpdateActiveTarget ();
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		

		//ai.Motor.MoveTarget(
		UpdateFloat(ai,"inputMagnitude",this.inputMagnitude);
		UpdateFloat (ai, "angularInput", this.angularInput);
        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }

	void UpdateFloat(AI ai,string var_name,float value){
		ai.WorkingMemory.SetItem (var_name, value);
	}
	float GetFloat(AI ai, string var_name){
		return (float)ai.WorkingMemory.GetItem (var_name);
	}
	void UpdateActiveTarget(){
		bool targetsExist = false;
		bool moreThanOne = false;
		int first_target = -1;
		for (int i = 0; i < this.activeTarget.Length; i++) {
			if (!(this.navTargetList [i] == null)) {
				if (targetsExist) {
					moreThanOne = true;
				} else{
					targetsExist = true;
					first_target = i;
				}
			}
		}

		//if !moreThanOne{
		//}

	}

	
}