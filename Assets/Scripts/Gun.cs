using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
	public Rigidbody rocket;				// Prefab of the rocket.
	public float speed = 20f;				// The speed the rocket will fire at.

	private PlayerControl playerCtrl;		// Reference to the PlayerControl script.
	private Animator anim;					// Reference to the Animator component.

    public float waveFrequency;
    public float waveLength;

	void Awake()
	{
		// Setting up the references.
		anim = transform.root.gameObject.GetComponent<Animator>();
		playerCtrl = transform.root.GetComponent<PlayerControl>();
	}


	void Update ()
	{
		if(Input.GetButtonDown("Fire1"))
		{
            Rigidbody bulletInstance = Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody;
            
            bulletInstance.AddForce(transform.forward * speed);
        }
	}
}
