using UnityEngine;

public class CircleGenerator : MonoBehaviour
{
    public GameObject circlePrefab;
    public float timeInterval = 5.0f;
    private float timeCounter = 0.0f;

    private void Update()
    {
        timeCounter += Time.deltaTime;
        if (timeCounter >= timeInterval)
        {
            timeCounter = 0.0f;
            Vector3 randomPosition = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), 0.0f);
            Instantiate(circlePrefab, randomPosition, Quaternion.identity);
        }
    }
}