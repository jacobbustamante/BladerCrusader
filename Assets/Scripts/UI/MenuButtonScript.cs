using UnityEngine;
using System.Collections;

public class MenuButtonScript : MonoBehaviour {

	public GameObject objectToOpen;
	public GameObject objectToClose;

	public void OpenMenu() {
		OpenMenu(objectToOpen, this.transform);
	}

	public static GameObject OpenMenu(GameObject toOpen, Transform myTransform) {
		GameObject menu = Object.Instantiate(toOpen);
		Transform newParent = myTransform.parent;
		while (newParent.parent)
			newParent = newParent.parent;
		menu.transform.SetParent(newParent);
		
		RectTransform rt = menu.GetComponent<RectTransform>();
		rt.offsetMin = Vector2.zero;
		rt.offsetMax = Vector2.zero;

		return menu;
	}

	public void CloseMenu() {
		Destroy(objectToClose);
	}

	public void ClosePauseMenu() {
		PauseMenuScript.pauseMenu.ClosePauseMenu();
	}
}
