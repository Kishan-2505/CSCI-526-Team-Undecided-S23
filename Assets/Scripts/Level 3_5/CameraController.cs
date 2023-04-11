using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace level3_5
{
    public class CameraController : MonoBehaviour
    {
        private Camera Camera;
        private float zoomOut = 20f;
        private const float zoomIn = 12.14533f; // Const do not change it
        void Start()
        {
            Camera = GetComponent<Camera>();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                Time.timeScale = 0;
                Camera.orthographicSize = zoomOut;
            }
            else if (Input.GetKeyUp(KeyCode.M))
            {
                Time.timeScale = 1;
                Camera.orthographicSize = zoomIn;
            }
        }
    }

}
