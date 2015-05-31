using UnityEngine;
using System.Collections;

public class GameStartScript : MonoBehaviour {

	public void StartGame() {
		GameManagerScript.gameManager.UnpauseGame();
	}
}
