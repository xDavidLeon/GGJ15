using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Countdown : MonoBehaviour {
    private Text text;
    private GameManager gm;
	// Use this for initialization
	void Start () {
        gm = GameManager.instance;
        text = GetComponent<Text>();

	}
	
	// Update is called once per frame
	void Update () {
        if (gm.playState == GameManager.PLAY_STATE.PLAYER_SELECTION && gm.timer == 5) text.text = "";
        else text.text = System.Convert.ToString(System.Math.Floor(gm.timer));
	}
}
