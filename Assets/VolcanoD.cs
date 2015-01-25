using UnityEngine;
using System.Collections;

public class VolcanoD : MonoBehaviour {

	public float height;
    public float deltaY;
    public float lavaHeight;
    public GameObject lava;

	// Use this for initialization
	void Start () 
    {
        deltaY = 0f;

        transform.position.Set(transform.position.x, transform.position.y + height, transform.position.z);
	}
	
	// Update is called once per frame
    void Update()
    {
        float deltaYNow = GameManager.instance.lavaVelocity * Time.deltaTime;
        transform.position = transform.position - Vector3.up * deltaYNow;
        deltaY += deltaYNow;

        if (deltaY >= lavaHeight)
        {
            lava = GameObject.Find("Lava");
            lava.transform.position = lava.transform.position - Vector3.up * deltaYNow;
        }

        //if (deltaY > height * 50)
        //{
        //    GameObject.Destroy(this.gameObject);
        //    GameObject.Destroy(lava);
        //}
    }
}
