using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.AI;

public class AnglerController : MonoBehaviour
{
    // Start is called before the first frame update

    public NavMeshAgent agent = null;
    public Transform fishronTransform = null;
    public Animator animator = null;
    private bool swimming = false;
    private float shotTimer = 5;
    private float actionTimer = 5;
    private bool shooting = false;
    private bool shootingShot = false;
    private float shootingTimer = 0;
    private GameObject currentLightBall = null;
    public GameObject lightBallPrefab = null;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (shooting)
        {
            if (!shootingShot)
            {
                animator.SetTrigger("Shoot");
                shootingShot = true;
            }

            if (shootingTimer > 1.0)
            {
                shooting = false;
                //currentLightBall.transform.position = new Vector3(.05f, 0, 0);
                LightBallScript currentLightBallScript = currentLightBall.GetComponent<LightBallScript>();
                currentLightBallScript.MakeActive();
                currentLightBallScript.ShootAt(fishronTransform.position);
            }

            //Debug.Log(animator.GetCurrentAnimatorStateInfo(0).IsName("Shooting") + " " + animator.GetCurrentAnimatorClipInfo(0).Length);
            //Debug.Log("BRO??");
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation((fishronTransform.position - transform.position)), 3.0f * Time.deltaTime);
            shootingTimer += 1 * Time.deltaTime;
        }


        shotTimer += 1 * Time.deltaTime;
        actionTimer += 2 * Time.deltaTime;

        if (!shooting)
        {
            if (actionTimer > 5)
            {
                int option = Random.RandomRange(1, 3);
                if (option == 1)
                {
                    agent.SetDestination(transform.position + new Vector3(Random.RandomRange(-1, 2), 0, Random.RandomRange(-1, 2)) * Random.RandomRange(-3, 3));
                }
                else
                {

                    shooting = true;
                    shootingShot = false;
                    shootingTimer = 0;
                    currentLightBall = Instantiate(lightBallPrefab, transform.Find("angler2/Armature/Bone/Bone.002/Bone.003/Bone.004"), false);
                    //currentLightBall.transform.SetParent(transform.Find("angler2/Armature/Bone/Bone.002/Bone.003/Bone.004"));
                    currentLightBall.transform.Find("Sphere").localScale = new Vector3(.01f, .01f, .01f);
                }
                actionTimer = 0;
            }
        }
        


        
        if (!shooting)
        {
            if (agent.velocity.magnitude > 2)
            {
                if (!swimming)
                {
                    swimming = true;
                    animator.SetTrigger("Swim");
                }
            }
            else
            {
                if (swimming)
                {
                    animator.SetTrigger("Idle");
                    swimming = false;
                }
            }
        }
        
    }
}
