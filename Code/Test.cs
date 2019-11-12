using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    
    void Start()
    {
        CheckUIMessagingSystem();
    }


    void CheckUIMessagingSystem()
    {
        UI.Instance.SetStatus(Constants.StatusTest);
    }

    
}
