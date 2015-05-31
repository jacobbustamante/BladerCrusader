using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NewProfileScript : MonoBehaviour {
	
	public InputField nameInputObject;
	public Toggle easyToggle, mediumToggle, hardToggle;
	public Slider rSlider, gSlider, bSlider;
	public Image previewImage;

	private string profileFileName;

	public void UpdatePreviewImage() {
		Color previewColor = new Color(rSlider.value, gSlider.value, bSlider.value);
		previewImage.color = previewColor;
	}

	public void SetFileName(string fileName) {
		profileFileName = fileName;
	}
	
	public void PlayGame() {
		ProfileDataScript profile = new ProfileDataScript(profileFileName, nameInputObject.text);
		profile.colors = new float[]{rSlider.value, gSlider.value, bSlider.value};
		profile.difficulty = GetDifficulty();
		this.GetComponent<LoadGameScript>().LoadGameFromProfile(profile);
	}

	private GameManagerScript.Difficulty GetDifficulty() {
		if (easyToggle.isOn)
			return GameManagerScript.Difficulty.EASY;
		else if (mediumToggle.isOn)
			return GameManagerScript.Difficulty.MEDIUM;
		else if (hardToggle.isOn)
			return GameManagerScript.Difficulty.HARD;
		else // default
			return GameManagerScript.Difficulty.MEDIUM;
	}
}
