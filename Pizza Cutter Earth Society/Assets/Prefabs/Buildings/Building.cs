using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour, IDeliver {

    public GameObject productionInstance;
    public List<ResourceTypes> requiredResources;
    private Dictionary<ResourceTypes, int> currentResources = new Dictionary<ResourceTypes, int>();

    public float totalProductionTime;
    private float productionTimer;

    private void OnTriggerEnter(Collider other) {
        if(other.name == "CrushBox") {
            Destroy(gameObject);
        }
    }

    void Start() {
        foreach(ResourceTypes type in requiredResources) {
            currentResources.Add(type, 0);
        }
    }

    public int Deliver(ResourceTypes type, int amount) {
        currentResources[type] += amount;
        return amount;
    }

    public bool RequiresResource(ResourceTypes type) {
        return requiredResources.Contains(type);
    }

    private void CreateResource() {
        foreach(ResourceTypes type in currentResources.Keys) {
            currentResources[type] -= 1;
        }
        Instantiate(productionInstance);
    }

    // Update is called once per frame
    void Update() {
        if(productionTimer < 0) {
            foreach(int amount in currentResources.Values) {
                if(amount == 0) {
                    return;
                }
            }
            //Have all materials.
            CreateResource();
            productionTimer = totalProductionTime;
        }
        productionTimer -= Time.deltaTime;
    }
}
