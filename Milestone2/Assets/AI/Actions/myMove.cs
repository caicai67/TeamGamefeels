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

	private GameObject character = null;

	private Transform navTarget1 = null;
	private Transform navTarget2 = null;
	private Transform navTarget3 = null;
	private Transform navTarget4 = null;

	private Transform[] navTargetList;
	private bool[] activeTarget = new bool[]{false,false,false,false};

	private Transform current_target;
	private RAIN.Navigation.Pathfinding.RAINPath current_path = null;


	// waypoint navigation
	private int last_waypoint_index = 0;
	private int next_waypoint_index = 0;
	private Vector3 next_waypoint_position;
	private Vector3 current_position;
	private Vector3 current_orientation;
	// first waypoint guard
	private bool first_waypoint = false;

    public override void Start(RAIN.Core.AI ai)
    {
		this.character = ai.WorkingMemory.GetItem<GameObject> ("character");
		this.navTarget1 = ai.WorkingMemory.GetItem<GameObject>("navTarget1").transform;
		this.navTarget2 = ai.WorkingMemory.GetItem<GameObject>("navTarget2").transform;
		this.navTarget3 = ai.WorkingMemory.GetItem<GameObject>("navTarget3").transform;
		this.navTarget4 = ai.WorkingMemory.GetItem<GameObject>("navTarget4").transform;

		this.navTargetList = new Transform[]{ navTarget1, navTarget2, navTarget3,navTarget4 };
		UpdateActiveTarget ();
		this.current_target = GetActiveTarget ();
		base.Start(ai);
    }
    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		if (!first_waypoint) {
			// I'll have to add logic to nullify the current path later.
			if (this.current_path == null) {
				Vector3 target_waypoint = ai.Navigator.ClosestPointOnGraph (this.current_target.position, 10f);
				bool path_found = ai.Navigator.GetPathTo (target_waypoint, int.MaxValue, float.MaxValue, true, out this.current_path);
			}
			this.next_waypoint_index = this.current_path.GetNextWaypoint (this.character.transform.position, 0.5f, this.last_waypoint_index);

			this.next_waypoint_position = this.current_path.GetWaypointPosition (this.next_waypoint_index);

			first_waypoint = true;
		}
		this.current_position = this.character.transform.position;
		this.current_orientation = this.character.transform.eulerAngles;





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
	Transform GetActiveTarget(){
		for (int i = 0; i < this.activeTarget.Length; i++){
			if (this.activeTarget[i]){
				return this.navTargetList[i];	
			}
		}
		return null;
	}
	void UpdateActiveTarget(){
		bool targetsExist = false;
		bool moreThanOne = false;
		int first_target = -1;
		int current_target = -1;
		for (int i = 0; i < this.activeTarget.Length; i++) {
			if (!(this.navTargetList [i] == null)) {
				if (targetsExist) {
					moreThanOne = true;
				} else{
					targetsExist = true;
					first_target = i;
				}
				if (this.activeTarget [i]) {
					current_target = i;
				}
			}
		}

		if (!targetsExist) {
			for (int i = 0; i < this.activeTarget.Length; i++) {
				this.activeTarget [i] = false;
			}
			return;
		}

		if (!moreThanOne){
			for (int i = 0; i < this.activeTarget.Length; i++) {
				if (i == first_target) {
					this.activeTarget [i] = true;
				} else {
					this.activeTarget [i] = false;
				}
			}
			return;
		}
		if (current_target == -1) {
			for (int i = 0; i < this.activeTarget.Length; i++) {
				if (this.navTargetList [i] != null) {
					this.activeTarget [i] = true;
					return;
				} else {
					throw new System.ArgumentException ("no non-null target in navigation target list");
				}
			}
		} else {
			for (int i = current_target; i < this.activeTarget.Length; i++) {
				if (i > current_target && this.navTargetList [i] != null) {
					this.activeTarget [i] = true;
					this.activeTarget [current_target] = false;
					return;
				}
			}
			for (int i = 0; i < current_target; i++) {
				if (this.navTargetList [i] != null) {
					this.activeTarget [i] = true;
					this.activeTarget [current_target] = false;
					return;
				}
			}

		}
		throw new System.Exception ("this part of the code should not be reached.");
		return;
	}

	
}