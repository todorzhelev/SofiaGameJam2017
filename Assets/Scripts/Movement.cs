using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour {
		
	float speed = 10.0f;
	
	// Update is called once per frame



	void Update () {
		
		float x = Input.GetAxisRaw ("Horizontal");

		Vector3 direction = new Vector3 (0.0f, 0.0f ,x); /// depends on the main camera movement
		direction.Normalize();
		transform.position += Time.deltaTime * speed * direction;
		if (Input.GetAxis ("Jump") != 0.0f) {
			Jump ();
		}
	}

	void Jump() {


		Rigidbody rigidbody = transform.GetComponent<Rigidbody> ();
		Debug.AssertFormat (rigidbody != null, "Movement script for {0} needs rigitbody", transform.name);

		if (rigidbody != null) {
			
			rigidbody.AddForce (Vector3.up, ForceMode.Impulse);
		}
	}
}
