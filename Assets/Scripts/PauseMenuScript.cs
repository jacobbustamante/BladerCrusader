using UnityEngine;
using System.Collections;

public class PauseMenuScript : MonoBehaviour {
	bool showGUI = false;

	void Update () {
		if(Input.GetKey(KeyCode.Escape))
		{
			showGUI=true;
			Time.timeScale = 0;
		}
		
	}
	
	void OnGUI()
	{
		if(showGUI)
		{
			GUI.Box(new Rect(Screen.width/2-150,Screen.height/2-150,300,500),"");
			if(GUI.Button(new Rect(Screen.width/2-50,Screen.height/2-100,100,50),"Continue"))
			{
				showGUI=false;
				Time.timeScale = 1;
			}
			if(GUI.Button(new Rect(Screen.width/2-50,Screen.height/2,100,50),"Settings"))
			{
				showGUI=false;
				GUI.Box(new Rect(Screen.width/2-150,Screen.height/2-150,300,500),"");

			}
			if(GUI.Button(new Rect(Screen.width/2-50,Screen.height/2+100,100,50),"Quit"))
			{
				showGUI=false;
				Application.LoadLevel("Menu");
			}

		}
	}
}