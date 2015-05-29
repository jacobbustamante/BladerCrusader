using UnityEngine;
using System.Collections;

public class QuitGameScript : MonoBehaviour {
	
	public void QuitGame() {
		if (GameManagerScript.gameManager != null) {
			Destroy(GameManagerScript.gameManager.gameObject);
			GameManagerScript.gameManager = null;
		}

		Time.timeScale = 1;
	}
	
	public void SaveProfile() {
		if (GameManagerScript.gameManager != null) {
			string profileName = GameManagerScript.gameManager.GetProfile().profileName;
			string fileName = ProfileDataScript.saveFileName + profileName + ProfileDataScript.saveFileExt;
			
			GameManagerScript.gameManager.SaveToProfile();
			
			PersistenceScript.SaveFile(fileName, GameManagerScript.gameManager.GetProfile());
		}
	}
	
	public void QuitApplication() {
		Application.Quit();
	}
}
