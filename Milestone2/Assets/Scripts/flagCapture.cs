using UnityEngine;
using System.Collections;

public class flagCapture : MonoBehaviour {

	public GameObject closedTower;
	public GameObject openedTower;

	// Use this for initialization
	void Start () {
		closedTower.SetActive (true);
		openedTower.SetActive (false);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.CompareTag("Player")){
			openTower();
		}
	}
	void openTower(){
		openedTower.SetActive (true);
		closedTower.SetActive (false);
	}
}
