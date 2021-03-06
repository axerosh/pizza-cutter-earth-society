﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour {

    public GameObject unitUIPrefab;
    public GameObject buildPlotUIPrefab;

    UnitUI selectedUnit;
    BuildPlotUI selectedBuildPlot;

    public Transform canvasTransform;

    TextMeshProUGUI modeText, dropText;

    CursorController cursor;

    void Start () {
        canvasTransform = transform.Find ("Canvas");
        modeText = canvasTransform.Find ("ModeText").GetComponent<TextMeshProUGUI> ();
        dropText = canvasTransform.Find ("DropText").GetComponent<TextMeshProUGUI> ();
        ShowDropText (false);

        cursor = transform.Find ("Cursor").GetComponent<CursorController> ();
		
		foreach (Unit unit in FindObjectsOfType<Unit>())
		{
			unit.AddUnitUI(canvasTransform);
		}
		foreach (Building building in FindObjectsOfType<Building>())
		{
			building.AddprogressUI(canvasTransform);
		}
	}

    void Update () {

    }

    void OnBuildButtonClicked (BuildButton btn, BuildingPlot plot) {
        Debug.Log ("Clicked building button: " + btn.type);
        UnselectBuildingPlot();
        plot.ChooseBuilding(btn.type);
    }

    public void SelectUnit (Unit unit) {
        if (selectedUnit != null) {
            UnselectUnit ();
        }
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

        GameObject plotGO = Instantiate (buildPlotUIPrefab,
            Camera.main.WorldToScreenPoint (plot.transform.position), Quaternion.identity, canvasTransform);
        plotGO.name = "SelectedBuildPlotUI";

        selectedBuildPlot = plotGO.GetComponent<BuildPlotUI> ();
        selectedBuildPlot.onBuildButtonClicked += OnBuildButtonClicked;
        selectedBuildPlot.onBuildPlotDestroyed += OnPlotDestroyed;
        selectedBuildPlot.Init (plot);
    }

    public void UnselectBuildingPlot () {
        if (selectedBuildPlot != null) {
            selectedBuildPlot.onBuildButtonClicked -= OnBuildButtonClicked;
            Destroy (selectedBuildPlot.gameObject);
            selectedBuildPlot = null;
        }
    }

    public void OnPlotDestroyed(BuildPlotUI plotUI) {
        if (selectedBuildPlot == plotUI) {
            UnselectBuildingPlot();
        }
    }

    public void ShowDropText (bool show) {
        dropText.gameObject.SetActive (show);
    }

    public void UpdateMode (Player.Mode mode) {
        modeText.text = string.Format ("<color=#ffff00>[B]</color> Switch to {0} mode", mode == Player.Mode.Build ? Player.Mode.Selection : Player.Mode.Build);
        cursor.ShowCursor (mode);
    }

    public BuildCursor GetCursor(){
        return cursor.GetCursor();
    }
}