using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour {

	public GameObject heroPrefab;
	public int mapWidth, mapHeight;

	private GameObject playerInstance;

	void Awake() {
		PlaceHero();
	}

	// Use this for initialization
	void Start () {
		playerInstance.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void PlaceHero() {
		GameObject hero = Object.Instantiate(heroPrefab);
		hero.transform.position = new Vector3(mapWidth / 2.0f, mapHeight / 2.0f, hero.transform.position.z);
		hero.SetActive(false);

		playerInstance = hero;
	}

	public GameObject GetPlayerInstance() {
		return playerInstance;
	}
}
