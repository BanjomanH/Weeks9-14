using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Knight : MonoBehaviour
{
    SpriteRenderer sr;
    Animator animator;
    public bool canRun = true;
    public float speed = 2f;
    

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float direction = Input.GetAxisRaw("Horizontal");
        sr.flipX = direction < 0;
        animator.SetFloat("movement", Mathf.Abs(direction));
        Vector3 velocity = Vector3.zero;
        velocity.x = direction * speed * Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("attack");
            canRun = false;
        }

        if (canRun == true)
        {
            transform.position = transform.position + velocity;
        }
    }

    public void attackHasFinished()
    {
        Debug.Log("Attack has finished");
        canRun = true;
    }
}
