using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Level2_4
{

    public class ballmovementscript : MonoBehaviour
    {

        public GameOverScreen gameOverScreen;
        public float speed = 4.0f;

        public float startTime;
        public int score = 0;
        private float elapsedTime;

        private int state = -1;//No state: 0 denotes kill by enemy, 1 denotes size death, 3 denotes win
        private Rigidbody2D rigidBody;
        private Vector2 direction;
        // Start is called before the first frame update

        public float freezeDuration = 5.0f;
        public bool isBallFrozen = false;
        public Rigidbody2D enemy;
        private Vector2 originalVelocity;

        private bool onTouch1 = true;
        private bool onTouch2 = true;
        private bool onTouch3 = true;
        public GameObject DiminishingWall;
        public int buttonCount = 0;
        public GameObject instructions;
        private bool canvasDispalay = false;

        public int bulletHit = 0;
        public displaypoints displaypoints;

        public displaypoints displaywarning;

        private int capsulecount = 0;
        // public displaypoints displaybutton;
        public GameObject spikePrefab;

        // public GameObject bulletPrefab;

        public displaypoints displaytimeofdeath;
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
            rigidBody.velocity = rigidBody.velocity.normalized * (speed / (Mathf.Max(size.x, size.y, 0.6f)));
        }
        public float timeInterval = 1.0f;
        private float timeCounter = 0.0f;
        private float max_health = 1.6f;
        private float min_health = 0.3f;
        public int bulletsFired = 0;
        public bool isGettingSmall = true;
        public bool wall2Broken = false;
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

            rigidBody.velocity = rigidBody.velocity.normalized * (speed / (Mathf.Max(size.x, size.y, 0.6f)));

            if (size.x <= 0.3f || size.y <= 0.3f)
            {
                state = 1;//No state: 0 denotes kill by enemy, 1 denotes size death.
                GameOver("You died of starvation");
                this.enabled = false;
            }

            displaytimeofdeath.displaytimeofdeath((transform.localScale.x - min_health) / (max_health - min_health));
            if (score >= 5)
            {
                displaypoints.display((int)Mathf.Floor(score / 5));
            }
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
                if (score >= 5) //Change this to 10
                {
                    displaywarning.displaywarning("You spawned a spike");
                    score -= 5;//change this to 10
                    Instantiate(spikePrefab, gameObject.transform.localPosition, Quaternion.identity);
                }
                else
                {
                    displaywarning.displaywarning("You need at least 5 points to spawn a spike");
                }
            }
            // if (Input.GetKeyDown(KeyCode.P))
            // {
            //     if (score >= 1)
            //     {//cnhange this to 10
            //         displaywarning.displaywarning("Bullet fired");
            //         bulletsFired += 1;
            //         score -= 1;
            //         if (isGettingSmall)
            //             transform.localScale += new Vector3(-0.08f, -0.08f, 0);
            //         var bullet = Instantiate(bulletPrefab, gameObject.transform.localPosition, Quaternion.identity);
            //         bullet.GetComponent<Rigidbody2D>().velocity = rigidBody.velocity * -1;
            //     }
            //     else
            //     {
            //         displaywarning.displaywarning("You need at least 1 points to fire a bullet");
            //     }

            // }
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
            if (capsulecount == 1)
            {
                GameOver("You Won!");
            }

        }

        private void ResetButtonCollision1()
        {
            onTouch1 = true;
        }
        private void ResetButtonCollision2()
        {
            onTouch2 = true;
        }

        private void ResetButtonCollision3()
        {
            onTouch3 = true;
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
                // Debug.Log(collision.gameObject.GetComponent<SpriteRenderer>().color);
                // Debug.Log(gameObject.GetComponent<SpriteRenderer>().color);
                SpriteRenderer renderer = GetComponent<SpriteRenderer>();
                Bounds bounds = renderer.bounds;
                Vector2 size = bounds.size;
                if (collision.gameObject.GetComponent<SpriteRenderer>().color == gameObject.GetComponent<SpriteRenderer>().color && size.x >= 0.8f)
                {
                    Destroy(collision.gameObject);
                }
                else
                {
                    GameOver("You collided with an enemy. Let's try again!");
                    this.enabled = false;
                }

            }
            if (collision.gameObject.CompareTag("Blue"))
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                Destroy(collision.gameObject);
                capsulecount++;
            }
            if (collision.gameObject.CompareTag("Black"))
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.black;
                Destroy(collision.gameObject);
                capsulecount++;
            }
            if (collision.gameObject.CompareTag("Green"))
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                Destroy(collision.gameObject);
                capsulecount++;
            }
            if (collision.gameObject.CompareTag("Wall1"))
            {
                if (onTouch1 == true)
                {
                    SpriteRenderer renderer = GetComponent<SpriteRenderer>();
                    Bounds bounds = renderer.bounds;
                    Vector2 size = bounds.size;
                    Debug.Log(size.x);
                    float x = size.x;
                    Invoke("ResetButtonCollision1", 2f);
                    onTouch1 = false;
                    collision.gameObject.transform.localScale -= new Vector3(x / 4.0f, 0, 0);
                    // Debug.Log(collision.gameObject.transform.localScale.x);
                    if (collision.gameObject.transform.localScale.x <= 0.0f)
                    {
                        Destroy(collision.gameObject);
                    }
                }

            }
            if (collision.gameObject.CompareTag("Wall2"))
            {
                if (onTouch2 == true)
                {
                    SpriteRenderer renderer = GetComponent<SpriteRenderer>();
                    Bounds bounds = renderer.bounds;
                    Vector2 size = bounds.size;
                    float x = size.x;
                    Invoke("ResetButtonCollision2", 2f);
                    onTouch2 = false;
                    collision.gameObject.transform.localScale -= new Vector3(x / 4.0f, 0, 0);
                    if (collision.gameObject.transform.localScale.x <= 0.0f)
                    {
                        Destroy(collision.gameObject);
                        wall2Broken = true;
                    }
                }
            }
            if (collision.gameObject.CompareTag("Wall3"))
            {

                if (onTouch3 == true)
                {
                    SpriteRenderer renderer = GetComponent<SpriteRenderer>();
                    Bounds bounds = renderer.bounds;
                    Vector2 size = bounds.size;
                    float x = size.x;
                    Invoke("ResetButtonCollision3", 2f);
                    onTouch3 = false;
                    collision.gameObject.transform.localScale -= new Vector3(x / 4.0f, 0, 0);
                    if (collision.gameObject.transform.localScale.x <= 0.0f)
                    {
                        Destroy(collision.gameObject);
                    }
                }
            }

        }
        public void GameOver(string message)
        {

            elapsedTime = Time.time - startTime;
            gameOverScreen.Setup(score, elapsedTime, state, message, bulletsFired, bulletHit, isGettingSmall);
        }
    }
}