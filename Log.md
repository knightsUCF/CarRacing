# Car Movement - Turning

Here is adding turning to our PlayerControls.cs:

using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class PlayerControls : MonoBehaviour
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

# Car Movement - Acceleration

So we found this script which works well with acceleration.

We can probably unpack the if / else stuff to just if statements to make the code readable. Or even put into state machine if we need more clarity for fine tuning this code later.

For now a faster acceleration works well with a slower deceleration, since we can just tap the movement key and start up the car. Now this poses a small problem. We will not be able to use the same variable for deceleration as our breaks. Because if we increase the deceleration variable to a higher threshold to simulate breaks, then that same deceleration prevents us from staying in motion as well when we accelerate. So we will need a separate variable for "breakDeceleration", and then use that in a separate metho to multiply by speed, breakDeceleration and Time.delta time. So another line that says:

if (breakPressed) speed = speed + breakDeceleration * Time.deltaTime;

So that works well. 

The other thing we want to implement now is moving side to side. At slower speeds we might need a custom routine which turns the wheels, or makes the car turn more in a circular fashion, but at high speeds just moving side to side is what we want.

After we implement turning side to side, we want to implement colliders.

Now at high speeds turning side to side will probably a constant value. But then at slower speeds we will detect the speed of the car, and if below the speed threshold we will need to apply a different turning mechanism, since we cannot turn side to side while remaining in a fixed position.


    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;





    public class PlayerControls : MonoBehaviour
    {

      // https://answers.unity.com/questions/29751/gradually-moving-an-object-up-to-speed-rather-then.html

      public float speed = 0.0f;
      public float maxSpeed = 10.0f; 
      public float acceleration = 30.0f;
      public float deceleration = 10.0f;

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


      void RunMovementRoutines()
      {
        Move();
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

# Infinite Runner

So this will become a looming big thing to figure out. For now we want to just get the basic car movement controls. Perhaps we can just stack a number of the same levels in a line to drive over, and then we will need to take a look at all sorts of infinite runner tutorials to pick the best and easiest workflow.


# Variety in Gameplay

So one of the ways we want to accomplish variety in the gameplay is to change the environments the player is going through. Kind of like Crossy Road changes the types of barriers we have to cross.

We want to come up with all sorts of "easy wins" in variety, where we don't have to do too much extra work.

The "Voxel World Pack" does just that. We have three environments we can use. Grass, snow, and desert. How we arrange them into ascending orders of level difficulty remains to be seen.

# "Least Risk" Elements

We want to start with the least risk elements, so that we don't have to redo any work. We will just go down the below list to go through these.

I. World

The skybox obviously cannot be swaped out easily so we can choose whatever we like for the skybox. Same goes for the trees, and the road. So basically the whole world design we can go ahead and implement without worrying about increasing scope or getting stuck in a corner. Trees will be a good idea to have in the beginning, since they allow us to guage speed.

II. Player

We can pick any car object for the player, since those can be easily swapped out. 

Here is where our real work begins with setting up the player controls. These will be relativelys simple. We will want to use WASD, or the arrow keys to set up movement. Left and right will be moving side to side on the highway, and up and down will be used for acceleration and decceleration. Perhaps the down button could be used to break. And perhaps on the phone screen we could have two pedals on the left side for controls, where one pedal is gas, and the other is break. And then on the left side we have a steering wheel we can drag left or right. This might feel more natural if we have the steering wheel on the right, and the pedals on the left. In either case we want the controls to feel good, and starting with the arrows will set up a good based to test on mobile.

Then at some part of the process we will need to start testing early on mobile, so we will have to rewrite the controls, maybe use the vertical and horizontal axis. But let's keep things simple until then, because we should be able to translate the controls easy.

Another thing we will want to do for the player right away is detect collisions. So we will use the unity physics engine, and we will give the player a few seconds to witness the physics collisions, but then we will reset the game to the beginning (with the state machine found in Data.cs)

III. UI

So we will want a score view, so we can keep track of points. Again this is something that can easily be customized later, so we can rest easy implementing the score view, without fearing we will be redoing our work later.

We also want some simple stuff to orient the player like, "Start Game", "Game Over", some simple state stuff to orient us.

IV. Gameplay

This will be the meat of the project. We will want to start by having a lane of random traffic moving in front of us. We can begin to start playing around with this prototype. Next would be to establish the traffic coming on down the left side.

And after that we want to establish physics collisions. We will need to play around with the mass physics properties of the rigid body, since this can only be set through experimentation.

Once the player colliders with a car, restart the game, to begin to give the player a sense of gameplay. Also save the high score so the player can test their high score against others.

Also add some screeshake on collision from the Moenen pack.

We also want to add some basic sounds here.

This actually is all we need to do for now. Once this becomes part of a solid prototype, we will want to also introduce some sort of powerups coming down the street for us to catch.

V. Code

Of course we want to keep code as simple as possible. Beginning right away the game as a state machine in Data.cs will make our lives much easier.


# Basic Starting Game Objects


I. World

So we will want a street view with the camera pointing up ahead. We will also like to have the voxel clouds found in the VoxelPlay app. We also want trees on the side to hover by, as a way of decoration but also a way of guaging speed.

II. Player

The player is a car. They can move the car left to right, and also accelerate and deccelerate.

III. UI

The UI will be designed for a touch screen, so we will need some keyboard shortcuts while we are testing on desktop. For the touch screen interface we will need a way to move left to right and also accelerate and deccelerate.

For the keyboard we will want to be able to:

Move the car side to side - left and right arrows

Accelerate / Deccelerate - up and down arrow

We will want smooth continuous movement so we will be using GetKey instead of GetKeyDown, (GetKeyDown just gets us the first press of the player continuously holding the key down.)

IV. Gameplay

We will take elements from rogue like gameplay and also Crossy Road.

We will have random cars in front of us, and random cars on the left lane. We have the choice to use the left lane to pass the cars, but as we do so we risk colliding with oncoming traffic. Once we collide with oncoming traffic we lose the game, like in Crossy Road and start over. Focus on making an enjoyable simple core loop, where the player can die and goes back to the beginning, but still enjoys replaying the game.

V. Code

We want to keep our code in a data oriented design, so we will be using the code from our platformer, to keep all of our data in a centralized file, Data.cs


Are there any other elements we're missing? There is of course monetization, and advertising, but we will address those later. For now we will build out the stuff that is least likely to get disrupted by any future changes to not waste any time or work.


# Gameplay

So the gameplay is an "infinite runner" type game. We should investigate assets and also tutorials to check how infinite runners are done, so we don't reinvent the wheel and save us some time.

We will essentially move the transform of the car in two directions:

1) Moving

2) Accelerating


Perhaps we should even design much of the game on paper so the process goes smoothly. Although we are very tempted just to jump in and go with the John Romero method of no prototypes. Either this really is not a complex project.

So then we will have two lanes of traffic. The traffic comes and goes in randomized intervals. Perhaps every car that you skip the player gets point, (which they are used to wanting to do in real life.)

So perhaps our first game loop will be skipping cars, and getting a point for every car that we pass.

Also if we go to fast then we are stopped, and perhaps we can get away if we drive fast enough.

The other thing on the highway will be bonus points we can simply move over to unlock. These will be randomized in a roguelike fashion, so sometimes this will be a health power up, or a bonus points power, whatever.





# 11 / 12 / 2019 - Getting Started


Okay, so we want to make a "hyper casual" mobile game. For us hyper casual really means something we can finish and ship quickly. And there seem to be no major blockers in getting this done.

At first we will ship on Android, and then iOS. So that will just cost us $25 to ship and get our product out there.

We are not really expecting to make too much money off this game, but we do want to monetize effectively like Crossy Road. We should follow the same sort of monetization Crossy Road does, since the gameplay is very similar.We believe we can make the gameplay as intriguing as Crossy Road. We do want however a "hipster" voxel art style, so we can add any sort of elements we need.

This is a great game idea, because this is something that we can actually implement easily. We will also need a "Score" up on top, to give the player a goal, and perhaps levels also. Again study Crossy Road because they have done this effectively.

Also, we want to keep our code and project as organized as possible so we have a smooth experience. There really should be no hassle in getting this done in under a week.

There is also that car asset pack we can use, to have more fun looking voxels.

