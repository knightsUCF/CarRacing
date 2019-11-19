using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Game : MonoBehaviour
{

    public GameObject player;
    public GameObject chunk;


    public Vector3 playerResetPos = new Vector3(0.0f, 0.0f, 0.0f);
    public Vector3 chunkStartPos = new Vector3(0.0f, 0.0f, 0.0f);



    State state;
    Clear clear;



    private void Start()
    {
        state = FindObjectOfType<State>();
        clear = FindObjectOfType<Clear>();
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

        clear.Score();
        clear.Cars();
        clear.Loot();
        clear.Chunks();

        // initialize

        CreateStartingChunk();
        ResetPlayerPosition(); 
    }




    void ResetPlayerPosition()
    {
        player.transform.position = playerResetPos;
    }



    void CreateStartingChunk()
    {
        Instantiate(chunk, chunkStartPos, Quaternion.Euler(new Vector3(0, 0, 0)));
    }

}
