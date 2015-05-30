using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDHealthScript : MonoBehaviour {
	
	public Text healthText;
	public Slider healthSlider;
	
	private static HUDHealthScript hudHealth;
	
	void Awake() {
		if (hudHealth == null) {
			hudHealth = this;
		} else if (hudHealth != this) {
			Destroy (this.gameObject);
		} 
	}

	void Start () {
		UpdateInfo();
	}
	
	private void UpdateAllInfo() {
		int curHealth = GameManagerScript.gameManager.playerHealth;
		int maxHealth = GameManagerScript.gameManager.playerMaxHealth;
		string healthTextString = curHealth.ToString() + " / " + maxHealth.ToString();

		healthText.text = healthTextString;
		healthSlider.value = curHealth;
		healthSlider.maxValue = maxHealth;
	}
	
	public static void UpdateInfo() {
		hudHealth.UpdateAllInfo();
	}
}
