using UnityEngine;
using System.Collections;

public class PlayerLeadingPosition : MonoBehaviour {
	public GameObject character;
	public Rigidbody character_rigidbody;
	public GameObject demon;
	public Vector3 sprint_velocity = new Vector3 (0.274f,0f,7.069f);

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 leading_position;
		GetLeadingPosition (out leading_position);
		transform.position = leading_position;
	}
	void GetLeadingPosition(out Vector3 leading_position){
		Vector3 demon_position = this.demon.transform.position;
		Vector3 character_position = this.character.transform.position;
		Vector3 character_velocity = this.character_rigidbody.velocity;

		Vector3 positional_difference = demon_position - character_position;

		float sprint_velocity_mag = sprint_velocity.magnitude;
		float positional_difference_mag = positional_difference.magnitude;


		float estimated_deltaT = positional_difference_mag/ (sprint_velocity_mag * 0.6f);

		leading_position.x = character_position.x + character_velocity.x * estimated_deltaT;
		leading_position.y = character_position.y + character_velocity.y * estimated_deltaT;
		leading_position.z = character_position.z + character_velocity.z * estimated_deltaT;
	}
}