using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	public Transform menuPanel;
	public Transform creditsPanel;

	public void StartGame() {
		SceneManager.LoadScene ("level01",LoadSceneMode.Single);
	}

	public void ShowCredits() {
		menuPanel.gameObject.SetActive (false);
		creditsPanel.gameObject.SetActive (true);
	}

	public void HideCredits () {
		menuPanel.gameObject.SetActive (true);
		creditsPanel.gameObject.SetActive (false);
	}

	public void Exit() {

	}
}
