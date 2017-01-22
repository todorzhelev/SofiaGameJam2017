using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderTrigger : MonoBehaviour {

	// Use this for initialization
	public Transform top;
	public Transform bottom;

	
	void OnTriggerStay(Collider other) {
		if (other.transform.CompareTag ("Player")) {
			Movement player = other.transform.GetComponent<Movement> ();


			if (player.CurrentState == State.Jumping) {
				if (top != null) {
					Debug.LogFormat ("Trigger {0} is teleporting player {1} to pos {2}", transform.name, other.transform.name, top.position);
					player.CurrentState = State.None;
					other.transform.position = top.position;
				}
			}
			if (player.CurrentState == State.Ducking) {
				if (bottom != null) {
					Debug.LogFormat ("Trigger {0} is teleporting player {1} to pos {2}", transform.name, other.transform.name, bottom.position);
					player.CurrentState = State.None;
					other.transform.position = bottom.position;
				}
			}
		}
	}
}
