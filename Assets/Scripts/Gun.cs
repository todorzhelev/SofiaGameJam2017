using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gun : MonoBehaviour
{
	public Transform wave;				// Prefab of the wave.

    //controls how big the wave is
    public float waveFrequency = 0.1f;
    //controls the lifetime of a wave
    public float waveLength = 1000;

	public void Fire() {	


	}

	void Shoot() {

		GameObject instance = Instantiate (wave, transform.position, Quaternion.identity);
		instance.transform.localScale = new Vector3 (0.1f, 0.1f, 0.1f); // TODO placeholder
		Wave waveScript = instance.GetComponent<Wave> ();
		waveScript.direction = (dir.position - transform.position);
		waveScript.frequency = waveFrequency;
		waveScript.length = waveLength;
		waveScript.shooter = transform;
		waveScript.CalculateTTL ();
	}
}
