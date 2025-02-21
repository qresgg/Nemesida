using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerController : MonoBehaviour
{
    public TMP_Text timerText;
    private float startTime;
    private bool isRunning = false;

    void Start()
    {
        startTime = Time.time;
        isRunning = true;
    }

    void Update()
    {
        if (isRunning)
        {
            float timeElapsed = Time.time - startTime;
            string minutes = ((int)(timeElapsed % 3600) / 60).ToString("00");
            string seconds = (timeElapsed % 60).ToString("00");

            if (timeElapsed >= 3600)
            {
                string hours = ((int)timeElapsed / 3600).ToString("00");
                timerText.text = hours + ":" + minutes + ":" + seconds;
            }
            else
            {
                timerText.text = minutes + ":" + seconds;
            }
        }
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public void RestartTimer()
    {
        startTime = Time.time;
        isRunning = true;
    }
}
