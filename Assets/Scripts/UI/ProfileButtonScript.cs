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
		if (LoadProfileInfo()) {

		}
		else {
			InitializeEmptyProfileInfo();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void InitializeEmptyProfileInfo() {
		profileName.text = "New Profile";
		playerInfo.text = "";
		sceneName.text = "";
		timePlayedInfo.text = "";
	}

	private void InitializeProfileInfo() {
		profileName.text = profile.profileName;
		playerInfo.text = "Lv." + profile.playerLevel + " " + "Blader";
		sceneName.text = "Lv." + profile.curLevel + " " + profile.curSceneName;
		timePlayedInfo.text = "TimePlayed: " + Mathf.RoundToInt(profile.totalTimePlayed);
	}

	private bool LoadProfileInfo() {
		object data = PersistenceScript.LoadFile(filePath);

		if (data != null) {
			profile = (ProfileDataScript)data;
			InitializeProfileInfo();

			return true;
		}
		else {
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
