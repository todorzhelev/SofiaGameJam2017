using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderTrigger : MonoBehaviour {

	// Use this for initialization

	private Ladder ladder;
	void Start () {
		ladder = transform.parent.parent.GetComponent<Ladder> ();
		ladder.RegisterTrigger (this);
	}
	
	void OnTriggerStay(Collider other) {
		if (other.transform.CompareTag ("Player")) {
			Movement player = other.transform.GetComponent<Movement> ();

		}
	}
}
