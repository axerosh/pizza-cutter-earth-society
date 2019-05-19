using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildCursor : MonoBehaviour {

    public Color disabledColor;

    Color initialColor;
    SpriteRenderer spriteRenderer;

    private bool legalBuildCursor = true;

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
            legalBuildCursor = false;
            spriteRenderer.material.color = disabledColor;
        }
    }

    private void OnTriggerExit (Collider other) {
        if (other.CompareTag ("BuildableArea")) {
            legalBuildCursor = false;
            spriteRenderer.material.color = disabledColor;
        }
        legalBuildCursor = true;
        spriteRenderer.material.color = initialColor;
    }

    public bool CanBuild() {
        return legalBuildCursor;
    }

    public BuildCursor GetCursor() {
        return this;
    }
}