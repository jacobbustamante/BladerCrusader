using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawnPointScript : MonoBehaviour {
	
	public List<GameObject> enemyTypes;
	public float spawnDelay;
	public int enemiesLeft;

	public int level;
	public int enemyCountToSpawn;
	public int enemiesDefeated;
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
		enemiesDefeated = 0;
		enemiesLeft = enemyCountToSpawn;
	}

	public void SetSpawnPointParams() {

		if (level < 3) {
			spawnDelay = 5.0f;
			enemyCountToSpawn = 3;

			enemyTypes.Clear();
			enemyTypes.Add(EnemyPool.enemyPool.Org);
			enemyTypes.Add(EnemyPool.enemyPool.Specty);
			enemyTypes.Add(EnemyPool.enemyPool.Snell);

		} else if (level < 6) {
			spawnDelay = 4.0f;
			enemyCountToSpawn = 3;

			enemyTypes.Clear();
			enemyTypes.Add(EnemyPool.enemyPool.Org);
			enemyTypes.Add(EnemyPool.enemyPool.Specty);
			enemyTypes.Add(EnemyPool.enemyPool.Snell);

		} else if (level < 9) {
			spawnDelay = 3.0f;
			enemyCountToSpawn = 3;

			enemyTypes.Clear();
			enemyTypes.Add(EnemyPool.enemyPool.Org);
			enemyTypes.Add(EnemyPool.enemyPool.Specty);
			enemyTypes.Add(EnemyPool.enemyPool.Snell);
		} else if (level < 12) {
			spawnDelay = 2.0f;
			enemyCountToSpawn = 3;

			enemyTypes.Clear();
			enemyTypes.Add(EnemyPool.enemyPool.Org);
			enemyTypes.Add(EnemyPool.enemyPool.Specty);
			enemyTypes.Add(EnemyPool.enemyPool.Snell);
		} else if (level < 15) {
			spawnDelay = 1.0f;
			enemyCountToSpawn = 3;

			enemyTypes.Clear();
			enemyTypes.Add(EnemyPool.enemyPool.Org);
			enemyTypes.Add(EnemyPool.enemyPool.Specty);
			enemyTypes.Add(EnemyPool.enemyPool.Snell);
		} else {
			spawnDelay = 0.0f;
			enemyCountToSpawn = 3;

			enemyTypes.Clear();
			enemyTypes.Add(EnemyPool.enemyPool.Org);
			enemyTypes.Add(EnemyPool.enemyPool.Specty);
			enemyTypes.Add(EnemyPool.enemyPool.Snell);
		}
	}

	private void SpawnEnemy() {
		if (enemyTypes.Count > 0) {
			int index = Random.Range(0, enemyTypes.Count);
			GameObject newEnemy =  (GameObject) Object.Instantiate(enemyTypes[index]);
			newEnemy.transform.position = this.transform.position;
			newEnemy.GetComponent<EnemyScript>().SetSpawnPoint(this);
		}
	}

	public void SpawnedEnemyDefeated() {
		enemiesLeft--;
	}

	public bool done {
		get {return enemiesLeft <= 0;}
	}

	public void IncrementLevel() {
		level++;
	}

	public void SetLevel(int level) {
		this.level = level;
	}
}
