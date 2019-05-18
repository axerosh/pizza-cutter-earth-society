using UnityEngine;

public class CameraController : MonoBehaviour
{
	public float cameraAcceleration;
	public float cameraSpeedDecay;
	public Rect boundingBox;

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

	void ClampBoundingBox()
	{
		Vector3 newLocalPosition = transform.localPosition;

		if (transform.localPosition.x < boundingBox.xMin)
		{
			newLocalPosition.x = boundingBox.xMin;
		}
		else if (transform.localPosition.x > boundingBox.xMax)
		{
			newLocalPosition.x = boundingBox.xMax;
		}

		if (transform.localPosition.z < boundingBox.yMin)
		{
			newLocalPosition.z = boundingBox.yMin;
		}
		else if (transform.localPosition.z > boundingBox.yMax)
		{
			newLocalPosition.z = boundingBox.yMax;
		}

		transform.localPosition = newLocalPosition;
	}

	void UpdatePosition()
	{
		transform.localPosition += cameraMovement;
		ClampBoundingBox();

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
