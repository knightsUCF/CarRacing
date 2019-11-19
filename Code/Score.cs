using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class Score : MonoBehaviour
{

    public Text score;


    public void Set(int amount)
    {
        score.text = amount.ToString();
    }
}
