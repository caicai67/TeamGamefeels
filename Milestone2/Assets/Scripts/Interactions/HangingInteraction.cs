//Team GameFeels
//Chris Donlan, Karan Pratap, Caitlin Morris, Ambrose Cheung, Justin Thornburgh, Charles Jolman
using UnityEngine;
using System.Collections;

public class HangingInteraction : MonoBehaviour {

    GameObject player;
    public Transform interactPosition;
    public Transform interactDirection;

    // Use this for initialization
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            PlayerController.interactionPosition = interactPosition.position;
            PlayerController.interactionDirection = interactDirection.position;
            PlayerController.canInteract = true;
        }
    }

    void OnTriggerLeave(Collider other)
    {
        if (other.gameObject == player)
        {
            PlayerController.interactionPosition = new Vector3(0, 0, 0);
            PlayerController.interactionDirection = new Vector3(0, 0, 0);
            PlayerController.canInteract = false;
        }
    }

    int GetInteraction()
    {
        return 1;
    }
}
