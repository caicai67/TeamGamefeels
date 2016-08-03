using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class checkInRange : RAINAction
{
	private string inputMagnitude = "inputMagnitude";
	private string angularInput = "angularInput";
	private RAIN.Navigation.Pathfinding.RAINPath current_path = null;
	private Vector3 target_waypoint;
	private Vector3 current_target_leading_position = new Vector3(0f,0f,0f);
	private Vector3 target_leading_position_upon_path_calculation = new Vector3(0f,0f,0f);
	private GameObject current_target;
	private GameObject self;
	private bool path_calculated_current_loop = false;
	private float approach_distance = 2f;
	private float recalc_proportion = 0.1f;
	private int last_waypoint_index = -1;
	private int next_waypoint_index = 0;
	private Vector3 next_waypoint_position;
	private bool waypoint_updated_current_loop = false;
	private Vector3 previous_position;

	public Vector3 sprint_velocity = new Vector3 (0.274f,0f,7.069f);

    public override void Start(RAIN.Core.AI ai)
    {
		UpdateFloat (ai, this.inputMagnitude, 0f);
		UpdateFloat (ai, this.angularInput, 0f);
		this.current_target = ai.WorkingMemory.GetItem<GameObject> ("playerCharacter");
		this.current_target_leading_position = this.current_target.transform.position;
		this.target_leading_position_upon_path_calculation = this.current_target.transform.position;
		this.self = ai.WorkingMemory.GetItem<GameObject> ("character");
        base.Start(ai);
		UpdateFloat (ai, "angularInput", 0f);
		UpdateFloat (ai, "inputMagnitude", 0f);
		this.previous_position = this.self.transform.position;
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
	{
		if (this.last_waypoint_index > this.next_waypoint_index) {
			this.current_path = null;
			this.last_waypoint_index = -1;
			this.next_waypoint_index = 0;
		}
		if (this.previous_position == this.self.transform.position && this.next_waypoint_index > 10) {
			this.current_path = null;
			this.last_waypoint_index = -1;
			this.next_waypoint_index = 0;
		}
		// calculate leading target; for now, just use current_target.transform.position
		Vector3 target_delta;
		this.current_target_leading_position = GetLeadingTarget(out target_delta);

		Vector2 shortest_distance_vector;
		GetShortestDistance (out shortest_distance_vector);

		// Calculate or Recalculate Path

		if (shortest_distance_vector.magnitude > 2f && this.current_path == null) {
			this.path_calculated_current_loop = GetPath (ai, out this.current_path);
			if (this.path_calculated_current_loop) {
				this.target_leading_position_upon_path_calculation = this.current_target_leading_position;
				this.last_waypoint_index = -1;
			}
		} else if (target_delta.magnitude > 3f) {
			this.path_calculated_current_loop = GetPath (ai, out this.current_path);
			if (this.path_calculated_current_loop) {
				this.target_leading_position_upon_path_calculation = this.current_target_leading_position;
				this.last_waypoint_index = -1;
			}
		} else if (shortest_distance_vector.magnitude <= this.approach_distance && this.current_path != null) {
			this.current_path = null;
			this.last_waypoint_index = -1;
			this.next_waypoint_index = 0;
		} 

		if (this.current_path != null) {
			if (this.last_waypoint_index == -1) {
				UpdateWaypoint ();
				this.waypoint_updated_current_loop = true;
			}
			// do steering
			Vector2 difference_vector;
			float turnAngle = AngularDisplacement (this.self.transform.position, this.self.transform.eulerAngles, this.next_waypoint_position, out difference_vector);
			float turnAngle_mag = Mathf.Abs (turnAngle);
			// angular update logic
			if (turnAngle_mag > 0.1f) {
				float adjusted_turnAngle = Mathf.Max (turnAngle_mag, 1.5f) * Mathf.Sign (turnAngle);
				UpdateFloat (ai, "angularInput", turnAngle);
			} else {
				UpdateFloat(ai,"angularInput",0f);
			}
			if (difference_vector.magnitude < 5f) {
				if (!this.waypoint_updated_current_loop) {
					UpdateWaypoint ();
					this.waypoint_updated_current_loop = true;
				}
			} else {
				if (shortest_distance_vector.magnitude < 5f) {
					float imag = 1.0f + 0.2f * (shortest_distance_vector.magnitude - 5f);
					UpdateFloat (ai, "inputMagnitude", imag);
				} else {
					UpdateFloat (ai, "inputMagnitude", 1.0f);
				}
			}
		}

		// End code
		this.path_calculated_current_loop = false;
		this.waypoint_updated_current_loop = false;
        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
	void UpdateWaypoint(){
		this.last_waypoint_index += 1;
		this.next_waypoint_index = this.current_path.GetNextWaypoint (this.self.transform.position, 0.5f, this.last_waypoint_index);
		this.next_waypoint_position = this.current_path.GetWaypointPosition (this.next_waypoint_index);
	}
	void XZ_Distance(Vector3 positionA,Vector3 positionB,out Vector2 distance){
		distance.x = positionB.x - positionA.x;
		distance.y = positionB.z - positionA.z;
	}
	float AngularDisplacement(Vector3 position,Vector3 orientation, Vector3 destination,out Vector2 difference_vector){
		float theta_y = Mathf.Deg2Rad * orientation.y;
		Vector2 start = new Vector2 (position.z, position.x);
		Vector2 end = new Vector2 (destination.z, destination.x);

		difference_vector.x = end.x - start.x;
		difference_vector.y = end.y - start.y;
		Vector2 face_unit_vector = new Vector2 (Mathf.Cos(theta_y), Mathf.Sin (theta_y));

		float phi_prime = Mathf.Acos ((face_unit_vector.x * difference_vector.x + face_unit_vector.y * difference_vector.y)/difference_vector.magnitude);
		return -1*phi_prime * Mathf.Sign (face_unit_vector.x * difference_vector.y - face_unit_vector.y * difference_vector.x);
	}
	void GetShortestDistance(out Vector2 crow_vector){
		crow_vector.x = this.current_target.transform.position.x - this.self.transform.position.x;
		crow_vector.y = this.current_target.transform.position.z - this.self.transform.position.z;
	}
	Vector3 GetLeadingTarget(out Vector3 target_delta){
		Vector3 last_target = this.target_leading_position_upon_path_calculation;


		// placeholder; actually calculate leading target next
		Rigidbody target_rigidbody = this.current_target.GetComponent<Rigidbody>();
		Vector3 target_velocity = target_rigidbody.velocity;


		Vector3 current_target_position = this.current_target.transform.position;
		Vector3 current_self_position = this.self.transform.position;

		Vector2 current_difference = new Vector2 ();

		current_difference.x = current_target_position.x - current_self_position.x;
		current_difference.y = current_target_position.y - current_self_position.y;


		float current_difference_length = current_difference.magnitude;
		float estimated_time_to_arrival = current_difference_length / this.sprint_velocity.magnitude;

		target_velocity.x *= estimated_time_to_arrival;
		target_velocity.y *= estimated_time_to_arrival;
		target_velocity.z *= estimated_time_to_arrival;

		Vector3 leading_position = new Vector3 ();
		leading_position.x = current_target_position.x + target_velocity.x;
		leading_position.y = current_target_position.y + target_velocity.y;
		leading_position.z = current_target_position.z + target_velocity.z;

		target_delta.x = leading_position.x - last_target.x;
		target_delta.y = leading_position.y - last_target.y;
		target_delta.z = leading_position.z - last_target.z;




		target_delta.x = 0f;
		target_delta.y = 0f;
		target_delta.z = 0f;
		return current_target_position;
		//return leading_position;
	}
	void UpdateFloat(AI ai,string var_name,float value){
		ai.WorkingMemory.SetItem (var_name, value);
	}
	float GetFloat(AI ai, string var_name){
		return (float)ai.WorkingMemory.GetItem (var_name);
	}
	bool GetPath(AI ai, out RAIN.Navigation.Pathfinding.RAINPath path){
		this.target_waypoint = ai.Navigator.ClosestPointOnGraph (this.current_target_leading_position, 10f);
		return ai.Navigator.GetPathTo (this.current_target_leading_position, int.MaxValue, float.MaxValue, true, out path);
	}
}