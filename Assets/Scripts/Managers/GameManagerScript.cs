using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour {

	public GameObject heroPrefab;
	public int mapWidth, mapHeight;
	
	public static GameManagerScript gameManager;
	private GameObject playerInstance;
	private ProfileDataScript profile;

	public int level = 4; 


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
	}
	
	public void SaveToProfile() {
		// profile.level = level
	}
	
	public ProfileDataScript GetProfile() {
		return profile;
	}


}
