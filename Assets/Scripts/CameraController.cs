using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField] private float zoomSpeed = 1f;

    private Vector3 panOrigin;
    private bool panning;

    public InputMain controls;

    private void Awake() {
        controls = new InputMain();
    }

    private void Update() {
        if (controls.Default.Pan.ReadValue<float>() == 1f) {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(controls.Default.MousePosition.ReadValue<Vector2>());
            Vector3 mouseDelta = mousePosition - transform.position;

            if (!panning) {
                panning = true;
                panOrigin = mousePosition;
            }

            transform.position = panOrigin - mouseDelta;
        } else {
            panning = false;
        }

        if (controls.Default.Zoom.ReadValue<Vector2>() != Vector2.zero) {
            Camera.main.orthographicSize -= controls.Default.Zoom.ReadValue<Vector2>().y * zoomSpeed;
        }
    }

    private void OnEnable() {
        controls.Enable();
    }

    private void OnDisable() {
        controls.Disable();
    }
}
