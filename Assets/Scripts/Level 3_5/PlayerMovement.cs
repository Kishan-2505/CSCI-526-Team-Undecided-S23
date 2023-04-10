using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Level3_5
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
        private int knifeCount = 0;
        private float throwForce = 10.0f;
        public TextMeshProUGUI diamondText;

        public TextMeshProUGUI spikeText;
        private GameObject health;
        public GameOverScript gameOverScript;
        private GameObject inGameCanvas;

        public bool isEnemy1Freeze = true;
        public bool isEnemy2Freeze = true;
        public bool isEnemy3Freeze = true;
        public bool isEnemy4Freeze = true;
        public bool hasMagnet = false;
        private bool onTouch1 = true;
        public GameObject spikePrefab;
        public GameObject knifePrefab;

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

            if (Input.GetKeyDown(KeyCode.Space) && spikeCount>0)
            {
                spikeCount -= 1;
                Instantiate(spikePrefab, new Vector3(gameObject.transform.localPosition.x + 1, gameObject.transform.localPosition.y + 1), Quaternion.identity);
            }
            if (Input.GetMouseButtonDown(0) && knifeCount>0)
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0f;

                GameObject thrownObject = Instantiate(knifePrefab, transform.position, Quaternion.identity);
                Rigidbody2D rb = thrownObject.GetComponent<Rigidbody2D>();
                Vector2 throwDirection = (mousePos - transform.position).normalized;
                rb.AddForce(throwDirection * throwForce, ForceMode2D.Impulse);
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
                transform.position = obj.transform.position + new Vector3(-1f, -1f, 0); 
            }
            if (collision.gameObject.CompareTag("teleportin2"))
            {
                GameObject obj = GameObject.FindGameObjectWithTag("teleportout2");
                transform.position = obj.transform.position;
            }
            if (collision.gameObject.CompareTag("Diamond"))
            {
                Destroy(collision.gameObject);
                diamondCount++;
                diamondText.text = "Diamonds: " + diamondCount + "/3";
            }
            if(collision.gameObject.CompareTag("GetKnife"))
            {
                Destroy(collision.gameObject);
                knifeCount += 5;
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
                if (size.x >= 1.8f)
                {
                    Destroy(collision.gameObject);
                }
                else
                {
                    Destroy(gameObject);
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
                if (size.x >= 1.8f)
                {
                    Destroy(collision.gameObject);
                }
                else
                {
                    Destroy(gameObject);
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
                if (size.x >= 1.8f)
                {
                    Destroy(collision.gameObject);
                    spikeCount+=3;
                    spikeText.text=":"+spikeCount;
                }
                else
                {
                    Destroy(gameObject);
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
            if (collision.gameObject.CompareTag("Enemy3Detector"))
            {
                isEnemy3Freeze = false;
                Destroy(collision.gameObject);
            }
            if(collision.gameObject.CompareTag("Enemy4Detector"))
            {
                isEnemy4Freeze = false;
                Destroy(collision.gameObject);
            }
            if (collision.gameObject.CompareTag("magnet"))
            {
                hasMagnet = true;
                Destroy(collision.gameObject);
            }
            if (collision.gameObject.CompareTag("Wall1"))
            {
                if (onTouch1 == true)
                {
                    //SpriteRenderer renderer = GetComponent<SpriteRenderer>();
                    //Bounds bounds = renderer.bounds;
                    //Vector2 size = bounds.size;
                    //float x = size.x;
                    Invoke("ResetButtonCollision1", 1f);
                    onTouch1 = false;
                    collision.gameObject.transform.localScale -= new Vector3(0.2f, 0, 0);
                    if (collision.gameObject.transform.localScale.x <= 0.0f)
                    {
                        Destroy(collision.gameObject);
                    }
                }
            }
        }
        private void ResetButtonCollision1()
        {
            onTouch1 = true;
        }
    }
}

