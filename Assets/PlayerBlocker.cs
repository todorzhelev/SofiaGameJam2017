using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlocker : MonoBehaviour {


	public Transform tpPos;

	void OnTriggerEnter(Collider other) {
		print ("enter");
		if (other.transform.CompareTag ("Player")) {
			other.transform.position = tpPos.position;
		}
	}
	void OnTriggerExit(Collider other) {

	}
}
