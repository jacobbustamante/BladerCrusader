using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManagerScript : MonoBehaviour {

	public GameObject enemySpawnPoint;
	public List<GameObject> spawnPoints;
	public List<EnemySpawnPointScript> spawnPointScripts;
	public float levelCooldown = 5.0f;

	private bool inCooldown = false;

	void Awake() {
		spawnPoints = new List<GameObject>();
		spawnPointScripts = new List<EnemySpawnPointScript>();
	}

	// Use this for initialization
	void Start () {
		CreateSpawnPoints ();
		StartLevel();
	}
	
	// Update is called once per frame
	void Update () {
		if (!inCooldown && IsLevelComplete()) {
			StartCoroutine(Cooldown(levelCooldown));
		}
	}

	void CreateSpawnPoints() {
		int level = GameManagerScript.gameManager.level;
		int mapWidth = GameManagerScript.gameManager.mapWidth;
		int mapHeight = GameManagerScript.gameManager.mapHeight;
		for (int i = 0; i < level; i++) {
			GameObject spawnPoint = Object.Instantiate(enemySpawnPoint);
			spawnPoint.transform.position = new Vector3(Random.Range(0, mapWidth), Random.Range(0, mapHeight), 0);
			spawnPoints.Add(spawnPoint);
			spawnPointScripts.Add(spawnPoint.GetComponent<EnemySpawnPointScript>());

		}
	}

	private IEnumerator Cooldown(float seconds) {
		inCooldown = true;
		yield return new WaitForSeconds(seconds);
		StartNextLevel();
		inCooldown = false;
	}

	private void StartLevel() {
		GameManagerScript.gameManager.StartLevel ();
		foreach (GameObject spawnPoint in spawnPoints) {
			spawnPoint.GetComponent<EnemySpawnPointScript>().StartLevel();
		}
	}

	private void StartNextLevel() {
		IncrementLevel();
		foreach (GameObject spawnPoint in spawnPoints) {
			spawnPoint.GetComponent<EnemySpawnPointScript>().StartLevel();
		}
	}

	private void IncrementLevel() {
		foreach (EnemySpawnPointScript spawnPoint in spawnPointScripts) {
			spawnPoint.incrementLevel();
		}
	}

	private bool IsLevelComplete() {
		bool isComplete = true;
		foreach (EnemySpawnPointScript spawnPoint in spawnPointScripts) {
			isComplete &= spawnPoint.done;
		}
		return isComplete;
	}
}
