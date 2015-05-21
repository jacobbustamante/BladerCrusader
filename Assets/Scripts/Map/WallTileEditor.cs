using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(WallTileScript))]
public class WallTileEditor : Editor {
	
	private int tileSpriteIndex = 0;
	private float[] tilePercentageList;
	private WallTileScript wallTileScript;
	
	public void OnEnable() {
		wallTileScript = (WallTileScript)target;
	}
	
	public override void OnInspectorGUI() {
		DrawDefaultInspector();
		
		for (int i = 0; i < wallTileScript.tilePercentageList.Length; i++) {
			wallTileScript.tilePercentageList[i] = EditorGUILayout.Slider(wallTileScript.GetWallTilesStringList()[i], wallTileScript.tilePercentageList[i], 0, 1);
		}
		
		if(GUILayout.Button("Reset tile probability"))
			wallTileScript.ResetTilePercentageList();
		tileSpriteIndex = EditorGUILayout.Popup(tileSpriteIndex, wallTileScript.GetWallTilesStringList());
		if(GUILayout.Button("Randomize tile sprite"))
			tileSpriteIndex = wallTileScript.RandomizeTile();
		if(GUILayout.Button("Round tile location to Int"))
			wallTileScript.RoundLocation();
		
		// Change tile sprite if index changed
		if (tileSpriteIndex != wallTileScript.GetCurrentTileSpriteIndex())
			wallTileScript.ChangeTile(tileSpriteIndex);
	}
	
}
