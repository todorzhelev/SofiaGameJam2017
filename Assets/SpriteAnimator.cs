using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class SpriteAnimator : MonoBehaviour {


	public string idle = "idle";
	public string shoot = "Shoot";
	public string run = "Run";
	public string RunAndShoot = "Run Shoot";
	public string Jump = "Jump";

	private Animator anim;
	private Movement player;
	// Use this for initialization
	void Start () {
		anim = transform.GetComponent<Animator> ();
		player = transform.GetComponentInParent<Movement> ();
	}
	
	public void StateChange() {
		switch (player.CurrentState) {
		case State.None:
			anim.Play (idle);
			break;
		case State.Running:
			anim.Play (run);
			if (player.direction.x > 0) {
				transform.localEulerAngles = new Vector3 (0, 0, 0);
			} else {
				transform.localEulerAngles = new Vector3 (0, -180, 0);
			}
			break;
		}


	}
}
