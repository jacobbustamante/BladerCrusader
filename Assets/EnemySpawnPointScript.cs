using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawnPointScript : MonoBehaviour {

	public int enemyCountToSpawn;
	public List<GameObject> enemyTypes;
	public float spawnDelay;
	public bool done;
	public int enemiesDefeated;
	int level;

	private float nextSpawnTime;

	// Use this for initialization
	void Start () {
		level = GameManagerScript.gameManager.level;


	}
	
	// Update is called once per frame
	void Update () {
		if (enemyCountToSpawn > 0 && Time.time > nextSpawnTime) {
			SpawnEnemy();
			enemyCountToSpawn--;
			nextSpawnTime = Time.time + spawnDelay;
		}
	}

	public void StartLevel() {
		SetSpawnPointParams();
		nextSpawnTime = Time.time + spawnDelay;
	}

	public void SetSpawnPointParams() {

		if (level < 3) {
			spawnDelay = 5.0f;
			enemyCountToSpawn = 3;
			enemyTypes.Add(EnemyPool.enemyPool.Org);
			enemyTypes.Add(EnemyPool.enemyPool.Specty);
			enemyTypes.Add(EnemyPool.enemyPool.Snell);

		} else if (level < 6) {
			spawnDelay = 4.0f;

		} else if (level < 9) {
			spawnDelay = 3.0f;
		} else if (level < 12) {
			spawnDelay = 2.0f;
		} else if (level < 15) {
			spawnDelay = 1.0f;
		} else {
			spawnDelay = 0.0f;
		}
	}

	private void SpawnEnemy() {
		if (enemyTypes.Count > 0) {
			int index = Random.Range(0, enemyTypes.Count);
			GameObject newEnemy =  (GameObject) Object.Instantiate(enemyTypes[index]);
			newEnemy.transform.position = this.transform.position;
		}
	}

}
