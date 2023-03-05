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

        }

        // Update is called once per frame
        public void LoadLevel()
        {
            SceneManager.LoadScene(levelName);
        }
    }
}

