using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 *
 * We need something that checks whether the rotation got out of whack with the camera.
 *
 * Then rotate the car with a sine lerp slowly back to the center position.
 *
 * 
 */




public class Controls : MonoBehaviour
{

    // acceleration

	// https://answers.unity.com/questions/29751/gradually-moving-an-object-up-to-speed-rather-then.html

	public float speed = 0.0f;
	 
	public float acceleration = 30.0f;
    public float mobileAcceleration = 100.0f;

	public float deceleration = 10.0f;
	public float breakDeceleration = 50.0f;

    public float breakMobileDeceleration = 100.0f;

    // turning

	public float pcTurnSpeed = 10.0f;
    public float mobileTurnSpeed = 50.0f;

	public float turnLeftBoundary = 0.0f;
	public float turnRightBoundary = 0.0f;

    public float maxSpeed = 50.0f; // was 10 before with maxCruiseSpeed being 20
    private float maxCruiseSpeed = 50.0f;// should be zero


	Vector3 pos;


    // mobile touch controls

    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position

    private float dragDistance;  //minimum distance for a swipe to be registered


    // we split off the velocity setting mechanism from Move() to make the code clear
    // Move() just allows us to set this speed with the user interface



    void SetPos()
    {


        // transform.position.x = transform.position.x + speed * Time.deltaTime;
        pos = transform.position;
        // pos.x = transform.position.x + speed * Time.deltaTime;

        // we're taking out cruise for now...
        // pos.x = transform.position.x + maxCruiseSpeed * Time.deltaTime;

        // really bad way of adding a max speed:
        pos.x = transform.position.x + maxSpeed * Time.deltaTime;

        transform.position = pos;
    }


    void SetMaxSpeed()
    {
        if (speed > maxCruiseSpeed) maxCruiseSpeed = speed;
        if (speed > maxSpeed) speed = maxSpeed;
    }



    void Move()
	{
		if ((Input.GetKey(KeyCode.S)) && (speed < maxSpeed)) speed = speed - acceleration * Time.deltaTime;
		else if ((Input.GetKey(KeyCode.W)) && (speed > -maxSpeed)) speed = speed + acceleration * Time.deltaTime;


        // the else deceleration loop is interfering with staying at cruise speed

		else
		{
			// if (speed > deceleration * Time.deltaTime) speed = speed - deceleration * Time.deltaTime;
            // else if (speed < -deceleration * Time.deltaTime) speed = speed + deceleration * Time.deltaTime;
            // else speed = 0;
		}

        // set the max speed as the minimum cruise speed, but also allow to reset with the break

        // Debug.Log("speed: " + speed);
        // Debug.Log("maxCruiseSpeed: " + maxCruiseSpeed);
        

	}


    void TurnLeft(float turnSpeed)
    {
        pos = transform.position;
        pos.z = transform.position.z + turnSpeed * Time.deltaTime;
        transform.position = pos;
    }


    void TurnRight(float turnSpeed)
    {
        pos = transform.position;
        pos.z = transform.position.z - turnSpeed * Time.deltaTime;
        transform.position = pos;
    }



    void Turn()
	{
		if (Input.GetKey(KeyCode.A))
		{
            TurnLeft(pcTurnSpeed);
		}

        if (Input.GetKey(KeyCode.D))
		{
            TurnRight(pcTurnSpeed);
		}
	}


    void Break()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = speed - breakDeceleration * Time.deltaTime;
        }

        if (speed <= 0) speed = 0; // so we don't go backwards with the break

        pos = transform.position;
        pos.x = transform.position.x + speed * Time.deltaTime;
        transform.position = pos;
    }


    void MobileBreak()
    {
        // we don't need this if statement if we are going to turn this in the mobile swipe  up / down condition
        // this one should go into swipe down

        // if (Input.GetKey(KeyCode.LeftShift))
        
        speed = speed - breakMobileDeceleration * Time.deltaTime;
        

        /* This stuff is already taken care of in regular Break() in the Update method
         
        if (speed <= 0) speed = 0; // so we don't go backwards with the break

        pos = transform.position;
        pos.x = transform.position.x + speed * Time.deltaTime;
        transform.position = pos;
        */
    }



	void RunMovementRoutines()
	{
		Move(); // changes the speed

        SetMaxSpeed();

		Turn();

        Break();

        SetPos(); // applies all the speed variables to actually move the car transform position

    }


    // UI ////////////////////////////////////////////////////////////

    void CheckForClose()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }


    //////////////////////////////////////////////////////////////////



    // TOUCH CONTROLS ////////////////////////////////////////////////


    void ProcessTouches()
    {
        if (Input.touchCount == 1) // user is touching the screen with a single touch
        {
            Touch touch = Input.GetTouch(0); // get the touch
            if (touch.phase == TouchPhase.Began) //check for the first touch
            {
                fp = touch.position;
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
            {
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                lp = touch.position;  //last touch position. Ommitted if you use list

                //Check if drag distance is greater than 20% of the screen height
                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                {//It's a drag
                 //check if the drag is vertical or horizontal
                    if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                    {   //If the horizontal movement is greater than the vertical movement...
                        if ((lp.x > fp.x))  //If the movement was to the right)
                        {   
                            // Right swipe

                            Debug.Log("Right Swipe");
                            // hudLog.UpdateText("Right Swipe");
                            
                            TurnRight(mobileTurnSpeed);

                        }
                        else
                        {   
                            // Left swipe

                            Debug.Log("Left Swipe");
                            // hudLog.UpdateText("Left Swipe");

                            TurnLeft(mobileTurnSpeed);
                        }
                    }

                    else
                    {   //the vertical movement is greater than the horizontal movement
                        if (lp.y > fp.y && (speed < maxSpeed))  //If the movement was up
                        {   
                            // Up swipe - speed up

                            Debug.Log("Up Swipe");
                            // hudLog.UpdateText("Up Swipe");
                            
                            speed = speed + mobileAcceleration * Time.deltaTime;
                        }

                        else
                        {   //Down swipe
                            Debug.Log("Down Swipe");
                            // hudLog.UpdateText("Down Swipe");
                            MobileBreak();
                        }
                    }
                }
                else
                {   //It's a tap as the drag distance is less than 20% of the screen height
                    Debug.Log("Tap");
                    // hudLog.UpdateText("Tap");
                }
            }
        }
    }



    //////////////////////////////////////////////////////////////////


    void Start()
    {
        // mobile

        dragDistance = Screen.height * 15 / 100; //dragDistance is 15% height of the screen
    }



    void Update()
	{
        // PC

		RunMovementRoutines();

        CheckForClose();


        // mobile 

        ProcessTouches();



        UI.Instance.SetStatus(speed.ToString());

	}

}




    /*
	public float speed = 1.0f;


	// acceleration: https://answers.unity.com/questions/769441/how-do-i-make-a-gameobject-accelerate.html

	public float velocity = 0.0f;
	public float maxVelocity = 1.0f;
	public float acceleration = 0.0f;
	public float accelerationSpeed = 0.1f;
	public float maxAcceleration = 1.0f;
	public float minAcceleration = -1.0f;




    void Update()
    {
		RunMovementRoutines();
    }



    void Move()
	{
		var move = new Vector3(Input.GetAxis("Vertical"), 0, 0);
		transform.position += move * velocity * Time.deltaTime;
	}



    void SetAcceleration()
	{
        if (Input.GetKey(KeyCode.W)) acceleration += accelerationSpeed;
		if (Input.GetKey(KeyCode.S)) acceleration -= accelerationSpeed;
		if (acceleration > maxAcceleration) acceleration = maxAcceleration;
		if (acceleration < minAcceleration) acceleration = minAcceleration;

		velocity += acceleration;

		if (velocity > maxVelocity) velocity = maxVelocity;
		if (velocity < -maxVelocity) velocity = -maxVelocity;
	}



    void RunMovementRoutines()
	{
		SetAcceleration();
		Move();
	}

    
}
*/
