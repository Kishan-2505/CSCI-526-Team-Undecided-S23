using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Level1
{
    public class displaypoints : MonoBehaviour
    {
        // Start is called before the first frame update
        public TMP_Text currentPoints;
        public void display(int score)
        {
            currentPoints.text = "Score:" + score.ToString();
        }
    }

}
