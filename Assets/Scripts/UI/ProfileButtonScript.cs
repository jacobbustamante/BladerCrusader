using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProfileButtonScript : MonoBehaviour {

	private const string saveFileName = "/profile-";
	private const string saveFileExt = ".dat";

	public GameObject newProfilePrefab;

	public string profileFileName;
	public bool getCurProfileName = false;
	public GameObject profileNameObject;
	public GameObject playerInfoObject;
	public GameObject sceneNameObject;
	public GameObject scoreInfoObject;

	private Text profileName;
	private Text playerInfo;
	private Text sceneName;
	private Text scoreInfo;
	private string filePath;
	private ProfileDataScript profile;

	void Awake() {
		profileName = profileNameObject.GetComponent<Text>();
		playerInfo = playerInfoObject.GetComponent<Text>();
		sceneName = sceneNameObject.GetComponent<Text>();
		scoreInfo = scoreInfoObject.GetComponent<Text>();
	}

	// Use this for initialization
	void Start () {
		if (getCurProfileName) {
			profileFileName = GameManagerScript.gameManager.GetProfile().profileName;
		}
		filePath = saveFileName + profileFileName + saveFileExt;

		UpdateStatus();
	}

	public void UpdateStatus() {
		if (LoadProfileInfo()) {
			
		}
		else {
			InitializeEmptyProfileInfo();
		}
	}

	private void InitializeEmptyProfileInfo() {
		profileName.text = "New Profile";
		playerInfo.text = "";
		sceneName.text = "";
		scoreInfo.text = "";
	}

	private void InitializeProfileInfo() {
		profileName.text = profile.username;
		playerInfo.text = "Level: " + profile.curLevel;
		sceneName.text = "Wave: " + profile.curWave;
		scoreInfo.text = "Score: " + profile.score;
	}

	private bool LoadProfileInfo() {
		object data = PersistenceScript.LoadFile(filePath);

		if (data != null) {
			profile = (ProfileDataScript)data;
			InitializeProfileInfo();

			return true;
		}
		else {
			profile = null;
			return false;
		}
	}

	public void LoadGame() {
		if (profile != null)
			this.GetComponent<LoadGameScript>().LoadGameFromProfile(profile);
		else {
			GameObject newProfileObject = MenuButtonScript.OpenMenu(newProfilePrefab, this.transform);
			NewProfileScript newProfile = newProfileObject.GetComponent<NewProfileScript>();
			newProfile.SetFileName(profileFileName);

			//profile = new ProfileDataScript(profileFileName, "John");
			//this.GetComponent<LoadGameScript>().LoadGameFromProfile(profile);
		}
	}
}
