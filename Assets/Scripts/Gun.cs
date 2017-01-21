using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gun : MonoBehaviour
{
	public Rigidbody rocket;				// Prefab of the rocket.
	public float speed = 20f;				// The speed the rocket will fire at.

	private PlayerControl playerCtrl;		// Reference to the PlayerControl script.
	private Animator anim;					// Reference to the Animator component.

    public float waveFrequency;
    public float waveLength;

    public List<Particle> particlesList;

    /// <summary>
    /// ////////////////////////////////////////////////////////////////////////
    /// </summary>
    public struct Particle
    {
        public Particle(Rigidbody body, float Lifetime)
        {
            rigidBody = body;
            lifetime = Lifetime;
        }

        public Rigidbody rigidBody;
        //in milliseconds
        public float lifetime;
    }

    /// <summary>
    /// ////////////////////////////////////////////////////////////////////////
    /// </summary>
    /// 
    void Awake()
	{
        particlesList = new List<Particle>();
        // Setting up the references.
        anim = transform.root.gameObject.GetComponent<Animator>();
		playerCtrl = transform.root.GetComponent<PlayerControl>();
	}

    /// <summary>
    /// ////////////////////////////////////////////////////////////////////////
    /// </summary>
    void Update ()
	{
		if(Input.GetButtonDown("Fire1"))
		{
            Rigidbody bulletInstance = Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody;

            Particle particle = new Particle();
            particle.rigidBody = bulletInstance;
            particle.lifetime = 1000;

            bulletInstance.AddForce(transform.forward * speed);

            particlesList.Add(particle);
        }

        for (int i = 0; i < particlesList.Count; i++)
        {
            Particle particle = particlesList[i];

            //in milliseconds
            var currentDt = Time.deltaTime * 1000;
            var currentLife = particle.lifetime - currentDt;
            particlesList[i] = new Particle(particle.rigidBody, currentLife);

            if (particlesList[i].lifetime <= 0)
            {
                Destroy(particlesList[i].rigidBody.gameObject);
                particlesList.RemoveAt(i);
            }
        }
	}
}
