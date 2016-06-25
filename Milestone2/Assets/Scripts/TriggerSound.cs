// Team GameFeels
// Chris, Ambrose, KP, Justin, Caitlin, Charlie
using UnityEngine;
using System.Collections;

public class TriggerSound : MonoBehaviour {

    GameObject player;
    AudioSource sfx;
    public AudioClip sound;

    void Awake()
    {
        sfx = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            sfx.Play();
        }
    }
}
