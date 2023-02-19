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
            Debug.Log("entered display button");
            buttonmessage.text = "You need " + tries.ToString() + " to unlock the door";
        }
    }

}
