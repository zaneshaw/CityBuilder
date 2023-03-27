using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField] private float zoomSpeed = 1f;

    private Vector3 panOrigin;
    private bool panning;

    private void Update() {
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

        if (Input.GetAxisRaw("Mouse ScrollWheel") != 0) {
            Camera.main.orthographicSize -= Input.mouseScrollDelta.y * zoomSpeed;
        }
    }
}
