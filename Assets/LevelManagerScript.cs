using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManagerScript : MonoBehaviour {

	public GameObject enemySpawnPoint;
	public List<GameObject> spawnPoints;

	void Awake() {
		spawnPoints = new List<GameObject>();
	}

	// Use this for initialization
	void Start () {
		CreateSpawnPoints ();
		StartLevel();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void CreateSpawnPoints() {
		int level = GameManagerScript.gameManager.level;
		int mapWidth = GameManagerScript.gameManager.mapWidth;
		int mapHeight = GameManagerScript.gameManager.mapHeight;
		for (int i = 0; i < level; i++) {
			GameObject spawnPoint = Object.Instantiate(enemySpawnPoint);
			spawnPoint.transform.position = new Vector3(Random.Range(0, mapWidth), Random.Range(0, mapHeight), 0);
			spawnPoints.Add(spawnPoint);

		}
	}

	private void StartLevel() {
		GameManagerScript.gameManager.StartLevel ();
		foreach (GameObject spawnPoint in spawnPoints) {
			spawnPoint.GetComponent<EnemySpawnPointScript>().StartLevel();
		}
	}

}
