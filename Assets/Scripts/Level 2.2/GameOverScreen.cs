using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Proyecto26;
using System;

namespace Level2_2
{
    public class GameOverScreen : MonoBehaviour
    {

        public TMP_Text pointsText;
        public TMP_Text timeText;
        public TMP_Text messageText;
        private readonly string basePath = "https://rich-teal-crayfish-coat.cyclic.app/level2";
        private readonly string basePath1 = "https://rich-teal-crayfish-coat.cyclic.app/retries";
        private readonly string basePathWin = "https://rich-teal-crayfish-coat.cyclic.app/level2won";
        private readonly string basePathLoss = "https://rich-teal-crayfish-coat.cyclic.app/level2lost";
        private RequestHelper currentRequest;
        private int count=0;

        public void Setup(int score, float time, int state, string message, int bulletsFired, int bulletHit, bool isGettingSmall)
        {
            gameObject.SetActive(true);
            count++;
            pointsText.text = score.ToString() + " Points"; 
            if (message == "Game Won!")
            {
                if(count==1)
                    PostWin(time);
                messageText.color = Color.green;
                messageText.text = message;
            }
            else
            {
                if(count==1)
                    PostLoss(time, state);
                messageText.text = message;
            }
            timeText.text = Mathf.Round(time).ToString() + " seconds";
            Time.timeScale = 0;
        }

        public void RestartButton()
        {
            // Debug.Log("Restart");
            Post1();
            Time.timeScale = 1;
            SceneManager.LoadScene("Level 2.2");
        }
        public void MenuButton()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Level Selector");
        }

        public void PostWin(float time)
        {
            Dictionary<string, string> head = new Dictionary<string, string>();
            head.Add("Content-Type", "application/json");
            head.Add("Access-Control-Allow-Origin", "*");

            currentRequest = new RequestHelper
            {
                Uri = basePathWin,
                Headers = head,
                Body = new PlayerRequestWin
                {
                    time = time
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
        public void PostLoss(float time, int causeOfDeath)
        {
            Dictionary<string, string> head = new Dictionary<string, string>();
            head.Add("Content-Type", "application/json");
            head.Add("Access-Control-Allow-Origin", "*");

            currentRequest = new RequestHelper
            {
                Uri = basePathLoss,
                Headers = head,
                Body = new PlayerRequestLoss
                {
                    time = time,
                    causeOfDeath = causeOfDeath,
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
                    bulletsFired = bulletsFired,
                    bulletHit = bulletHit,
                    isGettingSmall = isGettingSmall
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
        public void Post1()
        {
            Dictionary<string, string> head = new Dictionary<string, string>();
            head.Add("Content-Type", "application/json");
            head.Add("Access-Control-Allow-Origin", "*");

            currentRequest = new RequestHelper
            {
                Uri = basePath1,
                Headers = head,
                Body = new PlayerRequest1
                {
                    level = "level2"
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
    public class PlayerRequest1
    {
        public string level;
        
    }
    [Serializable]
    public class PlayerRequestWin
    {

        public float time;


        public override string ToString()
        {
            return UnityEngine.JsonUtility.ToJson(this, true);
        }
    }
    [Serializable]
    public class PlayerRequestLoss
    {

        public float time;

        public int causeOfDeath;

       

        public override string ToString()
        {
            return UnityEngine.JsonUtility.ToJson(this, true);
        }
    }

}
