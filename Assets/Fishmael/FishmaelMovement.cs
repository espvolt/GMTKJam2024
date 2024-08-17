using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishmaelMovement : MonoBehaviour {

    public float movementSpeed;

    public Rigidbody body;

    // Start is called before the first frame update
    void Start() {
        
    }

    public void FixedUpdate() {
        if (Input.GetKey(KeyCode.W))
        {
            
            body.AddForce(0, 0, movementSpeed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            body.AddForce(0, 0, -movementSpeed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            body.AddForce(movementSpeed, 0, 0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            body.AddForce(-movementSpeed, 0, 0);
        }

        float idealAngle = Mathf.Atan2(body.velocity.x, body.velocity.z);

        transform.rotation = Quaternion.Euler(0, idealAngle * 180 / Mathf.PI, 0);
    }

    // Update is called once per frame
    void Update() {
        
    }
}
