using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Wave : MonoBehaviour {

	public Vector3 direction;
	public float frequency;
	public float length;
	public Transform shooter;


	private BoxCollider box;

	float ttl;
	void Start () {
		box = transform.GetComponent<BoxCollider> ();
	}

	void FixedUpdate () {
		Vector3 norm = direction.normalized ;
		transform.position += norm* Time.deltaTime * length;

		Debug.DrawLine (transform.position , transform.position + direction , Color.red);
		Debug.DrawLine (transform.position, transform.position + new Vector3(1,0,0) , Color.green);
	}

	// when a collision happens
	void OnCollisionEnter (Collision collision) {
		
		if (collision.transform.CompareTag ("Wave")) {
			return;
		}
		if (collision.transform.CompareTag ("Player")) {
			//TODO hit the player;
			return;
		}

		// get the point of contact
		ContactPoint contact = collision.contacts[0];

		// reflect our old velocity off the contact point's normal vector
		Vector3 reflectedVelocity = Vector3.Reflect(direction, contact.normal);        

		// assign the reflected velocity back to the rigidbody

		// rotate the object by the same ammount we changed its velocity
		Quaternion rotation = Quaternion.FromToRotation(direction, reflectedVelocity);
		transform.rotation = rotation * transform.rotation;
		direction = reflectedVelocity;
		StartCoroutine ("SleepCollision");

	}
	// HAX
	IEnumerator SleepCollision() {
		box.enabled = false;
		yield return new WaitForSeconds (0.1f);
		box.enabled = true;
	}

	public void CalculateTTL() {
		ttl = 1 / frequency;
		transform.localScale += new Vector3 (0, length /10, 0);

	}
}
