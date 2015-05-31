using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class PowerUpScript : MonoBehaviour {

	public enum PowerUpType {
		HealthUp,
		WeaponUp
	};

	public PowerUpType powerUpType;
	public int upHealth = 3;
	public int upMaxHealth = 1;
	public int upWeaponLevel = 1;
	
	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.CompareTag("Player")) {
			Retrieved();
		}
	}

	public void Retrieved() {
		Destroy(this.gameObject);
	}

}
