using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace Level3_3
{
    public class GameOverScript : MonoBehaviour
    {
        public TextMeshProUGUI gameOverText; 
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }
        public void Setup(string message)
        {
            gameObject.SetActive(true);
            gameOverText.text = message;
        }
        public void RestartButton()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Level 3_2");
        }
        public void MainMenuButton()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Level Selector");
        }
    }
}