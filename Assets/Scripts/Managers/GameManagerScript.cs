using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour {

	public static GameManagerScript gameManager;
	public GameObject heroPrefab;
	public int mapWidth, mapHeight;
	public int level = 1;
	public int score = 0;

	private GameObject playerInstance;
	private ProfileDataScript profile;

	void Awake() {
		if (gameManager == null) {
			DontDestroyOnLoad(this.gameObject);
			gameManager = this;
		} else if (gameManager != this) {
			Destroy (this.gameObject);
		} 

		if (profile == null)
			profile = new ProfileDataScript();
	}

	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void PlaceHero() {
		GameObject hero = Object.Instantiate(heroPrefab);
		hero.transform.position = new Vector3(mapWidth / 2.0f, mapHeight / 2.0f, hero.transform.position.z);
		hero.SetActive(false);

		HeroScript heroScript = hero.GetComponent<HeroScript>();
		heroScript.SetLevel(profile.playerLevel);
		heroScript.hitPoints = (int)(heroScript.maxHitPoints * profile.health);

		playerInstance = hero;
	}

	public GameObject GetPlayerInstance() {
		return playerInstance;
	}

	public void StartLevel() {
		PlaceHero();
		playerInstance.SetActive(true);

	}

	public void EndLevel() {

	}

	public void EndGame() {

	}

	public void LoadFromProfile(ProfileDataScript loadedProfile) {
		profile = loadedProfile;

		level = profile.curLevel;
		score = profile.score;
	}
	
	public void SaveToProfile() {
		HeroScript hero = playerInstance.GetComponent<HeroScript>();

		profile.curLevel = level;
		profile.playerLevel = hero.playerLevel;
		profile.score = score;
		profile.health = (float)hero.hitPoints / hero.maxHitPoints;
	}
	
	public ProfileDataScript GetProfile() {
		return profile;
	}

	public void AddScore(int points) {
		score += points;
	}
}
