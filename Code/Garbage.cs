using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Garbage : MonoBehaviour
{

    // place on Chunk parent game object


    public float LifeTime = 10f;


    bool playerOnTile;

    int totalNumberOfCars;

    GameObject[] cars;







    void Start()
    {
        
        // DEBUG: Total cars count /////////////////////////////////


        cars = GameObject.FindGameObjectsWithTag("Car");

        int count = 0;

        foreach (GameObject car in cars)
        {
            count += 1;

           //Destroy(car);
        }

        Debug.Log("Total car count in scene: " + count);

        ////////////////////////////////////////////////////////////

        
        Invoke("DestroyObject", LifeTime);
    }



    void DestroyObject()
    {

        Destroy(gameObject);

        /*
        if (!playerOnTile)
        {
            Debug.Log("Destroying game object");
            Destroy(gameObject);
        }
        */

        /*
        if (Game.Instance.GameState != GameState.Dead)
        {
            if (!playerOnTile)
            {
                Debug.Log("Destroying game object");
                Destroy(gameObject);
            }
        }
        */
    }



    // detect whether the player is in the collider so we don't destroy the tile the player is resting on

    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            playerOnTile = true;
        }
    }



    private void OnTriggerExit(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            playerOnTile = false;
        }

    }





}
