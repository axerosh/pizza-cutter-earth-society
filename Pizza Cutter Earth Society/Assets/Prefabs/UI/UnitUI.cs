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
        if (unit != null && resourceGO != null) {
            transform.position = Camera.main.WorldToScreenPoint (unit.transform.position);
            if (unit.CarriedResourceType != ResourceTypes.NONE && unit.CarriedResourceAmount > 0) {
                resourceGO.SetActive (true);
                nameText.text = unit.CarriedResourceType.ToString () + ":";
                valueText.text = unit.CarriedResourceAmount.ToString ();
            } else {
                resourceGO.SetActive (false);
            }
        }
    }

    public void Init (Unit unit) {
        this.unit = unit;
        resourceGO = Instantiate (resourcePrefab, Vector3.zero, Quaternion.identity, transform);
        resourceGO.name = unit.CarriedResourceType.ToString () + "_Resource";
        nameText = resourceGO.transform.Find ("NameText").GetComponent<TextMeshProUGUI> ();
        valueText = resourceGO.transform.Find ("ValueText").GetComponent<TextMeshProUGUI> ();
    }
}