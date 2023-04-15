using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level3_2
{
    public class EnemyMovement2 : MonoBehaviour
    {
        public Sprite sadSprite;
        public Sprite angrySprite;
        public PlayerMovement playerMovement;
        public GameObject player;
        public float speed = 2.0f;

        private float speedReduced=2.0f;
        private float elapsedTime;
        private SpriteRenderer spriteRenderer;
        private int count=0;
        // Start is called before the first frame update
        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame       
        void Update()
        {

            if (!playerMovement.isEnemy2Freeze)
            {
                // Vector3 targetPosition = player.transform.position;
                // Vector3 enemyPosition = transform.position;
                // float step = speed * Time.deltaTime;
                // transform.position = Vector3.MoveTowards(enemyPosition, targetPosition, step);
                Bounds bounds = spriteRenderer.sprite.bounds;
                Vector3 scale = transform.localScale;
                float scaleFactor = Mathf.Min(scale.x / bounds.size.x, scale.y / bounds.size.y);
                Vector2 enemysize = bounds.size;
                Bounds playerbound = player.GetComponent<SpriteRenderer>().bounds;
                Vector2 playersize = playerbound.size;
                // Set the new sprite image and scale it to fit the current object size
                if (playersize.x < enemysize.x && playersize.y < enemysize.y)
                {
                    Vector3 targetPosition = player.transform.position;
                    Vector3 enemyPosition = transform.position;
                    float step = speed * Time.deltaTime;
                    transform.position = Vector3.MoveTowards(enemyPosition, targetPosition, step);
                    spriteRenderer.sprite = angrySprite;
                    transform.localScale = new Vector3(angrySprite.bounds.size.x * scaleFactor, angrySprite.bounds.size.y * scaleFactor, 1);
                }
                else
                {
                    if(count==0){
                        playerMovement.isEnemyColourChange=true;
                        count++;
                    }
                    
                    Vector3 direction = transform.position - player.transform.position;
                    transform.Translate(direction.normalized * speedReduced * Time.deltaTime);
                    spriteRenderer.sprite = sadSprite;
                    transform.localScale = new Vector3(sadSprite.bounds.size.x * scaleFactor, sadSprite.bounds.size.y * scaleFactor, 1);
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {

        }
    }

}
