using UnityEngine;
using System.Collections;

public class PauseMenuScript : MonoBehaviour {

	public GameObject pauseScreenPrefab;
	public GameObject parentCanvas;
	public static PauseMenuScript pauseMenu;

	private bool inPause = false;
	private GameObject curPauseInstance;

	void Awake() {
		if (pauseMenu == null) {
			pauseMenu = this;
		} else if (pauseMenu != this) {
			Destroy (this.gameObject);
		} 
	}

	void Update () {
		if (Input.GetButtonDown("Pause") && !inPause && !GameManagerScript.gameManager.gameOver) {
			Time.timeScale = 0;
			inPause = true;
			OpenPauseMenu();
		}
	}

	private void OpenPauseMenu() {
		curPauseInstance = Object.Instantiate(pauseScreenPrefab);
		curPauseInstance.transform.SetParent(parentCanvas.transform);
		
		RectTransform rt = curPauseInstance.GetComponent<RectTransform>();
		rt.offsetMin = Vector2.zero;
		rt.offsetMax = Vector2.zero;
	}

	public void ClosePauseMenu() {
		inPause = false;
		Destroy(curPauseInstance);
		Time.timeScale = 1;
	}

}