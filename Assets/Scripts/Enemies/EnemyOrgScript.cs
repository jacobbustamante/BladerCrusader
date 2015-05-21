using UnityEngine;
using System.Collections;

public class EnemyOrgScript : EnemyScript {

	private const float attackTimeout = 2.0f;
	private float attackTimer = 0;

	protected override void Awake() {
		base.Awake();
	}

	// Use this for initialization
	protected override void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update();

		if (Time.time > attackTimer) {
			MoveDirectlyTowardsTarget();
		}
		else {
			rb.velocity = Vector2.zero;
		}
	}

	private void HitPlayer() {
		// hurt player
		attackTimer = Time.time + attackTimeout;
	}

	// Collissions
	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.CompareTag("Player")) {
			HitPlayer();

			// enemy is attacked on collission too just for dev testing
			Attacked(4);
		}
	}
	
	// Triggers
	void OnTriggerEnter2D(Collider2D coll)
	{

	}
}
