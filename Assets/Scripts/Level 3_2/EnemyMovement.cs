using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level3_2
{
    public class EnemyMovement : MonoBehaviour
    {
        public PlayerMovement playerMovement;
        public GameObject player;
        public float speed = 2.0f;
        private float elapsedTime;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame       
        void Update()
        {
            if (!playerMovement.isEnemy1Freeze)
            {
                Vector3 targetPosition = player.transform.position;
                Vector3 enemyPosition = transform.position;
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(enemyPosition, targetPosition, step);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {

        }
    }

}
