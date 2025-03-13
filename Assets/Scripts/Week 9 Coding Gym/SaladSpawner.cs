using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaladSpawner : MonoBehaviour
{
    public RectTransform sizingObject;
    public GameObject prefab;
    public Transform parentObject;
    float increaseAmount = 1.5f;

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
    public void mouseClick()
    {
       Instantiate(prefab, parentObject);
    }
}
