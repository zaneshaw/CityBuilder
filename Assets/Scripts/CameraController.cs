using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    private Vector3 panOrigin;
    private bool panning;

    private void LateUpdate() {
        if (Input.GetMouseButton(2)) {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 mouseDelta = mousePosition - transform.position;

            if (!panning) {
                panning = true;
                panOrigin = mousePosition;
            }

            transform.position = panOrigin - mouseDelta;
        } else {
            panning = false;
        }
    }
}
