using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// attach to Game


public class Clear : MonoBehaviour
{

    State state;
    Score score;


    GameObject[] cars;
    GameObject[] loot;
    GameObject[] chunks;




    private void Start()
    {
        state = FindObjectOfType<State>();
        score = FindObjectOfType<Score>();
    }

    


    public void Score()
    {
        state.score = 0;
        score.Set(0);
    }


    public void Cars()
    {
        cars = GameObject.FindGameObjectsWithTag("Car");
        foreach (GameObject car in cars) Destroy(car);
    }


    public void Loot()
    {
        loot = GameObject.FindGameObjectsWithTag("Loot");
        foreach (GameObject l in loot) Destroy(l);
    }


    public void Chunks()
    {
        chunks = GameObject.FindGameObjectsWithTag("Chunk");
        foreach (GameObject chunk in chunks) Destroy(chunk);
    }

}
