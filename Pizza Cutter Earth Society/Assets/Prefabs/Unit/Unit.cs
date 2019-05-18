using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour {

    public NavMeshAgent agent;
    public TextMeshPro selectorText;
    public float moveThreshold;

    private Orders currentOrder;
    private Vector3 moveTarget;

    public void Select() {
        selectorText.gameObject.SetActive(true);
    }

    public void Unselect() {
        selectorText.gameObject.SetActive(false);
    }

    public void Order(Targetable targetObject, Vector3 targetPosition) {
        switch (targetObject.targetType) {
            case Targets.GROUND:
                MoveOrder(targetPosition);
                break;
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
        }
    }

    // Update is called once per frame
    void Update() {
        UpdateBehavior();
    }
}
