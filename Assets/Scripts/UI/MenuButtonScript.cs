using UnityEngine;
using System.Collections;

public class MenuButtonScript : MonoBehaviour {

	public GameObject objectToOpen;
	public GameObject objectToClose;

	public void OpenMenu() {
		GameObject menu = Object.Instantiate(objectToOpen);
		Transform newParent = transform.parent;
		while (newParent.parent)
			newParent = newParent.parent;
		menu.transform.SetParent(newParent);

		RectTransform rt = menu.GetComponent<RectTransform>();
		rt.offsetMin = Vector2.zero;
		rt.offsetMax = Vector2.zero;
	}

	public void CloseMenu() {
		Destroy(objectToClose);
	}

	public void ClosePauseMenu() {
		PauseMenuScript.pauseMenu.ClosePauseMenu();
	}
}
