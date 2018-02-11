using UnityEngine.UI;
using UnityEngine;

public class Timer : MonoBehaviour
{

    public Text TimerText;
    private float startTime;


    void Start()
    {
        startTime = Time.time;
    }


    void Update()
    {
        float t = Time.time - startTime;

        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("f2"); // change the f value to change the amount of decimals in the timer

        TimerText.text = minutes + ":" + seconds;

    }
}
