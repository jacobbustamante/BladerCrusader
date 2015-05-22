using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class ProjectileScript : MonoBehaviour {

	public float speed = 1.0f;
	public int damage = 1;
	public float attackCooldown = 0.1f;

	private Rigidbody2D rb;
	private string targetTag;

	void Awake() {
		rb = this.GetComponent<Rigidbody2D>();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetTargetTag(string targetTag) {
		this.targetTag = targetTag;
	}

	public void SetTargetLocation(Vector3 location) {
		Vector2 direction = location - this.transform.position;
		rb.velocity = direction.normalized * speed;
	}
	
	// Triggers
	void OnTriggerEnter2D(Collider2D coll)
	{
		bool hitTarget = coll.gameObject.CompareTag(targetTag);

		if (hitTarget || coll.gameObject.CompareTag("Wall")) {
			if (hitTarget && targetTag == "Player") {
				HeroScript hero = coll.gameObject.GetComponent<HeroScript>();
				// have hero attacked here
			}
			else if (hitTarget && targetTag == "Enemy") {
				EnemyScript enemy = coll.gameObject.GetComponent<EnemyScript>();
				enemy.Attacked(damage);
			}

			Destroy(this.gameObject);
		}
	}

}
