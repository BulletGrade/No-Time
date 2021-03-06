﻿using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    // Thanks for N3k for his tutorial on timers
    public Text timer;
    private float startTime;
    public bool timerStarted = false; // Timer won't count til elevator opens

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        if (timerStarted)
        {
            float t = Time.time - startTime;

            string minutes = ((int) t / 60).ToString();
            string seconds = (t % 60).ToString("f2");

            if (minutes == "0")
            {
                timer.text = seconds;
            }
            else if (minutes != "0")
            {
                timer.text = minutes + ":" + seconds;
            }
        }
    }
}
