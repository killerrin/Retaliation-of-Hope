using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {
	public float speed = 6.0f;
	public float rotationSpeed = 100.0f;

	public bool MouseControl = false;
	public bool KeyboardControl = false;
	public bool TouchControl = false;
	public bool VirtualJoystickMovementControl = false;
	public bool VirtualJoystickRotationControl = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		#region Keyboard
		if (KeyboardControl)
		{
			// Forward, Back
			if (Input.GetKey(KeyCode.W))
			{
				Vector3 position = transform.position;
				position.z += speed * Time.deltaTime;

				transform.position = position;
			}
			else if (Input.GetKey(KeyCode.S))
			{
				Vector3 position = transform.position;
				position.z -= speed * Time.deltaTime;

				transform.position = position;
			}

			// Left, Right
			if (Input.GetKey(KeyCode.A))
			{
				Vector3 position = transform.position;
				position.x -= speed * Time.deltaTime;

				transform.position = position;
			}
			else if (Input.GetKey(KeyCode.D))
			{
				Vector3 position = transform.position;
				position.x += speed * Time.deltaTime;

				transform.position = position;
			}

			// Up, Down
			if (Input.GetKey(KeyCode.Q))
			{
				Vector3 position = transform.position;
				position.y -= speed * Time.deltaTime;

				transform.position = position;
			}
			else if (Input.GetKey(KeyCode.E))
			{
				Vector3 position = transform.position;
				position.y += speed * Time.deltaTime;

				transform.position = position;
			}
		}
		#endregion

		#region Mouse
		if (MouseControl)
		{
			if (Input.GetMouseButton(0))
			{
				transform.Rotate(new Vector3(0, 0, -Input.GetAxis("Mouse X")) * Time.deltaTime * rotationSpeed);
			}
			if (Input.GetMouseButton(1))
			{
				transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * Time.deltaTime * rotationSpeed);
			}
		}
		#endregion

		#region Virtual Thumbsticks
		if (VirtualJoystickMovementControl)
		{
			if (VirtualJoysticks.LeftThumbstickCenter.HasValue)
			{
				Vector3 leftThumbStick = new Vector3(VirtualJoysticks.LeftThumbstick.x, VirtualJoysticks.LeftThumbstick.y, 0);
				Vector3 position = transform.position;

				position += leftThumbStick * (Time.deltaTime * (speed / 1));

				transform.position = position;
			}
		}
		if (VirtualJoystickRotationControl)
		{
			if (VirtualJoysticks.RightThumbstickCenter.HasValue)
			{
				transform.Rotate(new Vector3(-VirtualJoysticks.RightThumbstick.y, VirtualJoysticks.RightThumbstick.x, 0) * Time.deltaTime * (rotationSpeed / 3));
			}
		}
		#endregion
	}
}
