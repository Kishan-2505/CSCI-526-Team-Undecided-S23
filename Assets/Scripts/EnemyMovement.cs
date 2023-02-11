using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public GameObject player;
    public float speed = 2.0f;

    void Update()
    {
        Vector3 targetPosition = player.transform.position;
        Vector3 enemyPosition = transform.position;
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(enemyPosition, targetPosition, step);
    }
}
