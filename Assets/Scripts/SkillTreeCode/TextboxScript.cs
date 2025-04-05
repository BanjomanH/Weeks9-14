using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using UnityEngine.Experimental.AI;
using UnityEngine.Events;

public class TextboxScript : MonoBehaviour
{
    public GameObject selectedNode; // A reference to the latest node the player is hovering over
    public Coroutine createBox; //The coroutine responsible for managing the text box.
    public AnimationCurve sizeCurve; // An animation curve for the growing/shrinking animation
    public TMP_Text Title; // A reference to the 'title' gameObject contained on the gameObjects child.
    public TMP_Text Body; // A reference to the 'body' gameObject contained on the gameObjects child.
    public TMP_Text barText; // A reference to the text gameObject overlaid on the slider.
    public Slider bar; // A reference to the bar contained on this gameObjects child.
    public GameObject barVisuals; // A reference to the bar fill gameObject
    public string[] barResponses; // A list of the possible texts overlayed on the bar

    bool createBoxRunning = false; // A boolean that turns on when the 'createBox' coroutine is running
    float time = 0; // An inputted number into the sizeCurve for smooth transitions

    // Called from the nodes 'hover' unityEvent
    public void newTextBox(GameObject nodeToSelect)
    {
        // Sets the inputted gameObject to be the next node to select
        selectedNode = nodeToSelect;
        // Arranges all of the visuals accordingly
        bar.value = 0;
        NodeCode temp = nodeToSelect.GetComponent<NodeCode>();
        Title.text = temp.title;
        Body.text = temp.body;
        GetComponentInChildren<SpriteRenderer>().sprite = temp.iconFiles[1];
        // Sets the positions of the textBox to be offset from the normal
        Vector3 mousePosition = nodeToSelect.transform.position;
        mousePosition.z = -3;
        mousePosition.y += 2.2f;
        transform.position = mousePosition;
        
        // If any textBox is currently active, then stop them
        if (createBox != null)
        {
            StopCoroutine(createBox);
        }
        // Start a new textbox with the 'createBox' coroutine
        createBox = StartCoroutine(CreateBox());
    }

    // The code responsible for running the textBox function
    IEnumerator CreateBox()
    {
        time = 0;
        createBoxRunning = true;
        // Runs forever. Only stopped by the 'exitNode' method
        while (true)
        {
            // before a second, set the localScale to be the sizeCurve according to the time float
            if (time < 1)
            {
                time += Time.deltaTime * 3;
                transform.localScale = Vector3.one * sizeCurve.Evaluate(time);
            }

            // All of the code responsible for ensuring the bar works properly
            // Sets the correct response based on the state of the hovered node
            barText.text = barResponses[selectedNode.GetComponent<NodeCode>().state];
            // If the player clicks while hovering over a selectable node...
            if (Input.GetMouseButton(0) && selectedNode.GetComponent<NodeCode>().state == 1)
            {
                // Enable the visuals and begin to increment the sliders value
                barVisuals.SetActive(true);
                bar.value += Time.deltaTime;
                if (bar.value == 1)
                {
                    // Calls the unlock script on the hovered over node
                    selectedNode.GetComponent<NodeCode>().Unlock();
                }
            } else if (selectedNode.GetComponent<NodeCode>().state == 2)
            {
                // If the hovered over node is unlocked, display the unlocked visuals
                bar.value = 1;
                barVisuals.SetActive(true);
            } else
            {
                // If the hovered over node is locked, display the locked visuals
                bar.value = 0;
                barVisuals.SetActive(false);
            }

            yield return null;
        }
    }

    // The script for closing the box
    IEnumerator BoxEnd()
    {
        // If time hasn't hit zero yet and the createBox coroutine is not running...
        while (time > 0 && createBoxRunning == false)
        {
            // Grabs whatever the time value was set to and begins to decrement it
            time -= Time.deltaTime * 3;
            // Sets the localScale to be the sizeCurve according to the time float
            transform.localScale = Vector3.one * sizeCurve.Evaluate(time);
            yield return null;
        }
    }

    // Called from a nodes ‘pointer exit’ unity event.
    public void exitNode()
    {
        // Stops whatever coroutine was running and sets the corrosponding variable to false
        StopCoroutine(createBox);
        createBoxRunning = false;
        // Begins the shrinking coroutine
        StartCoroutine(BoxEnd());
    }
}
