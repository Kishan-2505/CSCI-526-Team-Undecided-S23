using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace level3_4
{
    public class CameraController : MonoBehaviour
    {
        private Camera Camera;
        private float zoom = 7.829389f;
        void Start()
        {
            Camera = GetComponent<Camera>();
        }

        void Update()
        {
            // if (Input.GetKeyDown(KeyCode.M))
            // {
            //     Time.timeScale=0;
            //     Camera.orthographicSize = zoomOut;
            // }
            // else if (Input.GetKeyUp(KeyCode.M))
            // {
            //     Time.timeScale=1;
            //     Camera.orthographicSize = zoomIn;
            // }
        }
        public void zoomInCamera()
        {
            if (zoom >= 3.829389f)
            {
                zoom--;
                Camera.orthographicSize = zoom;
            }
        }
        public void zoomOutCamera()
        {
            if (zoom <= 15f)
            {
                zoom++;
                Camera.orthographicSize = zoom;
            }
        }
    }
}
