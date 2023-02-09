using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ballmovementscript : MonoBehaviour
{
    public float speed = 4.0f;

    public GameObject background;
    float startTime;
    int score=0;
    float elapsedTime;
    public Text pointsText;
    public Text timeText;
    public void RestartButton()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private Rigidbody2D rigidBody;
    private Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        startTime=Time.time;
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
        elapsedTime = Time.time - startTime;
        timeCounter += Time.deltaTime;
        // Vector3 v = new Vector3(0.1f, 0.1f, 2.0f);

        if (timeCounter >= timeInterval)
        {
            transform.localScale += new Vector3(-0.1f, -0.1f, 0);
            timeCounter = 0.0f;
            // if (transform.localScale.Equals(v))
            // {
            //     Debug.Log("Game Over!");
            // }
        }

        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        Bounds bounds = renderer.bounds;
        Vector2 size = bounds.size;

        rigidBody.velocity = rigidBody.velocity.normalized * (speed / (Mathf.Max(size.x, size.y)));

        if (size.x <= 0.3f || size.y <= 0.3f)
        {
            background.SetActive(true);
            pointsText.text=score.ToString()+" Points";
            timeText.text=elapsedTime.ToString()+" seconds";
            // GameOverScreen screen = new GameOverScreen();
            // screen.Setup();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("food"))
        {
            Destroy(collision.gameObject);
            transform.localScale += new Vector3(0.1f, 0.1f, 0);
            score+=1;
        }

        if (collision.gameObject.CompareTag("enemy"))
        {
            background.SetActive(true);
            pointsText.text=score.ToString()+" Points";
            timeText.text=elapsedTime.ToString()+" seconds";
        }
    }
}