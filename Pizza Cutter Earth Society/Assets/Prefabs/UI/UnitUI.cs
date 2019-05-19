using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnitUI : MonoBehaviour {
    public Unit unit;
    public ResourceUI resourcePrefab;


	ResourceUI resourceUI;

    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        if (unit != null && resourceUI != null) {
            transform.position = Camera.main.WorldToScreenPoint (unit.transform.position);
            if (unit.CarriedResourceType != ResourceTypes.NONE && unit.CarriedResourceAmount > 0) {
				resourceUI.gameObject.SetActive (true);
				resourceUI.SetResource(unit.CarriedResourceType, unit.CarriedResourceAmount.ToString());
			} else {
				resourceUI.gameObject.SetActive (false);
            }
        }
    }

    public void Init (Unit unit) {
        this.unit = unit;
		resourceUI = Instantiate (resourcePrefab, Vector3.zero, Quaternion.identity, transform);
		resourceUI.gameObject.name = unit.CarriedResourceType.ToString () + "_Resource";
		resourceUI.gameObject.SetActive (false);
		resourceUI.Init();
	}
}