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
			if (movement.CurrentState == State.Jumping) {
				MovePlayer (other.transform, Move.up);
			}
			if (movement.CurrentState == State.Ducking) {
				
				MovePlayer (other.transform, Move.down);
			}
			movement.CurrentState = State.None;
		}
	}

	private void MovePlayer(Transform player, Move dir ) {
		
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
