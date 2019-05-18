using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    List<Unit> selected = new List<Unit>();

    void UnselectAll() {
        foreach(Unit u in selected) {
            u.Unselect();
        }
        selected.Clear();
    }

    void UpdateInput() {
        if (Input.GetMouseButtonDown(0)){
            //Unselect all units on left-click, always.
            UnselectAll();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit hit)) {
                //If a left-click hits a unit, select it.
                Unit hitUnit = hit.transform.gameObject.GetComponent<Unit>();
                if (hitUnit) {
                    hitUnit.Select();
                    selected.Add(hitUnit);
                }
            }
        }
        if (Input.GetMouseButtonDown(1) && selected.Count > 0) {
            //On a right-click, try to give orders to selected units, if there are any.
            Debug.Log("Right-click!");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit)) {
                Debug.Log("Ray hit!");
                //If a left click hits a unit, select it.
                Targetable hitTarget = hit.transform.gameObject.GetComponent<Targetable>();

                if (hitTarget) {
                    Debug.Log("Hit target!");
                    //Pass hit object and hit position to every unit, let them figure out what to do with it.
                    foreach (Unit u in selected) {
                        u.Order(hitTarget, hit.point);
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update() {
        UpdateInput();
    }
}
