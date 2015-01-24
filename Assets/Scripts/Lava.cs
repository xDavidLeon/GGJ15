using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Lava : MonoBehaviour {
    public float speed = 1;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Rotate(new Vector3(0, 1, 0) * speed * Time.deltaTime);
	}
}
