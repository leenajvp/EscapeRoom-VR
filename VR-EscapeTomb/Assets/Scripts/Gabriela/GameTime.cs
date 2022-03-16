using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTime: MonoBehaviour
{
    private static bool startGame = false;

    public Text timerText;
    public float totalTime;

    // Start is called before the first frame update
    void Start()
    {
        startGame = true;
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
    }

    void Timer()
    {
        if (startGame == true)

        {
            totalTime -= Time.deltaTime;
            int minutes = (int)totalTime / 60;
            int seconds = (int)totalTime % 60;

            timerText.text = string.Format(" {0} : {1} ", minutes.ToString("00"), seconds.ToString("00"));
        }
    }
}
