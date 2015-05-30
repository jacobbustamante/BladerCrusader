using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public class DeleteProfileScript : MonoBehaviour {

	public string profileFileName;
	public bool isButton = true;

	private Button button;
	private string fileName, filePath;

	void Start() {
		fileName = ProfileDataScript.saveFileName + profileFileName + ProfileDataScript.saveFileExt;
		filePath = Application.persistentDataPath + fileName;

		if (isButton) {
			button = this.GetComponent<Button>();
			DeactivateIfNoProfile();
		}
	}

	public void DeleteProfile() {
		Debug.Log("Deleting " + filePath);
		File.Delete(filePath);
	}

	public void DeactivateIfNoProfile() {
		if (isButton) {
			button.interactable = (PersistenceScript.LoadFile(fileName) != null);
		}
	}
}
