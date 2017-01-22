using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

	// Use this for initialization

	public GameObject wave;
	public float ms = .10f;

	public float force = 10.0f;

	private float lastWaveMs;
	private Transform dir;
	void Start () {
		dir = transform.FindChild ("dir");

	}
	
	// Update is called once per frame
	void Update () {
		if (lastWaveMs + ms < Time.time) {
			SpawnWave ();
			lastWaveMs = Time.time;
		}
	}

	void SpawnWave() {
		
		GameObject instance = Instantiate (wave, transform.position, Quaternion.identity);
		instance.transform.localScale = new Vector3 (0.1f, 0.1f, 0.1f);
		Wave waveScript = instance.GetComponent<Wave> ();
		waveScript.direction = (dir.position - transform.position);
		waveScript.frequency = 100;
		waveScript.length = 1;
		waveScript.shooter = transform;
		waveScript.CalculateTTL ();


	}
}
