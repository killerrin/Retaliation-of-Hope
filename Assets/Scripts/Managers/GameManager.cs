using UnityEngine;
using System.Collections;
using System;

public class GameManager : MonoBehaviour {
	public static GameManager me;

	public enum GameState
	{
		Playing, 
		Paused,
		GameOver
	}

	public GameState gameState = GameState.Playing;

	// Use this for initialization
	void Start () {
		me = this;
	}
	
	// Update is called once per frame
	void Update () {
		if (!Player.me.health.Alive) { 
			gameState = GameState.GameOver;
			Application.LoadLevel(2);

#if UNITY_STANDALONE || UNITY_WEBPLAYER
			Screen.lockCursor = false;
#endif
		}
	}

	void OnGUI()
	{
		switch (gameState)
		{
			case GameState.Playing:
				break;
			case GameState.Paused:
				break;
			case GameState.GameOver:
				break;
			default:
				break;
		}

		if (ConstManager.DebugMode)
		{
			if (VirtualJoysticks.LeftThumbstickCenter.HasValue)
			{
				GUI.Label(new Rect(10, Screen.height - 20, 100, 20), new GUIContent(VirtualJoysticks.LeftThumbstickCenter.Value.ToString()));
				GUI.Label(new Rect(130, Screen.height - 20, 100, 20), new GUIContent("ID: " + VirtualJoysticks.LeftID.ToString()));
				GUI.Label(new Rect(225, Screen.height - 20, 100, 20), new GUIContent(VirtualJoysticks.LeftThumbstick.ToString()));
			}
			else
			{
				GUI.Label(new Rect(10, Screen.height - 20, 100, 20), new GUIContent("No Left Thumbstick Center"));
			}

			if (VirtualJoysticks.RightThumbstickCenter.HasValue)
			{
				GUI.Label(new Rect(10, Screen.height - 40, 100, 20), new GUIContent(VirtualJoysticks.RightThumbstickCenter.Value.ToString()));
				GUI.Label(new Rect(130, Screen.height - 40, 100, 20), new GUIContent("ID: " + VirtualJoysticks.RightID.ToString()));
				GUI.Label(new Rect(225, Screen.height - 40, 100, 20), new GUIContent(VirtualJoysticks.RightThumbstick.ToString()));
			}
			else
			{
				GUI.Label(new Rect(10, Screen.height - 40, 100, 20), new GUIContent("No Right Thumbstick Center"));
			}

			int width = 100, height = width;
			foreach (Touch t in VirtualJoysticks.oldTouches)
			{
				Vector2 guiSpace = ConstManager.ConvertScreenSpaceToGuiSpace(t.position);

				GUI.Label(new Rect(guiSpace.x, guiSpace.y, width, height), new GUIContent("Touch Here"));
			}
		}
	}
}
