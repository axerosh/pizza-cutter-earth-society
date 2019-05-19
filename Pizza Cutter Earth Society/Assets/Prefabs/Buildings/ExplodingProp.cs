using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingProp : MonoBehaviour {

    public float forceRadius;
    public float explosionForce;

    public float despawnTime;

    // Start is called before the first frame update
    void Start() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, forceRadius);
        foreach(Collider c in colliders) {
            if(c.name == "Prop") {
                c.attachedRigidbody.AddExplosionForce(explosionForce, transform.position, forceRadius);
            }
        }
    }

    // Update is called once per frame
    void Update() {
        despawnTime -= Time.deltaTime;
        if(despawnTime < 0) {
            Destroy(gameObject);
        }
    }
}
