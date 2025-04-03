using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using UnityEngine.Experimental.AI;

public class TextboxScript : MonoBehaviour
{
    public GameObject selectedNode;
    public Coroutine createBox;
    public AnimationCurve sizeCurve;
    public TMP_Text Title;
    public TMP_Text Body;
    public Slider bar;

    bool createBoxRunning = false;
    float time = 0;

    public void newTextBox(GameObject nodeToSelect)
    {
        selectedNode = nodeToSelect;
        NodeCode temp = nodeToSelect.GetComponent<NodeCode>();
        Title.text = temp.title;
        Body.text = temp.body;
        GetComponentInChildren<SpriteRenderer>().sprite = temp.iconFiles[1];
        Vector3 mousePosition = nodeToSelect.transform.position;
        mousePosition.z = -3;
        mousePosition.y += 2.2f;
        transform.position = mousePosition;
        
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
            
            if (Input.GetMouseButton(0) && selectedNode.GetComponent<NodeCode>().state == 1)
            {
                bar.value += Time.deltaTime;
                if (bar.value == 1)
                {
                    selectedNode.GetComponent<NodeCode>().Unlock();
                }
            } else
            {
                bar.value = 0;
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
