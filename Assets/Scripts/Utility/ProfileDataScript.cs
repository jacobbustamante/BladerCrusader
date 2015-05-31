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
	public string username;
	public int score;
	public GameManagerScript.Difficulty difficulty;

	// character
	public int playerLevel;
	public int curHealth;
	public int maxHealth;
	public int[] weaponLevels;
	public float[] colors;

	// level
	public string curSceneName;
	public int curLevel;
	public int curWave;

	public ProfileDataScript() {
		profileName = "0";
		username = "0";
		score = 0;
		
		totalTimePlayed = 0;
		playerLevel = 1;
		curHealth = 10;
		maxHealth = 10;
		weaponLevels = null;
		colors = new float[]{1,1,1};
		difficulty = GameManagerScript.Difficulty.MEDIUM;
		
		curSceneName = "Game";
		curLevel = 1;
		curWave = 1;
	}

	public ProfileDataScript(string name, string username) {
		profileName = name;
		this.username = username;
		score = 0;

		totalTimePlayed = 0;
		playerLevel = 1;
		curHealth = 10;
		maxHealth = 10;
		weaponLevels = null;
		colors = new float[]{1,1,1};
		difficulty = GameManagerScript.Difficulty.MEDIUM;

		curSceneName = "Game";
		curLevel = 1;
		curWave = 1;
	}
}
