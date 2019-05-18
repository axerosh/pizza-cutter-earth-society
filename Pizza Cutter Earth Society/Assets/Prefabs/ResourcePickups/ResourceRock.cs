using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ResourceRock : MonoBehaviour
{
    public int hp;
    private int currentHp;

    public GameObject pickupPrefab;
    public int totalContents;
    public int LARGEST_SINGLE_DROP = 10;
    public int DROP_RANGE = 5;

    void Start() {
        currentHp = hp;
    }

    public void Damage(int amount) {
        Debug.Log("A rock lost " + amount + " health and now has " + (currentHp - amount) + " health");
        int hpBefore = currentHp;
        int hpAfter = currentHp - amount;

        int hpPerResource = (int) Math.Ceiling((float) hp / totalContents);

        int resourcesDropped = (hpBefore / hpPerResource) - (hpAfter / hpPerResource);

        if (resourcesDropped > 0) {
            Debug.Log("A rock dropped " + resourcesDropped + " resources");
            GameObject newDrop = Instantiate(pickupPrefab);
            newDrop.GetComponent<ResourcePickup>().resourceQuantity = resourcesDropped;
            Vector2 rand = Random.insideUnitCircle * DROP_RANGE;
            newDrop.transform.position = transform.position + new Vector3(rand.x, 0, rand.y);
        }
        
        currentHp = hpAfter;
        
        if (currentHp <= 0) {
            Destroy(gameObject);
        }
    }
}
