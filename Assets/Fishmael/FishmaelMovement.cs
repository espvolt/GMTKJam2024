using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UIElements;

public class FishmaelMovement : MonoBehaviour {

    public float movementSpeed;
    public float rotationSpeed;
    public bool dead = false;

    public Rigidbody body;
    public Animator animator;

    private bool transitioning = false;
    private Transform transitionPosition;
    private bool swimming = false;

    private float health = 10;
    private float damagedTimer = 0;
    private bool damageAnimationPlayed = false;
    private bool tookDamage = false;
    // Start is called before the first frame update
    void Start() {
        
    }

    public void StartTransition(Transform transitionPosition)
    {
        if (!transitioning)
        {
            this.transitionPosition = transitionPosition;
            transitioning = true;
            Debug.Log("OH YEAHHHH");
        }
    }

    public void AddForce(Vector3 force)
    {
        body.AddForce(force);
    }

    public void DealDamage(float damage)
    {
        health -= damage;
        tookDamage = true;
    }
    public void FixedUpdate() {

        if (health <= 0)
        {
            if (!dead)
            {
                animator.SetTrigger("death");
            }
            dead = true;
        }

        if (tookDamage)
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Shoot") && !damageAnimationPlayed)
            {
                damageAnimationPlayed = true;
                Debug.Log("BRO?");
                animator.SetTrigger("shoot");
            }


            damagedTimer += Time.deltaTime;

            if (damagedTimer > .5)
            {
                tookDamage = false;
                damagedTimer = 0;
                damageAnimationPlayed = false;
                swimming = false;
            }
            return;
        }

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
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Swim") && !transitioning)
            {
                animator.SetTrigger("trSwim");
            }
            Quaternion toRotation = Quaternion.LookRotation(moveDir, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed);

            body.AddForce(transform.forward * movementSpeed);
        } else
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") && !transitioning)
            {
                swimming = false;
                animator.SetTrigger("trIdle");
            }
        }
    }

    // Update is called once per frame
    void Update() {
        
    }
}
