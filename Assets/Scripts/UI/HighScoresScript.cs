using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class HighScoresScript : MonoBehaviour {

	private const int MAX_SCORES = 10;

	public Text nameText;
	public Text scoreText;
	public bool insertHighScore = false;

	private int[] scoreList;
	private string[] nameList;
	private HighScoresDataScript dataScript;

	// Use this for initialization
	void Start () {
		GetHighScoresData();
		if (insertHighScore) {
			InsertHighScore();
			SaveHighScoresData();
		}
		UpdateHighScoreText();
	}

	public void UpdateHighScoreText() {
		string displayText = "";
		string numberText = "";
		for (int i = 0; scoreList!= null && i < scoreList.Length; i++) {
			displayText += (i+1).ToString() + "\t";
			displayText += nameList[i].ToString() + "\n";
			numberText += scoreList[i].ToString() + "\n";
		}
		nameText.text = displayText;
		scoreText.text = numberText;
	}

	public void InsertHighScore() {
		string newName = GameManagerScript.gameManager.GetProfile().username;
		int newScore = GameManagerScript.gameManager.score;

		List<int> tempScores = new List<int>();
		List<string> tempNames = new List<string>();
		int newScoreIndex = -1;
		for (int i = 0; scoreList != null && i < scoreList.Length && i < MAX_SCORES; i++) {
			if (newScore > scoreList[i] && newScoreIndex < 0) {
				tempScores.Add(newScore);
				tempNames.Add (newName);
				newScoreIndex = i;
			}
			tempScores.Add(scoreList[i]);
			tempNames.Add (nameList[i]);

		}
		if (newScoreIndex < 0 && (scoreList == null || scoreList.Length < 10)) {
			tempScores.Add(newScore);
			tempNames.Add (newName);
			newScoreIndex = tempScores.Count - 1;
		}

		scoreList = (int[])tempScores.ToArray();
		nameList = (string[])tempNames.ToArray();
		dataScript.scoreList = (int[])scoreList.Clone();
		dataScript.nameList = (string[])nameList.Clone();
	}

	private void GetHighScoresData() {
		string fileName = HighScoresDataScript.fileName + HighScoresDataScript.fileExt;
		object data = PersistenceScript.LoadFile(fileName);

		if (data != null) {
			dataScript = (HighScoresDataScript) data;
			scoreList = (int[])dataScript.scoreList.Clone();
			nameList = (string[])dataScript.nameList.Clone();
		}
		else {
			dataScript = new HighScoresDataScript();
			scoreList = null;
			nameList = null;
		}
	}

	private void SaveHighScoresData() {
		string fileName = HighScoresDataScript.fileName + HighScoresDataScript.fileExt;
		PersistenceScript.SaveFile(fileName, dataScript);
	}
}

[Serializable]
public class HighScoresDataScript {
	public const string fileName = "/highscores";
	public const string fileExt = ".dat";

	public int[] scoreList;
	public string[] nameList;

	public HighScoresDataScript() {
		scoreList = null;;
		nameList = null;
	}
}
