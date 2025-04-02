using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TextboxScript : MonoBehaviour
{
    public GameObject selectedNode;
    public IEnumerator createBox;
    public AnimationCurve sizeCurve;

    int textBoxPhase = 0;
    float time = 0;

    private void Start()
    {
        createBox = CreateBox();
    }

    public void newTextBox()
    {
        StopCoroutine(createBox);
        StartCoroutine(createBox);
    }

    public void Update()
    {
        Debug.Log(textBoxPhase);
    }

    IEnumerator CreateBox()
    {
        textBoxPhase = 0;
        yield return StartCoroutine(boxStart());
        yield return StartCoroutine(boxUnlock());
        yield return StartCoroutine(boxEnd());
    }

    IEnumerator boxStart()
    {
        time = 0;
        while(time < 1 && textBoxPhase == 0)
        {
            time += Time.deltaTime;
            transform.localScale = Vector3.one * sizeCurve.Evaluate(time);
            yield return null;
        }
        if (textBoxPhase == 0)
        {
            textBoxPhase = 1;
        }
    }

    IEnumerator boxUnlock()
    {
        while (textBoxPhase == 1)
        {
            yield return null;
        }
    }

    IEnumerator boxEnd()
    {
        while (time > 0 && textBoxPhase == 2)
        {
            time -= Time.deltaTime;
            transform.localScale = Vector3.one * sizeCurve.Evaluate(time);
            yield return null;
        }
        StopCoroutine(createBox);
    }

    public void exitNode()
    {
        textBoxPhase = 2;
    }
}
