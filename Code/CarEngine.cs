using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CarEngine : MonoBehaviour
{

    public enum State
    {
        Driving,
        Turning
    }

    public State state;

    int startSpeedRandomRange = 4;
    int endSpeedRandomRange = 10;

    Vector3 fwd;
    Vector3 rayPos;


    float rayAngle = 0.0f;
    RaycastHit hit;
    Vector3 angle; // not to be confused with rayAngle, fix later
    int layerMask;


    float speed = 10.0f;

    


    private void Start()
    {
        // speed = RandomizeSpeed(startSpeedRandomRange, endSpeedRandomRange);
    }



    public void SetSpeed(float Speed)
    {
        speed = Speed;
    }


    private void Update()
    {
        MoveForward();

        // TestRay();

        TestRay2();


    }



    private void MoveForward()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }



    private int RandomizeSpeed(int start, int end)
    {
        return Random.Range(start, end);
    }


    void TestRay()
    {
        layerMask = 1 << 8; // here is some code to avoid layers, is the method overriden? can we just not pass the layer, Bit shift the index of the layer (8) to get a bit mask
        layerMask = ~layerMask; // This would cast rays only against colliders in layer 8, But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.

        angle = Quaternion.AngleAxis(rayAngle, Vector3.right) * Vector3.forward;

        rayPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        Debug.DrawRay(rayPos, transform.TransformDirection(angle) * hit.distance, Color.yellow);

        if (Physics.Raycast(rayPos, transform.TransformDirection(angle), out hit, Mathf.Infinity, layerMask)) // Does the ray intersect any objects excluding the player layer
        {
            Debug.DrawRay(rayPos, transform.TransformDirection(angle) * hit.distance, Color.yellow);
            
            // logs
            
            // Debug.Log("Did Hit");
            // Debug.Log(hit.collider.gameObject.name);
            // Debug.Log(hit.collider.tag);


            if (hit.collider.gameObject.tag == "Player") //  && !currentlyTurning)
            {
                Debug.Log("Detected the player");
            }
        }

            /*
            Vector3 forward = transform.TransformDirection(Vector3.forward) * 20;

            Debug.DrawRay(transform.position, forward, Color.green);

            if (Physics.Raycast(transform.position, forward, 10))
            {
                Debug.Log("BANG!");
                // forward.collider.tag;
                // print("There is something in front of the object!");
            }
            */

    }


    void TestRay2()
    {
        DrawRay();
    }


    void DrawRay()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(transform.position, forward, Color.green);

        //if (Physics.Raycast(forward, hit, 100))
        // {
        // ask in Discord
        // }
    }
            

}



/*
 * 
 * void CanDrive()
    {
        layerMask = 1 << 8; // here is some code to avoid layers, is the method overriden? can we just not pass the layer, Bit shift the index of the layer (8) to get a bit mask
        layerMask = ~layerMask; // This would cast rays only against colliders in layer 8, But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        
        angle = Quaternion.AngleAxis(rayAngle, Vector3.right) * Vector3.forward;

        rayPos = new Vector3(transform.position.x, yPos, transform.position.z);
        // rayPos = transform.position;

        // transform.position.y = yPos;
        
        // replaced transform.position with rayPos

        if (Physics.Raycast(rayPos, transform.TransformDirection(angle), out hit, Mathf.Infinity, layerMask)) // Does the ray intersect any objects excluding the player layer
        
        {
            // also replaced transform.position with rayPos
            Debug.DrawRay(rayPos, transform.TransformDirection(angle) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
            print(hit.collider.gameObject.name);
            print(hit.collider.tag);
            
            if (hit.collider.gameObject.name == "Road") //  && !currentlyTurning)
            {
                DriveForward();
            }
            
            // not sure if this is the right place...
            if (hit.collider.gameObject.name != "Road" && carState == CarState.driving)
            {
                Turn180Degrees();
            }
                        

        }
        else
        {
            // also replaced transform.position with rayPos
            Debug.DrawRay(rayPos, transform.TransformDirection(angle) * 1000, Color.white);
            Debug.Log("Did not Hit");

        }
        // rotates the car... 
        // transform.Rotate(0, rotSpeed, 0); // transform.position += Vector3.up * speed;
    }



}
*/

