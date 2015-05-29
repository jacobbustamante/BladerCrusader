using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour {

	private const string VOLUME_KEY = "VOLUME";
	private const float DEFAULT_VOLUME = 1.0f;

	public bool updateOnAwake;
	public GameObject volumeSliderObject;

	void Awake() {
		if (updateOnAwake) {
			SetSettings();
			UpdateSettingsPanel();
		}
	}

	public void SaveSettings() {
		Slider volumeSlideScript = volumeSliderObject.GetComponent<Slider>();

		PlayerPrefs.SetFloat(VOLUME_KEY, volumeSlideScript.value);

		SetSettings();
		UpdateSettingsPanel();
	}
	
	public void ResetSettings() {
		Slider volumeSlideScript = volumeSliderObject.GetComponent<Slider>();

		volumeSlideScript.value = DEFAULT_VOLUME;
	}

	public static void SetSettings() {
		if (PlayerPrefs.HasKey(VOLUME_KEY)) {
			AudioListener.volume = PlayerPrefs.GetFloat(VOLUME_KEY);
		}
		else {
			AudioListener.volume = DEFAULT_VOLUME;
			PlayerPrefs.SetFloat(VOLUME_KEY, DEFAULT_VOLUME);
		}
	}

	public void UpdateSettingsPanel() {
		Slider volumeSlideScript = volumeSliderObject.GetComponent<Slider>();

		volumeSlideScript.value = PlayerPrefs.GetFloat(VOLUME_KEY);
	}
}
