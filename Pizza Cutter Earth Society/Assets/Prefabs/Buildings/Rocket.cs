using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
	public float flightSpeed;
	public float rotationpeed;

	private bool hasLaunched = false;

	private void Update()
	{
		if (hasLaunched)
		{
			transform.position += Vector3.up * flightSpeed * Time.deltaTime;
			transform.Rotate(Vector3.up, rotationpeed * Time.deltaTime);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		Unit unit = other.GetComponent<Unit>();
		if (unit)
		{
			int numUnitsPreDestruction = FindObjectsOfType<Unit>().Length;
			Destroy(unit.gameObject);
			if (numUnitsPreDestruction <= 1)
			{
				TakeOff();
			}
		}
	}

	private void TakeOff()
	{
		Debug.Log("YOU WON!");
		hasLaunched = true;
        Invoke("Restart", 5);
	}

    private void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
