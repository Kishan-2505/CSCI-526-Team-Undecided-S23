using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Level2_2
{
    public class displaypoints : MonoBehaviour
    {
        // Start is called before the first frame update
        public TMP_Text currentPoints;
        public TMP_Text warningmessage;
        public TMP_Text buttonmessage;
        public TMP_Text timeofdeath;
        public void display(int score)
        {
            currentPoints.text = "Score:" + score.ToString();
        }
        public void displaywarning(string warning)
        {
            warningmessage.text = warning;
            Invoke("clearText", 3.0f);
        }
        private void clearText()
        {
            warningmessage.text = "";
        }

        public void displaytimeofdeath(float health_percent)
        {
            health_percent = Mathf.Round(health_percent * 100);
            timeofdeath.text = "Health: " + health_percent.ToString() + " %";
        }
    }

}
