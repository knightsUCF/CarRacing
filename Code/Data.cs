using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Data : MonoBehaviour
{


    public struct Counts
    {
        public int carCount;
        public int chunkCount;
        public int score;
    }

    
    Counts counts;




    void Start()
    {
        // initialize data values

        counts.carCount = 0;
        counts.chunkCount = 1; // start at 1 because there is a starting chunk in the scene
        counts.score = 0;

    }
    

}
