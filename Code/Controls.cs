using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class Controls : MonoBehaviour
{

    // acceleration

	// https://answers.unity.com/questions/29751/gradually-moving-an-object-up-to-speed-rather-then.html

	public float speed = 0.0f;
	public float maxSpeed = 10.0f; 
	public float acceleration = 30.0f;
	public float deceleration = 10.0f;
	public float breakDeceleration = 50.0f;

    // turning

	public float turnSpeed = 10.0f;
	public float turnLeftBoundary = 0.0f;
	public float turnRightBoundary = 0.0f;


	Vector3 pos;





    void Move()
	{
		if ((Input.GetKey(KeyCode.S)) && (speed < maxSpeed)) speed = speed - acceleration * Time.deltaTime;
		else if ((Input.GetKey(KeyCode.W)) && (speed > -maxSpeed)) speed = speed + acceleration * Time.deltaTime;

		else
		{
			if (speed > deceleration * Time.deltaTime) speed = speed - deceleration * Time.deltaTime;
            else if (speed < -deceleration * Time.deltaTime) speed = speed + deceleration * Time.deltaTime;
            else speed = 0;
		}

		// transform.position.x = transform.position.x + speed * Time.deltaTime;
		pos = transform.position;
		pos.x = transform.position.x + speed * Time.deltaTime;
		transform.position = pos;

	}



    void Turn()
	{
		if (Input.GetKey(KeyCode.A))
		{
			pos = transform.position;
			pos.z = transform.position.z + turnSpeed * Time.deltaTime;
			transform.position = pos;
		}

        if (Input.GetKey(KeyCode.D))
		{
			pos = transform.position;
			pos.z = transform.position.z - turnSpeed * Time.deltaTime;
			transform.position = pos;
		}
	}



	void RunMovementRoutines()
	{
		Move();
		Turn();
	}


	void Update()
	{
		RunMovementRoutines();
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
