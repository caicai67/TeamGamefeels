using UnityEngine;
using System.Collections;

public class MeleeSystem : MonoBehaviour {

	public PlayerController player_controller = null;
	private Keymapping keymap = new Keymapping();
	public int damage = 30;
	float distance;
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

		if (player_controller.isFighting() && player_controller.activeController.RightTrigger.WasPressed && Input.GetKeyDown (this.keymap.fight.keyboard) ) {


			if (Physics.Raycast (swordRay, out hit, meleeReach)) {
				//if(hit.collider.tag == "Enemy"){
					Debug.Log ("Enemy was hit!");
				//}
			}
				
			
		}
	}
}
