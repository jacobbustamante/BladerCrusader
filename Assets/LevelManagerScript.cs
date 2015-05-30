using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManagerScript : MonoBehaviour {

	public GameObject enemySpawnPoint;
	public List<GameObject> spawnPoints;
	public List<EnemySpawnPointScript> spawnPointScripts;
	public float levelCooldown = 5.0f;

	private bool inCooldown = false;
	private const int pointsPerLevelMultiplier = 100;

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
		if (!inCooldown && IsWaveComplete()) {
			StartCoroutine(Cooldown(levelCooldown));
		}
	}

	private void CreateSpawnPoints() {
		int level = GameManagerScript.gameManager.level;
		int mapWidth = GameManagerScript.gameManager.mapWidth;
		int mapHeight = GameManagerScript.gameManager.mapHeight;
		for (int i = 0; i < level + 2; i++) {
			GameObject spawnPoint = Object.Instantiate(enemySpawnPoint);
			spawnPoint.transform.position = new Vector3(Random.Range(1, mapWidth-2), Random.Range(1, mapHeight-2), 0);
			spawnPoints.Add(spawnPoint);
			spawnPointScripts.Add(spawnPoint.GetComponent<EnemySpawnPointScript>());

		}
	}

	private void AddSpawnPoint(int level) {
		int mapWidth = GameManagerScript.gameManager.mapWidth;
		int mapHeight = GameManagerScript.gameManager.mapHeight;

		GameObject spawnPoint = Object.Instantiate(enemySpawnPoint);
		EnemySpawnPointScript spawnPointScript = spawnPoint.GetComponent<EnemySpawnPointScript>();
		spawnPoint.transform.position = new Vector3(Random.Range(1, mapWidth-2), Random.Range(1, mapHeight-2), 0);

		spawnPoints.Add(spawnPoint);
		spawnPointScripts.Add(spawnPointScript);
	}

	private IEnumerator Cooldown(float seconds) {
		inCooldown = true;
		yield return new WaitForSeconds(seconds);
		StartNextWave();
		inCooldown = false;
	}

	private void StartLevel() {
		GameManagerScript.gameManager.StartLevel ();
		SendWave();
	}

	private void SendWave() {
		HUDInfoScript.UpdateInfo();
		foreach (GameObject spawnPoint in spawnPoints) {
			spawnPoint.GetComponent<EnemySpawnPointScript>().StartWave();
		}
	}

	private void StartNextWave() {
		if (GameManagerScript.gameManager.wave >= GameManagerScript.MAX_WAVE) {
			IncrementLevel();
			GameManagerScript.gameManager.wave = GameManagerScript.STARTING_WAVE;
		}
		else {
			GameManagerScript.gameManager.wave++;
		}
		SendWave();
	}

	private void IncrementLevel() {
		GameManagerScript.gameManager.level++;
		AddSpawnPoint(GameManagerScript.gameManager.level);
	}

	private bool IsWaveComplete() {
		bool isComplete = true;
		foreach (EnemySpawnPointScript spawnPoint in spawnPointScripts) {
			isComplete &= spawnPoint.done;
		}
		return isComplete;
	}
}
