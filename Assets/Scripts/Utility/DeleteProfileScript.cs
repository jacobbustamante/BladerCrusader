using UnityEngine;
using System.Collections;
using System.IO;

public class DeleteProfileScript : MonoBehaviour {

	public string profileFileName;

	public void DeleteProfile() {
		string fileName = ProfileDataScript.saveFileName + profileFileName + ProfileDataScript.saveFileExt;
		string filePath = Application.persistentDataPath + fileName;

		Debug.Log("Deleting " + filePath);
		File.Delete(filePath);
	}
}
