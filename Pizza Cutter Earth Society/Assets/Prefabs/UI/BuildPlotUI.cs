using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildPlotUI : MonoBehaviour {

    public delegate void OnBuildButtonClicked (BuildButton btn, BuildingPlot plot);
    public event OnBuildButtonClicked onBuildButtonClicked;

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
            Vector3 newPos = Camera.main.WorldToScreenPoint (buildingPlot.transform.position);
            newPos.z = 0;
            transform.position = newPos;
		}
    }

    public void Init (BuildingPlot plot) {
        buttonContainerTransform = transform.Find ("ButtonContainer");
        for (int i = 0; i < System.Enum.GetValues (typeof (BuildButton.Type)).Length; i++) {
            GameObject buildButtonGO = Instantiate (buildButtonPrefab, Vector3.zero, Quaternion.identity, buttonContainerTransform);
            BuildButton btn = buildButtonGO.GetComponent<BuildButton> ();
            btn.Init ((BuildButton.Type) i);
            btn.button.onClick.AddListener (() => {
                if (onBuildButtonClicked != null) {
                    onBuildButtonClicked (btn, plot);
                }
            });
            buttons.Add (btn);
        }
	}

    public void Refresh (BuildingPlot plot) {
        buildingPlot = plot;

        buttons.ForEach ((btn) => {
            btn.UpdateResources ();
        });
	}
}