using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace Level3_6
{
    public class GameOverScript : MonoBehaviour
    {
        public TextMeshProUGUI gameOverText;
        public GameObject restartButton;
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
            Time.timeScale=0;
            gameObject.SetActive(true);
            gameOverText.text = message;
            if (message == "You died!" || message == "Enemy ate you!")
            {
                restartButton.SetActive(true);
            }
            if (message == "You won!")
            {
                restartButton.SetActive(false);
            }
        }
        public void RestartButton()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Level 3_6");
        }
        public void MainMenuButton()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Level Selector");
        }
    }
}