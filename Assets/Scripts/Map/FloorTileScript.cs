using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(SpriteRenderer))]
public class FloorTileScript : MonoBehaviour {

	public List<Sprite> floorTiles;
	public float[] tilePercentageList;

	private int currentTileSpriteIndex = 0;

	public void ChangeTile(int tileSpriteIndex) {
		SpriteRenderer spriteRenderer = GetComponentInParent<SpriteRenderer>();
		spriteRenderer.sprite = floorTiles[tileSpriteIndex];

		currentTileSpriteIndex = tileSpriteIndex;
	}

	public int RandomizeTile() {
		if (tilePercentageList.Length < floorTiles.Count)
			ResetTilePercentageList();

		float random = Random.Range(0, tilePercentageList.Sum());
		int index = 0;
		for (float cur = 0; index < tilePercentageList.Length; index++) {
			cur += tilePercentageList[index];
			if (cur > random) {
				ChangeTile(index);
				break;
			}
		}

		return index;
	}

	public int GetCurrentTileSpriteIndex() {
		return currentTileSpriteIndex;
	}

	public string[] GetFloorTilesStringList() {
		string[] stringList = floorTiles.Select(s => s.name).ToArray();
		return stringList;
	}

	public void RoundLocation() {
		this.transform.position = new Vector3(
			Mathf.Round(this.transform.position.x),
			Mathf.Round(this.transform.position.y),
			Mathf.Round(this.transform.position.z)
			);
	}

	public void ResetTilePercentageList() {
		tilePercentageList = Enumerable.Repeat(1.0f, floorTiles.Count).ToArray();
	}
}
