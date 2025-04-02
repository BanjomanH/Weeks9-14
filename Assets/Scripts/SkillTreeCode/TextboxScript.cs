using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TextboxScript : MonoBehaviour
{
    public GameObject selectedNode;
    public Coroutine createBox;
    public AnimationCurve sizeCurve;

    bool createBoxRunning = false;
    float time = 0;

    public void newTextBox()
    {
        if (createBox != null)
        {
            StopCoroutine(createBox);
        }
        createBox = StartCoroutine(CreateBox());
    }

    IEnumerator CreateBox()
    {
        time = 0;
        createBoxRunning = true;
        while (true)
        {
            if (time < 1)
            {
                time += Time.deltaTime * 3;
                transform.localScale = Vector3.one * sizeCurve.Evaluate(time);
            }
            yield return null;
        }
    }

    IEnumerator BoxEnd()
    {
        while (time > 0 && createBoxRunning == false)
        {
            time -= Time.deltaTime * 3;
            transform.localScale = Vector3.one * sizeCurve.Evaluate(time);
            yield return null;
        }
    }

    public void exitNode()
    {
        StopCoroutine(createBox);
        createBoxRunning = false;
        StartCoroutine(BoxEnd());
    }
}
