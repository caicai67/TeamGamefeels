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
            //PlayerMovement.interactionPosition = interactPosition.position;
            //PlayerMovement.interactionDirection = interactDirection.position;
            //PlayerMovement.canInteract = true;
        }
    }

    void OnTriggerLeave(Collider other)
    {
        if (other.gameObject == player)
        {
            //PlayerMovement.interactionPosition = new Vector3(0, 0, 0);
            //PlayerMovement.interactionDirection = new Vector3(0, 0, 0);
            //PlayerMovement.canInteract = false;
        }
    }
}
