using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerPrefix
{
	P1_,
	P2_
}

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour {
		
	public float speed = 10.0f;
	public float jumpForce = 10.0f;

	public PlayerPrefix prefix;

	private Transform groundCheck;
	private bool grounded = false;
	private bool jump = false;
	
	// Update is called once per frame

	void Start() {
		groundCheck = transform.FindChild ("groundCheck");
	}

	void Update() {
		
		grounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);

		Debug.Log (grounded);
		// If the jump button is pressed and the player is grounded then the player should jump.
		if(Input.GetButtonDown("Jump") && grounded)
			jump = true;
	}

	void FixedUpdate () {
		
		float x = Input.GetAxisRaw (prefix.ToString() + "Horizontal");

		Vector3 direction = new Vector3 (0.0f, 0.0f ,-x); /// depends on the main camera movement
		direction.Normalize();
		transform.position += Time.deltaTime * speed * direction;
		if (Input.GetAxis (prefix.ToString() + "Jump") != 0.0f && jump) {
			Jump ();

		}
	}

	void Jump() {

		jump = false;

		Rigidbody rigidbody = transform.GetComponent<Rigidbody> ();
		Debug.AssertFormat (rigidbody != null, "Movement script for {0} needs rigitbody", transform.name);

		if (rigidbody != null) {
			
			rigidbody.AddForce (Vector3.up * jumpForce, ForceMode.Impulse);
		}
	}
}
