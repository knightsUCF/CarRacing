// https://github.com/dgkanatsios/InfiniteRunner3D/blob/master/Assets/Scripts/PathSpawnCollider.cs


using UnityEngine;
using System.Collections;

/*

Attach to chunk object, this will talk to the collider that is attached.
When the player enters the collider this spawns another object.

Also make sure that the player car object has an is trigger check and a rigid body, and is tagged as "Player" (not the parent game object, but the object at the hierarchy level of the rigid body and collider)


*/



public class Procedural : MonoBehaviour
{



    public GameObject chunk; // randomized chunks: public GameObject[] chunks

    public float offset = 250.0f;

    Vector3 pos;

    Vector3 rotation; // we don't really need this, but our tiles are rotated 90, and we might have to rewrite Controls.cs because the car goes the other way

    State state;


    private void Start()
    {
        state = FindObjectOfType<State>(); 
    }


    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {

            // Debug.Log("Detected player");

            pos = transform.position;
            // pos.x = transform.position.x + offset; // spawns to the right of us
            pos.z = transform.position.z + offset; // spawns in front


            // Instantiate(chunk, pos, Quaternion.Euler(new Vector3(0, 90, 0)));

            Instantiate(chunk, pos, Quaternion.Euler(new Vector3(0, 0, 0)));

            // state.chunkCount += 1;
        }
    }


}







/*
public GameObject chunk; // later for randomized chunks: public GameObject[] chunks; // and we will need a method to pick random element out of them, well in this case we can implement the for loop code


void OnTriggerEnter(Collider hit)
{

    // player entered chunk collider

    if (hit.gameObject.tag == Constants.PlayerTag) // set this to our player
    {

        // okay first test this, to check how many times we get "entered collider" output to the log, because we don't want to spawn too much stuff and crash

        // find whether the next path will be straight, left or right

        // int randomSpawnPoint = Random.Range(0, PathSpawnPoints.Length);



        for (int i = 0; i < PathSpawnPoints.Length; i++)
        {
            //instantiate the path, on the set rotation
            if (i == randomSpawnPoint)
                Instantiate(Path, PathSpawnPoints[i].position, PathSpawnPoints[i].rotation);
            else
            {
                //instantiate the border, but rotate it 90 degrees first
                Vector3 rotation = PathSpawnPoints[i].rotation.eulerAngles;
                rotation.y += 90;
                Vector3 position = PathSpawnPoints[i].position;
                position.y += positionY;
                Instantiate(DangerousBorder, position, Quaternion.Euler(rotation));
            }
        }

    }

}
*/
