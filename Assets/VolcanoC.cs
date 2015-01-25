using UnityEngine;
using System.Collections;

public class VolcanoC : MonoBehaviour {

    public float height;
    public float deltaY;
    bool createdNext = false;

	// Use this for initialization
	void Start () 
    {
        deltaY = 0f;

        transform.position.Set(transform.position.x, transform.position.y + height, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () 
    {
        float deltaYNow = GameManager.instance.lavaVelocity * Time.deltaTime;
        transform.position = transform.position - Vector3.up * deltaYNow;
        deltaY += deltaYNow;

        if (!createdNext && deltaY >= height)
        {
            if (GameManager.instance.startPart3)
            {
                GameObject.Instantiate(GameManager.instance.volcanoPartD);
            }
            else
            {
                GameObject.Instantiate(GameManager.instance.volcanoPartC);
            }

            createdNext = true;
        }

        if (deltaY > height * 5) GameObject.Destroy(this.gameObject);
	}
}
