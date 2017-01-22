using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gun : MonoBehaviour
{
	public GameObject wave;				// Prefab of the wave.
	public Vector3 dir;
    //controls how big the wave is
    public float waveFrequency = 1f;
    //controls the lifetime of a wave
    public float waveLength = 10;


	void Start  () {
		dir = transform.position + Vector3.right;
	}
	public void Fire() {
		Shoot ();
	}

	void Shoot() {

		GameObject instance = Instantiate (wave, transform.position, Quaternion.identity);
		instance.transform.localScale = new Vector3 (0.1f, 0.1f, 0.1f); // TODO placeholder
		Wave waveScript = instance.GetComponent<Wave> ();
		waveScript.direction = (dir - transform.position);
		waveScript.frequency = waveFrequency;
		waveScript.length = waveLength;
		waveScript.shooter = transform;
		waveScript.CalculateTTL ();
	}
}
