using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildCursor : MonoBehaviour {

    public Color disabledColor;

    Color initialColor;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer> ();
        initialColor = spriteRenderer.material.color;
    }

    // Update is called once per frame
    void Update () {

    }

    private void OnTriggerEnter (Collider other) {
        if (!other.CompareTag ("BuildableArea")) {
            spriteRenderer.material.color = disabledColor;
        }
    }

    private void OnTriggerExit (Collider other) {
        if (other.CompareTag ("BuildableArea")) {
            spriteRenderer.material.color = disabledColor;
        }
        spriteRenderer.material.color = initialColor;
    }
}