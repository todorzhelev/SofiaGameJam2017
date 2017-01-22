using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour 
{
	public float gameTimer = 60.0f;
	public RectTransform timer;

	private float timeLeft;

	// Use this for initialization
	void Start () {
		timeLeft = gameTimer;
	}

	// Update is called once per frame
	void Update () {
		timeLeft -= Time.deltaTime;
		if (timeLeft <= 0.0f) {
			Time.timeScale = 0;
			EndGame ();
		}
		UpdateTimer ();
	}

	void UpdateTimer() {
		Text text = timer.FindChild ("Text").GetComponent<Text>();
		text.text = timeLeft.ToString ("0");
	}

	public void PlayerDied(Transform player) {

	}

	void EndGame() {

	}
}
