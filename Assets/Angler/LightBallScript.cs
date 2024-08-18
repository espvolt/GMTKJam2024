using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class LightBallScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Collider sphereCollider = null;
    
    public Vector3 force = new Vector3(0, 0, 0);
    private float lifetime = 3;

    void Start()
    {
        sphereCollider.enabled = false;
    }

    public void MakeActive()
    {
        sphereCollider.enabled = true;
        transform.SetParent(null, true);
    }

    public void ShootAt(Vector3 target)
    {
        Vector3 diff = target - transform.position;
        float angle = Mathf.Atan2(diff.z, diff.x);

        force = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * 5;
        GetComponent<Rigidbody>().velocity = force;
    }
    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;

        if (lifetime < 0)
        {
            Object.Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FishmaelMovement script = collision.gameObject.GetComponent<FishmaelMovement>();
            script.AddForce(force / 2);
            script.DealDamage(1);
            Object.Destroy(gameObject);
            
        }
    }
}
