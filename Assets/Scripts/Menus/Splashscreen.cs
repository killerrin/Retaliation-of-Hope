using UnityEngine;
using System.Collections;

public class Splashscreen : MonoBehaviour {
	public int LoadAfterSplash = 1;

	public float MaxSplashTimeInMiliseconds = 2.0f; // Default to 2.0 = 1 Seconds
	private float counter = 0.0f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		counter += Time.deltaTime;
		Debug.Log("SplashScreen Timer: " + counter + " / " + MaxSplashTimeInMiliseconds);

		if (counter >= MaxSplashTimeInMiliseconds)
		{
			Debug.Log("Changing to Main Menu");

			counter = 0.0f;
			Application.LoadLevel(LoadAfterSplash);
		}
	}

	void OnGUI()
	{
		if (ConstManager.DebugMode)
		{
			int width = 100, height = width;
			foreach (Touch t in Input.touches)
			{
				Vector2 guiSpace = ConstManager.ConvertScreenSpaceToGuiSpace(t.position);

				GUI.Label(new Rect(guiSpace.x, guiSpace.y, width, height), new GUIContent("Touch Here"));
			}
		}
	}
}
