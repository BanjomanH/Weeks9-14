using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Timeline;
using UnityEngine;

public class BroccoliSpawn : MonoBehaviour
{
    Vector3 velocity;
    public float gravValue = -80;
    public float xMove;

    private void Start()
    {
        xMove = Random.Range(-200, 200);
        velocity.y = 50f;
    }

    void Update()
    {
        velocity.y += gravValue;
        velocity.x += xMove;
        velocity *= Time.deltaTime;
        transform.position = transform.position + velocity;

        if (transform.position.y < -365)
        {
            Destroy(gameObject);
        }
    }
}
