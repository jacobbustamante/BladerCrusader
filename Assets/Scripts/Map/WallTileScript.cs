using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class WallTileScript : MonoBehaviour {
	
	public List<Sprite> wallTiles;
	public float[] tilePercentageList;
	
	private int currentTileSpriteIndex = 0;
	
	public void ChangeTile(int tileSpriteIndex) {
		SpriteRenderer spriteRenderer = GetComponentInParent<SpriteRenderer>();
		spriteRenderer.sprite = wallTiles[tileSpriteIndex];
		
		currentTileSpriteIndex = tileSpriteIndex;
	}
	
	public int RandomizeTile() {
		if (tilePercentageList.Length < wallTiles.Count)
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
	
	public string[] GetWallTilesStringList() {
		string[] stringList = wallTiles.Select(s => s.name).ToArray();
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
		tilePercentageList = Enumerable.Repeat(1.0f, wallTiles.Count).ToArray();
	}
}
