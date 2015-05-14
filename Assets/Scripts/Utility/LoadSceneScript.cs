using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class LoadSceneScript : MonoBehaviour {

	public Scene sceneToLoad;
	
	public enum Scene {
		Menu,
		Game,
		Placeholder
	}
	
	public static readonly Dictionary<Scene, String> SceneToString = new Dictionary<Scene, String>()
	{
		{Scene.Menu, "Menu"},
		{Scene.Game, "Game"},
		{Scene.Placeholder, "Placeholder"}
	};
	
	public void LoadScene() {
		Application.LoadLevel(SceneToString[sceneToLoad]);
	}
	
	public static void LoadSceneFromString(String sceneName) {
		Application.LoadLevel(sceneName);
	}
}
