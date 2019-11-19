using System.Collections;
using System.Collections.Generic;
using UnityEngine;



class State : MonoBehaviour
{

    public enum Game
    {
        New,
        Playing,
        Over
    }




    public Game game;



    // move these over into a struct at Data.cs

    public int carCount = 0;
    public int chunkCount = 1; // start at 1 because there is a starting chunk in the scene
    public int score = 0;

}

