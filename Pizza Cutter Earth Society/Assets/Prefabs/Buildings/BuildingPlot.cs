using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlot : MonoBehaviour, IDeliver {

    public List<ResourceTypes> requiredTypes;
    public List<int> requiredAmounts;

    public GameObject building; //Finished building.
    public GameObject plotBox; //Used to display the building site.

    private Dictionary<ResourceTypes, int> buildRequirements = new Dictionary<ResourceTypes, int>();
    private Dictionary<ResourceTypes, int> gatheredResources = new Dictionary<ResourceTypes, int>();

    private void Start() {

        Debug.Assert(requiredTypes.Count == requiredAmounts.Count, "INCORRECT BUILD REQUIREMENTS");

        for(int i = 0; i < requiredTypes.Count; ++i) {
            buildRequirements.Add(requiredTypes[i], requiredAmounts[i]);
            gatheredResources.Add(requiredTypes[i], 0);
        }
    }

    //Checks if building complete, if so, activates it and makes the plot invisible.
    private void CheckCompletion() {
        foreach(ResourceTypes type in buildRequirements.Keys) {
            if(gatheredResources[type] < buildRequirements[type]) {
                return; //Materials still missing.
            }
        }
        building.SetActive(true);
        plotBox.SetActive(false);
    }

    /*
     * Returns actual amount of resources used.
    */
    public int Deliver(ResourceTypes type, int amount) {
        if (buildRequirements.ContainsKey(type)) {
            //either use all of amount, or as many as are needed to fill the type requirement.
            int actualAmount = Mathf.Min(amount, (buildRequirements[type] - gatheredResources[type]));
            gatheredResources[type] += actualAmount;
            
            return actualAmount;
        }
        return 0;
    }

    public bool RequiresResource(ResourceTypes type) {
        return buildRequirements.ContainsKey(type);
    }
}
