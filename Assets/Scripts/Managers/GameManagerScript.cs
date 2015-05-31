using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour {

	public const int MAX_WAVE = 3;
	public const int STARTING_WAVE = 1;
	public enum Difficulty {
		EASY,
		MEDIUM,
		HARD
	};

	public static GameManagerScript gameManager;
	public GameObject gameOverPanel;
	public GameObject heroPrefab;
	public int mapWidth, mapHeight;
	public int level = 1;
	public int wave = 1;
	public int score = 0;
	public Difficulty curDifficulty = Difficulty.MEDIUM;

	private GameObject playerInstance;
	private HeroScript playerInstanceScript;
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
		heroScript.SetLevels(profile.playerLevel, profile.weaponLevels);
		heroScript.maxHitPoints = profile.maxHealth;
		heroScript.hitPoints = profile.curHealth;

		playerInstance = hero;
		playerInstanceScript = hero.GetComponent<HeroScript>();
	}

	public GameObject GetPlayerInstance() {
		return playerInstance;
	}

	public int playerLevel {
		get { return playerInstance ? playerInstanceScript.playerLevel : 1;}
	}

	public int playerHealth {
		get { return playerInstance ? playerInstanceScript.hitPoints : 10;}
	}

	public int playerMaxHealth {
		get { return playerInstance ? playerInstanceScript.maxHitPoints : 10;}
	}

	public int playerWeaponIndex {
		get { return playerInstance ? playerInstanceScript.curAttack : 0;}
	}

	public int[] playerWeaponLevels {
		get { return playerInstance ? playerInstanceScript.weaponLevels : null;}
	}

	public void StartLevel() {
		PlaceHero();
		playerInstance.SetActive(true);
		UpdateAllHUDS();
	}

	public void EndLevel() {

	}

	public void EndGame() {
		Time.timeScale = 0;
		MenuButtonScript.OpenMenu(gameOverPanel, GameObject.Find("Canvas").transform.GetChild(0).transform);
	}

	public void LoadFromProfile(ProfileDataScript loadedProfile) {
		profile = loadedProfile;

		level = profile.curLevel;
		wave = profile.curWave;
		score = profile.score;
	}
	
	public void SaveToProfile() {
		HeroScript hero = playerInstance.GetComponent<HeroScript>();

		profile.curLevel = level;
		profile.curWave = wave;
		profile.playerLevel = hero.playerLevel;
		profile.weaponLevels = (int[])hero.weaponLevels.Clone();
		profile.curHealth = hero.hitPoints;
		profile.maxHealth = hero.maxHitPoints;
		profile.score = score;
	}
	
	public ProfileDataScript GetProfile() {
		return profile;
	}

	public void AddScore(int points) {
		score += points;
		HUDInfoScript.UpdateInfo();
	}

	private void UpdateAllHUDS() {
		HUDHealthScript.UpdateInfo();
		HUDInfoScript.UpdateInfo();
		HUDWeaponsScript.UpdateInfo();
	}
}
