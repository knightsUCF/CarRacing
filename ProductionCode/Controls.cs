using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Controls2 : MonoBehaviour
{

    // S E T T I N G S ///////////////////////////////////////////////////////

    public float speed = 0.0f;
    public float maxSpeed = 50.0f;
    public float acceleration = 30.0f;
    public float deceleration = 50.0f;


    // G L O B A L S /////////////////////////////////////////////////////////

    Vector3 pos;



    // M O V E M E N T //////////////////////////////////////////////////////


    void Move()
    {
        if ((Input.GetKey(KeyCode.S)) && (speed < maxSpeed)) speed = speed - acceleration * Time.deltaTime;
        else if ((Input.GetKey(KeyCode.W)) && (speed > -maxSpeed)) speed = speed + acceleration * Time.deltaTime;
    }


    void SetMaxAndMinSpeed()
    {
        if (speed > maxSpeed) speed = maxSpeed;
        if (speed <= 0) speed = 0;
    }


    void Turn()
    {

    }


    void Break()
    {
        if (Input.GetKey(KeyCode.LeftShift)) speed = speed - deceleration * Time.deltaTime;
    }


    void SetPos()
    {
        pos = transform.position;
        pos.z = transform.position.z + maxSpeed * Time.deltaTime;
        transform.position = pos;
    }

    
    // M A I N  M O V E M E N T  C O N T R O L L E R ////////////////////////

    void RunPCMovementRoutines()
    {
        Move();
        SetMaxAndMinSpeed();
        Turn();
        Break();
        SetPos(); // applies all the speed variables to actually move the car transform position
    }


    
    // C L O S E ////////////////////////////////////////////////////////////

    void CheckForClose()
    {
        if (Input.GetKey("escape")) Application.Quit();
    }



    // M A I N //////////////////////////////////////////////////////////////

    void Update()
    {
        RunPCMovementRoutines();
        CheckForClose();
    }



    /////////////////////////////////////////////////////////////////////////

}
