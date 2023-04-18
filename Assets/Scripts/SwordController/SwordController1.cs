using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    public class SwordController1 : MonoBehaviour
    {
        public float lifeTime = 6.0f;
        // Start is called before the first frame update
        void Start()
        {
            Invoke("DestroyClone", lifeTime);
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void DestroyClone()
        {
            Destroy(gameObject);
        }
    }


