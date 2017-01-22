using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour {


	public DoorScript door;

	// Use this for initialization
	void Start () {
		door = transform.GetComponentInParent<DoorScript> ();
	}

	void OnTriggerEnter(Collider other) {
		if (other.transform.CompareTag ("Player") && door.closed) {
			door.Open ();
		}
	}
}
