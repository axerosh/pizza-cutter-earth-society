using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ResourceRock : MonoBehaviour
{
    public int hp;

    public GameObject pickupPrefab;
    public int totalContents;
    public int LARGEST_SINGLE_DROP = 10;
    public int DROP_RANGE = 5;

    public void Damage(int amount) {
        hp -= amount;
        Debug.Log("A rock lost " + amount + " health and now has " + hp + " health");
        if (hp <= 0) {
            while (totalContents > 0) {
                int maxDrop = Math.Min(totalContents, LARGEST_SINGLE_DROP);
                int dropSize = UnityEngine.Random.Range(1, maxDrop);
                totalContents -= dropSize;

                GameObject newDrop = Instantiate(pickupPrefab);
                newDrop.GetComponent<ResourcePickup>().resourceQuantity = dropSize;
                Vector2 rand = Random.insideUnitCircle * DROP_RANGE;
                newDrop.transform.position = transform.position + new Vector3(rand.x, 0, rand.y);
            }
            Destroy(gameObject);
        }
    }
}
