using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Collision : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    }

    void OnTriggerEnter(Collider other)
    {

		print (transform.name + "   "+ other.transform.name);
        Vector3 triggerForwardVec = other.transform.forward;

        RaycastHit[] arr = Physics.RaycastAll(other.transform.position, triggerForwardVec);

        if(arr.Length > 0)
        {
            Vector3 reflectedVector = Vector3.Reflect(triggerForwardVec, arr[0].normal);
            //should debug this
            other.transform.forward = reflectedVector;
            other.GetComponent<Rigidbody>().AddForce(other.transform.forward * 1000);
        }
    }
}
