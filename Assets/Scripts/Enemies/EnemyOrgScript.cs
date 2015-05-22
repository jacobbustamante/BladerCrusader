using UnityEngine;
using System.Collections;

public class EnemyOrgScript : EnemyScript {



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

}
