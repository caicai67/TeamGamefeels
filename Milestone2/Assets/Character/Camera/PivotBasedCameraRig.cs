using UnityEngine;
using System.Collections;

public abstract class PivotBasedCameraRig : AbstractTargetFollower {
	// This script is designed to be placed on the root object of a camera rig,
	// comprising 3 gameobjects, each parented to the next:

	// 	Camera Rig
	// 		Pivot
	// 			Camera
	protected Transform cam; // the transform of the camera
	protected Transform pivot; // position of the pivot point
	protected Vector3 lastTargetPosition;

	protected virtual void Awake()
	{
		// find the camera in the object hierarchy
		this.cam = GetComponentInChildren<Camera>().transform;
		//this.pivot = this.target.transform;
		this.pivot = this.cam.parent;
	}
}
