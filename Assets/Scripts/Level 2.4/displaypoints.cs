using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Level2_4
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
        
        public void displaytimeofdeath(float time)
        {
            time=Mathf.Round(time);
            timeofdeath.text = "Time of Death:"+time.ToString()+" seconds";
        }
    }

}
