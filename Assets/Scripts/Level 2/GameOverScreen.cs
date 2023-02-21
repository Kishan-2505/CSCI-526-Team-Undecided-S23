using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Proyecto26;
using System;

namespace Level2
{
    public class GameOverScreen : MonoBehaviour
    {

        public TMP_Text pointsText;
        public TMP_Text timeText;
        public TMP_Text messageText;
        private readonly string basePath = "https://rich-teal-crayfish-coat.cyclic.app/level2";
        private RequestHelper currentRequest;

        public void Setup(int score, float time, int state, string message, int bulletsFired, int bulletHit, bool isGettingSmall)
        {
            Post(score, time, state, bulletsFired, bulletHit, isGettingSmall);
            gameObject.SetActive(true);
            pointsText.text = score.ToString() + " Points";
            messageText.text = message;
            timeText.text = Mathf.Round(time).ToString() + " seconds";
            Time.timeScale = 0;
        }

        public void RestartButton()
        {
            // Debug.Log("Restart");
            Time.timeScale = 1;
            SceneManager.LoadScene("Level Selector");
        }

        public void Post(int score, float time, int causeOfDeath, int bulletsFired, int bulletHit, bool isGettingSmall)
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
                    causeOfDeath = causeOfDeath,
                    bulletsFired=bulletsFired,
                    bulletHit=bulletHit,
                    isGettingSmall=isGettingSmall
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

        public bool isGettingSmall;

        public int bulletsFired;

        public int bulletHit;

        public override string ToString()
        {
            return UnityEngine.JsonUtility.ToJson(this, true);
        }
    }

}
