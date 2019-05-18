using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceDeposit : MonoBehaviour
{
    public Color color;
    public string resourceName;
    public int quantity;

    // Start is called before the first frame update
    void Start()
    {
        var mat = gameObject.GetComponent<Material>();
        mat.SetColor("_Color", color);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
