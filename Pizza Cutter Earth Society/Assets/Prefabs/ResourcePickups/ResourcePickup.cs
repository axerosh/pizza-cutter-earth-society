using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePickup : MonoBehaviour
{
    public ResourceTypes resourceType;

    public int _resqty = 1;
    public int resourceQuantity { get { return _resqty; } set {
            _resqty = value;
            float scale = (float)System.Math.Pow(value, 1.0f / 3);
            gameObject.transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}
