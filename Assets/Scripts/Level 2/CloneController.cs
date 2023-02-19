using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level2
{
    public class CloneController : MonoBehaviour
    {
        public float lifeTime = 5.0f;
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

}
