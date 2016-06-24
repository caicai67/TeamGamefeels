
//a component of "Moving"
using UnityEngine;
using System.Collections;

public class MovingSidewalkAnimCycle : MonoBehaviour {

	ArrayList storeChildren;

	void Start() {
		storeChildren = new ArrayList();
	}

	public void AnimationCycleEnd() {
		Transform captured = this.transform.Find ("CapturedObjs");
		storeChildren = new ArrayList ();
		for (int i = 0; i < captured.transform.childCount; i++) {
			Transform child = captured.GetChild (i);
			storeChildren.Add(child);
			child.parent = null;
		}
	}
	public void AnimationCycleStart() {
		foreach (Transform child in storeChildren) {
			child.parent = this.transform.Find ("CapturedObjs");
			storeChildren.Clear ();
		}
	}
}
