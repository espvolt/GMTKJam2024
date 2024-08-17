using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScript : MonoBehaviour
{
    public Camera camera = null;
    public Transform fishmaelTransform = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        camera.transform.position = fishmaelTransform.position + new Vector3(20, 30, 20);
        camera.transform.LookAt(fishmaelTransform, Vector3.up);
    }
}
