using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour {
    Transform buildCursor;
    // Start is called before the first frame update
    void Start () {
        buildCursor = transform.Find ("BuildCursor");
        buildCursor.gameObject.SetActive (false);
    }

    // Update is called once per frame
    void Update () {
        //TODO: move cursor
    }

    public void ShowCursor (Player.Mode mode) {
        buildCursor.gameObject.SetActive (mode == Player.Mode.Build);
    }
}