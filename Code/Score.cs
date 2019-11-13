using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Score : MonoBehaviour
{

    float reward;


    // for some reason on trigger enter works better than on exit

    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "AI")
        {
            reward = UI.Instance.GetScore() + 50.0f;
            UI.Instance.SetScore(reward);
        }
    }
}
