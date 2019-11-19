using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Game : MonoBehaviour
{

    public GameObject player;
    public GameObject chunk;


    public Vector3 playerResetPos = new Vector3(0.0f, 0.0f, 0.0f);
    public Vector3 chunkStartPos = new Vector3(0.0f, 0.0f, 0.0f);


    GameObject[] cars;
    GameObject[] loot;
    GameObject[] chunks;


    State state;
    Score score;


    private void Start()
    {
        state = FindObjectOfType<State>();
        score = FindObjectOfType<Score>();
    }


    private void Update()
    {
        if (state.game == State.Game.Over)
        {
            state.game = State.Game.New;
            NewGame();
        }
    }



    void NewGame()
    {
        // deinitialize

        ClearScore();
        ClearCars();
        ClearLoot();
        ClearChunks();

        // initialize

        CreateStartingChunk();
        ResetPlayerPosition(); 
    }



    void ResetPlayerPosition()
    {
        player.transform.position = playerResetPos;
    }


    void ClearScore()
    {
        state.score = 0;
        score.Set(0);
    }


    void ClearCars()
    {
        cars = GameObject.FindGameObjectsWithTag("Car");
        foreach (GameObject car in cars) Destroy(car);
    }


    void ClearLoot()
    {
        loot = GameObject.FindGameObjectsWithTag("Loot");
        foreach (GameObject l in loot) Destroy(l);
    }


    void ClearChunks()
    {
        chunks = GameObject.FindGameObjectsWithTag("Chunk");
        foreach (GameObject chunk in chunks) Destroy(chunk);
    }

    

    void CreateStartingChunk()
    {
        Instantiate(chunk, chunkStartPos, Quaternion.Euler(new Vector3(0, 0, 0)));
    }

}
