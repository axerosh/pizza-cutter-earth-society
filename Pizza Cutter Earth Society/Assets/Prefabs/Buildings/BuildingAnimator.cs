using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingAnimator : MonoBehaviour {

    public float scaleSpeed = 5;
    Vector3 initialScale;

    public float fallSpeed;

    void OnEnable () {
        initialScale = transform.localScale;
        transform.localScale = Vector3.zero;
        StartCoroutine (ScaleUpAsync ());
    }

    void OnDisable () {
        StopCoroutine ("ScaleUpAsync");
    }

    IEnumerator ScaleUpAsync () {
        while (transform.localScale != initialScale) {
            transform.localScale = Vector3.MoveTowards (transform.localScale, initialScale, Time.deltaTime * scaleSpeed);
            yield return null;
        }
        yield return null;
    }
}