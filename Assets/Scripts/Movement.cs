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



[RequireComponent(typeof(JumpScript))]
public class Movement : MonoBehaviour {
		
	//movement speed
	public float speed = 10.0f;
	//whitch player
	public PlayerPrefix prefix;

	//where are we going
	public Vector3 direction;

	//detect movement
	private string horizontalAxis;
	private string jumpAxis;
    private string verticalAxis;
	private string fireAxis;

    
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
	// control jump
	private JumpScript jumpScript;
	//animate
	private SpriteAnimator anim;
	private Gun gun;


	void Start() {
		// get refs
		anim = transform.GetComponentInChildren<SpriteAnimator> ();
		jumpScript = transform.GetComponent<JumpScript> ();
		gun = transform.GetComponent<Gun> ();
		if (prefix == PlayerPrefix.None) {
			jumpAxis = "Jump";
			horizontalAxis = "Horizontal";
			verticalAxis = "Vertical";
			fireAxis = "Fire1";
		} else {
			jumpAxis = prefix.ToString () + "Jump";
			horizontalAxis = prefix.ToString () + "Horizontal";
			verticalAxis = prefix.ToString () + "Vertical";
			fireAxis = prefix.ToString () + "Fire1";
		}


	}

	void Update() {
		float x = Input.GetAxisRaw (horizontalAxis);
		float y = Input.GetAxisRaw(verticalAxis);
		if (Input.GetButtonDown (fireAxis)) {
			gun.Fire ();
		}

		// If the jump button is pressed and the player is grounded then the player should jump.

		if (x != 0.0f) {
			Run ();
		} else {
			if (CurrentState == State.Running) {
				CurrentState = State.None;
			}
		}
		if(y < -controllerThreshold)
        {
			Jump ();
        }

        if(y > controllerThreshold)
        {
			Duck();
        }

		if (y < controllerThreshold && y > -controllerThreshold) {
			if (CurrentState == State.Ducking) {
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
