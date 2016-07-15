using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof (MyThirdPersonCharacter))]
public class MyThirdPersonUserControl : MonoBehaviour {
	private MyThirdPersonCharacter m_Character;
	private Transform m_Cam;
	private Vector3 m_CamForward;
	private Vector3 m_Move;
	// Use this for initialization
	void Start () {
		if (Camera.main != null) {
			m_Cam = Camera.main.transform;
		} else {
			Debug.LogWarning (
				"Warning: no main camera found.  Third person character needs a Camera tagged \"MainCamera\" for camera-relative controls.");
		}

		m_Character = GetComponent<MyThirdPersonCharacter> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void FixedUpdate()
	{
		float h = CrossPlatformInputManager.GetAxis ("Horizontal");
		float v = CrossPlatformInputManager.GetAxis ("Vertical");

	}
}
