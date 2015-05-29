using UnityEngine;
using System.Collections;

public class BackButtonScript : MonoBehaviour {

	public GameObject objectToClose;

	public void CloseMenu() {
		Destroy(objectToClose);
	}
}
