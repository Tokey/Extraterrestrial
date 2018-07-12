﻿using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoreCounter : MonoBehaviour {

    private Text scoreText;

    void Awake()
    {
        scoreText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = GameMaster.getScore.ToString()+"\n Highscore: "+GameMaster.getHighScore.ToString();
    }
}
