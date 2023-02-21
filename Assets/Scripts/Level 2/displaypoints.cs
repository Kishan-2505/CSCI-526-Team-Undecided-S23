using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Level2
{
    public class displaypoints : MonoBehaviour
    {
        // Start is called before the first frame update
        public TMP_Text currentPoints;
        public TMP_Text warningmessage;
        public TMP_Text buttonmessage;
        public void display(int score)
        {
            currentPoints.text = "Score:" + score.ToString();
        }
        public void displaywarning(string warning)
        {
            warningmessage.text =warning;
        }
        public void displaybutton(int tries)
        {
            buttonmessage.text = "You need " + tries.ToString() + " points to press the button to decrease the size of the door";
        }
    }

}
