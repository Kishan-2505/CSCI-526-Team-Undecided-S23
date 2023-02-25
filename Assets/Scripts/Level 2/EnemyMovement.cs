using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level2
{
    public class EnemyMovement : MonoBehaviour
    {
        public GameOverScreen gameOverScreen;
        public ballmovementscript ballmovementscript;

        private int health = 100;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        public GameObject player;
        public float speed = 2.0f;
        private float elapsedTime;

        
        void Update()
        {
            if (!ballmovementscript.isBallFrozen)
            {
                Vector3 targetPosition = player.transform.position;
                Vector3 enemyPosition = transform.position;
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(enemyPosition, targetPosition, step);
            }
            if(health <= 0)
            {
                Destroy(gameObject);
                elapsedTime = Time.time - ballmovementscript.startTime;
                gameOverScreen.Setup(ballmovementscript.score, elapsedTime, 3, "You Won!",ballmovementscript.bulletsFired,ballmovementscript.bulletHit,ballmovementscript.isGettingSmall); // 3 is win state
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Spike")
            {
                Destroy(gameObject);
                Destroy(collision.gameObject);
                Debug.Log("You Won!");
                elapsedTime = Time.time - ballmovementscript.startTime;
                gameOverScreen.Setup(ballmovementscript.score, elapsedTime, 3, "You Won!",ballmovementscript.bulletsFired,ballmovementscript.bulletHit,ballmovementscript.isGettingSmall); // 3 is win state
            }
            if (collision.gameObject.tag == "Bullet")
            {
                // Destroy(gameObject);
                ballmovementscript.bulletHit+=1;
                Destroy(collision.gameObject);
                health-=25; // 3 is win state
            }
        }
    }

}
