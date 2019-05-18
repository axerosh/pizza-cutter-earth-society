﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePickup : MonoBehaviour
{
    public ResourceTypes resourceType;
    public int resourceQuantity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        var unitScript = other.gameObject.GetComponent<Unit>();
        if (unitScript != null) {
            if (unitScript.CarriedResourceType == null) {
                unitScript.CarriedResourceType = resourceType;
                unitScript.CarriedResourceAmount = resourceQuantity;
            } else if (unitScript.CarriedResourceType == resourceType) {
                // TODO: Cap
                unitScript.CarriedResourceAmount += resourceQuantity;
            } else return;

            Object.Destroy(gameObject);
        }
    }
}