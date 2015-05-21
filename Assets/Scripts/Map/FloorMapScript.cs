using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class FloorMapScript : MonoBehaviour {

	public GameObject floorTileObject;
	public GameObject WallTileObject;

	public int fillWidth, fillHeight;
	public bool fillWalls;
	public bool fillRandomize;
	
	public void FillWithTiles() {
		DestroyAllChildren();

		foreach (int x in Enumerable.Range(0, fillWidth)) {
			foreach (int y in Enumerable.Range(0, fillHeight)) {
				GameObject tileObject = Object.Instantiate(floorTileObject);
				tileObject.transform.SetParent(this.transform); 

				tileObject.transform.position = new Vector3(x, y, 0);
				if (fillRandomize)
					tileObject.GetComponent<FloorTileScript>().RandomizeTile();
			}
		}

		if (fillWalls)
			FillWalls();
	}

	public void FillWalls() {
		foreach (int x in Enumerable.Range(0, fillWidth)) {
			foreach (int y in Enumerable.Range(0, fillHeight)) {
				if (x == 0 || y == 0 || x == fillWidth-1 || y == fillHeight-1) {
					GameObject tileObject = Object.Instantiate(WallTileObject);
					tileObject.transform.SetParent(this.transform); 
					
					tileObject.transform.position = new Vector3(x, y, 0);
					if (fillRandomize)
						tileObject.GetComponent<WallTileScript>().RandomizeTile();
				}
			}
		}
	}

	public void DestroyAllChildren() {
		List<GameObject> childList = new List<GameObject>();
		foreach (Transform child in this.transform)
			childList.Add(child.gameObject);

		foreach (GameObject child in childList)
			DestroyImmediate(child);
	}
}
