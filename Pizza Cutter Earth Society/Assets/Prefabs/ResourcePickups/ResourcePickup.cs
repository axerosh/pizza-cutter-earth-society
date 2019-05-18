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
}
