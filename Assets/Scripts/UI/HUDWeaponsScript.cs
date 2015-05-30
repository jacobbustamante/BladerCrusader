using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class HUDWeaponsScript : MonoBehaviour {

	public RectTransform selector;
	public List<RectTransform> weaponList;
	public List<Text> weaponLevelList;

	private static HUDWeaponsScript hudWeapons;
	
	void Awake() {
		if (hudWeapons == null) {
			hudWeapons = this;
		} else if (hudWeapons != this) {
			Destroy (this.gameObject);
		} 
	}

	private void SetSelector(int index) {
		if (index < weaponList.Count && index >= 0) {
			selector.SetParent(weaponList[index].transform);
			selector.SetAsFirstSibling();
			selector.offsetMin = Vector2.zero;
			selector.offsetMax = Vector2.zero;
		}
	}

	private void SetLevels() {
		int[] weaponLevels = GameManagerScript.gameManager.playerWeaponLevels;
		if (weaponLevels != null) {
			for (int i = 0; i < weaponLevelList.Count && i < weaponLevels.Length; i++) {
				weaponLevelList[i].text = weaponLevels[i].ToString();
			}
		}
	}

	private void UpdateAllInfo() {
		int weaponIndex = GameManagerScript.gameManager.playerWeaponIndex;
		SetSelector(weaponIndex);
		SetLevels();
	}

	public static void UpdateInfo() {
		hudWeapons.UpdateAllInfo();
	}
}
