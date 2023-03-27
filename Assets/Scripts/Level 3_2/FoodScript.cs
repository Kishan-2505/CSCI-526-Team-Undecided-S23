using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level3_2
{
    public class FoodScript : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.transform.localScale += new Vector3(0.15f, 0.15f, 0);
                // gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }
    }
}
