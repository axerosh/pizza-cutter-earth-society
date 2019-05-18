using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIController : MonoBehaviour {

    public GameObject unitUIPrefab;
    public GameObject buildPlotUI;

    UnitUI selectedUnit;

    Transform canvasTransform;

    public Unit testUnit;

    void Start () {
        canvasTransform = transform.Find ("Canvas");
    }

    void Update () {
        if (Input.GetMouseButtonDown (0)) {
            SelectUnit (testUnit);
        }
        if (Input.GetMouseButtonDown (1)) {
            DeselectUnit ();
        }
    }

    public void SelectUnit (Unit unit) {
        if (selectedUnit != null) {
            DeselectUnit ();
        }

        GameObject unitGO = Instantiate (unitUIPrefab, Camera.main.WorldToScreenPoint (unit.transform.position), Quaternion.identity, canvasTransform);
        unitGO.name = "SelectedUnitUI";
        selectedUnit = unitGO.GetComponent<UnitUI> ();
        selectedUnit.Init (unit);
    }

    public void DeselectUnit () {
        if (selectedUnit != null) {
            Destroy (selectedUnit.gameObject);
            selectedUnit = null;
        }
    }

    public void ShowBuildMenu () {

    }

    public void HideBuildMenu () {

    }
}