using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
	private float rotationSpeed = 0.2f * Mathf.PI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
