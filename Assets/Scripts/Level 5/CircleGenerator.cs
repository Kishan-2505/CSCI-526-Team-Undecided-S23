using UnityEngine;

namespace Level5
{

    public class CircleGenerator : MonoBehaviour
    {
        public GameObject circlePrefab;
        public float timeInterval = 2.0f;
        private float timeCounter = 0.0f;

        private void Update()
        {
            timeCounter += Time.deltaTime;
            if (timeCounter >= timeInterval)
            {
                timeCounter = 0.0f;
                circlePrefab.tag = "food";
                Vector3 randomPosition = new Vector3(Random.Range(-8.0f, 5.0f), Random.Range(-4.0f, 4.0f), 0.0f);
                Instantiate(circlePrefab, randomPosition, Quaternion.identity);
            }
        }

    }
}