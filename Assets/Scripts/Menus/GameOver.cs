using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {
    public GUIStyle guiStyle;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
        GUI.Label(new Rect(Screen.width / 2.5f, Screen.height / 3.5f, 300, 200), new GUIContent("Game Over"), guiStyle);

        if (GUI.Button(new Rect(Screen.width / 2.6f, Screen.height / 2.0f, 300, 100), new GUIContent("Go to Main Menu"))) 
        {
            Application.LoadLevel(1);
        }
	}
}
