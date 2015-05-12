using UnityEngine;
using System.Collections;

public class EnemyPool : MonoBehaviour {

	public GameObject Org;
	public GameObject HighOrg;
	public GameObject BezerkerOrg;
	public GameObject Specty;
	public GameObject StakeHolder;
	public GameObject Wizlock;
	public GameObject Snell;

	public static EnemyPool enemyPool;

	void Awake() {
		if (enemyPool == null) {
			DontDestroyOnLoad(this.gameObject);
			enemyPool = this;
		} else if (enemyPool != this) {
			Destroy (this.gameObject);
		} 
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
