using UnityEngine;
using System.Collections;

public class SceneEntry : MonoBehaviour {
	public MovieTexture cutscene;

	void Awake() {
		if (cutscene != null) {
			Debug.Log ("cutscene playing");
			cutscene.Play ();
		}
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnGUI() {
		if (cutscene != null && cutscene.isPlaying)
		{
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), cutscene);
		}
	}
}
