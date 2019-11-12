using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * This script spawns a game object at the spot this script is placed on.
 *
 * So we want to place this script on the "Spawn Spot" prefab, which is just a marker cube.
 *
 * But by placing on the cube we can spawn any game object there easily.
 *
 * 
 */



public class Spawn : MonoBehaviour
{

    public GameObject GO;

    Vector3 pos;

    // spawn at spot

    public void AtSpot()
    {
        pos = transform.position;
        Instantiate(GO, pos, Quaternion.identity);
    }


    void Start()
    {
        AtSpot();
    }
}
