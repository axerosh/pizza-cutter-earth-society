using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitUI : MonoBehaviour {
    public Unit unit;
    public GameObject resourcePrefab;
    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {

        if (unit != null) {
            transform.position = Camera.main.WorldToScreenPoint (unit.transform.position);

            //TODO: Update Unit resources
        }
    }

    public void Init (Unit unit) {
        this.unit = unit;

        //TODO: Init resources

    }
}