using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerPrefix
{
	None,
	P1_,
	P2_
}

public enum State
{
    None,
    Jumping,
    Ducking,
    Running
}

[RequireComponent(typeof(Rigidbody))]

[RequireComponent(typeof(JumpScript))]
public class Movement : MonoBehaviour {
		
	public float speed = 10.0f;

	public PlayerPrefix prefix;


	public Vector3 direction;
	private bool grounded = false;

	// Update is called once per frame

	private string horizontalAxis;
	private string jumpAxis;
    private string verticalAxis;

    
	[SerializeField]
	public State CurrentState { 
		get{ return currentState;} 
		set{ 
			//Debug.LogFormat ("Change Player {0} state from {1} to {2}", transform.name, currentState, value);
			currentState = value;
			anim.StateChange ();

		}}
	[SerializeField]
	private State currentState;
    public float controllerThreshold = 0.9f;

	private JumpScript jumpScript;
	private SpriteAnimator anim;


	void Start() {

		anim = transform.GetComponentInChildren<SpriteAnimator> ();
		jumpScript = transform.GetComponent<JumpScript> ();

		if (prefix == PlayerPrefix.None) {
			jumpAxis = "Jump";
			horizontalAxis = "Horizontal";
			verticalAxis = "Vertical";
		} else {
			jumpAxis = prefix.ToString () + "Jump";
			horizontalAxis = prefix.ToString () + "Horizontal";
			verticalAxis = prefix.ToString () + "Vertical";
		}


	}

	void Update() {
		
		grounded = Physics.Linecast(transform.position, transform.position + 1.1f * Vector3.down,1 << LayerMask.NameToLayer("Ground"));
		// If the jump button is pressed and the player is grounded then the player should jump.
		if(Input.GetAxisRaw(verticalAxis) < -controllerThreshold && grounded)
        {
			Jump ();
        }

        if(Input.GetAxisRaw(verticalAxis) > controllerThreshold && grounded)
        {
			Duck();
        }

		if (Input.GetAxisRaw (verticalAxis) < controllerThreshold && Input.GetAxisRaw (verticalAxis) > -controllerThreshold) {
			if (CurrentState == State.Ducking) {
				CurrentState = State.None;
			}
		}
    }

	void FixedUpdate () {
		
		float x = Input.GetAxisRaw (horizontalAxis);
        float y = Input.GetAxisRaw(verticalAxis);
		if (x != 0.0f || y != 0.0f) {
			Run ();
		} else {
			if (CurrentState == State.Running) {
				CurrentState = State.None;
			}
		}
	}

	void Jump() {

		if (jumpScript.Jump ()) {
			CurrentState = State.Jumping;
		}
	}

    void Duck()
    {
        CurrentState = State.Ducking;
    }

	void Run() {
		float x = Input.GetAxisRaw (horizontalAxis);

		direction = new Vector3 (-x, 0.0f , 0.0f); /// depends on the main camera movement
		direction.Normalize();
		transform.position += Time.deltaTime * speed * direction; 
		CurrentState = State.Running;
	}

}
