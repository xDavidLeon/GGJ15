using UnityEngine;
using System.Collections;

public class Rock : MonoBehaviour {
    public float lifeTimer = 3.0f;
    private float timer = 0;
    public AudioClip clip;

    bool fallen = false;
	void Start () {

        float delay = Random.Range(1.0f, 2.0f);
        audio.PlayDelayed(delay + 1.0f);

        Vector3 destiny = this.transform.position - new Vector3(0, 24.5f, 0);

        iTween.MoveTo(this.gameObject, iTween.Hash(
    "x", destiny.x,
    "z", destiny.z,
     "y", destiny.y,
     "delay", delay,
     "time", 1.5,
     "oncomplete", "Fallen",
     "easeType", iTween.EaseType.easeInOutBounce
 ));

        iTween.ShakePosition(Camera.main.gameObject, iTween.Hash(
    "x", 0.15f,
    "z", 0.15f,
     "y", 0.15f,
     "delay", delay + 1.0f,
     "time", 0.75f
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
