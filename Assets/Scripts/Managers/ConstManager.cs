using UnityEngine;
using System.Collections;

public class ConstManager : MonoBehaviour {
	public static bool DebugMode = false;

	public static System.Random random = new System.Random();

	public bool isDebug = false;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (DebugMode != isDebug) DebugMode = isDebug;
	}

	public static Vector2 ConvertScreenSpaceToGuiSpace(Vector2 screenspace)
	{
		return new Vector2(screenspace.x, Screen.height - screenspace.y);
	}
}
