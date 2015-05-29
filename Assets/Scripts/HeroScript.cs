using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class HeroScript : MonoBehaviour {

	const int SWORD = 0;
	const int AXE = 1;
	const int DAGGER = 2;
	const int BOLT = 3;
	const int BOMB = 4;
	const int SPIKE = 5;

	public int hitPoints = 10;
	public int baseAttack = 1;
	public int baseDefense = 1;
	public int baseSpeed = 1;
	public int[] maxActiveAttacks;
	public int[] curActiveAttacks;

	public List<GameObject> attacks;

	private Rigidbody2D rb;
	private int curAttack = 0;

	void Awake() {
		rb = GetComponent<Rigidbody2D> ();
	}

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		curActiveAttacks[SWORD] = 0;
		curActiveAttacks[AXE] = 0;
		curActiveAttacks[DAGGER] = 0;
		curActiveAttacks[BOLT] = 0;
		curActiveAttacks[BOMB] = 0;
		curActiveAttacks[SPIKE] = 0;
		maxActiveAttacks[SWORD] = 2;
		maxActiveAttacks[AXE] = 1000;
		maxActiveAttacks[DAGGER] = 4;
		maxActiveAttacks[BOLT] = 8;
		maxActiveAttacks[BOMB] = 2;
		maxActiveAttacks[SPIKE] = 4;

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Weapon1")) { //Sword, "1"
			curAttack = 0;
		}
		if (Input.GetButtonDown ("Weapon2")) { //Axe, "2"
			curAttack = 1;
		}
		if (Input.GetButtonDown ("Weapon3")) { //Dagger, "3"
			curAttack = 2;
		}
		if (Input.GetButtonDown ("Weapon4")) { //Dagger, "3"
			curAttack = 3;
		}
		if (Input.GetButtonDown ("Weapon5")) { //Dagger, "3"
			curAttack = 4;
		}
		if (Input.GetButtonDown ("Weapon6")) { //Dagger, "3"
			curAttack = 5;
		}

	}

	// Update is called once per physics frame
	void FixedUpdate () {
		rb.velocity = new Vector2 (Input.GetAxisRaw ("Horizontal") * baseSpeed, Input.GetAxisRaw ("Vertical") * baseSpeed);

		if (Input.GetButtonDown("Fire1")) {
			//Debug.Log("curActiveAttacks[" + curAttack+ "]: " + curActiveAttacks[curAttack]);
			if(curActiveAttacks[curAttack] < maxActiveAttacks[curAttack]){
				curActiveAttacks[curAttack]++;
				FireProjectile();
			}
		}

		// Test build code for Fallessi install
		// GetComponent<SpriteRenderer>().color = new Color(rb.position.x, rb.position.y, 0.5f);
	}

	private void FireProjectile() {
		Vector3 target = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		GameObject projectile = Object.Instantiate(attacks[curAttack]);
		projectile.transform.position = this.transform.position;
		ProjectileScript projScript = projectile.GetComponent<ProjectileScript>();
		if (curAttack == BOLT) {
			//projScript.SetTargetLocation (Vector3.RotateTowards(projectile.transform.position, target, 30.0f, 0.0f));
			projScript.SetTargetLocationAtAngle(target, 30.0f);
		} else {
			projScript.SetTargetLocation (target);
		}
		projScript.SetTargetTag("Enemy");
		projScript.SetWeaponType(curAttack);
		projScript.SetOriginator (this);
		if (curAttack == BOLT) {
			GameObject projectile2 = Object.Instantiate(attacks[curAttack]);
			projectile2.transform.position = this.transform.position;
			ProjectileScript projScript2 = projectile2.GetComponent<ProjectileScript>();
			//projScript2.SetTargetLocation(Vector3.RotateTowards(projectile2.transform.position, target, -30.0f, 0.0f));
			projScript2.SetTargetLocationAtAngle(target, -30.0f);
			projScript2.SetTargetTag("Enemy");
			projScript2.SetWeaponType(curAttack);
			projScript2.SetOriginator (this);
			curActiveAttacks[curAttack]++;
		}

	}

	private Vector2 GetMousePos() {
		Vector3 worldLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		return new Vector2(worldLocation.x, worldLocation.y);
	}

	public void DecreaseActiveAttacks(int type){
		curActiveAttacks[type]--;
	}


}
