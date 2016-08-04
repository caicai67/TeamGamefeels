using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;


[RAINAction]
public class VampireMove : RAINAction
{
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
	private int last_waypoint_index = -1;
	private int next_waypoint_index = 0;
	private int current_target_index;
	private Vector3 next_waypoint_position;
	private Vector3 current_position;
	private Vector3 current_orientation;
	private Vector3 target_waypoint;
	// first waypoint guard
	private bool first_waypoint = false;
	private bool initialized = false;

	public override void Start(RAIN.Core.AI ai)
	{
		this.character = ai.WorkingMemory.GetItem<GameObject> ("character");
		this.navTarget1 = ai.WorkingMemory.GetItem<GameObject>("navTarget1").transform;
		this.navTarget2 = ai.WorkingMemory.GetItem<GameObject>("navTarget2").transform;
		this.navTarget3 = ai.WorkingMemory.GetItem<GameObject>("navTarget3").transform;
		this.navTarget4 = ai.WorkingMemory.GetItem<GameObject>("navTarget4").transform;

		this.navTargetList = new Transform[]{ navTarget1, navTarget2, navTarget3,navTarget4 };
		if (!initialized) {
			this.current_target_index = 0;
			this.current_target = this.navTargetList [this.current_target_index];
			this.initialized = true;
		}
		base.Start(ai);
	}

	bool GetNextPath(AI ai){
		this.current_target = GetActiveTarget();
		this.target_waypoint = ai.Navigator.ClosestPointOnGraph(this.current_target.position, 10f);
		return ai.Navigator.GetPathTo(this.current_target.position,int.MaxValue,float.MaxValue,true,out this.current_path);
	}
	void UpdateWaypoint(){
		this.last_waypoint_index += 1;
		this.next_waypoint_index = this.current_path.GetNextWaypoint (this.character.transform.position, 0.5f, this.last_waypoint_index);
		this.next_waypoint_position = this.current_path.GetWaypointPosition (this.next_waypoint_index);
	}
	public override ActionResult Execute(RAIN.Core.AI ai)
	{
		if (!first_waypoint) {
			// I'll have to add logic to nullify the current path later.
			bool path_found = GetNextPath (ai);
			UpdateWaypoint ();
			first_waypoint = true;
		}

		this.current_position = this.character.transform.position;
		this.current_orientation = this.character.transform.eulerAngles;
		Vector2 difference_vector;
		float turnAngle = AngularDisplacement_VerticalAxis (this.current_position, this.current_orientation, this.next_waypoint_position, out difference_vector);
		float turnAngleMagnitude = Mathf.Abs (turnAngle);

		if (turnAngleMagnitude > 0.1f && difference_vector.magnitude > 2f) {
			UpdateFloat (ai, "angularInput", turnAngle);
		} else {
			UpdateFloat(ai,"angularInput",0f);
		}

		if (difference_vector.magnitude > 5f) {
			UpdateFloat (ai, "inputMagnitude", 1f);
		} else {
			UpdateFloat (ai, "inputMagnitude", difference_vector.magnitude / 3f);
			// check if at destination
			Vector2 distance;
			XZ_Distance (this.current_position, this.target_waypoint,out distance);
			if (distance.magnitude < 5f) {
				// If at destination, choose next target and calculate path
				UpdateFloat (ai, "inputMagnitude", 0f);
				bool path_found = GetNextPath (ai);
				this.last_waypoint_index = -1;
				UpdateWaypoint ();
			} else {
				// Choose Next Waypoint
				UpdateWaypoint();
			}

		}
		return ActionResult.SUCCESS;
	}
	public override void Stop(RAIN.Core.AI ai)
	{
		base.Stop(ai);
	}

	void XZ_Distance(Vector3 positionA,Vector3 positionB,out Vector2 distance){
		distance.x = positionB.x - positionA.x;
		distance.y = positionB.z - positionA.z;
	}
	float AngularDisplacement_VerticalAxis(Vector3 position,Vector3 orientation, Vector3 destination,out Vector2 difference_vector){
		float theta_y = Mathf.Deg2Rad * orientation.y;
		Vector2 start = new Vector2 (position.z, position.x);
		Vector2 end = new Vector2 (destination.z, destination.x);

		difference_vector.x = end.x - start.x;
		difference_vector.y = end.y - start.y;
		Vector2 face_unit_vector = new Vector2 (Mathf.Cos(theta_y), Mathf.Sin (theta_y));

		float phi_prime = Mathf.Acos ((face_unit_vector.x * difference_vector.x + face_unit_vector.y * difference_vector.y)/difference_vector.magnitude);
		return -1*phi_prime * Mathf.Sign (face_unit_vector.x * difference_vector.y - face_unit_vector.y * difference_vector.x);
	}

	void UpdateFloat(AI ai,string var_name,float value){
		ai.WorkingMemory.SetItem (var_name, value);
	}
	float GetFloat(AI ai, string var_name){
		return (float)ai.WorkingMemory.GetItem (var_name);
	}
	Transform GetActiveTarget(){
		if (this.current_target_index >= this.navTargetList.Length - 1) {
			this.current_target_index = 0;
		} else {
			this.current_target_index += 1;
		}
		return this.navTargetList[this.current_target_index];
	}
}