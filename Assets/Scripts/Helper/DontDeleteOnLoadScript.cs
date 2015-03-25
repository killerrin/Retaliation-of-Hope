using UnityEngine;
using System.Collections;

public class DontDeleteOnLoadScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
