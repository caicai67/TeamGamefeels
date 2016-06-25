using UnityEngine;
using System.Collections;



// Team GameFeels
// Chris, Ambrose, KP, Justin, Caitlin, Charlie

public class SphereRoll : MonoBehaviour {
	private Rigidbody sphere_rigid;
	private SphereCollider sphere;
	public float target_speed = 15f;
	public float torque_magnitude = 50000f;
	public float force_magnitude = 50000f;
	// Use this for initialization
	void Start () {
		this.sphere_rigid = GetComponent<Rigidbody> ();
		this.sphere = GetComponent<SphereCollider> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void FixedUpdate(){
		if (this.sphere_rigid.velocity.magnitude < this.target_speed && this.sphere_rigid.position.y < 20f) {
			this.sphere_rigid.AddRelativeTorque (this.torque_magnitude,this.torque_magnitude/2f,0f,ForceMode.Impulse);
		}
	}
}
