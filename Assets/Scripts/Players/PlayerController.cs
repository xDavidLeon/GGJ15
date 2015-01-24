using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    Rigidbody rb;

    public float speedFactor;
    public float speed;
    public float bounceForce;
    public float bounceStunTime, bounceStunTimer;
    public float stunFactor;
    float stunFactorNow = 1.0f;

    public bool glitchedMode = false;

	// Use this for initialization
	void Start () 
    {
        rb = transform.GetComponent<Rigidbody>();
        rb.maxAngularVelocity = 10f;
	}
	
	// Update is called once per frame
	void Update () 
    {
        speed = rb.velocity.magnitude;

        if (bounceStunTimer > 0.0f)
        {
            bounceStunTimer -= Time.deltaTime;
            if (bounceStunTimer <= 0.0f)
            {
                bounceStunTimer = 0.0f;
                stunFactorNow = 1.0f;
            }
        }

        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(Vector3.forward * speedFactor * stunFactorNow);
        }

        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-Vector3.forward * speedFactor * stunFactorNow);
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(-Vector3.right * speedFactor * stunFactorNow);
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Vector3.right * speedFactor * stunFactorNow);
        }


        Debug.Log(rb.angularVelocity.magnitude);

        //Por si te caes
        if (transform.position.y < -1f)
        {
            transform.position = Vector3.zero + Vector3.up * 3f;
            rb.angularVelocity = Vector3.zero;
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        if(     (collision.gameObject.tag == "Player")
            ||  (collision.gameObject.tag == "Obstacle")
            ||  (collision.gameObject.tag == "Wall") )
        {
            if(glitchedMode) rb.AddForce(collision.contacts[0].normal*(bounceForce+speed*100f));
            else rb.AddForce(collision.contacts[0].normal * (bounceForce + Mathf.Clamp(speed, 0f, 7f)));

            Debug.Log("BOING!");
            bounceStunTimer = bounceStunTime;
            stunFactorNow = stunFactor;
        }

        //foreach (ContactPoint contact in collision.contacts)
        //{
        //    Debug.DrawRay(contact.point, contact.normal, Color.white);
        //}
        


    }
}
