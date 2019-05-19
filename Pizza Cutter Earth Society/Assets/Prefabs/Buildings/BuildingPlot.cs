using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlot : MonoBehaviour {

    public GameObject CapriccioniumBuildingPrefab;
    public GameObject KebabiteBuildingPrefab;
    public GameObject RocketPrefab;

    public void ChooseBuilding(BuildButton.Type type) {
        Destroy(gameObject);
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
}
