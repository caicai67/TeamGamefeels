using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class VampireController : MonoBehaviour {
	public GameObject player_character;
	private Animator anim;

    // Audio
    public AudioSource audio;
    public AudioClip die;
    //public AudioClip breathing;
    public AudioClip kickGrunt;


    // Use this for initialization
    void Start () {
		this.anim = this.GetComponent<Animator> ();
        this.audio = this.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 self = this.transform.position;
		Vector3 dreyar = this.player_character.transform.position;

		float distance = Vector3.Distance (self, dreyar);
		this.anim.SetFloat ("DreyarDistance", distance);
		if (this.anim.GetCurrentAnimatorStateInfo (0).IsName ("FastPursuitMode") && !(this.anim.IsInTransition(0))) {
			this.anim.ResetTrigger("Attack");
		}
		if (distance < 2f && !(this.anim.IsInTransition(0)) && !(this.anim.GetCurrentAnimatorStateInfo(0).IsName("roundhouse_kick"))) {
			this.anim.SetTrigger ("Attack");
        }
	}

    void VampireKickGrunt()
    {
        this.audio.clip = this.kickGrunt;
        this.audio.Play();
    }
}
