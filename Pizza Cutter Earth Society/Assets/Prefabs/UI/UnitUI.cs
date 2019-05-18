using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnitUI : MonoBehaviour {
    public Unit unit;
    public GameObject resourcePrefab;

    TextMeshProUGUI nameText, valueText;

    GameObject resourceGO;

    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        if (unit != null) {
            transform.position = Camera.main.WorldToScreenPoint (unit.transform.position);
            if (unit.CarriedResourceType != null && unit.CarriedResourceAmount > 0) {
                if (resourceGO == null) {
                    resourceGO = Instantiate (resourcePrefab, Vector3.zero, Quaternion.identity, transform);
                    resourceGO.name = unit.CarriedResourceType.ToString () + "_Resource";
                    nameText = resourceGO.transform.Find ("NameText").GetComponent<TextMeshProUGUI> ();
                    valueText = resourceGO.transform.Find ("ValueText").GetComponent<TextMeshProUGUI> ();
                }
                nameText.text = unit.CarriedResourceType.ToString () + ":";
                valueText.text = unit.CarriedResourceAmount.ToString ();
            } else {
                Destroy (resourceGO);
                nameText = null;
                valueText = null;
            }
        }
    }

    public void Init (Unit unit) {
        this.unit = unit;
    }
}