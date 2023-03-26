using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace Level3_1
{
    public class PlayerMovement : MonoBehaviour
    {
        private float speed = 8.0f;
        private Rigidbody2D rigidBody;
        private float timeInterval = 1.0f;
        private float timeCounter = 0.0f;
        private int diamondCount = 0;
        public TextMeshProUGUI diamondText;
        public GameOverScript gameOverScript;
        private GameObject inGameCanvas;
        // Start is called before the first frame update
        void Start()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            inGameCanvas = GameObject.Find("In Game Canvas");
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector2 direction = new Vector2(horizontal, vertical);
            rigidBody.velocity = direction * speed;
        }

        private void Update()
        {
            gettingSmall();
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            Bounds bounds = renderer.bounds;
            Vector2 size = bounds.size;
            if (size.x <= 0.3f || size.y <= 0.3f)
            {
                gameOverScript.Setup("You died!");
                inGameCanvas.SetActive(false);
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
            if (collision.gameObject.CompareTag("Diamond"))
            {
                Destroy(collision.gameObject);
                diamondCount++;
                diamondText.text = "Diamonds: " + diamondCount + "/3"; 
            }
            if (collision.gameObject.CompareTag("Door"))
            {
                if(diamondCount==3)
                {
                    gameOverScript.Setup("You won!");
                    inGameCanvas.SetActive(false);
                }
                else
                {
                    Debug.Log("Collect all 3 diamonds");
                }
            }
        }
        public void RestartButton()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Level 3_1");
        }
        public void MainMenuButton()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Level Selector");
        }
    }
}

