using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {

	public bool closed = true;

	public float smooth = .1f;
	private Vector3 targetAngles;

	private Transform door;
	private Transform wall;

	private bool moving = false;
	void Start() {
		door = transform.FindChild ("door");
		wall = transform.FindChild ("wall");

	}

	public void Open() {
		closed = false;
		targetAngles = door.eulerAngles + 120f * Vector3.up; 
		wall.gameObject.SetActive (false);
		StartCoroutine ("ExecOpen");
	}

	public void Close() {
		closed = true;
		targetAngles = door.eulerAngles - 120f * Vector3.up; 
		wall.gameObject.SetActive (true);
		StartCoroutine ("ExecOpen");
	}

	bool IsMoving() {
		return moving;
	}

	IEnumerator ExecOpen()
	{
		
		yield return new WaitWhile (IsMoving);

		moving = true;
		while(true)
		{
			
			if (Mathf.Abs( door.eulerAngles.y - targetAngles.y) < 0.1f ) {
				moving = false;
				StopAllCoroutines();
				if (closed == false) {
					
					Invoke ("Close", 2 * Random.value);
				}
			}
			door.eulerAngles = Vector3.Lerp (door.eulerAngles, targetAngles, 10 * smooth * Time.deltaTime);

			yield return new WaitForFixedUpdate();
		}
	}
}
