using UnityEngine;
using System.Collections;



/*
https://github.com/dgkanatsios/InfiniteRunner3D/blob/master/Assets/Scripts/PathSpawnCollider.cs

Attach to chunk object, this will talk to the collider that is attached.
When the player enters the collider this spawns another object.

Also make sure that the player car object has an is trigger check and a rigid body, and is tagged as "Player" (not the parent game object, but the object at the hierarchy level of the rigid body and collider)


*/



public class Procedural : MonoBehaviour
{

    // S E T T I N G S ///////////////////////////////////////////////////////

    public float offset = 250.0f;


    // R E F E R E N C E S ///////////////////////////////////////////////////

    public GameObject chunk;
    ChunksHolder chunksHolder;


    // G L O B A L S /////////////////////////////////////////////////////////

    Vector3 pos;



    // S T A R T /////////////////////////////////////////////////////////////

    private void Start()
    {
        chunksHolder = FindObjectOfType<ChunksHolder>();
    }


    // T R I G G E R  E N T E R //////////////////////////////////////////////

    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            pos = transform.position;
            pos.z = transform.position.z + offset;
            InstantiateRandomChunkLand(chunksHolder.grassLandChunks);
        }
    }


    // M A K E  R A N D O M  C H U N K /////////////////////////////////////////


    void InstantiateRandomChunkLand(GameObject[] chunkLands)
    {
        Instantiate(chunkLands[Random.Range(0, chunkLands.Length)], pos, Quaternion.Euler(new Vector3(0, 0, 0)));
    }


}

