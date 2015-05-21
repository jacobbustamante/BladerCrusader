using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class EnemyScript : MonoBehaviour {

	public int level = 1;
	public int hitPoints = 10;
	public int baseAttack = 1;
	public int baseDefense = 1;
	public int baseSpeed = 1;

	protected EnemySpawnPointScript originSpawnPoint;
	protected Rigidbody2D rb;
	protected GameObject target;

	protected virtual void Awake() {
		rb = this.GetComponent<Rigidbody2D>();
	}

	// Use this for initialization
	protected virtual void Start () {
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

	protected void OnDeath() {
		if (originSpawnPoint) {
			originSpawnPoint.SpawnedEnemyDefeated();
		}
		Destroy(this.gameObject);
	}

	protected void MoveDirectlyTowardsTarget() {
		if (target != null) {
			Vector2 direction = target.transform.position - this.transform.position;
			rb.velocity = direction.normalized * baseSpeed;
		}
	}
	
	protected void UpdateTarget() {
		if (target == null)
			target = GameManagerScript.gameManager.GetPlayerInstance();
	}

}
