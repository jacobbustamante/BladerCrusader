using UnityEngine;
using System.Collections;

//For bomb, it uses a circle collider
//[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class ProjectileScript : MonoBehaviour {

	const int SWORD = 0;
	const int AXE = 1;
	const int DAGGER = 2;
	const int BOLT = 3;
	const int BOMB = 4;
	const int SPIKE = 5;

	public float speed = 1.0f;
	public int damage = 1;
	public float attackCooldown = 0.1f;

	private Rigidbody2D rb;
	private string targetTag;
	private int weaponType;
	public int numberOfHits;
	private HeroScript originator;
	private Animator anim;
	private Renderer rend;


	void Awake() {
		rb = this.GetComponent<Rigidbody2D>();
		rend = this.GetComponent<Renderer> ();
	}

	// Use this for initialization
	void Start () {
		anim = this.GetComponent<Animator> ();
		if (weaponType == SPIKE) {
			anim.SetInteger("Color", Random.Range(0, 2));
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (weaponType == AXE) {
			rb.rotation += 5;
		} else if (weaponType == SPIKE) {
			this.transform.rotation = Quaternion.identity;
		}
//		if (numberOfHits <= 0) {
//			Destroy (gameObject, 0);
//			originator.DecreaseActiveAttacks(weaponType);
//		}
	}

	public void SetWeaponType(int type){
		this.weaponType = type;
	}

	public void SetTargetTag(string targetTag) {
		this.targetTag = targetTag;
	}

	public void SetTargetLocation(Vector3 location) {
		Vector2 direction = location - this.transform.position;
		rb.velocity = direction.normalized * speed;
		if(location.x > this.transform.position.x) {
			rb.rotation -= Vector3.Angle (new Vector3(0, 1, 0), direction.normalized);
		} else {
			rb.rotation += Vector3.Angle (new Vector3(0, 1, 0), direction.normalized);
		}
		//Debug.Log ("rot: " + rb.rotation);
	}

	public void SetTargetLocationAtAngle(Vector3 location, float angle) {
		Vector2 direction = location - this.transform.position;
		Vector2 newDirection = RotateVector(direction, angle);
		rb.velocity = newDirection.normalized * speed;
		if(newDirection.x > 0) {
			rb.rotation -= Vector3.Angle (new Vector3(0, 1, 0), newDirection.normalized);
		} else {
			rb.rotation += Vector3.Angle (new Vector3(0, 1, 0), newDirection.normalized);
		}
		//Debug.Log ("rot: " + rb.rotation);
	}

	public void SetOriginator(HeroScript orig){
		originator = orig;
	}

	//Had this, but decided not to. May still be useful at somepoint, so Nathan'll ask ya guys
//	private void DestroyIfDead() {
//		if (numberOfHits <= 0) {
//			Destroy(gameObject, 0);
//		}
//	}
	
	// Triggers
	void OnTriggerEnter2D(Collider2D coll)
	{
		bool hitTarget = coll.gameObject.CompareTag(targetTag);
		//Sometimes this gets a NULL exception, but I can't get this to occur reliably. I think it has to do with the axe




		if (coll.gameObject.CompareTag("Wall")) {
			numberOfHits = 0;
			if (numberOfHits <= 0) {
				if(weaponType == BOMB) {
					//Instantiate explosion
				}
				Destroy(gameObject, 0);
				originator.DecreaseActiveAttacks(weaponType);
				
			} 
		}
		if (hitTarget && targetTag == "Player") {
			HeroScript hero = coll.gameObject.GetComponent<HeroScript>();
			numberOfHits--;
			if (numberOfHits <= 0) {
				Destroy(gameObject, 0);
			
			//If we wanna have limits on their attacks per screen
	//			 if(originator == "Enemy") { 
	//				hero.DecreaseActiveAttacks();
	//			}
			}

			// have hero attacked here
		}
		else if (hitTarget && targetTag == "Enemy") {
			EnemyScript enemy = coll.gameObject.GetComponent<EnemyScript>();
			numberOfHits--;
			if (numberOfHits <= 0) {
				if(weaponType == BOMB) {
					//Instantiate explosion
				}
				Destroy(gameObject, 0);
				originator.DecreaseActiveAttacks(weaponType);

			}


			enemy.Attacked(damage);
		}
		

		//Nathan - would like to ask about this - why this structure with the wall??
//		if (hitTarget || coll.gameObject.CompareTag("Wall")) {
//			if (hitTarget && targetTag == "Player") {
//				HeroScript hero = coll.gameObject.GetComponent<HeroScript>();
//				// have hero attacked here
//			}
//			else if (hitTarget && targetTag == "Enemy") {
//				EnemyScript enemy = coll.gameObject.GetComponent<EnemyScript>();
//				enemy.Attacked(damage);
//			}
//			
//			Destroy(this.gameObject);
//		}
	}

	void OnBecameInvisible() {
		Destroy(gameObject, 0);
		originator.DecreaseActiveAttacks(weaponType);
	}

	private Vector2 RotateVector(Vector2 v, float degrees) {
		float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
		float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);
		float tx = v.x;
		float ty = v.y;
		
		return new Vector2((cos * tx) - (sin * ty), (sin * tx) + (cos * ty));
	}
}
