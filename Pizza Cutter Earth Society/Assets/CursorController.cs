using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour {

    public float buildCursorOffsetY = .2f;

    Transform buildCursor;

    void Start () {
        buildCursor = transform.Find ("BuildCursor");
        buildCursor.gameObject.SetActive (false);
    }

    void Update () {
        if (buildCursor.gameObject.activeInHierarchy) {
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast (ray, out hit, 100, LayerMask.NameToLayer ("UI"))) {
                buildCursor.transform.SetPositionAndRotation (
                    new Vector3 (hit.point.x, hit.point.y + buildCursorOffsetY, hit.point.z),
                    Quaternion.Euler (
                        buildCursor.eulerAngles.x,
                        Camera.main.transform.eulerAngles.y,
                        buildCursor.eulerAngles.z
                    )
                );
            }
        }
    }

    public void ShowCursor (Player.Mode mode) {
        buildCursor.gameObject.SetActive (mode == Player.Mode.Build);
    }
}