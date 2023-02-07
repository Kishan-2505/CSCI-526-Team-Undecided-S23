using UnityEngine;
using UnityEngine.SceneManagement;

public class ballmovementscript : MonoBehaviour
{
    public float speed = 4.0f;

    public GameObject background;

    public void RestartButton()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private Rigidbody2D rigidBody;
    private Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
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

    }
    public float timeInterval = 1.0f;
    private float timeCounter = 0.0f;
    private void Update()
    {
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

        if (size.x <= 0.1f || size.y <= 0.1f)
        {
            background.SetActive(true);
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
        }
    }
}