using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {

	public bool closed = true;

	public float smooth = .1f;
	private Vector3 targetAngles;

	private Transform door;
	private Transform wall;
	void Start() {
		door = transform.FindChild ("door");
		wall = transform.FindChild ("wall");
		targetAngles = transform.eulerAngles + 120f * Vector3.up; 
	}

	public void Open() {
		closed = false;
		wall.gameObject.SetActive (false);
		StartCoroutine ("ExecOpen");
	}


	IEnumerator ExecOpen()
	{
		while(true)
		{
			if (door.eulerAngles.z >= 130.0f) {
				StopAllCoroutines ();
			}
			door.eulerAngles = Vector3.Lerp (door.eulerAngles, targetAngles, 10 * smooth * Time.deltaTime);

			yield return new WaitForFixedUpdate();
		}
	}
}
