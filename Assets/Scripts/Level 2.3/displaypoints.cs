using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Level2_3
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
            warningmessage.text =warning;
            Invoke("clearText", 3.0f);
        }
        // public void displaybutton(int tries)
        // {
        //     buttonmessage.text = "You need " + tries.ToString() + " points to press the button to decrease the size of the door";
        // }
        private void clearText()
        {
            warningmessage.text = "";
        }
        
        public void displaytimeofdeath(float health_percent)
        {
            health_percent=Mathf.Round(health_percent*100);
            timeofdeath.text = "Health: "+health_percent.ToString()+" %";
        }
    }

}
