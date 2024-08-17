using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;


public class FishmaelController : MonoBehaviour
{
    public GameObject fishMael = null;
    private Material basicScale = null;
    public Animator animator;

    public Rigidbody body;

    private bool swimming = false;
    public bool blocking = false;
    public float blockFrames = 0;
    public class ScaleGroup {
        public GameObject[] scaleObjects = new GameObject[8];

        public ScaleGroup(GameObject[] scaleObjects)
        {
            this.scaleObjects = scaleObjects;
        }
    }

    public Dictionary<int, ScaleGroup> scaleGroups = new Dictionary<int, ScaleGroup>();

    // Start is called before the first frame update
    void Start()
    {
        basicScale = Resources.Load("BasicScale", typeof(Material)) as Material;

        Transform t = fishMael.transform;
        scaleGroups.Add(0, new ScaleGroup(new GameObject[] { t.Find("Plane.001").gameObject, t.Find("Plane.002").gameObject, t.Find("Plane.004").gameObject, t.Find("Plane.017").gameObject,
            t.Find("Plane.012").gameObject, t.Find("Plane.016").gameObject, t.Find("Plane.026").gameObject, t.Find("Plane.009").gameObject }));

        scaleGroups.Add(1, new ScaleGroup(new GameObject[] { t.Find("Plane.037").gameObject, t.Find("Plane.029").gameObject, t.Find("Plane.006").gameObject, t.Find("Plane.007").gameObject,
            t.Find("Plane.008").gameObject}));




        ScaleGroup mainScaleGroup = scaleGroups[0];

        for (int i = 0; i < mainScaleGroup.scaleObjects.Length; i++)
        {
            GameObject currentScale = mainScaleGroup.scaleObjects[i];
            currentScale.GetComponent<Renderer>().material = basicScale;
        }
    }

    bool AnimatorIsPlaying()
    {
        return animator.GetCurrentAnimatorStateInfo(0).length >
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    bool AnimatorIsPlaying(string stateName)
    {
        return AnimatorIsPlaying() && animator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.Space))
        {
            if (!blocking)
            {
                blocking = true;
                blockFrames = 0;
                animator.SetTrigger("trBlock");
                animator.SetBool("blockHeld", true);
            }

        }
        else
        {
            if (blocking)
            {
                animator.SetBool("blockHeld", false);
            }
        }


        if (blocking)
        {
            blockFrames += 1f * Time.deltaTime;

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("BLOCKRECOVER") && animator.GetCurrentAnimatorStateInfo(0).length <=
                animator.GetCurrentAnimatorStateInfo(0).normalizedTime)
            {
                blocking = false;
            }
        }


        if (body.velocity.magnitude > 2)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AxisAngle(Vector3.up, Mathf.Atan2(body.velocity.z, -body.velocity.x)), .3f);
        }

        if (!blocking)
        {
            if (body.velocity.magnitude > 5)
            {
                if (!swimming)
                {
                    swimming = true;
                    animator.SetTrigger("trSwim");
                }
            }
            else
            {
                if (swimming)
                {
                    animator.SetTrigger("trIdle");
                    swimming = false;
                }
            }
        }
    }
}
