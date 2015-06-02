using UnityEngine;
using System.Collections;

public class GameOverScript : MonoBehaviour {
	
	void Start () {
		ResetProfile();
	}
	
	private void ResetProfile() {
		ProfileDataScript oldProfile = GameManagerScript.gameManager.GetProfile();
		ProfileDataScript newProfile = new ProfileDataScript(oldProfile.profileName, oldProfile.username);
		newProfile.colors = (float[])oldProfile.colors.Clone();
		newProfile.difficulty = oldProfile.difficulty;

		string fileName = ProfileDataScript.saveFileName + oldProfile.profileName + ProfileDataScript.saveFileExt;
		PersistenceScript.SaveFile(fileName, newProfile);
	}

	public void PlayNewGame() {
		string profileName = GameManagerScript.gameManager.GetProfile().profileName;
		string fileName = ProfileDataScript.saveFileName + profileName + ProfileDataScript.saveFileExt;
		ProfileDataScript profile = (ProfileDataScript)PersistenceScript.LoadFile(fileName);
		QuitGameScript.QuitGame();
		this.GetComponent<LoadGameScript>().LoadGameFromProfile(profile);
	}
}
