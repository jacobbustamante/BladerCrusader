using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class HeroScript : MonoBehaviour {

	const int SWORD = 0;
	const int AXE = 1;
	const int DAGGER = 2;
	const int BOLT = 3;
	const int BOMB = 4;
	const int SPIKE = 5;

	public int playerLevel = 1;
	public int hitPoints = 10;
	public int maxHitPoints = 10;
	private int speed = 1;
	public int baseHitPoints = 10;
	public int baseSpeed = 1;
	public int[] maxActiveAttacks;
	public int[] curActiveAttacks;
	public int[] weaponLevels;

	public List<GameObject> attacks;

	private Rigidbody2D rb;
	private SpriteRenderer sr;
	public int curAttack = 0;
	private bool canSwitchWeapon = true;
	private const float invincibiltyTimeout = 0.8f;
	private const float invincibiltyVisualTimeout = 0.2f;
	private const float knockbackTimeout = 0.3f;
	private float invincibiltyTimer = 0;
	private float knockbackTimer = 0;
	private float knockbackDistanceMulitplier = 5;


	void Awake() {
		rb = GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer>();
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

		UpdateStats();
	}
	
	// Update is called once per frame
	void Update () {
		// Switches weapon with q or e keydown
		int nextWeapon = (int) Input.GetAxisRaw("SwitchWeapons");
		if (nextWeapon != 0 && canSwitchWeapon) {
			if (curAttack == 0 && nextWeapon < 0)
				curAttack = attacks.Count - 1;
			else
				curAttack = (curAttack + nextWeapon) % attacks.Count;
			canSwitchWeapon = false;
			HUDWeaponsScript.UpdateInfo();
		}
		canSwitchWeapon = (nextWeapon == 0);

		if (Input.GetButtonDown ("Weapon1")) { //Sword, "1"
			curAttack = 0;
			HUDWeaponsScript.UpdateInfo();
		}
		else if (Input.GetButtonDown ("Weapon2")) { //Axe, "2"
			curAttack = 1;
			HUDWeaponsScript.UpdateInfo();
		}
		else if (Input.GetButtonDown ("Weapon3")) { //Dagger, "3"
			curAttack = 2;
			HUDWeaponsScript.UpdateInfo();
		}
		else if (Input.GetButtonDown ("Weapon4")) { //Dagger, "3"
			curAttack = 3;
			HUDWeaponsScript.UpdateInfo();
		}
		else if (Input.GetButtonDown ("Weapon5")) { //Dagger, "3"
			curAttack = 4;
			HUDWeaponsScript.UpdateInfo();
		}
		else if (Input.GetButtonDown ("Weapon6")) { //Dagger, "3"
			curAttack = 5;
			HUDWeaponsScript.UpdateInfo();
		}

	}

	// Update is called once per physics frame
	void FixedUpdate () {
		if (Time.time > knockbackTimer) {
			if (sr.color.a < 1 && Time.time > invincibiltyTimer - invincibiltyVisualTimeout)
				sr.color = Color.white;
			rb.velocity = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical")).normalized * speed;

			if (Input.GetButtonDown("Fire1")) {
				//Debug.Log("curActiveAttacks[" + curAttack+ "]: " + curActiveAttacks[curAttack]);
				if(curActiveAttacks[curAttack] < maxActiveAttacks[curAttack]){
					curActiveAttacks[curAttack]++;
					FireProjectile();
				}
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
			projScript.SetTargetLocation(target, 30.0f);
		} else {
			projScript.SetTargetLocation (target);
		}
		projScript.SetTargetTag("Enemy");
		projScript.SetWeaponType(curAttack);
		projScript.SetOriginator (this);
		projScript.SetLevel(weaponLevels[curAttack]);
		if (curAttack == BOLT) {
			GameObject projectile2 = Object.Instantiate(attacks[curAttack]);
			projectile2.transform.position = this.transform.position;
			ProjectileScript projScript2 = projectile2.GetComponent<ProjectileScript>();
			projScript2.SetTargetLocation(target, -30.0f);
			projScript2.SetTargetTag("Enemy");
			projScript2.SetWeaponType(curAttack);
			projScript2.SetOriginator (this);
			projScript.SetLevel(weaponLevels[curAttack]);
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

	private void UpdateStats() {
		maxHitPoints = baseHitPoints + (playerLevel / 4);
		speed = baseSpeed + (playerLevel / 8);
	}

	public void SetLevels(int playerLevel, int[] weaponLevels) {
		this.playerLevel = playerLevel;
		if (weaponLevels == null)
			this.weaponLevels = Enumerable.Repeat(1, attacks.Count).ToArray();
		else
			this.weaponLevels = (int[])weaponLevels.Clone();
	}

	public void HitByEnemy(GameObject enemy) {
		if (invincibiltyTimer < Time.time) { 
			EnemyScript enemyScript = enemy.GetComponent<EnemyScript>(); 

			hitPoints = Mathf.Max(hitPoints - enemyScript.attack, 0);
			HUDHealthScript.UpdateInfo();
			if (hitPoints <= 0) {
				OnDeath();
			}

			Vector3 knockback = (this.transform.position - enemy.transform.position) * knockbackDistanceMulitplier;
			rb.velocity = knockback;
			sr.color = new Color(1,1,1,.3f);

			invincibiltyTimer = Time.time + invincibiltyTimeout;
			knockbackTimer = Time.time + knockbackTimeout;
		}
	}

	private void OnDeath() {

	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.CompareTag("Enemy")) {
			HUDHealthScript.UpdateInfo();
		}
	}

}
