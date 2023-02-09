using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{

    public TMP_Text pointsText;
    public TMP_Text timeText;

    public void Setup(int score, float time)
    {
        gameObject.SetActive(true);
        pointsText.text = score.ToString() + " Points";
        timeText.text = Mathf.Round(time).ToString() + " seconds";
        Time.timeScale = 0;
    }

    public void RestartButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("SampleScene");
    }
}
