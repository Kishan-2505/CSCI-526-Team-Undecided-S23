using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Proyecto26;
using System;
namespace LevelSelector
{

    public class LevelSelector : MonoBehaviour
    {
        private readonly string basePath = "https://rich-teal-crayfish-coat.cyclic.app/mainmenu";
        private RequestHelper currentRequest;
        public string levelName;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        public void LoadLevel()
        {
            string level_backend=levelName;
            if(levelName=="Level 2.1"){
                level_backend="level1";
            }else if(levelName=="Level 2.2"){
                level_backend="level2";
            }else if(levelName=="Level 2.3"){
                level_backend="level3";
            }else if(levelName=="Level 2.4"){
                level_backend="level4";
            }else if(levelName=="Level 5"){
                level_backend="level5";
            }
            Post(level_backend);
            SceneManager.LoadScene(levelName);

        }

        public void Post(string level_name)
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
                    level = level_name
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
        public string level;
        
    }
    
}

