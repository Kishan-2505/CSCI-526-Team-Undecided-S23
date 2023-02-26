using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Level1
{

    public class ballmovementscript : MonoBehaviour
    {

        public GameOverScreen gameOverScreen;
        public float speed = 4.0f;

        public float startTime;
        public int score = 0;
        public GameObject instructions;
        private bool canvasDispalay = false;
        private float elapsedTime;

        private int state = -1;//No state: 0 denotes kill by enemy, 1 denotes size death, 3 denotes win
        private Rigidbody2D rigidBody;
        private Vector2 direction;
        // Start is called before the first frame update

        public float freezeDuration = 5.0f;
        public bool isBallFrozen = false;
        public Rigidbody2D enemy;
        private Vector2 originalVelocity;

        private bool onTouch = true;
        public GameObject DiminishingWall;
        public int buttonCount = 0;

        public displaypoints displaypoints;

        public displaypoints displaywarning;
        // public displaypoints displaybutton;

        public displaypoints displaytimeofdeath;
        public GameObject spikePrefab;
        void Start()
        {
            startTime = Time.time;
            rigidBody = GetComponent<Rigidbody2D>();
            direction = Random.insideUnitCircle.normalized;
            enemy = GetComponent<Rigidbody2D>();
            instructions.SetActive(true);
            canvasDispalay = true;
            Time.timeScale = 0;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector2 direction = new Vector2(horizontal, vertical);
            rigidBody.velocity = direction * speed;
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            Bounds bounds = renderer.bounds;
            Vector2 size = bounds.size;
            rigidBody.velocity = rigidBody.velocity.normalized * (speed / (Mathf.Max(size.x, size.y,0.6f)));
        }
        public float timeInterval = 1.0f;
        private float timeCounter = 0.0f;

        public bool isGettingSmall = true;
        private void Update()
        {
            if (isGettingSmall)
            {
                timeCounter += Time.deltaTime;
                if (timeCounter >= timeInterval)
                {
                    transform.localScale += new Vector3(-0.05f, -0.05f, 0);
                    timeCounter = 0.0f;
                }
            }


            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            Bounds bounds = renderer.bounds;
            Vector2 size = bounds.size;

            rigidBody.velocity = rigidBody.velocity.normalized * (speed / (Mathf.Max(size.x, size.y,0.6f)));

            Debug.Log("size of x"+size.x+" size of y"+size.y);
            if (size.x <= 0.3f || size.y <= 0.3f)
            {
                state = 1;//No state: 0 denotes kill by enemy, 1 denotes size death.
                GameOver("You died of starvation");
                this.enabled = false;
            }

            displaytimeofdeath.displaytimeofdeath((size.x-0.3f)/0.05f);

            displaypoints.display(score);
            // if (score >= 3)
            // {
            //     displaybutton.displaybutton(0);
            // }
            // else
            // {
            //     displaybutton.displaybutton(3 - score);
            // }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (score >= 15) //Change this to 10
                {
                    displaywarning.displaywarning("You spawned a spike");
                    score -= 15;//change this to 10
                    Instantiate(spikePrefab, gameObject.transform.localPosition, Quaternion.identity);
                }
                else
                {
                    displaywarning.displaywarning("You need at least 15 points to spawn a spike");
                }
            }
            if (Input.GetKeyDown(KeyCode.Tab) && canvasDispalay == false)
            {
                instructions.SetActive(true);
                canvasDispalay = true;
                Time.timeScale = 0;
            }
            else if (Input.GetKeyDown(KeyCode.Tab) && canvasDispalay == true)
            {
                instructions.SetActive(false);
                canvasDispalay = false;
                Time.timeScale = 1;
            }


        }

        private void ResetButtonCollision()
        {
            onTouch = true;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("food"))
            {
                Destroy(collision.gameObject);
                if (isGettingSmall)
                {
                    transform.localScale += new Vector3(0.15f, 0.15f, 0);
                }
                score += 1;
            }

            if (collision.gameObject.CompareTag("enemy"))
            {
                state = 0;// 0 denotes kill by enemy, 1 denotes size death.
                GameOver("You collided with an enemy");
                this.enabled = false;
            }
            if (collision.gameObject.CompareTag("Button"))
            {
                if (score >= 7)
                {
                    if (onTouch == true)
                    {
                        buttonCount++;
                        score -= 7; //change this to 5
                        Invoke("ResetButtonCollision", 1f);
                        onTouch = false;
                        DiminishingWall.transform.localScale -= new Vector3(0.2f, 0, 0);
                        Debug.Log("Collided with button");
                        // if (buttonCount == 5)
                        // {
                        //     Destroy(collision.gameObject);
                        // }
                        Destroy(collision.gameObject);
                    }
                }
            }
            if (collision.gameObject.CompareTag("InfiniteTag"))
            {
                isGettingSmall = false;
                gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                Destroy(collision.gameObject);
            }
        }
        public void GameOver(string message)
        {

            elapsedTime = Time.time - startTime;
            gameOverScreen.Setup(score, elapsedTime, state, message, isGettingSmall);
        }
    }
}