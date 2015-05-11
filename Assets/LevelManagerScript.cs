using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManagerScript : MonoBehaviour {

	public GameObject enemySpawnPoint;
	public List<GameObject> spawnPoints;

	void Awake() {

	}

	// Use this for initialization
	void Start () {
		GameManagerScript.gameManager.StartLevel ();
		CreateSpawnPoints ();
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

}
