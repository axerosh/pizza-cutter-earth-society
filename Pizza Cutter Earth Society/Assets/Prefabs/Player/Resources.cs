using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public enum ResourceTypes { VESUVIUM, HAWAIIUM, FUNGHITE, SALAMITE };

public class Resources : MonoBehaviour
{
    public int[] resources;
    public string[] resourceNames;

    // Start is called before the first frame update
    void Start()
    {
        resources = new int[resourceNames.Length];
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

    bool spend(string type, int amount) {
        for (int i = 0; i < resourceNames.Length; ++i) {
            if (resourceNames[i] == type) {
                if (resources[i] < amount) return false;
                resources[i] -= amount;
                return true;
            }
        }
        throw new System.ArgumentException("Invalid resource: " + type);
    }

    void gain(int[] amounts) {
        Assert.AreEqual(amounts.Length, resources.Length);

        for (int i = 0; i < amounts.Length; ++i) {
            resources[i] += amounts[i];
        }
    }

    void gain(string type, int amount) {
        spend(type, -amount);
    }
}
