﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour {

    public static float timeLeft = 375.0f;
    private Text timerText;
    private PlatformerCharacter2D avatar;
	// Use this for initialization
	void Start () {
        avatar = FindObjectOfType<PlatformerCharacter2D>();
        timerText = gameObject.GetComponentInChildren<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        timeLeft -= Time.deltaTime;
        timerText.text = Mathf.FloorToInt(timeLeft).ToString();
        if (timeLeft < 0)
        {
            avatar.GameOver(2);
        }
    }
}
