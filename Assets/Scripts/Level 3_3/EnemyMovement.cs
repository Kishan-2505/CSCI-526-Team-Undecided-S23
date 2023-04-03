using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level3_3
{
    public class EnemyMovement : MonoBehaviour
    {
        public Sprite sadSprite;
        public Sprite angrySprite;
        public PlayerMovement playerMovement;
        public GameObject player;
        public float speed = 2.0f;
        private float elapsedTime;
        private SpriteRenderer spriteRenderer;
        // Start is called before the first frame update
        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame       
        void Update()
        {
            // if (!playerMovement.isEnemy1Freeze)
            // {
            //     Vector3 targetPosition = player.transform.position;
            //     Vector3 enemyPosition = transform.position;
            //     float step = speed * Time.deltaTime;
            //     transform.position = Vector3.MoveTowards(enemyPosition, targetPosition, step);
            //     Bounds bounds = spriteRenderer.sprite.bounds;
            //     Vector3 scale = transform.localScale;
            //     float scaleFactor = Mathf.Min(scale.x / bounds.size.x, scale.y / bounds.size.y);
            //     Vector2 enemysize = bounds.size;
            //     Bounds playerbound = player.GetComponent<SpriteRenderer>().bounds;
            //     Vector2 playersize = playerbound.size;
            //     // Set the new sprite image and scale it to fit the current object size
            //     Debug.Log("Player size: " + playersize);
            //     Debug.Log("Enemy size: " + enemysize);
            //     if(playersize.x<enemysize.x && playersize.y<enemysize.y)
            //     {
            //         spriteRenderer.sprite = angrySprite;
            //         transform.localScale = new Vector3(angrySprite.bounds.size.x * scaleFactor, angrySprite.bounds.size.y * scaleFactor, 1);
            //     }
            //     else
            //     {
            //         spriteRenderer.sprite = sadSprite;
            //         transform.localScale = new Vector3(sadSprite.bounds.size.x * scaleFactor, sadSprite.bounds.size.y * scaleFactor, 1);
            //     }
            // }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {

        }
    }

}
