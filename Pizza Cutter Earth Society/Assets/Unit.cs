using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour {

    public NavMeshAgent agent;
    public TextMeshPro selectorText;
    public float moveThreshold;

    private bool selected = false;
    private Orders currentOrder;
    private Vector3 moveTarget;

    public void Select() {
        selected = true;
        selectorText.gameObject.SetActive(true);
        Debug.Log("Unit selected");
    }

    public void Unselect() {
        selected = false;
        selectorText.gameObject.SetActive(false);
        Debug.Log("Unit unselected");
    }

    public void Order(GameObject targetObject, Vector3 targetPosition) {
        if (targetObject.name == "Ground") {
            MoveOrder(targetPosition);
        }
    }

    private void MoveOrder(Vector3 targetPosition) {
        currentOrder = Orders.MOVE;
        moveTarget = targetPosition;
        agent.SetDestination(targetPosition);
    }

    void OnMouseOver() {
        if (Input.GetMouseButtonDown(0)) {
            Debug.Log("Unit selected, yaaay");
        }
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
        
    }
}
