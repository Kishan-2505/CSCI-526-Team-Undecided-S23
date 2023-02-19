using UnityEngine;

namespace Level2
{
    public class FreezeFoodGenerator : MonoBehaviour
    {
        public GameObject trianglePrefab;
        // public float timeInterval = 5.0f;
        public float timeInterval = 20.0f;
        private float timeCounter = 0.0f;

        private void Update()
        {
            timeCounter += Time.deltaTime;
            if (timeCounter >= timeInterval)
            {
                timeCounter = 0.0f;
                trianglePrefab.tag = "FreezeFood";
                Vector3 randomPosition = new Vector3(Random.Range(-8.0f, 8.0f), Random.Range(-4.0f, 4.0f), 0.0f);
                Instantiate(trianglePrefab, randomPosition, Quaternion.identity);
            }
        }

    }
}
