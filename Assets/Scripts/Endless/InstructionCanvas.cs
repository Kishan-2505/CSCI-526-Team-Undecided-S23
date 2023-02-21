using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionCanvas : MonoBehaviour
{
    public Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("InstructionCanvas.Start()");
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the tab key is pressed
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // Show the canvas
            canvas.enabled = true;
        }

        // Check if the tab key is released
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            // Hide the canvas
            canvas.enabled = false;
        }
    }
}
