using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePickup : MonoBehaviour
{
    public ResourceTypes resourceType;
    public int resourceQuantity;

    void Start() {
        float scale = (float) System.Math.Pow(resourceQuantity, 1 / 3);
        gameObject.transform.localScale = new Vector3(scale, scale, scale);
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
