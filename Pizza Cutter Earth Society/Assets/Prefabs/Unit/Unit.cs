using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour {
    public delegate void OnResourceCollected(Unit unit);
    public event OnResourceCollected onResourceCollected;


    public NavMeshAgent agent;
    public TextMeshPro selectorText;
    public float moveThreshold;
    public float interactRadius;
    public float gatherAquisitionRadius;

    private Orders currentOrder;
    private Vector3 moveTarget;
    private ResourcePickup gatherTarget;
    private Targetable deliverTarget;

    public ResourceTypes CarriedResourceType;
    public int CarriedResourceAmount = 0;
    public List<GameObject> PickupPrefabs;



    public void Select() {
        selectorText.gameObject.SetActive(true);
    }

    public void Unselect() {
        selectorText.gameObject.SetActive(false);
    }

    public void Order(Targetable target, Vector3 targetPosition) {
        Debug.Log("Interact with " + target.targetType);
        switch (target.targetType) {
            case Targets.GROUND:
                MoveOrder(targetPosition);
                break;
            case Targets.RESOURCE:
                GatherOrder(target.targetObject.GetComponent<ResourcePickup>());
                break;
            case Targets.BUILD_PLOT:
            case Targets.BUILDING:
                //These are essentially the same.
                DeliverOrder(target);
                break;
            case Targets.ROCK:
                MineOrder(target);
                break;
        }
    }

    private void PickupResource(ResourcePickup pickup) {
        CarriedResourceAmount += pickup.resourceQuantity;
        CarriedResourceType = pickup.resourceType; // Just in case
        Destroy(pickup.gameObject);

        onResourceCollected?.Invoke(this);
    }

    private void DeliverOrder(Targetable target) {
        if(CarriedResourceAmount > 0 && target.RequiresResource(CarriedResourceType)) {
            currentOrder = Orders.DELIVER;
            deliverTarget = target;
            agent.SetDestination(target.targetObject.transform.position);
        }
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

    public int miningRadius;
    public float miningCooldown;
    public float miningTimer;
    public int miningDamage;
    GameObject miningTarget = null;
    private void MineOrder(Targetable target) {
        currentOrder = Orders.MINE;
        miningTarget = target.targetObject;
        agent.SetDestination(miningTarget.transform.position);
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
                        ResourcePickup newTarget = null;
                        Transform parent = potentialTargets[i].gameObject.transform.parent;
                        if (parent) {
                            newTarget = potentialTargets[i].gameObject.transform.parent.GetComponent<ResourcePickup>();
                        }
                        if (newTarget && newTarget.resourceType == CarriedResourceType) {
                            gatherTarget = newTarget;
                            agent.SetDestination(gatherTarget.transform.position);
                            return;
                        }
                    }
                    //No new targets found
                    if (CarriedResourceAmount == 0) {
                        CarriedResourceType = ResourceTypes.NONE;
                    }
                    currentOrder = Orders.IDLE;
                } else {
                    Debug.Log("Have target");
                    //Check if we have arrived at our target.
                    if(Vector3.Distance(gameObject.transform.position, gatherTarget.transform.position) < interactRadius) {
                        Debug.Log("In range");
                        PickupResource(gatherTarget);
                    }
                }
                break;
            case Orders.MINE:
                if (miningTarget == null) {
                    currentOrder = Orders.IDLE;
                    agent.SetDestination(transform.position);
                    break;
                }
                if (Vector3.Distance(transform.position, miningTarget.transform.position) < miningRadius) {
                    agent.SetDestination(transform.position);
                    if (miningTimer <= 0) {
                        miningTimer = miningCooldown;
                        miningTarget.GetComponent<ResourceRock>().Damage(miningDamage);
                    }
                } else {
                    agent.SetDestination(miningTarget.transform.position);
                }
                break;
            case Orders.DELIVER:
                if (deliverTarget) {
                    //Check if we have arrived at target.
                    if(Vector3.Distance(gameObject.transform.position, deliverTarget.targetObject.transform.position) < interactRadius) {
                        Debug.Log("In range of building");
                        CarriedResourceAmount -= deliverTarget.targetObject.GetComponent<IDeliver>().Deliver(CarriedResourceType, CarriedResourceAmount);
                        currentOrder = Orders.IDLE;
                    }
                } else {
                    currentOrder = Orders.IDLE;
                }
                break;
        }
    }

    public void DropItems() {
        Debug.Log("A unit wants to drop items. It is carrying " + CarriedResourceAmount + (CarriedResourceType == ResourceTypes.NONE ? " of no resource" : " of the resource " + CarriedResourceType));
        if (CarriedResourceType != ResourceTypes.NONE && CarriedResourceAmount > 0) {
            foreach (GameObject prefab in PickupPrefabs) {
                if (prefab.GetComponent<ResourcePickup>().resourceType == CarriedResourceType) {
                    GameObject newPickup = Instantiate(prefab);
                    newPickup.transform.position = transform.position;
                    var pickupScript = newPickup.GetComponent<ResourcePickup>();
                    pickupScript.resourceQuantity = CarriedResourceAmount;
                    CarriedResourceAmount = 0;
                    CarriedResourceType = ResourceTypes.NONE;
                    break;
                }
            }
        }
    }

    // Update is called once per frame
    void Update() {
        if (miningTimer > 0) miningTimer -= Time.deltaTime;
        UpdateBehavior();
    }
}

