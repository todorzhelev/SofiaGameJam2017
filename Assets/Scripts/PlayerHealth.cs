using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{	
	public float maxHealh = 100f;					// The player's health.
	public RectTransform healthBar;
	public float health;
	void Start() {
		health = maxHealh;
	}

	public void TakeDamage (float damageAmount)
	{
		// Reduce the player's health by 10.
		health -= damageAmount;

		// Update what the health bar looks like.
		UpdateHealthBar();
	}


	public void UpdateHealthBar ()
	{
		Transform healthObj = healthBar.FindChild ("Image");
		Image healthImage = healthObj.GetComponent<Image> ();
		healthImage.fillAmount = health / maxHealh;
	}
}
