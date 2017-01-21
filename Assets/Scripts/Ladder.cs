using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider))]
public class Ladder : MonoBehaviour {

	private Transform top;
	private Transform bottom;

	private enum Move {
		up,
		down
	}

	private void Start() {
		top = transform.FindChild ("Top");
		bottom = transform.FindChild ("Bottom");
	}
	 
	private void OnTriggerStay(Collider other) {
		
		if (other.transform.CompareTag ("Player")) {
			print (other.name);
			Movement movement = other.transform.GetComponent<Movement> ();
			if (movement.jumping == true) {
				MovePlayer (other.transform, Move.up);
			}
			if (movement.duck == true) {
				MovePlayer (other.transform, Move.down);
			}
		}
	}

	private void MovePlayer(Transform player, Move dir ) {
		Rigidbody rigidbody = player.GetComponent<Rigidbody> ();
		rigidbody.velocity = Vector3.zero;
		Vector3 newPosition = top.position;
		if (dir == Move.up) {
			newPosition = top.position;
		} 
		if (dir == Move.down) {
			newPosition = bottom.position;
		}
		player.position = newPosition;
	}
}
