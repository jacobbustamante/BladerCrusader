using UnityEngine;
using System.Collections;

public class LoadGameScript : MonoBehaviour {

	public GameObject gameManagerPrefab;

	public void LoadNewGame() {
		Object.Instantiate(gameManagerPrefab);

		LoadSceneScript.LoadSceneFromString(LoadSceneScript.SceneToString[LoadSceneScript.Scene.Game]);
	}

	public void LoadGameFromProfile(ProfileDataScript profile) {
		GameObject gameManagerObject = Object.Instantiate(gameManagerPrefab);
		GameManagerScript gameManager = gameManagerObject.GetComponent<GameManagerScript>();

		gameManager.LoadFromProfile(profile);
		LoadSceneScript.LoadSceneFromString(profile.curSceneName);
	}
}
