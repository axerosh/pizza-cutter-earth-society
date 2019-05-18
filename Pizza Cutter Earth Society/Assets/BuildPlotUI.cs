using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildPlotUI : MonoBehaviour {

    public GameObject buildButtonPrefab;

    public BuildingPlot buildingPlot;

    Transform resourcesTransform, buttonContainerTransform;
    List<BuildButton> buttons = new List<BuildButton> ();

    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        if (buildingPlot) {
            transform.position = Camera.main.WorldToScreenPoint (buildingPlot.transform.position);
        }
    }

    public void Init (BuildingPlot plot) {
        buttonContainerTransform = transform.Find ("ButtonContainer");
        for (int i = 0; i < System.Enum.GetValues (typeof (BuildButton.Type)).Length; i++) {
            GameObject buildButtonGO = Instantiate (buildButtonPrefab, Vector3.zero, Quaternion.identity, buttonContainerTransform);
            BuildButton btn = buildButtonGO.GetComponent<BuildButton> ();
            btn.Init ((BuildButton.Type) i);
            btn.button.onClick.AddListener (() => OnButtonClicked (btn));
            buttons.Add (btn);
        }

        buildingPlot = plot;
    }

    public void Refresh (BuildingPlot plot) {
        buildingPlot = plot;

        buttons.ForEach ((btn) => {
            btn.UpdateResources();
        });
    }

    void OnButtonClicked (BuildButton btn) {
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
}