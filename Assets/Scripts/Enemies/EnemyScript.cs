using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class EnemyScript : MonoBehaviour {

	public int level = 1;
	public int hitPoints = 10;
	public int maxHitPoints = 10;
	public int attack = 1;
	private int speed = 1;
	public int baseHitPoints = 10;
	public int baseAttack = 1;
	public int baseSpeed = 1;

	protected EnemySpawnPointScript originSpawnPoint;
	protected Rigidbody2D rb;
	protected GameObject target;
	protected string targetTag = "Player";

	protected const int pointsMultiplier = 10;
	protected const float attackTimeout = 2.0f;
	protected const float hitTimeout = 0.5f;
	protected float attackTimer = 0;

	protected virtual void Awake() {
		rb = this.GetComponent<Rigidbody2D>();
	}

	// Use this for initialization
	protected virtual void Start () {
		UpdateStats();
		UpdateTarget();
	}

	// Update is called once per frame
	protected virtual void Update () {
		UpdateTarget();
	}

	public void Attacked(int damage) {
		hitPoints -= damage;
		if (hitPoints <= 0) {
			OnDeath();
		}
	}

	public void SetSpawnPoint(EnemySpawnPointScript spawnPoint) {
		originSpawnPoint = spawnPoint;
	}

	public void SetLevel(int level) {
		this.level = level;
		UpdateStats();
	}

	protected void OnDeath() {
		if (originSpawnPoint) {
			originSpawnPoint.SpawnedEnemyDefeated();
		}
		GameManagerScript.gameManager.AddScore(pointsMultiplier * level);
		Destroy(this.gameObject);
	}

	protected void MoveDirectlyTowardsTarget() {
		if (target != null) {
			Vector2 direction = target.transform.position - this.transform.position;
			rb.velocity = direction.normalized * speed;
		}
	}
	
	protected void UpdateTarget() {
		if (target == null)
			target = GameManagerScript.gameManager.GetPlayerInstance();
	}
	
	private void HitPlayer(GameObject player) {
		attackTimer = Time.time + attackTimeout;
		player.GetComponent<HeroScript>().HitByEnemy(this.gameObject);
	}

	private void HitByProjectile() {
		attackTimer = Time.time + hitTimeout;
	}

	private void UpdateStats() {
		maxHitPoints = baseHitPoints + (level / 4);
		attack = baseAttack + (level / 4);
		speed = baseSpeed + (level / 8);
	}

	// Collissions
	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.CompareTag("Player")) {
			HitPlayer(coll.gameObject);
		}
	}

	void OnCollisionStay2D(Collision2D collisionInfo) {
		if (collisionInfo.gameObject.CompareTag("Player")) {
			HitPlayer(collisionInfo.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.CompareTag("Projectile")) {
			//This doesn't seem to work :/ ...
			rb.AddForce(10*(this.transform.position - coll.gameObject.transform.position));
			//Debug.Log("push");
			if (Time.time > attackTimer) {
				HitByProjectile();
			}
		}
	}
}
