using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(FloorTileScript))]
public class FloorTileEditor : Editor {

	private int tileSpriteIndex = 0;
	private float[] tilePercentageList;
	private FloorTileScript floorTileScript;

	public void OnEnable() {
		floorTileScript = (FloorTileScript)target;
	}

	public override void OnInspectorGUI() {
		DrawDefaultInspector();

		for (int i = 0; i < floorTileScript.tilePercentageList.Length; i++) {
			floorTileScript.tilePercentageList[i] = EditorGUILayout.Slider(floorTileScript.GetFloorTilesStringList()[i], floorTileScript.tilePercentageList[i], 0, 1);
		}

		if(GUILayout.Button("Reset tile probability"))
			floorTileScript.ResetTilePercentageList();
		tileSpriteIndex = EditorGUILayout.Popup(tileSpriteIndex, floorTileScript.GetFloorTilesStringList());
		if(GUILayout.Button("Randomize tile sprite"))
			tileSpriteIndex = floorTileScript.RandomizeTile();
		if(GUILayout.Button("Round tile location to Int"))
			floorTileScript.RoundLocation();

		// Change tile sprite if index changed
		if (tileSpriteIndex != floorTileScript.GetCurrentTileSpriteIndex())
			floorTileScript.ChangeTile(tileSpriteIndex);
	}
	
}
