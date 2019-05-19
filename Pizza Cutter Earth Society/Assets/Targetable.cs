using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetable : MonoBehaviour {

    public Targets targetType;
    public GameObject targetObject;

    public bool RequiresResource(ResourceTypes type) {
        Building deliveryPoint = targetObject.GetComponent<Building>();
        if(deliveryPoint != null) {
            return deliveryPoint.RequiresResource(type);
        }
        return false;
    }
}
