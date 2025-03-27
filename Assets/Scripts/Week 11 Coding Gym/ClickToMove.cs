using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ClickToMove : MonoBehaviour
{
    LineRenderer line;
    public AnimationCurve moveCurve;
    Vector3 positionGoal;
    public float speed = 5;
    Coroutine currentMode;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 2;
        currentMode = StartCoroutine(drawPhase());
    }

    IEnumerator drawPhase()
    {
        while (true)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            line.SetPosition(line.positionCount - 1, transform.position);
            transform.position = Vector3.MoveTowards(transform.position, positionGoal, speed * Time.deltaTime);

            if (Input.GetMouseButtonDown(0))
            {
                positionGoal = mousePosition;
                line.positionCount++;
            }
            yield return null;
        }
    }
    IEnumerator followPhase()
    {
        int phase = 1;
        positionGoal = line.GetPosition(phase);
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, positionGoal, speed * Time.deltaTime);

            if (transform.position == positionGoal)
            {
                phase++;
                if (phase == line.positionCount)
                {
                    phase = 0;
                }
                positionGoal = line.GetPosition(phase);
            }
            yield return null;
        }
    }

    public void traceLine()
    {
        StopCoroutine(currentMode);
        line.SetPosition(line.positionCount - 1, line.GetPosition(0));
        currentMode = StartCoroutine(followPhase());
    }
}
