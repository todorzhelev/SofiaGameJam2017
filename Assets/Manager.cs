using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour 
{
	public float gameTimer = 60.0f;
	public RectTransform timer;
	public RectTransform endScreen;

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
			EndGame ("Draw");
		}
		UpdateTimer ();
	}

	void UpdateTimer() {
		Text text = timer.FindChild ("Text").GetComponent<Text>();
		text.text = timeLeft.ToString ("0");
	}

	public void PlayerDied(Transform player) {
		EndGame (string.Format ("Mastermind {0} has faild", player.name));
	}

	void EndGame(string endText) {
		Text text = endScreen.FindChild ("Text").GetComponent<Text> ();
		endScreen.gameObject.SetActive (true);
		text.text = endText;


		Invoke ("GoToMainMenu", 4);
	}

	void GoToMainMenu() {
		SceneManager.LoadScene ("main",LoadSceneMode.Single);
	}
}
