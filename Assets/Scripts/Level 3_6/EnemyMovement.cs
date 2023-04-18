using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level3_6
{
    public class EnemyMovement : MonoBehaviour
    {
        public Sprite sadSprite;
        public Sprite angrySprite;
        public PlayerMovement playerMovement;
        private GameObject player;
        public float speed = 2.0f;
        private float elapsedTime;
        private SpriteRenderer spriteRenderer;
        // Start is called before the first frame update
        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            player = GameObject.Find("Player");
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
                Bounds bounds = spriteRenderer.sprite.bounds;
                Vector3 scale = transform.localScale;
                float scaleFactor = Mathf.Max(scale.x / bounds.size.x, scale.y / bounds.size.y);
                Vector2 enemysize = bounds.size;
                Bounds playerbound = player.GetComponent<SpriteRenderer>().bounds;
                Vector2 playersize = playerbound.size;
                // Set the new sprite image and scale it to fit the current object size
                if (playersize.x < transform.localScale.x && playersize.y < transform.localScale.y)
                {
                    spriteRenderer.sprite = angrySprite;
                    transform.localScale = new Vector3(angrySprite.bounds.size.x * scaleFactor, angrySprite.bounds.size.y * scaleFactor, 1);
                }
                else
                {
                    spriteRenderer.sprite = sadSprite;
                    transform.localScale = new Vector3(sadSprite.bounds.size.x * scaleFactor, sadSprite.bounds.size.y * scaleFactor, 1);
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Spike")
            {
                Destroy(collision.gameObject);
                // Transform transform = gameObject.GetComponent<Transform>();
                // Vector3 newScale = transform.localScale / 1.4f;
                // transform.localScale = newScale;
                // transform.localScale = new Vector3(enemysize.x / 1.4f, enemysize.y / 1.4f, 1);
                Vector3 currentSize = spriteRenderer.transform.localScale;
                Vector3 newSize = new Vector3(currentSize.x / 1.4f, currentSize.y / 1.4f, currentSize.z/1.4f);
                spriteRenderer.transform.localScale = newSize;
            }
            if(collision.gameObject.tag == "knife")
            {
                Destroy(collision.gameObject);
                Vector3 spawnPosition1 = transform.position + transform.right * 0.5f;
                Vector3 spawnPosition2 = transform.position - transform.right * 0.5f;

                GameObject newObject1 = Instantiate(gameObject, spawnPosition1, Quaternion.identity);
                newObject1.transform.localScale = transform.localScale / 2;

                GameObject newObject2 = Instantiate(gameObject, spawnPosition2, Quaternion.identity);
                newObject2.transform.localScale = transform.localScale / 2;
                Destroy(gameObject);
            }
            if (collision.gameObject.CompareTag("WallSpike"))
            {
                Destroy(gameObject);
            }
        }
    }

}
