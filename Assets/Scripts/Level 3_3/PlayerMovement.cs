using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Level3_3
{
    public class PlayerMovement : MonoBehaviour
    {
        private float speed = 8.0f;
        private Rigidbody2D rigidBody;
        private float timeInterval = 1.0f;
        private float timeCounter = 0.0f;
        private float max_health = 2.1f;
        private float min_health = 0.3f;
        private int diamondCount = 0;
        public TextMeshProUGUI diamondText;

        public TextMeshProUGUI spikeText;
        private GameObject health;
        public GameOverScript gameOverScript;
        private GameObject inGameCanvas;

        public bool isEnemy1Freeze = true;
        public bool isEnemy2Freeze = true;

        public GameObject spikePrefab;

        private int spikeCount = 0;
        // Start is called before the first frame update
        void Start()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            inGameCanvas = GameObject.Find("In Game Canvas");
            health = GameObject.Find("Health");
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector2 direction = new Vector2(horizontal, vertical);
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            Bounds bounds = renderer.bounds;
            Vector2 size = bounds.size;
            rigidBody.velocity = direction * speed;
            //rigidBody.velocity = rigidBody.velocity.normalized * (speed / (Mathf.Max(size.x, size.y, 0.6f)));
        }

        private void Update()
        {
            gettingSmall();
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            Bounds bounds = renderer.bounds;
            Vector2 size = bounds.size;
            //rigidBody.velocity = rigidBody.velocity.normalized * (speed / (Mathf.Max(size.x, size.y, 0.6f)));
            if (size.x <= min_health || size.y <= min_health)
            {
                gameOverScript.Setup("You died!");
                inGameCanvas.SetActive(false);
            }
            health.GetComponent<TextMeshPro>().text = Mathf.Round((transform.localScale.x - min_health) / (max_health - min_health) * 100).ToString();

            if (Input.GetKeyDown(KeyCode.Space)&& spikeCount>0)
            {
                Debug.Log("spike pressed");
                Instantiate(spikePrefab, gameObject.transform.localPosition, Quaternion.identity);
            }
        }

        private void gettingSmall()
        {
            timeCounter += Time.deltaTime;
            if (timeCounter >= timeInterval)
            {
                transform.localScale += new Vector3(-0.03f, -0.03f, 0);
                timeCounter = 0.0f;
            }
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("teleportin"))
            {
                GameObject obj = GameObject.FindGameObjectWithTag("teleportout");
                transform.position = obj.transform.position;

            }
            if (collision.gameObject.CompareTag("Diamond"))
            {
                Destroy(collision.gameObject);
                diamondCount++;
                diamondText.text = "Diamonds: " + diamondCount + "/3";
            }
            if (collision.gameObject.CompareTag("Door"))
            {
                if (diamondCount == 3)
                {
                    gameOverScript.Setup("You won!");
                    inGameCanvas.SetActive(false);
                }
                else
                {
                    Debug.Log("Collect all 3 diamonds");
                }
            }
            if (collision.gameObject.CompareTag("enemy1"))
            {
                SpriteRenderer renderer = GetComponent<SpriteRenderer>();
                Bounds bounds = renderer.bounds;
                Vector2 size = bounds.size;
                Vector2 enemysize = collision.gameObject.GetComponent<SpriteRenderer>().bounds.size;
                if (size.x >= 1.2f)
                {
                    Destroy(collision.gameObject);
                }
                else
                {
                    Debug.Log("You collided with an enemy");
                    gameOverScript.Setup("Enemy ate you!");
                    inGameCanvas.SetActive(false);
                }

            }
            if (collision.gameObject.CompareTag("enemy2"))
            {
                SpriteRenderer renderer = GetComponent<SpriteRenderer>();
                Bounds bounds = renderer.bounds;
                Vector2 size = bounds.size;
                Vector2 enemysize = collision.gameObject.GetComponent<SpriteRenderer>().bounds.size;
                Debug.Log("enemy2" + enemysize);
                if (size.x >= 1.2f)
                {
                    Destroy(collision.gameObject);
                }
                else
                {

                    Debug.Log("You collided with an enemy");
                    gameOverScript.Setup("Enemy ate you!");
                    inGameCanvas.SetActive(false);
                }

            }
            if (collision.gameObject.CompareTag("enemy3"))
            {
                SpriteRenderer renderer = GetComponent<SpriteRenderer>();
                Bounds bounds = renderer.bounds;
                Vector2 size = bounds.size;
                Vector2 enemysize = collision.gameObject.GetComponent<SpriteRenderer>().bounds.size;
                Debug.Log("enemy2" + enemysize);
                if (size.x >= 1.2f)
                {
                    Destroy(collision.gameObject);
                    spikeCount++;
                    spikeText.text=":"+spikeCount;
                    Debug.Log("spikeCount" + spikeCount);
                }
                else
                {

                    Debug.Log("You collided with an enemy");
                    gameOverScript.Setup("Enemy ate you!");
                    inGameCanvas.SetActive(false);
                }

            }
            if (collision.gameObject.CompareTag("Enemy1Detector"))
            {
                isEnemy1Freeze = false;
                Destroy(collision.gameObject);
            }
            if (collision.gameObject.CompareTag("Enemy2Detector"))
            {
                isEnemy2Freeze = false;
                Destroy(collision.gameObject);
            }
        }
    }
}

