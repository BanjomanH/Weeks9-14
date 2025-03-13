using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventDemo : MonoBehaviour
{
    public RectTransform sizingObject;
    public UnityEvent OnTimerHasFinished;
    public float increaseAmount = 1.5f;
    public float timerLength = 3;
    public float t;

    private void Update()
    {
        t += Time.deltaTime;
        if (t > timerLength)
        {
            t = 0;
            OnTimerHasFinished.Invoke();
        }
    }

    public void mouseEnterSprite()
    {
        Debug.Log("Mouse just entered the sprite");
        sizingObject.localScale = new Vector3(sizingObject.transform.localScale.x * increaseAmount, sizingObject.transform.localScale.y * increaseAmount, sizingObject.transform.localScale.z * increaseAmount);
    }

    public void mouseExitSprite()
    {
        Debug.Log("Mouse just exited the sprite");
        sizingObject.localScale = new Vector3(sizingObject.transform.localScale.x / increaseAmount, sizingObject.transform.localScale.y / increaseAmount, sizingObject.transform.localScale.z / increaseAmount);
    }
}
