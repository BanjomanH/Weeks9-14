using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NodeCode : MonoBehaviour
{
    public GameObject[] futureNodes;
    public UnityEvent becomeSelectable;
    public Material lineMaterial;
    public int state = 0;

    void Start()
    {
        for (int i = 0; i < futureNodes.Length; i++)
        {
            futureNodes[i].GetComponent<NodeCode>().drawLine(transform.position);
            becomeSelectable.AddListener(futureNodes[i].GetComponent<NodeCode>().Selectable);
        }
    }

    public void Unlock()
    {
        gameObject.GetComponent<Button>().interactable = false;
        state = 2;
        becomeSelectable.Invoke();
    }

    public void Selectable()
    {
        gameObject.GetComponent<Button>().interactable = true;
        state = 1;
        // start the 'grow then shrink' coroutine
    }

    public void drawLine(Vector3 pastNode)
    {
        Vector3 currentPos = transform.position;
        currentPos.z = 0;
        LineRenderer temp = gameObject.AddComponent<LineRenderer>();

        temp.material = lineMaterial;
        temp.startWidth = 0.3f;
        temp.endWidth = 0.3f;

        temp.positionCount = 2;
        temp.SetPosition(0, pastNode);
        temp.SetPosition(1, currentPos);
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
            }
        }
    }
}
