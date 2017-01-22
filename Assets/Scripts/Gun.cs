﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gun : MonoBehaviour
{
	public GameObject wave;				// Prefab of the wave.
	public Vector3 dir;
    //controls how big the wave is
    private float waveFrequency = 1f;
    //controls the lifetime of a wave
    private float waveLength = 10;

    public float WaveFrequency
    {
        get
        {
            return waveFrequency;
        }

        set
        {
            waveFrequency = value;
        }
    }

    public float WaveLength
    {
        get
        {
            return waveLength;
        }

        set
        {
            waveLength = value;
        }
    }

    void Start  () {
		dir = transform.position + Vector3.right;
	}
	public void Fire() {
		Shoot ();
	}

	void Shoot() {

        Movement mov = transform.GetComponent<Movement>();

        dir = transform.position + Vector3.right * 100 + Vector3.down * mov.currentWaveControlVal;

        GameObject instance = Instantiate (wave, transform.position, Quaternion.identity);
		instance.transform.localScale = new Vector3 (0.1f, 0.1f, 0.1f); // TODO placeholder
		Wave waveScript = instance.GetComponent<Wave> ();
		waveScript.direction = (dir - transform.position);
		waveScript.frequency = mov.currentWaveFrequency;
		waveScript.length = mov.currentWaveLength;
		waveScript.shooter = transform;
		waveScript.CalculateTTL ();
	}
}
