using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public enum PLAYER_NUMBER
    {
        PLAYER_1,
        PLAYER_2,
        PLAYER_3,
        PLAYER_4
    };

    public PLAYER_NUMBER playerNumber = PLAYER_NUMBER.PLAYER_1;

    Rigidbody rb;

    public float speedFactor;
    public float speed;
    public float bounceForce;
    public float bounceStunTime, bounceStunTimer;
    public float stunFactor;
    float stunFactorNow = 1.0f;
    public float dieTime, dieTimer = -1f;

    public bool glitchedMode = false;
    public AudioClip[] sfx;

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

        if (dieTimer > 0f)
        {
            dieTimer -= Time.deltaTime;
            if (dieTimer <= 0)
            {
                transform.gameObject.SetActive(false);
                dieTimer = -1f;
            }

            return;
        }

        if (bounceStunTimer > 0.0f)
        {
            bounceStunTimer -= Time.deltaTime;
            if (bounceStunTimer <= 0.0f)
            {
                bounceStunTimer = 0.0f;
                stunFactorNow = 1.0f;
            }
        }

        switch(playerNumber)
        {
            case PLAYER_NUMBER.PLAYER_1:
                ControlP1();
                break;
            case PLAYER_NUMBER.PLAYER_2:
                ControlP2();
                break;
            case PLAYER_NUMBER.PLAYER_3:
                ControlP3();
                break;
            case PLAYER_NUMBER.PLAYER_4:
                ControlP4();
                break;
        }

        //Debug.Log(rb.angularVelocity.magnitude);

        //Por si te caes
        if (transform.position.y < -1f)
        {
            transform.position = Vector3.zero + Vector3.up * 3f;
            rb.angularVelocity = Vector3.zero;
        }
	}

    private void ControlP1()
    {
        float h = Input.GetAxis("Horizontal01");
        float v = Input.GetAxis("Vertical01");

        Debug.Log(h);

        rb.AddForce(Vector3.right * speedFactor * stunFactorNow * h);
        rb.AddForce(Vector3.forward * speedFactor * stunFactorNow * v);
    }

    private void ControlP2()
    {
        float h = Input.GetAxis("Horizontal02");
        float v = Input.GetAxis("Vertical02");

        rb.AddForce(Vector3.right * speedFactor * stunFactorNow * h);
        rb.AddForce(Vector3.forward * speedFactor * stunFactorNow * v);
    }

    private void ControlP3()
    {
        float h = Input.GetAxis("Horizontal03");
        float v = Input.GetAxis("Vertical03");

        rb.AddForce(Vector3.right * speedFactor * stunFactorNow * h);
        rb.AddForce(Vector3.forward * speedFactor * stunFactorNow * v);
    }

    private void ControlP4()
    {
        float h = Input.GetAxis("Horizontal04");
        float v = Input.GetAxis("Vertical04");

        rb.AddForce(Vector3.right * speedFactor * stunFactorNow * h);
        rb.AddForce(Vector3.forward * speedFactor * stunFactorNow * v);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (dieTimer > 0f) return;

        if(     (collision.gameObject.tag == "Player")
            ||  (collision.gameObject.tag == "Obstacle")
            ||  (collision.gameObject.tag == "Wall") )
        {
            if(glitchedMode) rb.AddForce(collision.contacts[0].normal*(bounceForce+speed*100f));
            else rb.AddForce(collision.contacts[0].normal * (bounceForce + Mathf.Clamp(speed, 0f, 7f)));

            //Debug.Log("BOING!");
            bounceStunTimer = bounceStunTime;
            stunFactorNow = stunFactor;

            audio.PlayOneShot(sfx[Random.Range(0, sfx.Length)]);
        }

        if (collision.gameObject.tag == "Death")
        {
            if (glitchedMode) rb.AddForce(Vector3.up);
            else rb.AddForce(Vector3.up*500f);
            dieTimer = dieTime;
            Debug.Log("DEATH!");
        }

        //foreach (ContactPoint contact in collision.contacts)
        //{
        //    Debug.DrawRay(contact.point, contact.normal, Color.white);
        //}
    }
}
