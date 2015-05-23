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
	private int weaponType;
	public int numberOfHits;
	private HeroScript originator;


	void Awake() {
		rb = this.GetComponent<Rigidbody2D>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (weaponType == 1) {
			rb.rotation += 5;
		}
		if (numberOfHits <= 0) {
			Destroy (gameObject, 0);
		} else {

		}
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
		rb.rotation += Vector3.Angle (new Vector3(0, 1, 0), location);
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

}
