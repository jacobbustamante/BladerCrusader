using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(FloorMapScript))]
public class FloorMapEditor : Editor {

	private FloorMapScript floorMapScript;

	public void OnEnable() {
		floorMapScript = (FloorMapScript)target;
	}

	public override void OnInspectorGUI() {
		DrawDefaultInspector();

		if(GUILayout.Button("Fill with tiles"))
			floorMapScript.FillWithTiles();

		if(GUILayout.Button("Destroy children"))
			floorMapScript.DestroyAllChildren();
	}

}
