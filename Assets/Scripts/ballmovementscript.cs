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

    private Rigidbody2D rigidBody;
    private Vector2 direction;
    // Start is called before the first frame update
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


    public void StopScript()
    {
        enabled = false;
    }

    public void StartScript()
    {
        enabled = true;
    }

    private void Update()
    {
        timeCounter += Time.deltaTime;

        if (timeCounter >= timeInterval)
        {
            transform.localScale += new Vector3(-0.1f, -0.1f, 0);
            timeCounter = 0.0f;
        }

        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        Bounds bounds = renderer.bounds;
        Vector2 size = bounds.size;

        Debug.Log(size);


        rigidBody.velocity = rigidBody.velocity.normalized * (speed / (Mathf.Max(size.x, size.y)));

        if (size.x <= 0.3f || size.y <= 0.3f)
        {
            GameOver();
            this.enabled = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("food"))
        {
            Destroy(collision.gameObject);
            transform.localScale += new Vector3(0.1f, 0.1f, 0);
            score += 1;
        }

        if (collision.gameObject.CompareTag("enemy"))
        {
            GameOver();
            this.enabled = false;
        }

    }
    public void GameOver()
    {
        elapsedTime = Time.time - startTime;
        gameOverScreen.Setup(score, elapsedTime);
    }
}