using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class BubbleColumnScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FishmaelMovement playerScript = collision.gameObject.GetComponent<FishmaelMovement>();
            playerScript.StartTransition(transform);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
