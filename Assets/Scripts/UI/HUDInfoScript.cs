using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDInfoScript : MonoBehaviour {

	public Text scoreText;
	public Text levelText;
	public Text waveText;

	private static HUDInfoScript hudInfo;

	void Awake() {
		if (hudInfo == null) {
			hudInfo = this;
		} else if (hudInfo != this) {
			Destroy (this.gameObject);
		} 
	}

	// Use this for initialization
	void Start () {
		UpdateInfo();
	}

	private void UpdateAllInfo() {
		string waveTextString = GameManagerScript.gameManager.wave.ToString() +
			" / " + GameManagerScript.MAX_WAVE.ToString();

		scoreText.text = GameManagerScript.gameManager.score.ToString();
		waveText.text = waveTextString;
		levelText.text = GameManagerScript.gameManager.level.ToString();
	}

	public static void UpdateInfo() {
		hudInfo.UpdateAllInfo();
	}
}
