using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Controls : MonoBehaviour
{

    public float speed = 1.0f;

    float turnSpeed = 4.0f;


    Vector3 pos;

    float acceleration = 30.0f;
    float maxSpeed = 50.0f;




    bool speedUp = false;
    bool slowDown = false;
    bool turnLeft = false;
    bool turnRight = false;


    private void Update()
    {
        // ProcessManualInput();

        // SelfDrive();

        // SetPos();

        CapLowerBoundSpeed();
        CapUpperBoundSpeed();


        MoveForward();

        if (speedUp) Accelerate();
        if (slowDown) Break();
        if (turnLeft) TurnLeft();
        if (turnRight) TurnRight();
    }


    void ProcessManualInput()
    {
        if (Input.GetKey(KeyCode.W)) // mobile controls here, maybe an ifdef
        {
            MoveForward();
        }

        if (Input.GetKey(KeyCode.A))
        {
            MoveLeft();
        }

        if (Input.GetKey(KeyCode.D))
        {
            MoveRight();
        }

        if (Input.GetKey(KeyCode.S))
        {
            MoveBack();
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }


    private void MoveForward()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    private void MoveLeft()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed);
    }

    private void MoveRight()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed);
    }

    private void MoveBack()
    {
        transform.Translate(Vector3.back * Time.deltaTime * speed);
    }


    private void SelfDrive()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }


    void SetPos()
    {
        pos = transform.position;
        pos.z = transform.position.z  * Time.deltaTime;
        transform.position = pos;
    }


    public void OnButtonClick()
    {
        // Debug.Log("Click");

        // speed = speed + acceleration * Time.deltaTime; // breaking: speed = speed - acceleration * Time.deltaTime;
       //  MoveForward();
    }


    public void OnMouseDown()
    {
        // Debug.Log("Pressed mouse");
        speedUp = true;
    }


    public void OnMouseRelease()
    {
        // Debug.Log("Released mouse");
        speedUp = false;
    }


    public void OnBreakPedalDown()
    {
        slowDown = true;
    }


    public void OnBreakPedalRelease()
    {
        slowDown = false;
    }


    void Accelerate()
    {
        speed = speed + acceleration * Time.deltaTime;
    }


    void Break()
    {
        if (speed <= 0.1) return;
        speed = speed - acceleration * Time.deltaTime;
    }


    public void OnLeftDown()
    {
        turnLeft = true;
    }


    public void OnLeftRelease()
    {
        turnLeft = false;
    }

    void TurnLeft()
    {
        transform.Translate(Vector3.left * Time.deltaTime * turnSpeed);
    }


    public void OnRightDown()
    {
        turnRight = true;
    }

    public void OnRightRelease()
    {
        turnRight = false;
    }


    void TurnRight()
    {
        transform.Translate(Vector3.right * Time.deltaTime * turnSpeed);
    }



    void CapLowerBoundSpeed()
    {
        if (speed <= 0) speed = 0;
    }

    void CapUpperBoundSpeed()
    {
        if (speed >= maxSpeed) speed = maxSpeed;
    }

}
