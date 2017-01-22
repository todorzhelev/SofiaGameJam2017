using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	public Sprite menuPanel;
	public Sprite creditsPanel;

	public Image background;

	private bool credistsShown = false;

	public void StartGame() {
		SceneManager.LoadScene ("level01",LoadSceneMode.Single);
	}

	public void ShowCredits() {
		background.sprite = credistsShown ? menuPanel : creditsPanel;
		credistsShown = !credistsShown;
	}



	public void Exit() {

	}
}
