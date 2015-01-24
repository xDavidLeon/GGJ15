using UnityEngine;
using System.Collections;

public class boulderScript : MonoBehaviour {

    Rigidbody rb;

    public float floatingTime, floatingTimer;
    public float boulderTime, boulderTimer;
    public float disappearTime, disappearTimer;


	// Use this for initialization
	void Start () 
    {
        rb = transform.GetComponent<Rigidbody>();

        floatingTimer = floatingTime;
        rb.useGravity = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (floatingTimer > 0f)
        {
            floatingTimer -= Time.deltaTime;
            if (floatingTimer <= 0f)
            {
                floatingTimer = 0f;
                boulderTimer = boulderTime;
                rb.useGravity = true;
            }
        }
        else if (boulderTimer > 0f)
        {
            boulderTimer -= Time.deltaTime;
            if (boulderTimer <= 0f)
            {
                boulderTimer = 0f;
                disappearTimer = disappearTime;
            }
        }
        else if (disappearTimer > 0f)
        {
            disappearTimer -= Time.deltaTime;
            if (disappearTimer <= 0f)
            {
                disappearTimer = 0f;
                GameObject.Destroy(this.gameObject, 0.1f);
            }
        }
	}
}
