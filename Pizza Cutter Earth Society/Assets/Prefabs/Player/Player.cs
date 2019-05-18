using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public enum Mode {
        Selection, Build
    }
    Mode mode = Mode.Selection;

    UIController ui;
    List<Unit> selected = new List<Unit>();
    BuildingPlot selectedBuildingPlot;

    void Start() {
        ui = transform.Find("UI").GetComponent<UIController>();
    }

    void Select(Unit unit) {
        UnselectPlot();

        unit.Select();
        unit.onResourceCollected += OnResourceCollected;
        ui.SelectUnit(unit);
        selected.Add(unit);
    }

    void UnselectAll() {
        foreach(Unit u in selected) {
            u.Unselect();
            u.onResourceCollected -= OnResourceCollected;
        }
        ui.UnselectUnit();
        selected.Clear();
    }

    void SelectPlot(BuildingPlot plot) {
        UnselectAll();
        UnselectPlot();

        if (plot){
            selectedBuildingPlot = plot;
            ui.SelectBuildingPlot(plot);    
        }
    }

    void UnselectPlot() {
        if (selectedBuildingPlot){
            selectedBuildingPlot = null;
            ui.UnselectBuildingPlot();
        }
    }

    void ToggleMode() {
        mode = mode == Mode.Build ? Mode.Selection : Mode.Build;
        ui.UpdateMode(mode);

        if (mode == Mode.Build) {
            UnselectAll();
        } else {
            UnselectPlot();
        }

        Debug.Log("Mode: " + mode.ToString());
    }

    void UpdateInput() {
        if (Input.GetMouseButtonDown(0)){
            //Unselect all units and plots on left-click, always.
            UnselectAll();

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit hit)) {
                //If a left-click hits a unit, select it.
                Unit hitUnit = hit.transform.GetComponent<Unit>();
                if (hitUnit) {
                    Select(hitUnit);                    
                } else {
                    BuildingPlot plot  = hit.transform.parent.GetComponent<BuildingPlot>();
                    if (plot) {                        
                        SelectPlot(plot);
                    } else if (hit.transform.gameObject.layer != LayerMask.NameToLayer("UI")) {
                        //Deselect plot if no UI element was clicked
                        UnselectPlot();
                    }
                }
            }
        }
        if (Input.GetMouseButtonDown(1) && selected.Count > 0) {
            //On a right-click, try to give orders to selected units, if there are any.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit)) {
                //If a left click hits a unit, select it.
                Targetable hitTarget = hit.transform.gameObject.GetComponent<Targetable>();

                if (hitTarget) {
                    //Pass hit object and hit position to every unit, let them figure out what to do with it.
                    foreach (Unit u in selected) {
                        u.Order(hitTarget, hit.point);
                    }
                }
            }
        }

        if (Input.GetAxis("Drop") > 0) {
            foreach (Unit u in selected) {
                u.DropItems();
            }
            ui.ShowDropText(false);
        }

        if (Input.GetKeyDown(KeyCode.B)) {
            ToggleMode();
        }
    }

    void OnResourceCollected(Unit unit) {
        ui.ShowDropText(true);
    }

    // Update is called once per frame
    void Update() {
        UpdateInput();
    }
}