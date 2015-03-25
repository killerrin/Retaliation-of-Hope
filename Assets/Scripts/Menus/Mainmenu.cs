using UnityEngine;
using System.Collections;

public class Mainmenu : MonoBehaviour {
	public int xAlign = 100;
	public int yPosItem1 = 200;

	public int width = 200;
	public int height = 100;
	public int buttonSeparation = 10;

	public GUIStyle guiStyle;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		if (GUI.Button(new Rect(xAlign, yPosItem1, width, height), new GUIContent("Play Game")))
		{
			Debug.Log("Loading Level");
			Application.LoadLevel(3);
		}

		if (GUI.Button(new Rect(xAlign, yPosItem1 + (buttonSeparation + height * 1), width, height), new GUIContent("Exit Game")))
		{
			Debug.Log("Closing Game");
			Application.Quit();
		}



		if (ConstManager.DebugMode)
		{
			foreach (Touch t in Input.touches)
			{
				Vector2 guiSpace = ConstManager.ConvertScreenSpaceToGuiSpace(t.position);

				GUI.Label(new Rect(guiSpace.x, guiSpace.y, 100, 100), new GUIContent("Touch Here"));
			}
		}
	}
}
