using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gun : MonoBehaviour
{
	public Rigidbody rocket;				// Prefab of the rocket.
	public float speed = 200f;				// The speed the rocket will fire at.

	private Movement playerCtrl;		// Reference to the PlayerControl script.
	private Animator anim;					// Reference to the Animator component.

    //controls how big the wave is
    public float waveFrequency = 0.1f;
    //controls the lifetime of a wave
    public float waveLength = 1000;
    public float waveMaxScale = 4.0f;
    //gradually scaling the wave over time
    public float waveScaleModifier = 0.01f;
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
		playerCtrl = transform.GetComponent<Movement>();
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

            bulletInstance.transform.localScale = new Vector3(waveFrequency, waveFrequency, 0.0f);
            bulletInstance.transform.Rotate(new Vector3(90,0,0));
			bulletInstance.AddForce(playerCtrl.direction * speed);

            particlesList.Add(particle);
        }

        for (int i = 0; i < particlesList.Count; i++)
        {
            Particle particle = particlesList[i];

            //in milliseconds
            var currentDt = Time.deltaTime * 1000;
            var currentLife = particle.m_lifetime - currentDt;
            particlesList[i] = new Particle(particle.m_rigidBody, currentLife, waveFrequency);

            var currentScale = particlesList[i].m_rigidBody.transform.localScale;
            if (currentScale.x <= waveMaxScale && 
                currentScale.y <= waveMaxScale && 
                currentScale.z <= waveMaxScale)
            {
                particlesList[i].m_rigidBody.transform.localScale += new Vector3(waveScaleModifier, waveScaleModifier, waveScaleModifier);
            }

            if (particlesList[i].m_lifetime <= 0)
            {
                Destroy(particlesList[i].m_rigidBody.gameObject);
                particlesList.RemoveAt(i);
            }
        }
	}
}
