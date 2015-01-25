using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreUI : MonoBehaviour {
    public PlayerController.PLAYER_NUMBER playerNumber = PlayerController.PLAYER_NUMBER.PLAYER_1;
    Text text;
    GameManager gm;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        gm = GameManager.instance;
	}
	
	// Update is called once per frame
	void Update () {
        switch (playerNumber)
        {
            case PlayerController.PLAYER_NUMBER.PLAYER_1:
                text.text = gm.scores.scoreP1.ToString();
                break;
            case PlayerController.PLAYER_NUMBER.PLAYER_2:
                text.text = gm.scores.scoreP2.ToString();
                break;
            case PlayerController.PLAYER_NUMBER.PLAYER_3:
                text.text = gm.scores.scoreP3.ToString();
                break;
            case PlayerController.PLAYER_NUMBER.PLAYER_4:
                text.text = gm.scores.scoreP4.ToString();
                break;
        }
	}
}
