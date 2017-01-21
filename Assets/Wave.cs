using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Wave : MonoBehaviour {

	float fireSpeed = 1.0f;
	private Rigidbody rb;
	void Start () {
		rb = transform.GetComponent<Rigidbody> ();
		// set our laser on its merry way. no need to update transform manually
		rb.velocity = Vector3.forward * fireSpeed;

		// freeze the rotation so it doesnt go spinning after a collision
		rb.freezeRotation = true;
	}

	// we want to store the laser's velocity every frame
	// so we can use this data during collisions to reflect
	private Vector3 oldVelocity;
	void FixedUpdate () {
		// because we want the velocity after physics, we put this in fixed update
		oldVelocity = rb.velocity;
	}

	// when a collision happens
	void OnCollisionEnter (Collision collision) {
		// get the point of contact
		ContactPoint contact = collision.contacts[0];

		// reflect our old velocity off the contact point's normal vector
		Vector3 reflectedVelocity = Vector3.Reflect(oldVelocity, contact.normal);        

		// assign the reflected velocity back to the rigidbody
		rb.velocity = reflectedVelocity;
		// rotate the object by the same ammount we changed its velocity
		Quaternion rotation = Quaternion.FromToRotation(oldVelocity, reflectedVelocity);
		transform.rotation = rotation * transform.rotation;
	}
}
