using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {

    public GameObject buildingMesh;
    public GameObject plotBox;

    public GameObject productionPrefab;
    public List<ResourceTypes> constructionTypes;
    public List<int> constructionAmounts;
    public Dictionary<ResourceTypes, int> constructionRequirements = new Dictionary<ResourceTypes, int>();
    public List<ResourceTypes> processingResources;
    private Dictionary<ResourceTypes, int> gatheredConstructionResources = new Dictionary<ResourceTypes, int>();
    private Dictionary<ResourceTypes, int> gatheredProcessingResources = new Dictionary<ResourceTypes, int>();

    public float totalProductionTime;
    private float productionTimer;

    public float resourceSpawnRadius;

    bool built = false;

    private void OnTriggerEnter(Collider other) {
        Debug.Log("Collided with " + other.name);
        if(other.name == "CrushBox") {
            Destroy(gameObject);
        }
    }

    void Start() {
        Debug.Assert(constructionTypes.Count == constructionAmounts.Count, "INCORRECT BUILD REQUIREMENTS");
        for (int i = 0; i < constructionTypes.Count; ++i) {
            constructionRequirements.Add(constructionTypes[i], constructionAmounts[i]);
            gatheredConstructionResources.Add(constructionTypes[i], 0);
        }
        foreach (ResourceTypes t in processingResources) {
            gatheredProcessingResources.Add(t, 0);
        }
    }

    public int Deliver(ResourceTypes type, int amount) {
        Debug.Log(string.Format("Recieved {0} {1}", amount, type));
        
        if (built) {
            if (gatheredProcessingResources.ContainsKey(type)) {
                Debug.LogFormat("Finished building accepted {0}/{0} {1}", amount, type);
                gatheredProcessingResources[type] += amount;
                return amount;
            } else {
                Debug.LogFormat("Finished building rejected {0} {1}", amount, type);
            }
        } else {
            if (gatheredConstructionResources.ContainsKey(type)) {
                int acceptedAmount = System.Math.Min(constructionRequirements[type] - gatheredConstructionResources[type], amount);
                Debug.LogFormat("Unfinished building accepted {0}/{1} {2}", acceptedAmount, amount, type);
                gatheredConstructionResources[type] += acceptedAmount;
                CheckCompletion();
                return acceptedAmount;
            } else {
                Debug.LogFormat("Unfinished building rejected {0} {1}", amount, type);
            }
        }
        return 0;
    }

    private void CheckCompletion() {
        foreach (ResourceTypes type in new List<ResourceTypes>(constructionRequirements.Keys)) {
            if (gatheredConstructionResources[type] < constructionRequirements[type]) {
                Debug.Log("A building is still unfinished");
                return; // Not enough materials
            }
        }
        Debug.Log("A building has been finished");
        buildingMesh.SetActive(true);
        plotBox.SetActive(true);
        gameObject.GetComponent<Targetable>().targetType = Targets.BUILDING;
        built = true;
    }

    public bool RequiresResource(ResourceTypes type) {
        if (built) {
            return processingResources.Contains(type);
        } else {
            return    constructionRequirements.ContainsKey(type)
                   && constructionRequirements[type] > gatheredConstructionResources[type];
        }
    }

    private void CreateResource() {
        foreach(ResourceTypes type in new List<ResourceTypes>(gatheredProcessingResources.Keys)) {
            Debug.Log("AAAAA");
            gatheredProcessingResources[type] -= 1;
        }
        GameObject instance = Instantiate(productionPrefab);
        Vector2 randPt = Random.insideUnitCircle * resourceSpawnRadius;
        instance.transform.Translate(randPt.x, 0, randPt.y);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.F2)) {
            string printString = "Printing building contents:\n";
            if (built) {
                foreach(var kv in gatheredProcessingResources) {
                    printString += string.Format("{0} of the resource {1}\n", kv.Value, kv.Key);
                }
            } else {
                foreach (KeyValuePair<ResourceTypes, int> kv in gatheredConstructionResources) {
                    printString += string.Format("{0} of the resource {1}\n", kv.Value, kv.Key);
                }
            }
            Debug.Log(printString);
        }

        if(built && productionTimer < 0) {
            foreach(int amount in gatheredProcessingResources.Values) {
                if(amount <= 0) {
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
