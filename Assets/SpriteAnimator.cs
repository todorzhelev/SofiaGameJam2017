using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SpriteAnimator : MonoBehaviour {


	public string idle = "idle";
	public string shoot = "Shoot";
	public string Run = "Run";
	public string RunAndShoot = "Run Shoot";

	private Animator anim;
	// Use this for initialization
	void Start () {
		
		anim = transform.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Jump")) {
			anim.Play ("Run");

		}
	}
}
