using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class BuildingPlot : MonoBehaviour {

    public GameObject CapriccioniumBuildingPrefab;
    public GameObject KebabiteBuildingPrefab;
    public GameObject RocketPrefab;

    public delegate void OnDestroyed ();
    public event OnDestroyed onDestroyed;
    private bool isDestroyed = false;

    public void Destroy() {
        Destroy(gameObject);
        isDestroyed = true;
        onDestroyed();
    }

    public void ChooseBuilding(BuildButton.Type type) {
        if (isDestroyed) {
            return;
        }
        Destroy();
        switch (type) {
            case BuildButton.Type.Capricosium:
                Instantiate(CapriccioniumBuildingPrefab, transform.position, transform.rotation);
                break;
            case BuildButton.Type.Kebabite:
                Instantiate(KebabiteBuildingPrefab, transform.position, transform.rotation);
                break;
            case BuildButton.Type.Rocket:
                Instantiate(RocketPrefab, transform.position, transform.rotation);
                break;
        }
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("Collided with " + other.name);
        if (other.name == "CrushBox" && !isDestroyed) {
            Destroy();
        }
    }
}
