using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
	public float rotationSpeed = 0.2f;

    public GameObject mainCamera;
    public float cameraAcceleration;
    public float cameraSpeedDecay;

    private Vector3 cameraMovement;

    void UpdateInput() {
        if (Input.GetKey(KeyCode.W)) {
            cameraMovement.z += cameraAcceleration;
        }
        if (Input.GetKey(KeyCode.A)) {
            cameraMovement.x -= cameraAcceleration;
        }
        if (Input.GetKey(KeyCode.S)) {
            cameraMovement.z -= cameraAcceleration;
        }
        if (Input.GetKey(KeyCode.D)) {
            cameraMovement.x += cameraAcceleration;
        }
    }

    void UpdatePosition() {
        mainCamera.transform.localPosition += cameraMovement;

        //Decay movementspeed.
        cameraMovement *= cameraSpeedDecay;
    }

    // Update is called once per frame
    void Update() {
        UpdateInput();
        UpdatePosition();
		transform.Rotate(Vector3.up, rotationSpeed * Mathf.PI * Time.deltaTime);
    }
}
