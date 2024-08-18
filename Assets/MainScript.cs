using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScript : MonoBehaviour {

    public Transform fishmaelTransform;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start() {
        offset = transform.localPosition;
    }

    // Update is called once per frame
    void Update() {
        transform.position = fishmaelTransform.position + offset;
        // transform.LookAt(fishmaelTransform, Vector3.up);
    }
}
