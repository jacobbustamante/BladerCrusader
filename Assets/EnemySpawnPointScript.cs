using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawnPointScript : MonoBehaviour {
	
	public List<GameObject> enemyTypes;
	public float spawnDelay;

	private int enemiesLeft;
	private int enemyCountToSpawn;
	private float nextSpawnTime;

	public int level {
		get { return GameManagerScript.gameManager.level; }
	}
	
	public bool done {
		get {return enemiesLeft <= 0;}
	}
	
	void Update () {
		if (enemyCountToSpawn > 0 && Time.time > nextSpawnTime) {
			SpawnEnemy();
			enemyCountToSpawn--;
			nextSpawnTime = Time.time + spawnDelay;
		}
	}

	private int levelAddition {
		get { return GameManagerScript.gameManager ? GameManagerScript.gameManager.GetDifficultyAddition() : 0; }
	}

	public void StartWave() {
		SetSpawnPointParams();
		nextSpawnTime = Time.time + spawnDelay;
		enemiesLeft = enemyCountToSpawn;
	}
	
	public void SpawnedEnemyDefeated() {
		enemiesLeft--;
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
			GameObject newEnemy = Object.Instantiate(enemyTypes[index]);
			EnemyScript enemy = newEnemy.GetComponent<EnemyScript>();
			newEnemy.transform.position = this.transform.position;
			enemy.SetSpawnPoint(this);
			enemy.SetLevel(level + levelAddition);
		}
		else {
			SpawnedEnemyDefeated();
		}
	}
}
