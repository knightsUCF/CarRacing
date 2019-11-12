// https://github.com/dgkanatsios/InfiniteRunner3D/blob/master/Assets/Scripts/PathSpawnCollider.cs


using UnityEngine;
using System.Collections;

/*

Attach to chunk object, this will talk to the collider that is attached.
When the player enters the collider this spawns another object.

*/



public class Procedural : MonoBehaviour
{



    public GameObject chunk; // randomized chunks: public GameObject[] chunks

    public float offset = 250.0f;

    Vector3 pos;

    Vector3 rotation; // we don't really need this, but our tiles are rotated 90, and we might have to rewrite Controls.cs because the car goes the other way



    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == Constants.PlayerTag)
        {
            Debug.Log("Entered collider area");

            pos = transform.position;
            pos.x = transform.position.x + offset;
        

            Instantiate(chunk, pos, Quaternion.Euler(new Vector3(0, 90, 0)));
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
