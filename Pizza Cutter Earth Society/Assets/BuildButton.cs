using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildButton : MonoBehaviour {
    public enum Type {
        Rocket,
        Capricosium,
        Kebabite
    }
    public Type type;

    public Button button;

    public GameObject resourcePrefab;

    public void Init (Type type) {
        this.type = type;

        button = GetComponent<Button> ();
        var text = transform.GetComponentInChildren<TextMeshProUGUI> ();
        if (text) {
            text.text = type.ToString ();
        }

        UpdateResources ();
    }

    public void UpdateResources () {
        //TODO: update resources
    }
}