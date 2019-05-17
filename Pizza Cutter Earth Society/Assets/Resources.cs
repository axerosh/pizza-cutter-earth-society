using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Resources : MonoBehaviour
{
    public int[] resources;
    public string[] resourceNames;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool spend(int[] amounts) {
        Assert.AreEqual(amounts.Length, resources.Length);

        for (int i = 0; i < amounts.Length; ++i) {
            if (amounts[i] > resources[i]) return false;
        }

        for (int i = 0; i < amounts.Length; ++i) {
            resources[i] -= amounts[i];
        }

        return true;
    }

    void gain(int[] amounts) {
        Assert.AreEqual(amounts.Length, resources.Length);

        for (int i = 0; i < amounts.Length; ++i) {
            resources[i] += amounts[i];
        }
    }
}
