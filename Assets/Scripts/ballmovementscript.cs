using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ballmovementscript : MonoBehaviour
{

    public GameOverScreen gameOverScreen;

    public float speed = 4.0f;

    private float startTime;
    private int score = 0;
    private float elapsedTime;

    private int state = -1;//No state: 0 denotes kill by enemy, 1 denotes size death.
    private Rigidbody2D rigidBody;
    private Vector2 direction;
    // Start is called before the first frame update

    public float freezeDuration = 5.0f;
    private bool isBallFrozen = false;
    public Rigidbody2D enemy;
    private Vector2 originalVelocity;


    void Start()
    {
        startTime = Time.time;
        rigidBody = GetComponent<Rigidbody2D>();
        direction = Random.insideUnitCircle.normalized;
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
        rigidBody.velocity = rigidBody.velocity.normalized * (speed / (Mathf.Max(size.x, size.y)));
    }
    public float timeInterval = 1.0f;
    private float timeCounter = 0.0f;

    private void Update()
    {
        timeCounter += Time.deltaTime;

        if (timeCounter >= timeInterval)
        {
            transform.localScale += new Vector3(-0.05f, -0.05f, 0);
            timeCounter = 0.0f;
        }

        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        Bounds bounds = renderer.bounds;
        Vector2 size = bounds.size;

        rigidBody.velocity = rigidBody.velocity.normalized * (speed / (Mathf.Max(size.x, size.y)));

        if (size.x <= 0.3f || size.y <= 0.3f)
        {
            state = 1;//No state: 0 denotes kill by enemy, 1 denotes size death.
            GameOver();
            this.enabled = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("food"))
        {
            Destroy(collision.gameObject);
            transform.localScale += new Vector3(0.15f, 0.15f, 0);
            score += 1;
        }

        if (collision.gameObject.CompareTag("enemy"))
        {
            state = 0;// 0 denotes kill by enemy, 1 denotes size death.
            GameOver();
            this.enabled = false;
        }
        if(collision.gameObject.CompareTag("FreezeFood"))
        {
            Destroy(collision.gameObject);
            // enemy.color=Random.ColorHSV();
            FreezeBall();
        }
    }
    public void GameOver()
    {

        elapsedTime = Time.time - startTime;
        gameOverScreen.Setup(score, elapsedTime, state);
    }
    void FreezeBall()
    {
        if (!isBallFrozen)
        {
            isBallFrozen = true;
            originalVelocity = enemy.velocity;
            enemy.velocity = Vector2.zero;
            Invoke("UnfreezeBall", freezeDuration);
        }
    }

    void UnfreezeBall()
    {
        enemy.velocity = originalVelocity;
        isBallFrozen = false;
    }

}