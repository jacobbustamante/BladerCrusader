using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuPlayButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.GetComponent<Button>().onClick.AddListener(this.OnClick);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClick() {
		Application.LoadLevel("Placeholder");
	}
}
