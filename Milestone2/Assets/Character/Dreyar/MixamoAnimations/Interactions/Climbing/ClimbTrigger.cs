using UnityEngine;
using System.Collections;

public class ClimbTrigger : MonoBehaviour {

    [Range(0, 90)]
    public int ClimbAngle = 30;

    public AnchorPoint[] Anchors;
    public GameObject Anchor { get; set; }

    private bool InRange;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
