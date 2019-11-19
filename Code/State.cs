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


    public enum Player
    {
        Test1,
        Test2
    }


    public Game game;

    public Player player;


    public int carCount = 0;

    public int chunkCount = 1; // start at 1 because there is a starting chunk in the scene


    public int score = 0;




}

