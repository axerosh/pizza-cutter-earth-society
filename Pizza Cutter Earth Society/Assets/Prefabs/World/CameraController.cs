using UnityEngine;

public class CameraController : MonoBehaviour
{
	public float cameraAcceleration;
	public float cameraSpeedDecay;

	private Vector3 cameraMovement;

	void UpdateInput()
	{
		if (Input.GetKey(KeyCode.W))
		{
			cameraMovement.z += cameraAcceleration;
		}
		if (Input.GetKey(KeyCode.A))
		{
			cameraMovement.x -= cameraAcceleration;
		}
		if (Input.GetKey(KeyCode.S))
		{
			cameraMovement.z -= cameraAcceleration;
		}
		if (Input.GetKey(KeyCode.D))
		{
			cameraMovement.x += cameraAcceleration;
		}
	}

	void UpdatePosition()
	{
		transform.localPosition += cameraMovement;

		//Decay movementspeed.
		cameraMovement *= cameraSpeedDecay;
	}

	// Update is called once per frame
	void Update()
	{
		UpdateInput();
		UpdatePosition();
	}
}
