using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level3_3
{
    public class EnemyMovement3 : MonoBehaviour
    {
        public Sprite sadSprite;
        public Sprite angrySprite;
        public PlayerMovement playerMovement;

        public GameObject player;
        public float speed = 2.0f;

        public Sprite sadSprite85;

        public Sprite angrySprite85;
        private float speedReduced = 2.0f;
        private SpriteRenderer spriteRenderer;
        // Start is called before the first frame update
        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame       
        void Update()
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
            if (playersize.x < transform.localScale.x && playersize.y < transform.localScale.y && playerMovement.isEnemy3spiked == false)
            {
                Vector3 targetPosition = player.transform.position;
                Vector3 enemyPosition = transform.position;
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(enemyPosition, targetPosition, step);
                spriteRenderer.sprite = angrySprite;
                transform.localScale = new Vector3(angrySprite.bounds.size.x * scaleFactor, angrySprite.bounds.size.y * scaleFactor, 1);
            }
            else if (playerMovement.isEnemy3spiked == false)
            {
                Vector3 direction = transform.position - player.transform.position;
                transform.Translate(direction.normalized * speedReduced * Time.deltaTime);
                spriteRenderer.sprite = sadSprite;
                transform.localScale = new Vector3(sadSprite.bounds.size.x * scaleFactor, sadSprite.bounds.size.y * scaleFactor, 1);
            }
            if (playersize.x < transform.localScale.x && playersize.y < transform.localScale.y && playerMovement.isEnemy3spiked == true)
            {
                Vector3 targetPosition = player.transform.position;
                Vector3 enemyPosition = transform.position;
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(enemyPosition, targetPosition, step);
                spriteRenderer.sprite = angrySprite85;
                transform.localScale = new Vector3(angrySprite85.bounds.size.x * scaleFactor, angrySprite85.bounds.size.y * scaleFactor, 1);
            }
            else if (playerMovement.isEnemy3spiked == true)
            {
                Vector3 direction = transform.position - player.transform.position;
                transform.Translate(direction.normalized * speedReduced * Time.deltaTime);
                spriteRenderer.sprite = sadSprite85;
                transform.localScale = new Vector3(sadSprite85.bounds.size.x * scaleFactor, sadSprite85.bounds.size.y * scaleFactor, 1);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Spike")
            {
                Destroy(collision.gameObject);
                // transform.localScale = new Vector3(transform.localScale.x / 1.2f, transform.localScale.y / 1.2f, 1);
                // Transform transform = gameObject.GetComponent<Transform>();
                // Vector3 newScale = transform.localScale / 1.4f;
                // transform.localScale = newScale;
                // transform.localScale = new Vector3(enemysize.x / 1.4f, enemysize.y / 1.4f, 1);
                // Vector3 currentSize = spriteRenderer.transform.localScale;
                // Vector3 newSize = new Vector3(currentSize.x / 1.4f, currentSize.y / 1.4f, currentSize.z / 1.4f);
                // spriteRenderer.transform.localScale = newSize;
                playerMovement.isEnemy3spiked = true;

            }
        }
    }

}
