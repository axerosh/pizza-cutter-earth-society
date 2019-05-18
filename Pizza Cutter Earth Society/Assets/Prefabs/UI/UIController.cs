using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour {

    public GameObject unitUIPrefab;
    public GameObject buildPlotUI;

    UnitUI selectedUnit;

    Transform canvasTransform;

    TextMeshProUGUI modeText, dropText;

    CursorController cursor;

    void Start () {
        canvasTransform = transform.Find ("Canvas");
        modeText = canvasTransform.Find ("ModeText").GetComponent<TextMeshProUGUI> ();
        dropText = canvasTransform.Find ("DropText").GetComponent<TextMeshProUGUI> ();
        ShowDropText (false);

        cursor = transform.Find ("Cursor").GetComponent<CursorController> ();
    }

    void Update () {

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

    public void ShowBuildMenu () {

    }

    public void HideBuildMenu () {

    }

    public void ShowDropText (bool show) {
        dropText.gameObject.SetActive (show);
    }

    public void UpdateMode (Player.Mode mode) {
        modeText.text = string.Format ("<color=#ffff00>[B]</color> Switch to {0} mode", mode == Player.Mode.Build ? Player.Mode.Selection : Player.Mode.Build);
        cursor.ShowCursor (mode);
    }
}