using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour {

    public NavMeshAgent agent;
    public TextMeshPro selectorText;
    public float moveThreshold;
    public float pickupRadius;
    public float gatherAquisitionRadius;

    private Orders currentOrder;
    private Vector3 moveTarget;
    private ResourcePickup gatherTarget;

    public ResourceTypes? CarriedResourceType = null;
    public int CarriedResourceAmount = 0;
    public List<GameObject> PickupPrefabs;



    public void Select() {
        selectorText.gameObject.SetActive(true);
    }

    public void Unselect() {
        selectorText.gameObject.SetActive(false);
    }

    public void Order(Targetable target, Vector3 targetPosition) {
        switch (target.targetType) {
            case Targets.GROUND:
                MoveOrder(targetPosition);
                break;
            case Targets.RESOURCE:
                GatherOrder(target.targetObject.GetComponent<ResourcePickup>());
                break;
        }
    }

    private void PickupResource(ResourcePickup pickup) {
        CarriedResourceAmount += pickup.resourceQuantity;
        Destroy(pickup.gameObject);
    }

    private void GatherOrder(ResourcePickup pickup) {
        if(CarriedResourceAmount == 0 || CarriedResourceType == pickup.resourceType) {
            currentOrder = Orders.GATHER;
            gatherTarget = pickup;
            CarriedResourceType = pickup.resourceType;
            agent.SetDestination(pickup.gameObject.transform.position);
        }
    }

    private void MoveOrder(Vector3 targetPosition) {
        currentOrder = Orders.MOVE;
        moveTarget = targetPosition;
        agent.SetDestination(targetPosition);
    }

    void UpdateBehavior() {
        switch (currentOrder) {
            case Orders.MOVE:
                if (Vector3.Distance(gameObject.transform.position, moveTarget) <= moveThreshold) {
                    currentOrder = Orders.IDLE;
                }
                break;
            case Orders.GATHER:
                if (gatherTarget == null) {
                    //If old target lost, look around for new resource of that same type.
                    Collider[] potentialTargets = Physics.OverlapSphere(gameObject.transform.position, gatherAquisitionRadius);
                    for(int i = 0; i < potentialTargets.Length; ++i) {
                        ResourcePickup newTarget = potentialTargets[i].gameObject.GetComponent<ResourcePickup>();
                        if (newTarget && newTarget.resourceType == CarriedResourceType) {
                            gatherTarget = newTarget;
                            agent.SetDestination(gatherTarget.transform.position);
                            return;
                        }
                    }
                    //No new targets found
                    if (CarriedResourceAmount == 0) {
                        CarriedResourceType = null;
                    }
                    currentOrder = Orders.IDLE;
                } else {
                    //Check if we have arrived at our target.
                    if(Vector3.Distance(gameObject.transform.position, gatherTarget.transform.position) < pickupRadius) {
                        PickupResource(gatherTarget);
                    }
                }
                break;
        }
    }

    public void DropItems() {
        Debug.Log("A unit wants to drop items. It is carrying " + CarriedResourceAmount + (CarriedResourceType == null ? " of no resource" : " of the resource " + CarriedResourceType));
        if (CarriedResourceType != null && CarriedResourceAmount > 0) {
            foreach (GameObject prefab in PickupPrefabs) {
                if (prefab.GetComponent<ResourcePickup>().resourceType == CarriedResourceType) {
                    GameObject newPickup = Instantiate(prefab);
                    newPickup.transform.position = transform.position;
                    var pickupScript = newPickup.GetComponent<ResourcePickup>();
                    pickupScript.resourceQuantity = CarriedResourceAmount;
                    CarriedResourceAmount = 0;
                    CarriedResourceType = null;
                    break;
                }
            }
        }
    }

    // Update is called once per frame
    void Update() {
        UpdateBehavior();
    }
}
