using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Movement))]
public class JumpScript : MonoBehaviour {

	public float maxJumpHeight = 3.0f;
	public float jumpSpeed = 7.0f;
	public float fallSpeed = 12.0f;
	public bool inputJump = false;
	public bool grounded = true;

	Movement movement;

	void Start()
	{
		movement = transform.GetComponent<Movement> ();
		maxJumpHeight = transform.position.y + maxJumpHeight;
	}

	public bool Jump() {
		if (grounded) {
			inputJump = true;
			grounded = false;
			StartCoroutine ("ExecJump");
			return true;
		} else {
			return false;
		}
	}

	IEnumerator ExecJump()
	{
		while(true)
		{
			if (transform.position.y >= maxJumpHeight) {
				inputJump = false;
			}
			if (inputJump) {
				transform.Translate (Vector3.up * jumpSpeed * Time.smoothDeltaTime);
			}else if(!inputJump)
			{
				transform.Translate(Vector3.down * fallSpeed * Time.smoothDeltaTime);
				if(isGrounded()){
					grounded = true;
					if (movement.CurrentState == State.Jumping)
						movement.CurrentState = State.None;
					StopAllCoroutines();
				}
			}

			yield return new WaitForFixedUpdate();
		}
	}

	bool isGrounded () {
		return Physics.Linecast(transform.position, transform.position + 0.3f * Vector3.down,1 << LayerMask.NameToLayer("Ground"));
	}
}
