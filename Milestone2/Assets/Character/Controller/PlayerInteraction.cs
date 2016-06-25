using UnityEngine;
using System.Collections;



// Team GameFeels
// Ambrose, Chris, KP, Justin, Caitlin, Charlie

public class PlayerInteraction : MonoBehaviour {

    private Keymapping keymap = new Keymapping();
    private Rigidbody rigid_body;
    private Animator animator;
    private PlayerMetrics metrics;
    private CharacterController controller;
    private CapsuleCollider collider_;
    private AudioSource sfx;

    // Collider/Controller Defaults
    float controller_height;
    float collider_height;
    Vector3 controller_center;
    Vector3 collider_center;

    // Use this for initialization
    void Awake()
    {
        this.rigid_body = GetComponent<Rigidbody>();
        this.animator = GetComponent<Animator>();
        this.metrics = GetComponent<PlayerMetrics>();
        this.controller = GetComponent<CharacterController>();
        this.collider_ = GetComponent<CapsuleCollider>();
        this.controller_height = this.controller.height;
        this.collider_height = this.collider_.height;
        this.controller_center = this.controller.center;
        this.collider_center = this.collider_.center;
        sfx = GetComponent<AudioSource>();

        //No longer needed as I have set rig's layer(i.e. Ragdoll) to not 
        //interact with Character Model's layer(aka Character) in the Physics settings

        //make the ragdoll kinematic for now
        //makeRagdollKinematic(true);
    }

    // Update is called once per frame
    void Update () {
	    if(this.animator.GetFloat("InputMagnitude") != 0)
        {
            if(sfx.isPlaying == false && this.animator.GetInteger("CurrentInteraction") == 0)
            {
                sfx.Play();
            }
        } else
        {
            sfx.Stop();
        }
	}
}
