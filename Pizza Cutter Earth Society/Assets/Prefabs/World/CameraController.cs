using UnityEngine;

public class CameraController : MonoBehaviour
{
	public float cameraAcceleration;
	public float cameraSpeedDecay;
	public Rect boundingBox;
	public Vector2 boundingSphereOrigin;
	public float boundingSphereRadius;

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

		// Bound to box
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

		// Bound to sphere
		Vector2 sphereOffset = new Vector2(newLocalPosition.x, newLocalPosition.z) - boundingSphereOrigin;
		if (sphereOffset.magnitude > boundingSphereRadius)
		{
			sphereOffset = sphereOffset.normalized * boundingSphereRadius;
		}
		Vector2 newPos = boundingSphereOrigin + sphereOffset;
		newLocalPosition = new Vector3(newPos.x, transform.localPosition.y, newPos.y);

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
