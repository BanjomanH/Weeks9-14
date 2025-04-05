using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NodeCode : MonoBehaviour
{
    public GameObject[] futureNodes; // An array containing all of the nodes ahead of this one
    public GameObject textbox; // A reference to the textbox gameObject
    public AnimationCurve sizeCurve; // An animation curve for the unlocking animation
    public UnityEvent becomeSelectable; // calls the selectable method in the next gameObjects
    public Sprite[] iconFiles; // An array containing the possible icons to display
    public Image icon; // A reference to the child objects Image component
    public Material lineMaterial; // The material of a locked link/node
    public Material purchasedMaterial; // The material of a purchased link/node
    public Material purchasableMaterial; // The material of a standard link
    public int state = 0; // The current state (0 = locked, 1 = purchasable, 2 = purchased)
    public string title; // The title of the skill to send to the textbox
    public string body; // The description of the skill to send to the textbox

    Image background; // A reference to the current background of the gameObject

    // Code runs on startup
    void Start()
    {
        background = GetComponent<Image>(); // Properly references the background variable

        // The following sets the look to the correct state. Mostly for the first node.
        if (state == 0)
        {
            background.material = lineMaterial;
        } else if (state == 0)
        {
            background.material = null;
        }
        icon.sprite = iconFiles[state];

        // Iterates through the futureNodes array to create lines and subscribe them to the becomeSelectable event
        for (int i = 0; i < futureNodes.Length; i++)
        {
            futureNodes[i].GetComponent<NodeCode>().drawLine(transform.position);
            becomeSelectable.AddListener(futureNodes[i].GetComponent<NodeCode>().Selectable);
        }
    }


    // Update is called once per frame
    void Update()
    {
        // Iterates through the futureNodes array, testing to see if any of them have been purchased
        for (int i = 0; i < futureNodes.Length; i++)
        {
            if (futureNodes[i].GetComponent<NodeCode>().state == 2)
            {
                // If so, gets the lineRenderer to change its visuals to be thicker and a different material
                futureNodes[i].GetComponent<LineRenderer>().startWidth = 0.5f;
                futureNodes[i].GetComponent<LineRenderer>().endWidth = 0.5f;
                futureNodes[i].GetComponent<LineRenderer>().material = purchasedMaterial;
            }
        }
    }

    // Called from the textbox that holds a reference to this object
    public void Unlock()
    {
        // Changes the state of the node, and updates it's visuals accordingly
        state = 2;
        background.material = purchasedMaterial;
        icon.sprite = iconFiles[state];
        // Invokes the becomeSelectable event to activate the next nodes
        becomeSelectable.Invoke();
    }

    // Called from previous nodes who has a reference to this one
    public void Selectable()
    {
        // Changes the state of the node, and updates it's visuals accordingly
        state = 1;
        background.material = null;
        icon.sprite = iconFiles[state];
        GetComponent<LineRenderer>().material = purchasableMaterial;
        // Begins the cosmetic coroutine of this node unlocking
        StartCoroutine(GrowThenShrink());
    }

    // Referenced with the previous nodes position. Called from the previous node.
    public void drawLine(Vector3 pastNode)
    {
        // Arranges the variables to have that offset effect over the node
        Vector3 currentPos = transform.position;
        currentPos.z = -0.7f;
        pastNode.z = 1;
        LineRenderer temp = gameObject.GetComponent<LineRenderer>();

        // Sets the visuals to the correct size and material
        temp.material = lineMaterial;
        temp.startWidth = 0.3f;
        temp.endWidth = 0.3f;

        // Sets the positions to 2, and sets the two position variables accordingly.
        temp.positionCount = 2;
        temp.SetPosition(0, pastNode);
        temp.SetPosition(1, currentPos);
    }

    // Called from a ‘pointer enter’ unity trigger over this current node
    public void hover()
    {
        // Calls the method to create a new textBox, with itself as the reference
        textbox.GetComponent<TextboxScript>().newTextBox(gameObject);
    }

    // Called from the 'selectable method'
    IEnumerator GrowThenShrink()
    {
        // Create a temporary time float
        float t = 0;
        // before a second, set the localScale to be the sizeCurve according to the time float
        while (t < 1)
        {
            t += Time.deltaTime;
            transform.localScale = Vector3.one * sizeCurve.Evaluate(t);
            yield return null;
        }
    }
}
