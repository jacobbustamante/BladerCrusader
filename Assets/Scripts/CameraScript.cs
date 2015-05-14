using UnityEngine;
using System;
using System.Collections;

public class CameraScript : MonoBehaviour
{
	public float cameraHeight = -10;
	
	private GameManagerScript gameManager;
	private GameObject playerInstance;
	private Camera cam;

	private int mapHeight, mapWidth;
	private float minX, minY, maxX, maxY;
	private int prevScreenWidth = -1, prevScreenHeight = -1;
	
	void Awake() {
		gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
		cam = this.GetComponent<Camera>();
	}

	void Start () {
		mapWidth = gameManager.mapWidth;
		mapHeight = gameManager.mapHeight;
		playerInstance = gameManager.GetPlayerInstance();
	}

	void Update () {
		if(Screen.width != prevScreenWidth || Screen.height != prevScreenHeight)
			UpdateBorders();
		
		this.transform.position = new Vector3(
			Mathf.Clamp(playerInstance.transform.position.x, minX, maxX),
			Mathf.Clamp(playerInstance.transform.position.y, minY, maxY),
			cameraHeight);
	}

	void UpdateBorders() {
		float tileOffset = -0.5f;

		float camViewY = cam.orthographicSize;
		float camViewX = camViewY * (float)Screen.width / (float)Screen.height;

		minX = camViewX + tileOffset;
		minY = camViewY + tileOffset;
		maxX = mapWidth - camViewX + tileOffset;
		maxY = mapHeight - camViewY + tileOffset;

		prevScreenWidth = Screen.width;
		prevScreenHeight = Screen.height;
	}

}
