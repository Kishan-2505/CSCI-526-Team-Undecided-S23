using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Proyecto26;
using System;

namespace Endless
{
    public class GameOverScreen : MonoBehaviour
    {

        public TMP_Text pointsText;
        public TMP_Text timeText;
        private readonly string basePath = "https://backend-game-team-undecided.onrender.com/";
        private RequestHelper currentRequest;

        public void Setup(int score, float time, int state)
        {
            Post(score, time, state);
            gameObject.SetActive(true);
            pointsText.text = score.ToString() + " Points";
            timeText.text = Mathf.Round(time).ToString() + " seconds";
            Time.timeScale = 0;
        }

        public void RestartButton()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Endless Scene");
        }

        public void Post(int score, float time, int causeOfDeath)
        {
            Dictionary<string, string> head = new Dictionary<string, string>();
            head.Add("Content-Type", "application/json");
            head.Add("Access-Control-Allow-Origin", "*");

            currentRequest = new RequestHelper
            {
                Uri = basePath,
                Headers = head,
                Body = new PlayerRequest
                {
                    score = score,
                    time = time,
                    causeOfDeath = causeOfDeath
                },
                EnableDebug = true
            };
            RestClient.Post<PlayerRequest>(currentRequest)
            .Then(res =>
            {

                // And later we can clear the default query string params for all requests

                Debug.Log("Success");
            })
            .Catch(err => Debug.Log("Error"));
        }

    }

    [Serializable]
    public class PlayerRequest
    {
        public int score;

        public float time;

        public int causeOfDeath;

        public override string ToString()
        {
            return UnityEngine.JsonUtility.ToJson(this, true);
        }
    }

}
