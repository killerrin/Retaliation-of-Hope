using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour {
	public static Player me;
	
	public Health health;

	public Transform[] laserSpawnPoints;
	public GameObject laserPrefab;

	private CharacterController controller;

	private GUIStyle healthBarStyle;
	private GUIStyle healthBarBackgroundStyle;

	public float accelerationNormal = 40.0f;
	public float accelerationBoostMultiplyer = 1.5f;
	public float rotationalAcceleration = 100.0f;
	public float DamageUponCrash = 15.0f;
	public float maximumTimeBetweenShotsInSeconds = 0.2f;

	private bool isBoosting = false;

	private int buttonWidth = 200;
	private int buttonHeight = 100;
	private int distanceBetweenButtons = 20;
	private int distanceFromRight = 50;
	private int distanceFromBottom = 300;
	private int ctr = 0;

	private float shootTimer;// = maximumTimeBetweenShotsInSeconds;

	// Use this for initialization
	void Start () {
		me = this;

		controller = gameObject.GetComponent<CharacterController>();

		Texture2D healthBarTexture = new Texture2D(1, 1);
		healthBarTexture.SetPixel(1, 1, new Color(0.1f, 0.7f, 0.1f, 0.7f));
		healthBarTexture.wrapMode = TextureWrapMode.Repeat;
		healthBarTexture.Apply();

		Texture2D healthBarBackgroundTexture = new Texture2D(1, 1);
		healthBarBackgroundTexture.SetPixel(1, 1, new Color(0.0f, 0.0f, 0.0f, 0.7f));
		healthBarBackgroundTexture.wrapMode = TextureWrapMode.Repeat;
		healthBarBackgroundTexture.Apply();

		healthBarStyle = new GUIStyle();
		healthBarStyle.normal.background = healthBarTexture;

		healthBarBackgroundStyle = new GUIStyle();
		healthBarBackgroundStyle.normal.background = healthBarBackgroundTexture;

		shootTimer = maximumTimeBetweenShotsInSeconds;

#if UNITY_STANDALONE || UNITY_WEBPLAYER
		Screen.lockCursor = true;
#endif
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.me.gameState != GameManager.GameState.Playing) { return; }

		MovePlayer();

#if UNITY_STANDALONE || UNITY_WEBPLAYER
		if (Input.GetMouseButton(0)) {
			Shoot();
		}
		if (Input.GetMouseButtonUp(0)) { shootTimer = maximumTimeBetweenShotsInSeconds; }
#else

#endif
	}

	private void MovePlayer()
	{
#if UNITY_STANDALONE || UNITY_WEBPLAYER
		if (Input.GetMouseButton(1))
		{
			// If right mouse button is held down, Player can barrel roll and turn up/down
			transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y"), 0, -Input.GetAxis("Mouse X")) * Time.deltaTime * rotationalAcceleration);
		}
		else
		{
			// Move in position of move movement
			transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * Time.deltaTime * rotationalAcceleration);

			// Check if middle mouse button is being pressed, in which case allow barrel rolling during normal turning
			if (Input.GetMouseButton(2)) { transform.Rotate(new Vector3(0, 0, -Input.GetAxis("Mouse X")) * Time.deltaTime * rotationalAcceleration); }
		}

		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			isBoosting = true;
		}
		else if (Input.GetKeyUp(KeyCode.LeftShift))
		{
			isBoosting = false;
		}
#else
		// Rotate the Yaw/Pitch of the ship
		if (VirtualJoysticks.LeftThumbstickCenter.HasValue) {
			Vector3 leftThumbStick = new Vector3(VirtualJoysticks.LeftThumbstick.x, VirtualJoysticks.LeftThumbstick.y, 0);
			transform.Rotate(new Vector3(leftThumbStick.y, leftThumbStick.x, 0) * Time.deltaTime * (rotationalAcceleration / 3));
		}

		// Rotate the Roll/Pitch of the ship
		if (VirtualJoysticks.RightThumbstickCenter.HasValue) {
			Vector3 rightThumbStick = new Vector3(VirtualJoysticks.RightThumbstick.x, VirtualJoysticks.RightThumbstick.y, 0);
			transform.Rotate(new Vector3(rightThumbStick.y, 0, -rightThumbStick.x) * Time.deltaTime * (rotationalAcceleration / 3));
		}
#endif
		
		// Check Boost and Move Forward
		if (isBoosting)
			controller.Move(transform.forward * (Time.deltaTime * (accelerationNormal * accelerationBoostMultiplyer)));
		else
			controller.Move(transform.forward * (Time.deltaTime * accelerationNormal));

	}

	private void Shoot()
	{
		shootTimer += Time.deltaTime;
		if (shootTimer >= maximumTimeBetweenShotsInSeconds)
		{
			shootTimer -= maximumTimeBetweenShotsInSeconds;

			GameObject laser1 = Instantiate(laserPrefab, laserSpawnPoints[0].position, transform.rotation) as GameObject;
			GameObject laser2 = Instantiate(laserPrefab, laserSpawnPoints[1].position, transform.rotation) as GameObject;
	
			//ctr++;
			//Debug.Log("Shooting "+ctr);
		}
	}

	void OnTriggerEnter(Collider c) {
		if (c.gameObject.tag == "AI")
		{
			if (isBoosting)
				health.DecreaseHealth(DamageUponCrash * 1.5f);
			else
				health.DecreaseHealth(DamageUponCrash);
		}
	}

	void OnGUI()
	{
#if UNITY_STANDALONE || UNITY_WEBPLAYER
#else
		if (GUI.RepeatButton(new Rect(Screen.width - distanceFromRight - (buttonWidth * 1),
								Screen.height - (buttonHeight * 2) - distanceFromBottom - distanceBetweenButtons,
								buttonWidth,
								buttonHeight),
							 "Shoot"))
		{
			Shoot();
		}
		else
		{
			//shootTimer = maximumTimeBetweenShotsInSeconds;
		}


		if (GUI.RepeatButton(new Rect(Screen.width - distanceFromRight - (buttonWidth * 1),
								Screen.height - buttonHeight - distanceFromBottom, 
								buttonWidth,
								buttonHeight),
							 "Boost"))
		{
			isBoosting = true;
		}
		else
		{
			isBoosting = false;
		}
#endif

		Vector2 hbPos = new Vector2(Screen.width / 2.5f, 25.0f);
		float barWidth = 100.0f * 4.0f;
		float barHeight = 30.0f;

		// Health Bar Outline
		GUI.Label(new Rect(hbPos.x - 2.0f, hbPos.y - 2.0f, barWidth + 4.0f, barHeight + 4.0f), new GUIContent(""), healthBarBackgroundStyle);

		// Health Bar
		GUI.Label(new Rect(hbPos.x, hbPos.y, health.health * 4.0f, barHeight), new GUIContent(""), healthBarStyle);

		// Health Text
		GUI.Label(new Rect(hbPos.x + (barWidth / 2.1f), hbPos.y + (barHeight / 5.0f), health.health * 3.0f, barHeight), new GUIContent(health.health.ToString()));



		if (ConstManager.DebugMode)
		{
			GUI.Label(new Rect(300, 0, 200, 50), "Player Boosting: "+ isBoosting.ToString());
		}
	}
}
