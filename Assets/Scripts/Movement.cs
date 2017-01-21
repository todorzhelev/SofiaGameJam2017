using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerPrefix
{
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
public class Movement : MonoBehaviour {
		
	public float speed = 10.0f;
	public float jumpForce = 10.0f;

	public PlayerPrefix prefix;

	private bool grounded = false;
	private bool jump = false;
    private bool duck = false;
	
	// Update is called once per frame

	private string horizontalAxis;
	private string jumpAxis;
    private string verticalAxis;

    public State currentState;
    public float controllerThreshold = 0.9f;

    void Start() {
		horizontalAxis = prefix.ToString () + "Horizontal";
		jumpAxis = prefix.ToString() + "Jump";
        verticalAxis = prefix.ToString() + "Vertical";
       
    }

	void Update() {
		
		grounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);

		// If the jump button is pressed and the player is grounded then the player should jump.
		if(Input.GetAxisRaw(verticalAxis) < -controllerThreshold && grounded)
        {
            jump = true;
        }

        if(Input.GetAxisRaw(verticalAxis) > controllerThreshold && grounded)
        {
            duck = true;
        }
    }

	void FixedUpdate () {
		
		float x = Input.GetAxisRaw (horizontalAxis);
        float y = Input.GetAxisRaw(verticalAxis);

        Vector3 direction = new Vector3 (-x, 0.0f , 0.0f); /// depends on the main camera movement
		direction.Normalize();
		transform.position += Time.deltaTime * speed * direction;
		if (y < -controllerThreshold && jump)
        {
			Jump ();
		}

        if(y > controllerThreshold && duck)
        {
            Duck();
        }

        print(y);
        if( Math.Abs(y) < 0.001 )
        {
            currentState = State.None;
        } 
	}

	void Jump() {

        currentState = State.Jumping;
		jump = false;

		Rigidbody rigidbody = transform.GetComponent<Rigidbody> ();
		Debug.AssertFormat (rigidbody != null, "Movement script for {0} needs rigitbody", transform.name);

		if (rigidbody != null) {
			
			rigidbody.AddForce (Vector3.up * jumpForce, ForceMode.Impulse);
		}
	}

    void Duck()
    {
        currentState = State.Ducking;
        duck = false;

        Rigidbody rigidbody = transform.GetComponent<Rigidbody>();
        Debug.AssertFormat(rigidbody != null, "Movement script for {0} needs rigitbody", transform.name);
    }
}
