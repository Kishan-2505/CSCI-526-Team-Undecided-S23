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
        private float max_health = 2.1f;
        private float min_health = 0.3f;
        private int diamondCount = 0;
        public TextMeshProUGUI diamondText;
        private GameObject health;
        public GameOverScript gameOverScript;
        private GameObject inGameCanvas;

        public GameObject foodtutorial;

        public GameObject diamondtutorial;

        private int countDiamondtutorial = 0;

        private int countFoodtutorial = 0;

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
        }

        private void Update()
        {
            gettingSmall();
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            Bounds bounds = renderer.bounds;
            Vector2 size = bounds.size;
            rigidBody.velocity = rigidBody.velocity.normalized * (speed / (Mathf.Max(size.x, size.y, 0.6f)));
            health.GetComponent<TextMeshPro>().text  = Mathf.Round((transform.localScale.x - min_health) / (max_health - min_health) * 100).ToString();
            if (size.x <= min_health || size.y <= min_health)
            {
                gameOverScript.Setup("You died!");
                inGameCanvas.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                diamondtutorial.SetActive(false);
                foodtutorial.SetActive(false);
                Time.timeScale = 1;
            }
        }

        private void gettingSmall()
        {
            timeCounter += Time.deltaTime;
            if (timeCounter >= timeInterval)
            {
                transform.localScale += new Vector3(-0.05f, -0.05f, 0);
                timeCounter = 0.0f;
            }
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.CompareTag("Diamond"))
            {
                if(countDiamondtutorial==0)
                {
                    diamondtutorial.SetActive(true);
                    Time.timeScale = 0;
                    countDiamondtutorial++;
                }
            }
            if(collision.gameObject.CompareTag("food"))
            {
                if(countFoodtutorial==0)
                {
                    foodtutorial.SetActive(true);
                    Time.timeScale = 0;
                    countFoodtutorial++;
                }
            }
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

        public void QuitButton()
        {
            foodtutorial.SetActive(false);
            diamondtutorial.SetActive(false);
            Time.timeScale=1;
        }
    }
}

