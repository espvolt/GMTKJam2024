using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class FishmaelMovement : MonoBehaviour {

    public float movementSpeed;
    public float rotationSpeed;

    public Rigidbody body;

    // Start is called before the first frame update
    void Start() {
        
    }

    public void FixedUpdate() {
        Vector3 moveDir = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.W))
        {
            // body.AddForce(0, 0, movementSpeed);
            moveDir += new Vector3(0, 0, 1);
        }

        if (Input.GetKey(KeyCode.S))
        {
            // body.AddForce(0, 0, -movementSpeed);
            moveDir += new Vector3(0, 0, -1);
        }

        if (Input.GetKey(KeyCode.D))
        {
            // body.AddForce(movementSpeed, 0, 0);
            moveDir += new Vector3(1, 0, 0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            // body.AddForce(-movementSpeed, 0, 0);
            moveDir += new Vector3(-1, 0, 0);
        }

        float speed = moveDir.magnitude;

        moveDir.Normalize();

        if(speed != 0) {
            // float idealAngle = Mathf.Atan2(body.velocity.x, body.velocity.z);
            // transform.rotation = Quaternion.Euler(0, idealAngle * 180 / Mathf.PI, 0);

            Quaternion toRotation = Quaternion.LookRotation(moveDir, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed);

            body.AddForce(transform.forward * movementSpeed);
        }
    }

    // Update is called once per frame
    void Update() {
        
    }
}
