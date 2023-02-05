using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballmovementscript : MonoBehaviour
{

    public float speed = 4.0f;

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
        if (timeCounter >= timeInterval)
        {
            transform.localScale += new Vector3(-0.1f, -0.1f, 0);
            timeCounter = 0.0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("food"))
        {
            Debug.Log("Test");
            Destroy(collision.gameObject);
            transform.localScale += new Vector3(1.0f, 1.0f, 0);
        }
    }
}