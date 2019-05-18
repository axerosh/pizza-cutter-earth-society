using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Assertions;

public enum ResourceTypes { VESUVIUM, HAWAIIUM, FUNGHITE, SALAMITE };

public class Resources : MonoBehaviour
{
    public Dictionary<ResourceTypes, int> resources;

    // Start is called before the first frame update
    void Start()
    {
        resources = new Dictionary<ResourceTypes, int>();
        foreach (ResourceTypes type in (ResourceTypes[])Enum.GetValues(typeof(ResourceTypes))) {
            resources.Add(type, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool CanSpend(Dictionary<ResourceTypes, int> amounts) {
        foreach (KeyValuePair<ResourceTypes, int> kv in amounts) {
            if (resources[kv.Key] < kv.Value) return false;
        }

        return true;
    }

    bool Spend(Dictionary<ResourceTypes, int> amounts) {
        if (!CanSpend(amounts)) return false;

        foreach (KeyValuePair<ResourceTypes, int> kv in amounts) {
            resources[kv.Key] -= kv.Value;
        }

        return true;
    }

    bool Spend(ResourceTypes type, int amount) {
        var dict = new Dictionary<ResourceTypes, int>();
        dict.Add(type, amount);
        return Spend(dict);
    }

    void Gain(Dictionary<ResourceTypes, int> amounts) {

        foreach (KeyValuePair<ResourceTypes, int> kv in amounts) {
            resources[kv.Key] += kv.Value;
        }
    }

    void Gain(ResourceTypes type, int amount) {
        resources[type] += amount;
    }
}
