using UnityEngine;
using System.Collections;

public class AxeScript : ProjectileScript {

	private Rigidbody2D rbody;

	// Use this for initialization
	void Start () {
	
	}

	void Awake() {
		rbody = this.GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update () {
		rbody.rotation += 5;

	}
}
