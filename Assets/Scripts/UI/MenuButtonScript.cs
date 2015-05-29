using UnityEngine;
using System.Collections;

public class MenuButtonScript : MonoBehaviour {

	public GameObject objectToOpen;
	public GameObject objectToClose;

	public void OpenMenu() {
		GameObject menu = Object.Instantiate(objectToOpen);
		menu.transform.SetParent(transform.parent);

		RectTransform rt = menu.GetComponent<RectTransform>();
		rt.offsetMin = Vector2.zero;
		rt.offsetMax = Vector2.zero;
	}

	public void CloseMenu() {
		Destroy(objectToClose);
	}

}
