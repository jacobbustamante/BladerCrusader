using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class ProfileDataScript {

	public const string saveFileName = "/profile-";
	public const string saveFileExt = ".dat";

	public string profileName;

	// meta
	public float totalTimePlayed;

	// character
	public int playerLevel;

	// level
	public string curSceneName;
	public int curLevel;

	public ProfileDataScript() {
		profileName = "0";
		
		totalTimePlayed = 0;
		playerLevel = 1;
		
		curSceneName = "Game";
		curLevel = 1;
	}

	public ProfileDataScript(string name) {
		profileName = name;

		totalTimePlayed = 0;
		playerLevel = 1;

		curSceneName = "Game";
		curLevel = 1;
	}
}
