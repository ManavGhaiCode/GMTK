using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class AspictFitter : MonoBehaviour {
    public float sceneWidth = 10;

    private Camera cam;

    void Start() {
        cam = GetComponent<Camera>();
    }

    void Update() {
        float unitsPerPixel = sceneWidth / Screen.width;
        float desiredHalfHeight = 0.5f * unitsPerPixel * Screen.height;

        cam.orthographicSize = desiredHalfHeight;
    }
}
