using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProfileButtonScript : MonoBehaviour {

	private const string saveFileName = "/profile-";
	private const string saveFileExt = ".dat";

	public string profileFileName;
	public GameObject profileNameObject;
	public GameObject playerInfoObject;
	public GameObject sceneNameObject;
	public GameObject timePlayedInfoObject;

	private Text profileName;
	private Text playerInfo;
	private Text sceneName;
	private Text timePlayedInfo;
	private string filePath;
	private ProfileDataScript profile;

	void Awake() {
		profileName = profileNameObject.GetComponent<Text>();
		playerInfo = playerInfoObject.GetComponent<Text>();
		sceneName = sceneNameObject.GetComponent<Text>();
		timePlayedInfo = timePlayedInfoObject.GetComponent<Text>();

		filePath = saveFileName + profileFileName + saveFileExt;
	}

	// Use this for initialization
	void Start () {
		UpdateStatus();
	}
	
	// Update is called once per frame
	void Update () {
	
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
		timePlayedInfo.text = "";
	}

	private void InitializeProfileInfo() {
		profileName.text = profile.profileName;
		playerInfo.text = "Level: " + profile.playerLevel;
		sceneName.text = "Wave: " + profile.curLevel;
		timePlayedInfo.text = "Score: " + Mathf.RoundToInt(profile.totalTimePlayed);
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
			profile = new ProfileDataScript(profileFileName);
			this.GetComponent<LoadGameScript>().LoadGameFromProfile(profile);
		}
	}
}
