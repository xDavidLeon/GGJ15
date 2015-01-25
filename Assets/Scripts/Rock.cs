using UnityEngine;
using System.Collections;

public class Rock : MonoBehaviour {
    public float lifeTimer = 3.0f;
    private float timer = 0;

    bool fallen = false;
	void Start () {
        Vector3 destiny = this.transform.position - new Vector3(0, 20, 0);

        iTween.MoveTo(this.gameObject, iTween.Hash(
    "x", destiny.x,
    "z", destiny.z,
     "y", destiny.y,
     "delay", Random.Range(0,2.0f),
     "time", 1.5,
     "oncomplete", "Fallen",
     "easeType", iTween.EaseType.easeInOutBounce
 ));
	}
	
	void Update () {
        if (fallen == false) return;
        timer += Time.deltaTime;
        if (timer >= lifeTimer)
        {
            GameObject.Destroy(this.gameObject);
        }
	}

    void Fallen()
    {
        tag = "Obstacle";
        fallen = true;
    }
}
