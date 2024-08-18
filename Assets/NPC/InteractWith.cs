using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class InteractWith : MonoBehaviour {

    public float appearDistance = 1.5f;

    public String[] talkPages;

    public Transform playerTransform;
    public GameObject interactSign;
    public TextMeshPro textMesh;

    private int talkPage = -1;
    private float textTimer = 0;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        bool inRange = Vector3.Distance(transform.position, playerTransform.position) < appearDistance;

        interactSign.SetActive(inRange && talkPage == -1);

        if(inRange && Input.GetKeyDown(KeyCode.E)) {
            textTimer = 0;
            talkPage++;
            if(talkPage >= talkPages.Length) {
                talkPage = -1;
                textMesh.text = "";
            }
        }

        if(talkPage >= 0) {
            textTimer += Time.deltaTime;
            textMesh.text = talkPages[talkPage].Substring(0, Mathf.Min(talkPages[talkPage].Length, (int) (textTimer * 15.0f)));
        }
    }
}
