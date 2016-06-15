using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	private KeyCode[] level_keys = new KeyCode[] { 
		KeyCode.Alpha0, 
		KeyCode.Alpha1,
		KeyCode.Alpha2,
		KeyCode.Alpha3,
		KeyCode.Alpha4,
		KeyCode.Alpha5,
		KeyCode.Alpha6,};
		
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < 7; i++) {
			if(Input.GetKey(level_keys[i])){
				SceneManager.LoadScene(i);
			}
		}
	}
}
