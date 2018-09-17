using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
    public GameObject player;
    private Text text;
    private int score;
	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();

	}
	
	// Update is called once per frame
	void Update () {
        if (player.GetComponent<Player1>() == null)
        {
            score = player.GetComponent<Player2>().score;
        }
        else
        {
            score = player.GetComponent<Player1>().score;
        }
        text.text = score.ToString();
	}
}
