using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwingAnimation : MonoBehaviour
{
    public AnimationCurve swing;
    public float t;
    public Button attackButton;

    public void Attack()
    {
        StartCoroutine(RotateAnimation());
    }

    IEnumerator RotateAnimation()
    {
        attackButton.interactable = false;
        t = 0;
        while (t < 1)
        {
            t += Time.deltaTime;
            transform.eulerAngles = new Vector3(0, 0, -swing.Evaluate(t) * 180);
            yield return null;
        }
        attackButton.interactable = true;
    }
}
