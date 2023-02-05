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
}