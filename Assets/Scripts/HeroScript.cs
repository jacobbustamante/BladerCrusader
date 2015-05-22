using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeroScript : MonoBehaviour {

	public int hitPoints = 10;
	public int baseAttack = 1;
	public int baseDefense = 1;
	public int baseSpeed = 1;

	public List<GameObject> attacks;

	private Rigidbody2D rb;
	private int curAttack = 0;

	void Awake() {
		rb = GetComponent<Rigidbody2D> ();
	}

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Update is called once per physics frame
	void FixedUpdate () {
		rb.velocity = new Vector2 (Input.GetAxisRaw ("Horizontal") * baseSpeed, Input.GetAxisRaw ("Vertical") * baseSpeed);

		if (Input.GetButtonDown("Fire1")) {
			FireProjectile();
		}

		// Test build code for Fallessi install
		GetComponent<SpriteRenderer>().color = new Color(rb.position.x, rb.position.y, 0.5f);
	}

	private void FireProjectile() {
		GameObject projectile = Object.Instantiate(attacks[curAttack]);
		projectile.transform.position = this.transform.position;
		ProjectileScript projScript = projectile.GetComponent<ProjectileScript>();
		projScript.SetTargetLocation(Camera.main.ScreenToWorldPoint(Input.mousePosition));
		projScript.SetTargetTag("Enemy");
	}

	private Vector2 GetMousePos() {
		Vector3 worldLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		return new Vector2(worldLocation.x, worldLocation.y);
	}
}
