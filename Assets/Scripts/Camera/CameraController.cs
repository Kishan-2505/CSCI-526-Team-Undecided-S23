using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 sourcePosition;
    private Vector3 destinationPosition;
    private float duration = 3f;
    public float zoomSpeed= 6f;
    // Start is called before the first frame update
    void Start()
    {
        // transform.position = sourcePosition;
        // StartCoroutine(MoveCamera());
    }
    IEnumerator MoveCamera()
    {
        float time = 0f;
        float startOrthoSize = Camera.main.orthographicSize;
        float startOrthoSizeActual=startOrthoSize;
        Vector3 sourcePositionacutal=sourcePosition;
        destinationPosition=new Vector3(9.21f,-11.62f,-10);
        float newOrthoSize=startOrthoSize;
        while (time < duration)
        {
            transform.position = Vector3.Lerp(sourcePosition, destinationPosition, time / duration);
            newOrthoSize = Mathf.Lerp(startOrthoSize, startOrthoSize * 1.5f, time / duration);
            Camera.main.orthographicSize = newOrthoSize;
            time += Time.deltaTime;
            yield return null;
        }
        startOrthoSize=newOrthoSize;
        time = 0f;
        sourcePosition=destinationPosition;
        destinationPosition=new Vector3(14.9f,3.74f,-10);
        while (time < duration)
        {
            transform.position = Vector3.Lerp(sourcePosition, destinationPosition, time / duration);
            newOrthoSize = Mathf.Lerp(startOrthoSize, startOrthoSize * 2f, time / duration);
            Camera.main.orthographicSize = newOrthoSize;
            time += Time.deltaTime;
            yield return null;
        }
        
        transform.position = sourcePositionacutal;
        Camera.main.orthographicSize = startOrthoSizeActual;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }
}
