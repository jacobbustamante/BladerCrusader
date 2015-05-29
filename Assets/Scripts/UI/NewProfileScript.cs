using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NewProfileScript : MonoBehaviour {
	
	public InputField nameInputObject;

	private string profileFileName;

	public void SetFileName(string fileName) {
		profileFileName = fileName;
	}

	public void PlayGame() {
		ProfileDataScript profile = new ProfileDataScript(profileFileName, nameInputObject.text);
		this.GetComponent<LoadGameScript>().LoadGameFromProfile(profile);
	}
}
