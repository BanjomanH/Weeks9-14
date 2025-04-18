using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KitClock : MonoBehaviour
{
    public Transform hourHand;
    public Transform minuteHand;
    public float timeAnHourTakes = 5;

    public float t;
    public int hour = 0;
    Coroutine clockIsRunning;
    IEnumerator doOneHour;

    public UnityEvent <int> OnTheHour;

    void Start()
    {
        clockIsRunning = StartCoroutine(MoveTheClock());
    }

    IEnumerator MoveTheClock()
    {
        while (true)
        {
            doOneHour = MoveTheClockHands1Hour();
            yield return StartCoroutine(doOneHour);
        }
    }

    IEnumerator MoveTheClockHands1Hour()
    {
        t = 0;
        while (t < timeAnHourTakes)
        {
            t += Time.deltaTime;
            minuteHand.Rotate(0, 0, -(360/timeAnHourTakes * Time.deltaTime));
            hourHand.Rotate(0, 0, -(30/timeAnHourTakes * Time.deltaTime));
            yield return null;
        }
        hour++;
        OnTheHour.Invoke(hour);
    }

    public void stopTheClock()
    {
        if (clockIsRunning != null)
        {
            StopCoroutine(clockIsRunning);
        }
        if (doOneHour != null)
        {
            StopCoroutine(doOneHour);
        }
    }
    public void startTheClock()
    {
        clockIsRunning = StartCoroutine(MoveTheClock());
    }
}
