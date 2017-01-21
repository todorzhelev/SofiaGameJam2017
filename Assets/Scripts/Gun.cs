using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gun : MonoBehaviour
{
	public Rigidbody rocket;				// Prefab of the rocket.
	public float speed = 20f;				// The speed the rocket will fire at.

	private PlayerControl playerCtrl;		// Reference to the PlayerControl script.
	private Animator anim;					// Reference to the Animator component.

    public float waveFrequency = 0.1f;
    public float waveLength = 1000;

    public List<Particle> particlesList;

    /// <summary>
    /// ////////////////////////////////////////////////////////////////////////
    /// </summary>
    public struct Particle
    {
        public Particle(Rigidbody body, float lifetime, float scale)
        {
            m_rigidBody = body;
            m_lifetime  = lifetime;
            m_scale     = scale;
        }

        public Rigidbody m_rigidBody;
        //in milliseconds
        public float m_lifetime;
        public float m_scale;
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
            particle.m_rigidBody = bulletInstance;
            particle.m_lifetime  = waveLength;
            particle.m_scale     = waveFrequency;

            bulletInstance.transform.localScale = new Vector3(waveFrequency, waveFrequency, waveFrequency);

            bulletInstance.AddForce(transform.forward * speed);

            particlesList.Add(particle);
        }

        for (int i = 0; i < particlesList.Count; i++)
        {
            Particle particle = particlesList[i];

            //in milliseconds
            var currentDt = Time.deltaTime * 1000;
            var currentLife = particle.m_lifetime - currentDt;
            particlesList[i] = new Particle(particle.m_rigidBody, currentLife,10);

            if (particlesList[i].m_lifetime <= 0)
            {
                Destroy(particlesList[i].m_rigidBody.gameObject);
                particlesList.RemoveAt(i);
            }
        }
	}
}
