using UnityEngine;
using System.Collections;

public class MeleeSystem : MonoBehaviour {

	public PlayerController player_controller = null;
	private Keymapping keymap = new Keymapping();
	public int damage = 30;
	public float distance;
	public float meleeReach;
	RaycastHit hit;
	Ray swordRay;

	// Use this for initialization
	void Start () {

		if (this.player_controller == null) {
			this.player_controller = GetComponent<PlayerController> ();
		}

		swordRay = new Ray (transform.position, transform.TransformDirection(Vector3.forward));

	}
	
	// Update is called once per frame
	void Update () {

		this.transform.rotation = this.transform.parent.rotation;

		if (player_controller.isFighting() && (player_controller.activeController.RightTrigger.WasPressed || Input.GetKeyDown (this.keymap.fight.keyboard))) {

			if (Physics.Raycast (swordRay, out hit)) {
				//if(hit.collider.tag == "Enemy"){
				distance = hit.distance;
				Debug.DrawRay (transform.position, transform.TransformDirection(Vector3.forward), Color.green);
				Debug.Log ("Enemy was hit!");
				Debug.Log (distance);
				//}
			} else {
				Debug.DrawRay (transform.position, transform.forward * 1000f, Color.red);

			}
				
			
		}
	}
}
