using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Garbage : MonoBehaviour
{


    public float LifeTime = 10f;


    bool playerOnTile;





    void Start()
    {
        Invoke("DestroyObject", LifeTime);
    }



    void DestroyObject()
    {
        if (Game.Instance.GameState != GameState.Dead)
        {
            if (!playerOnTile) 
            {
                Debug.Log("Destroying game object");
                Destroy(gameObject);
            }
        }
    }

    

    // detect whether the player is in the collider so we don't destroy the tile the player is resting on

    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == Constants.PlayerTag)
        {
            playerOnTile = true;
        }
    }



    private void OnTriggerExit(Collider c)
    {
        if (c.gameObject.tag == Constants.PlayerTag)
        {
            playerOnTile = false;
        }

    }



}
