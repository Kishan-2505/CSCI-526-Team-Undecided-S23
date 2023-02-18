using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LevelSelector
{
    
    public class LevelSelector : MonoBehaviour
    {
        public string levelName;
        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("Level Selector");
        }

        // Update is called once per frame
        public void LoadLevel()
        {
            Debug.Log("Loading Level");
            SceneManager.LoadScene(levelName);
        }
    }
}

