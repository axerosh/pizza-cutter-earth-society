using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour {

    public GameObject unitUIPrefab;
    public GameObject buildPlotUIPrefab;

    UnitUI selectedUnit;
    BuildPlotUI selectedBuildPlot;

    Transform canvasTransform;

    TextMeshProUGUI modeText, dropText;

    CursorController cursor;

    public bool buildButtonClicked = false;

    void Start () {
        canvasTransform = transform.Find ("Canvas");
        modeText = canvasTransform.Find ("ModeText").GetComponent<TextMeshProUGUI> ();
        dropText = canvasTransform.Find ("DropText").GetComponent<TextMeshProUGUI> ();
        ShowDropText (false);

        cursor = transform.Find ("Cursor").GetComponent<CursorController> ();
    }

    void Update () {

    }

    void OnBuildButtonClicked (BuildButton btn, BuildingPlot plot) {
        buildButtonClicked = true;
        Debug.Log ("Clicked building button: " + btn.type);

        switch (btn.type) {
            case BuildButton.Type.Capricosium:
                break;
            case BuildButton.Type.Kebabite:
                break;
            case BuildButton.Type.Rocket:
                break;
            default:
                break;
        }
    }

    public void SelectUnit (Unit unit) {
        if (selectedUnit != null) {
            UnselectUnit ();
        }

        GameObject unitGO = Instantiate (unitUIPrefab, Camera.main.WorldToScreenPoint (unit.transform.position), Quaternion.identity, canvasTransform);
        unitGO.name = "SelectedUnitUI";
        selectedUnit = unitGO.GetComponent<UnitUI> ();
        selectedUnit.Init (unit);
    }

    public void UnselectUnit () {
        if (selectedUnit != null) {
            Destroy (selectedUnit.gameObject);
            selectedUnit = null;
        }
    }

    public void SelectBuildingPlot (BuildingPlot plot) {
        if (selectedBuildPlot && selectedBuildPlot.buildingPlot == plot) {
            //Do nothing if it's the same plot
            return;
        }

        if (selectedBuildPlot != null) {
            UnselectBuildingPlot ();
        }

        buildButtonClicked = false;

        GameObject plotGO = Instantiate (buildPlotUIPrefab,
            Camera.main.WorldToScreenPoint (plot.transform.position), Quaternion.identity, canvasTransform);
        plotGO.name = "SelectedBuildPlotUI";

        selectedBuildPlot = plotGO.GetComponent<BuildPlotUI> ();
        selectedBuildPlot.onBuildButtonClicked += OnBuildButtonClicked;
        selectedBuildPlot.Init (plot);
    }

    public void UnselectBuildingPlot () {
        if (buildButtonClicked) {
            buildButtonClicked = false;
            return;
        }

        if (selectedBuildPlot != null) {
            selectedBuildPlot.onBuildButtonClicked -= OnBuildButtonClicked;
            Destroy (selectedBuildPlot.gameObject);
            selectedBuildPlot = null;
        }
    }

    public void ShowDropText (bool show) {
        dropText.gameObject.SetActive (show);
    }

    public void UpdateMode (Player.Mode mode) {
        modeText.text = string.Format ("<color=#ffff00>[B]</color> Switch to {0} mode", mode == Player.Mode.Build ? Player.Mode.Selection : Player.Mode.Build);
        cursor.ShowCursor (mode);
    }
}