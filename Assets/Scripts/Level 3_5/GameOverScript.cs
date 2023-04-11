using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace Level3_5
{
    public class GameOverScript : MonoBehaviour
    {
        public TextMeshProUGUI gameOverText;
        public GameObject restartButton;
        public GameObject nextLevelButton;
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
            if (message == "You died!" || message == "Enemy ate you!")
            {
                restartButton.SetActive(true);
                nextLevelButton.SetActive(false);
            }
            if (message == "You won!")
            {
                restartButton.SetActive(false);
                nextLevelButton.SetActive(true);
                nextLevelButton.transform.position = restartButton.transform.position;
            }
        }
        public void RestartButton()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Level 3_5");
        }
        public void MainMenuButton()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Level Selector");
        }
        public void NextLevelButton()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Level 3_5");
        }
    }
}