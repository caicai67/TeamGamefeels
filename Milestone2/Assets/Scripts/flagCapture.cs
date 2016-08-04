using UnityEngine;
using System.Collections;

public class flagCapture : MonoBehaviour {

	public GameObject closedTower;
	public GameObject openedTower;
	public MovieTexture cutscene2;

	// Use this for initialization
	void Start () {
		closedTower.SetActive (true);
		openedTower.SetActive (false);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter(Collision collision) {
		Debug.Log ("Collided");
		if (collision.gameObject.CompareTag("Player")){
			openTower();
		}
	}
	void openTower(){
		if (cutscene2 != null) {
			cutscene2.Play ();
		}
		openedTower.SetActive (true);
		closedTower.SetActive (false);
	}
	void OnGUI() {
		if (cutscene2 != null && cutscene2.isPlaying) {
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), cutscene2);
		}
	}
}
