using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    public List<GameObject> spawnables;

    public float spawnChance;
    public float spawnChanceInterval;
    private float spawnTimer = 0;

    private BoxCollider spawnArea;

    void Start() {
        spawnArea = GameObject.Find("SpawnArea").GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3)) {
            Debug.Log(spawnArea.bounds);
        }
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnChanceInterval) {
            spawnTimer -= spawnChanceInterval;
            if (Random.value <= spawnChance) {
                int which = Random.Range(0, spawnables.Count); // TODO weight?
                GameObject go = Instantiate(spawnables[which], spawnArea.transform);

                
                float x = 20*Random.Range(-spawnArea.bounds.size.x / 2, spawnArea.bounds.size.x / 2);
                float y = 20*Random.Range(-spawnArea.bounds.size.y / 2, spawnArea.bounds.size.y / 2);
                float z = 20*Random.Range(-spawnArea.bounds.size.z / 2, spawnArea.bounds.size.z / 2);

                go.transform.localPosition = new Vector3(x, y, z);

                go.transform.SetParent(null, true);
                go.transform.localScale = new Vector3(1, 1, 1);
                
                
            } else Debug.Log("Did not spawn a resource");
        }
    }
}
