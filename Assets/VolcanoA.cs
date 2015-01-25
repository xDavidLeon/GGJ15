using UnityEngine;
using System.Collections;

public class VolcanoA : MonoBehaviour {

    public bool first;
    public float height;
    public float deltaY;
    bool createdNext = false;
    

	// Use this for initialization
	void Start () 
    {
        deltaY = 0f;

        if (!first) transform.position.Set(transform.position.x, transform.position.y + height, transform.position.z);
        else deltaY = height;
        
	}
	
	// Update is called once per frame
	void Update () 
    {
        float deltaYNow = GameManager.instance.lavaVelocity * Time.deltaTime;
        transform.position = transform.position - Vector3.up * deltaYNow;
        deltaY += deltaYNow;
        if (!createdNext && deltaY >= height)
        {
            if (GameManager.instance.startPart2)
            {
                GameObject.Instantiate(GameManager.instance.volcanoPartB);
            }
            else
            {
                GameObject.Instantiate(GameManager.instance.volcanoPartA);
            }

            createdNext = true;
        }

        if (deltaY > height * 5) GameObject.Destroy(this.gameObject);
	}
}
