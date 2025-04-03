using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NodeCode : MonoBehaviour
{
    public GameObject[] futureNodes;
    public GameObject textbox;
    public AnimationCurve sizeCurve;
    public UnityEvent becomeSelectable;
    public Sprite[] iconFiles;
    public Image icon;
    public Material lineMaterial;
    public Material purchasedMaterial;
    public int state = 0;
    public string title;
    public string body;

    Image background;

    void Start()
    {
        background = GetComponent<Image>();
        icon.sprite = iconFiles[state];
        for (int i = 0; i < futureNodes.Length; i++)
        {
            futureNodes[i].GetComponent<NodeCode>().drawLine(transform.position);
            becomeSelectable.AddListener(futureNodes[i].GetComponent<NodeCode>().Selectable);
        }
    }


    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < futureNodes.Length; i++)
        {
            if (futureNodes[i].GetComponent<NodeCode>().state == 2)
            {
                futureNodes[i].GetComponent<LineRenderer>().startWidth = 0.5f;
                futureNodes[i].GetComponent<LineRenderer>().endWidth = 0.5f;
                futureNodes[i].GetComponent<LineRenderer>().material = purchasedMaterial;
            }
        }
    }

    public void Unlock()
    {
        state = 2;
        background.material = purchasedMaterial;
        icon.sprite = iconFiles[state];
        becomeSelectable.Invoke();
    }

    public void Selectable()
    {
        state = 1;
        icon.sprite = iconFiles[state];
        StartCoroutine(GrowThenShrink());
    }

    public void drawLine(Vector3 pastNode)
    {
        Vector3 currentPos = transform.position;
        currentPos.z = 0;
        pastNode.z = 0;
        LineRenderer temp = gameObject.GetComponent<LineRenderer>();

        temp.material = lineMaterial;
        temp.startWidth = 0.3f;
        temp.endWidth = 0.3f;

        temp.positionCount = 2;
        temp.SetPosition(0, pastNode);
        temp.SetPosition(1, currentPos);
    }

    public void hover()
    {
        textbox.GetComponent<TextboxScript>().newTextBox(gameObject);
    }

    IEnumerator GrowThenShrink()
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime;
            transform.localScale = Vector3.one * sizeCurve.Evaluate(t);
            yield return null;
        }
    }
}
