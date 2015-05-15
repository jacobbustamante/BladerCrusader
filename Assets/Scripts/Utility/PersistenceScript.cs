using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class PersistenceScript : MonoBehaviour {
	
	public static object LoadFile(string fileName) {
		string filePath = Application.persistentDataPath + fileName;
		object data = null;
		
		if (File.Exists(filePath)) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(filePath, FileMode.Open);
			
			data = bf.Deserialize(file);
			file.Close();
			
			Debug.Log("Loaded from " + filePath);
		}
		else {
			Debug.Log("ERROR: " + filePath + " not found.");
		}
		
		return data;
	}
	
	public static void SaveFile(string fileName, object data) {
		string filePath = Application.persistentDataPath + fileName;
		
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(filePath);
		
		bf.Serialize(file, data);
		file.Close();
		
		Debug.Log("Saved to " + filePath);
	}
	
}
