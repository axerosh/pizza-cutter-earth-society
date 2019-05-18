using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetable : MonoBehaviour {

    public Targets targetType;
    private GameObject targetObject;

    public GameObject GetObject() {
        return targetObject;
    }

    private void Start() {
        targetObject = gameObject;
    }
}
