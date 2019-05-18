using UnityEngine;

public class Spinner : MonoBehaviour
{
	public float rotationSpeed = 0.2f;

    // Update is called once per frame
    void Update() {
		transform.Rotate(Vector3.up, rotationSpeed * Mathf.PI * Time.deltaTime);
    }
}
