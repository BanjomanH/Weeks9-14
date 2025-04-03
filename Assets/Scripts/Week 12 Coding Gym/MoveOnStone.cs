using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MoveOnStone : MonoBehaviour
{
    public Tilemap tilemap;
    public SpriteRenderer character;
    public Animator charaAnimator;
    AudioSource audioSource;
    public AudioClip[] footsteps;
    Vector3 destination;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<SpriteRenderer>();
        charaAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(walkTowardsMousePosition());
    }

    // Update is called once per frame
    IEnumerator walkTowardsMousePosition()
    {
        while (true)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);

            Vector3 direction = destination - transform.position;
            character.flipX = direction.x < 0;
            charaAnimator.SetFloat("movement", Mathf.Abs(direction.x));

            if (Input.GetMouseButtonDown(0) && checkPos(mousePosition))
            {
                destination = mousePosition;
            }
            yield return null;
        }
    }

    bool checkPos(Vector3 posToTest)
    {
        Vector3Int stonePosition = tilemap.WorldToCell(posToTest);

        if (tilemap.GetTile(stonePosition) != null)
        {
            return true;
        }

        return false;
    }

    public void  playFootStepSound()
    {
        if (checkPos(transform.position))
        {
            audioSource.PlayOneShot(footsteps[Random.Range(0, 9)]);
        }
    }
}
