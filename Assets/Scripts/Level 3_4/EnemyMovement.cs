using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level3_4
{
    public class EnemyMovement : MonoBehaviour
    {
        public Sprite sadSprite;
        public Sprite angrySprite;

        public Sprite sadSprite15;
        public Sprite angrySprite15;
        public PlayerMovement playerMovement;
        private float speedReduced = 2.0f;
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
                // Vector3 targetPosition = player.transform.position;
                // Vector3 enemyPosition = transform.position;
                // float step = speed * Time.deltaTime;
                // transform.position = Vector3.MoveTowards(enemyPosition, targetPosition, step);
                Bounds bounds = spriteRenderer.sprite.bounds;
                Vector3 scale = transform.localScale;
                float scaleFactor = Mathf.Max(scale.x / bounds.size.x, scale.y / bounds.size.y);
                Vector2 enemysize = bounds.size;
                Bounds playerbound = player.GetComponent<SpriteRenderer>().bounds;
                Vector2 playersize = playerbound.size;
                
                Debug.Log("enemy size: " + enemysize);
                Debug.Log("enemy scale: " + transform.localScale);
                // Set the new sprite image and scale it to fit the current object size
                if (playersize.x < transform.localScale.x && playersize.y < transform.localScale.y && playerMovement.isEnemy1spiked == false) 
                {
                    Vector3 targetPosition = player.transform.position;
                    Vector3 enemyPosition = transform.position;
                    float step = speed * Time.deltaTime;
                    transform.position = Vector3.MoveTowards(enemyPosition, targetPosition, step);
                    spriteRenderer.sprite = angrySprite;
                    transform.localScale = new Vector3(angrySprite.bounds.size.x * scaleFactor, angrySprite.bounds.size.y * scaleFactor, 1);
                }
                else if(playerMovement.isEnemy1spiked==false)
                {
                    Vector3 direction = transform.position - player.transform.position;
                    transform.Translate(direction.normalized * speedReduced * Time.deltaTime);
                    spriteRenderer.sprite = sadSprite;
                    transform.localScale = new Vector3(sadSprite.bounds.size.x * scaleFactor, sadSprite.bounds.size.y * scaleFactor, 1);
                }
                if (playersize.x < transform.localScale.x && playersize.y < transform.localScale.y && playerMovement.isEnemy1spiked == true)
                {
                    Vector3 targetPosition = player.transform.position;
                    Vector3 enemyPosition = transform.position;
                    float step = speed * Time.deltaTime;
                    transform.position = Vector3.MoveTowards(enemyPosition, targetPosition, step);
                    spriteRenderer.sprite = angrySprite15;
                    transform.localScale = new Vector3(angrySprite15.bounds.size.x * scaleFactor, angrySprite15.bounds.size.y * scaleFactor, 1);
                }
                else if (playerMovement.isEnemy1spiked == true)
                {
                    Vector3 direction = transform.position - player.transform.position;
                    transform.Translate(direction.normalized * speedReduced * Time.deltaTime);
                    spriteRenderer.sprite = sadSprite15;
                    transform.localScale = new Vector3(sadSprite15.bounds.size.x * scaleFactor, sadSprite15.bounds.size.y * scaleFactor, 1);
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Spike")
            {
                Destroy(collision.gameObject);
                transform.localScale = new Vector3(transform.localScale.x / 1.2f, transform.localScale.y / 1.2f, 1);
                // Transform transform = gameObject.GetComponent<Transform>();
                // Vector3 newScale = transform.localScale / 1.4f;
                // transform.localScale = newScale;
                // transform.localScale = new Vector3(enemysize.x / 1.4f, enemysize.y / 1.4f, 1);
                // Vector3 currentSize = spriteRenderer.transform.localScale;
                // Vector3 newSize = new Vector3(currentSize.x / 1.4f, currentSize.y / 1.4f, currentSize.z/1.4f);
                // spriteRenderer.transform.localScale = newSize;

                playerMovement.isEnemy1spiked = true;

            }
            if (collision.gameObject.CompareTag("WallSpike"))
            {
                Destroy(gameObject);
            }
        }
    }

}
