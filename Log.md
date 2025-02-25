# Organizing Todos

So there is actually not that much in the todo list. We should just be organized and do thigns in orders.

So come up with the priority order for the 3 different aspects.

1) Gameplay

- getting different cars spawning in both directions smooth on the entire board, including the different random traffic patterns

- add smooth game over state, including rolling car collisions

- add loot and possibly gas meter

- set slower turn speed on slower speeds

- cars stopping (decelerating in front of the train)

- garbage collection

2) Environments

- randomization of mini chunks, we have a different array set of chunks for grassland, versus desert

- for now let's just have two environments, well we could also do snow, since that is relatively easy, 3 environments feels like a "full" game

- just get the environments going, they don't have to be perfect


3) MONETIZATION & POLISH

- intro screen, smooth game over screen

- car store, saving cars to iOS and Android profiles

- sounds

- video ads

- camera spring follow


Alright that is enough for now. If we complete the above simple items, then we will almost have a game ready. This will take us a long way to completion...

Once we finish the above todo list, then reorganize the priorities into the next todo list. We should be able to finish this within a week...

# What's Next?

Well, we can split the work into three phases:

1) Main traffic gameplay

2) Environments

3) Monetization & Polish


Of course there is more, but this is enough to take out a big chunk of the work.

1) MAIN GAMEPLAY

- random pickups (variable reward), perhaps gas meter

- smooth game over state

- different cars, including the other asset packs

- cars stopping by in front of the train

- possibly lights on the cars (this will go into the polish section, but nightmode would add a lot)

- rolling over on collision

- different traffic patterns

- set slower turn speed on slower speed

2) ENVIRONMENTS

- at least have two or three environments, including desert

- perhaps birds

- randomization of "mini chunks" on the big chunks


3) MONETIZATION & POLISH

- smooth game over state

- intro screens

- car store

- saving unlocked cars to both iOS and Android profiles

- perhaps offer faster car speeds, or for now just keep things simple

- two main ways of monetization, ads and $.99 unlockable cars like Crossy Road, study Crossy Road GDC

- camera spring follow

- sounds

- video ads



# Adding Cars

To add a car:

1. Create a parent prefab, and call this "Car Name Left Lane"

2. Add car engine script to the parent prefab, and set to left (when duplicating car at the end of the process set to right

3. Drag car model under the parent prefab

4. Add rigid body with no gravity and is kinematic (will take off is kinematic when a collision occurs)

5. Add box collider no is trigger to the child car object

6. Set the rotation on the left lane cars to y -180 on the child car object

7. Set y to 1 on child car object

8. After done duplicate object, and change y to 0 on the new child car, and also set the engine to the right lane on the parent engine object 

# Beginning of Refactoring the Controls Code


Using the "///" from Tricks of the Game Programming Gurus is nice, because we are making the code modular, and much easier to glance over at a later time when we have forgotten the implementation details.



            using System.Collections;
            using System.Collections.Generic;
            using UnityEngine;




            public class Controls2 : MonoBehaviour
            {

                // S E T T I N G S ///////////////////////////////////////////////////////

                public float speed = 0.0f;
                public float maxSpeed = 50.0f;
                public float acceleration = 30.0f;


                // G L O B A L S /////////////////////////////////////////////////////////

                Vector3 pos;


                // M O V E M E N T //////////////////////////////////////////////////////

                void Move()
                {
                    if ((Input.GetKey(KeyCode.S)) && (speed < maxSpeed)) speed = speed - acceleration * Time.deltaTime;
                    else if ((Input.GetKey(KeyCode.W)) && (speed > -maxSpeed)) speed = speed + acceleration * Time.deltaTime;
                }

                void SetMaxSpeed()
                {

                }

                void Turn()
                {

                }

                void Break()
                {

                }

                void SetPos()
                {
                    pos = transform.position;
                    pos.x = transform.position.x + maxSpeed * Time.deltaTime;
                    transform.position = pos;
                }


                // M A I N  M O V E M E N T  C O N T R O L L E R ////////////////////////

                void RunPCMovementRoutines()
                {
                    Move();
                    SetMaxSpeed();
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



# Refactoring Controls Code

We are going to refactor the controls code. Here is the code before we do any refactoring:

            using System.Collections;
            using System.Collections.Generic;
            using UnityEngine;



            /*


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


                // Steering


                [SerializeField] private float turningSpeed = 40f;
                [SerializeField] private int limitTurningAngle = 25;

                private bool touchDisable = false;
                private Vector3 movingDirection = Vector3.forward;

                public float MovingSpeed { private set; get; }

                float yAngle;

                //





                // steering angles

                void SetAngles()
                {
                    float yAngle = transform.eulerAngles.y;
                    yAngle = (yAngle > 180) ? yAngle - 360 : yAngle;
                }



                private void Update()
                {
                    // ProcessManualInput();

                    // SelfDrive();

                    // SetPos();

                    SetAngles();
                    SetSteering();
                    SetSteeringSpeed();

                    CapX(); // rails

                    CapLowerBoundSpeed();
                    CapUpperBoundSpeed();


                    MoveForward();

                    if (speedUp) Accelerate();
                    if (slowDown) Break();
                    if (turnLeft) TurnLeft();
                    if (turnRight) TurnRight();
                }



                float leftXRail = -13;
                float rightXRail = -3.5f;

                void CapX()
                {
                    if (transform.position.x < leftXRail)
                    {
                        pos = transform.position;
                        pos.x = leftXRail;
                        transform.position = pos;
                    }

                    if (transform.position.x > rightXRail)
                    {
                        pos = transform.position;
                        pos.x = rightXRail;
                        transform.position = pos;
                    }
                }


                void SteerLeft()
                {
                    // steer left 

                    // #TODO, need to check for walls before turning


                    //Rotating left
                    if (yAngle > -limitTurningAngle)
                    {
                        transform.eulerAngles -= new Vector3(0, turningSpeed * Time.deltaTime, 0);
                    }
                    else
                    {
                        transform.eulerAngles = new Vector3(0, -limitTurningAngle, 0);
                    }

                    movingDirection = transform.TransformDirection(Vector3.forward);

                }


                void SteerRight()
                {

                    //Rotating right
                    if (yAngle < limitTurningAngle)
                    {
                        transform.eulerAngles += new Vector3(0, turningSpeed * Time.deltaTime, 0);
                    }
                    else
                    {
                        transform.eulerAngles = new Vector3(0, limitTurningAngle, 0);
                    }

                    movingDirection = transform.TransformDirection(Vector3.forward);

                }


                void SetSteering()
                {
                    transform.position += movingDirection * MovingSpeed * Time.deltaTime;
                }



                void SetSteeringSpeed()
                {
                    // increase steering at higher speeds if necessary

                    // Debug.Log("speed: " + speed);
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
                    // SteerLeft();
                    transform.Translate(Vector3.left * Time.deltaTime * speed);
                }

                private void MoveRight()
                {
                    // SteerRight();
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
                    pos.z = transform.position.z * Time.deltaTime;
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




                public void OnRightDown()
                {
                    turnRight = true;
                }

                public void OnRightRelease()
                {
                    turnRight = false;
                }


                void TurnLeft()
                {
                    SteerLeft();
                    transform.Translate(Vector3.left * Time.deltaTime * turnSpeed);
                }


                void TurnRight()
                {
                    SteerRight();
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




                // Fix rotation of player when colliding with left wall


                private IEnumerator FixingNegativeRotation()
                {
                    float yAngle = transform.eulerAngles.y;
                    yAngle = (yAngle > 180) ? yAngle - 360 : yAngle;

                    while (yAngle < 0)
                    {
                        transform.eulerAngles += new Vector3(0, turningSpeed * Time.deltaTime, 0);
                        yAngle = transform.eulerAngles.y;
                        yAngle = (yAngle > 180) ? yAngle - 360 : yAngle;
                        yield return null;
                    }

                    transform.eulerAngles = Vector3.zero;
                    touchDisable = false;
                }



                // Fix rotation of player when colliding with right wall


                private IEnumerator FixingPositiveRotation()
                {
                    float yAngle = transform.eulerAngles.y;
                    yAngle = (yAngle > 180) ? yAngle - 360 : yAngle;

                    while (yAngle > 0)
                    {
                        transform.eulerAngles -= new Vector3(0, turningSpeed * Time.deltaTime, 0);
                        yAngle = transform.eulerAngles.y;
                        yAngle = (yAngle > 180) ? yAngle - 360 : yAngle;
                        yield return null;
                    }

                    transform.eulerAngles = Vector3.zero;
                    touchDisable = false;
                }




            }

                */

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



                // M O B I L E   S E T T I N G S ///////////////////////////////////////////////////////////////////


                // touch settings (so we can detect a continuous press down)

                bool speedUp = false;
                bool slowDown = false;
                bool turnLeft = false;
                bool turnRight = false;



                public void OnRightDown()
                {
                    turnRight = true;
                }

                public void OnRightRelease()
                {
                    turnRight = false;
                }


                public void OnLeftDown()
                {
                    turnLeft = true;
                }


                public void OnLeftRelease()
                {
                    turnLeft = false;
                }

                public void OnGasDown()
                {
                    // Debug.Log("Pressed mouse");
                    speedUp = true;
                }


                public void OnGasRelease()
                {
                    // Debug.Log("Released mouse");
                    speedUp = false;
                }

                public void OnBreakDown()
                {
                    slowDown = true;
                }


                public void OnBreakRelease()
                {
                    slowDown = false;
                }

                void AccelerateMobile()
                {
                    speed = speed + acceleration * Time.deltaTime;
                }


                void BreakMobile()
                {
                    if (speed <= 0.1) return;
                    speed = speed - acceleration * Time.deltaTime;
                }


                /////////////////////////////////////////////////////////////////////////////





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


                    // This stuff is already taken care of in regular Break() in the Update method

                    // if (speed <= 0) speed = 0; // so we don't go backwards with the break

                    // pos = transform.position;
                    // pos.x = transform.position.x + speed * Time.deltaTime;
                    // transform.position = pos;

                }



                void RunPCMovementRoutines()
                {
                    Move(); // changes the speed

                    SetMaxSpeed();

                    Turn();

                    Break();

                    SetPos(); // applies all the speed variables to actually move the car transform position

                }

                void RunMobileMovementRoutines()
                {
                    if (speedUp) AccelerateMobile();
                    if (slowDown) BreakMobile();
                    if (turnLeft) TurnLeft(pcTurnSpeed); // pc turn speed also works for mobile
                    if (turnRight) TurnRight(pcTurnSpeed);

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

                    RunPCMovementRoutines();

                    RunMobileMovementRoutines();



                    CheckForClose();


                    // mobile 

                    ProcessTouches();



                    // UI.Instance.SetStatus(speed.ToString());

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



# New Refactored Procedural Code

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

        public GameObject chunk;
        public float offset = 250.0f;
        Vector3 pos;
        ChunksHolder chunksHolder;


        private void Start()
        {
            chunksHolder = FindObjectOfType<ChunksHolder>();
        }


        private void OnTriggerEnter(Collider c)
        {
            if (c.gameObject.tag == "Player")
            {
                pos = transform.position;
                pos.z = transform.position.z + offset;
                InstantiateRandomChunkLand(chunksHolder.grassLandChunks);
            }
        }


        void InstantiateRandomChunkLand(GameObject[] chunkLands)
        {
            Instantiate(chunkLands[Random.Range(0, chunkLands.Length)], pos, Quaternion.Euler(new Vector3(0, 0, 0)));
        }


    }


# Procedural Code from Both Projects

Here is the procedural code from the first project:

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


And here is the procedural code from the second project:


    public class Procedural : MonoBehaviour
    {



        public GameObject chunk; // randomized chunks: public GameObject[] chunks

        // public GameObject[] chunks; moved to ChunksHolder



        public float offset = 250.0f;

        Vector3 pos;

        Vector3 rotation; // we don't really need this, but our tiles are rotated 90, and we might have to rewrite Controls.cs because the car goes the other way

        State state;
        ChunksHolder chunksHolder;


        private void Start()
        {
            state = FindObjectOfType<State>();
            chunksHolder = FindObjectOfType<ChunksHolder>();
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

                // Instantiate(chunk, pos, Quaternion.Euler(new Vector3(0, 0, 0)));

                InstantiateRandomChunkLand(chunksHolder.grassLandChunks);

                // state.chunkCount += 1;
            }
        }



        void InstantiateRandomChunkLand(GameObject[] chunkLands)
        {
            Instantiate(chunkLands[Random.Range(0, chunkLands.Length)], pos, Quaternion.Euler(new Vector3(0, 0, 0)));
        }
    }


So how do both of these differ?

So we are going to go with our second version of the procedural code, since we have accomodated for the different chunklands in the second one. Also we will simply uncheck the old procedural code, and call this procedural code, ProceduralProduction.cs.




# Let's Wrap up the Octopus

So let's wrap up this octopus and release this game. We just purchased the below cars asset pack, so that should elevate our style. 

If our gameplay for now is just straight ahead driving, that is fine. Just like Crossy Road, there is not much there, just going straight ahead, and then an obscene amount of ads. 

As for turning, let's just cut that out and use the camera spring follow mode, along with some continuous speed to simply go ahead with turning for now.

So our todo list from before was:

- camera

- traffic, plus variety of traffic patterns, different colored cars

- environments, neon lights

- loot, possibly gas meter, variable reward, speed boost, invicibility, etc

- smooth game over state

- physics collisions

- intro with our own logos

- sounds

- unlockable cars

- monetization



Well, so first, let's open up the new asset pack, inspect the models. Then we should build the game as fast as possible. Just functioning enough to get that out there.

So what are we going to be working on today?

Well, let's consolidate our two projects, that makes the most sense. So let's take a look at the differences in the procedural code, and then transfer in over the street view.


using UnityEngine;
using System.Collections;

/*

Attach to chunk object, this will talk to the collider that is attached.
When the player enters the collider this spawns another object.

Also make sure that the player car object has an is trigger check and a rigid body, and is tagged as "Player" (not the parent game object, but the object at the hierarchy level of the rigid body and collider)


*/





# Cute Cars Asset Pack

https://assetstore.unity.com/packages/3d/twenty-one-voxel-vehicles-67841




# ToDo Order - No Prototypes! Just make the game and ship!


After we get the below chunk code working

- camera

- traffic, plus variety of traffic patterns, different colored cars

- environments, neon lights

- loot, possibly gas meter, variable reward, speed boost, invicibility, etc

- smooth game over state

- physics collisions

- intro with our own logos

- sounds

- unlockable cars

- monetization




# New Chunk Code to Fill Up the Horizon with More Chunks

New chunk code to fill up the horizon with more chunks, and generate a chunk five or so chunks down, to always have a few chunks ahead of the player.


    using UnityEngine;
    using System.Collections;




    /*

    Attach to chunk object, this will talk to the collider that is attached.
    When the player enters the collider this spawns another object.

    Also make sure that the player car object has an is trigger check and a rigid body, and is tagged as "Player" (not the parent game object, but the object at the hierarchy level of the rigid body and collider)

    https://github.com/dgkanatsios/InfiniteRunner3D/blob/master/Assets/Scripts/PathSpawnCollider.cs

    */



    public class Procedural : MonoBehaviour
    {

        public GameObject chunk;

        Vector3 chunkPos;
        Vector3 newChunkPos;
        ChunksHolder chunksHolder;

        public float chunkLength = 250.0f; // this offset needs to be 5 or so x chunks down
        public int startingNumberOfChunks = 3;



        private void Start()
        {
            chunksHolder = FindObjectOfType<ChunksHolder>();

            GenerateStartingChunksStrip();
        }


        private void GenerateStartingChunksStrip()
        {
            chunkPos = transform.position;

            chunkPos.z = transform.position.z + chunkLength;
            InstantiateRandomChunkLand(chunksHolder.grassLandChunks, chunkPos);
            chunkPos.z = chunkPos.z + chunkLength;
            InstantiateRandomChunkLand(chunksHolder.grassLandChunks, chunkPos);
            chunkPos.z = chunkPos.z + chunkLength;
            InstantiateRandomChunkLand(chunksHolder.grassLandChunks, chunkPos);
        }


        private void OnTriggerEnter(Collider c)
        {
            if (c.gameObject.tag == "Player")
            {
                newChunkPos = transform.position;
                newChunkPos.z = transform.position.z + chunkLength * startingNumberOfChunks;

                InstantiateRandomChunkLand(chunksHolder.grassLandChunks, newChunkPos);
            }
        }



        void InstantiateRandomChunkLand(GameObject[] chunkLands, Vector3 pos)
        {
            Instantiate(chunkLands[Random.Range(0, chunkLands.Length)], pos, Quaternion.Euler(new Vector3(0, 0, 0)));
        }


    }





# Let's Start with Chunks and the Player

- let's increase our chunk size

- replace our new chunk code

- generate five or so chunks ahead

# Rebuilding Project

We will be rebuilding the project in the Rebuiling.md log file here.


# Rebuilding Project from Scratch in Order

So to go in order:

- instantiate chunks 5 or so ahead

- add environments (we can come up with a basic MVP environment)

- add traffic variety, plus loot

- add collisions

- smooth game over state

- add intro

- have car closer to player

- fix camera stutter

- unlocking cars

- sounds

- paid video ads

- fix shading on shadows

- possibly add gas meter

# Rebuilding Project

We should rebuild the project in a new scene. This should not be too difficult.

The bugs we are looking to fix:

- camera stutter (https://forum.unity.com/threads/follow-camera-stutter.114997/)

- moving car closer in front of view

- adding traffic variety and environment

- moving the turn controls over to the side

- adding the intro screen with our own logo

- adding sounds

- car unlocking screen

- rewarded video ads

- adding different car models

- fixing shading on shadows


# Follow Camera Stutter

https://forum.unity.com/threads/follow-camera-stutter.114997/


# Spring Camera

The current attempt at implementing the spring camera is completely off. We are not sure where to even start.

So I guess we have to start from scratch, from the very beginning.

We can do this by starting with a new scene, or by deactivating the current player, duplicating the player game object and starting from scratch.

Before we had the problem where on acceleration the camera was not catching up. This might have been because the camera focal point was focused on the parent game object of the car. 

In any case, we have to backtrack, and get the camera code working. The good news is that we did not experience any stuttering.



# Next Sequence of ToDos

- spring camera

- scene lighting

- environment (getting started)



# Final Controls Code

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


                // Steering


                [SerializeField] private float turningSpeed = 40f;
                [SerializeField] private int limitTurningAngle = 25;

                private bool touchDisable = false;
                private Vector3 movingDirection = Vector3.forward;

                public float MovingSpeed { private set; get; }

                float yAngle;

                //





                // steering angles

                void SetAngles()
                {
                    float yAngle = transform.eulerAngles.y;
                    yAngle = (yAngle > 180) ? yAngle - 360 : yAngle;
                }



                private void Update()
                {
                    // ProcessManualInput();

                    // SelfDrive();

                    // SetPos();

                    SetAngles();
                    SetSteering();
                    SetSteeringSpeed();

                    CapX(); // rails

                    CapLowerBoundSpeed();
                    CapUpperBoundSpeed();


                    MoveForward();

                    if (speedUp) Accelerate();
                    if (slowDown) Break();
                    if (turnLeft) TurnLeft();
                    if (turnRight) TurnRight();
                }



                float leftXRail = -13;
                float rightXRail = -3.5f;

                void CapX()
                {
                    if (transform.position.x < leftXRail)
                    {
                        pos = transform.position;
                        pos.x = leftXRail;
                        transform.position = pos;
                    }

                    if (transform.position.x > rightXRail)
                    {
                        pos = transform.position;
                        pos.x = rightXRail;
                        transform.position = pos;
                    }
                }


                void SteerLeft()
                {
                    // steer left 

                    // #TODO, need to check for walls before turning


                        //Rotating left
                        if (yAngle > -limitTurningAngle)
                        {
                            transform.eulerAngles -= new Vector3(0, turningSpeed * Time.deltaTime, 0);
                        }
                        else
                        {
                            transform.eulerAngles = new Vector3(0, -limitTurningAngle, 0);
                        }

                        movingDirection = transform.TransformDirection(Vector3.forward);

                }


                void SteerRight()
                {

                        //Rotating right
                        if (yAngle < limitTurningAngle)
                        {
                            transform.eulerAngles += new Vector3(0, turningSpeed * Time.deltaTime, 0);
                        }
                        else
                        {
                            transform.eulerAngles = new Vector3(0, limitTurningAngle, 0);
                        }

                        movingDirection = transform.TransformDirection(Vector3.forward);

                }


                void SetSteering()
                {
                    transform.position += movingDirection * MovingSpeed * Time.deltaTime;
                }



                void SetSteeringSpeed()
                {
                    // increase steering at higher speeds if necessary

                    // Debug.Log("speed: " + speed);
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
                    // SteerLeft();
                    transform.Translate(Vector3.left * Time.deltaTime * speed);
                }

                private void MoveRight()
                {
                    // SteerRight();
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




                public void OnRightDown()
                {
                    turnRight = true;
                }

                public void OnRightRelease()
                {
                    turnRight = false;
                }


                void TurnLeft()
                {
                    SteerLeft();
                    transform.Translate(Vector3.left * Time.deltaTime * turnSpeed);
                }


                void TurnRight()
                {
                    SteerRight();
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




                // Fix rotation of player when colliding with left wall


                private IEnumerator FixingNegativeRotation()
                {
                    float yAngle = transform.eulerAngles.y;
                    yAngle = (yAngle > 180) ? yAngle - 360 : yAngle;

                    while (yAngle < 0)
                    {
                        transform.eulerAngles += new Vector3(0, turningSpeed * Time.deltaTime, 0);
                        yAngle = transform.eulerAngles.y;
                        yAngle = (yAngle > 180) ? yAngle - 360 : yAngle;
                        yield return null;
                    }

                    transform.eulerAngles = Vector3.zero;
                    touchDisable = false;
                }



                // Fix rotation of player when colliding with right wall


                private IEnumerator FixingPositiveRotation()
                {
                    float yAngle = transform.eulerAngles.y;
                    yAngle = (yAngle > 180) ? yAngle - 360 : yAngle;

                    while (yAngle > 0)
                    {
                        transform.eulerAngles -= new Vector3(0, turningSpeed * Time.deltaTime, 0);
                        yAngle = transform.eulerAngles.y;
                        yAngle = (yAngle > 180) ? yAngle - 360 : yAngle;
                        yield return null;
                    }

                    transform.eulerAngles = Vector3.zero;
                    touchDisable = false;
                }




            }


# First Draft of Car Turning Done


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


                // Steering


                [SerializeField] private float turningSpeed = 40f;
                [SerializeField] private int limitTurningAngle = 25;

                private bool touchDisable = false;
                private Vector3 movingDirection = Vector3.forward;

                public float MovingSpeed { private set; get; }

                float yAngle;

                //





                // steering angles

                void SetAngles()
                {
                    float yAngle = transform.eulerAngles.y;
                    yAngle = (yAngle > 180) ? yAngle - 360 : yAngle;
                }



                private void Update()
                {
                    // ProcessManualInput();

                    // SelfDrive();

                    // SetPos();

                    SetAngles();
                    SetSteering();
                    SetSteeringSpeed();

                    CapX(); // rails

                    CapLowerBoundSpeed();
                    CapUpperBoundSpeed();


                    MoveForward();

                    if (speedUp) Accelerate();
                    if (slowDown) Break();
                    if (turnLeft) TurnLeft();
                    if (turnRight) TurnRight();
                }



                float leftXRail = -13;
                float rightXRail = -3.5f;

                void CapX()
                {
                    if (transform.position.x < leftXRail)
                    {
                        pos = transform.position;
                        pos.x = leftXRail;
                        transform.position = pos;
                    }

                    if (transform.position.x > rightXRail)
                    {
                        pos = transform.position;
                        pos.x = rightXRail;
                        transform.position = pos;
                    }
                }


                void SteerLeft()
                {
                    // steer left 

                    // #TODO, need to check for walls before turning


                        //Rotating left
                        if (yAngle > -limitTurningAngle)
                        {
                            transform.eulerAngles -= new Vector3(0, turningSpeed * Time.deltaTime, 0);
                        }
                        else
                        {
                            transform.eulerAngles = new Vector3(0, -limitTurningAngle, 0);
                        }

                        movingDirection = transform.TransformDirection(Vector3.forward);

                }


                void SteerRight()
                {

                        //Rotating right
                        if (yAngle < limitTurningAngle)
                        {
                            transform.eulerAngles += new Vector3(0, turningSpeed * Time.deltaTime, 0);
                        }
                        else
                        {
                            transform.eulerAngles = new Vector3(0, limitTurningAngle, 0);
                        }

                        movingDirection = transform.TransformDirection(Vector3.forward);

                }


                void SetSteering()
                {
                    transform.position += movingDirection * MovingSpeed * Time.deltaTime;
                }



                void SetSteeringSpeed()
                {
                    Debug.Log("speed: " + speed);
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
                    // SteerLeft();
                    transform.Translate(Vector3.left * Time.deltaTime * speed);
                }

                private void MoveRight()
                {
                    // SteerRight();
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




                public void OnRightDown()
                {
                    turnRight = true;
                }

                public void OnRightRelease()
                {
                    turnRight = false;
                }


                void TurnLeft()
                {
                    SteerLeft();
                    transform.Translate(Vector3.left * Time.deltaTime * turnSpeed);
                }


                void TurnRight()
                {
                    SteerRight();
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



# Code for Car Turning from the Asset Pack

            // Update is called once per frame
            void Update () {

                if (GameManager.Instance.GameState == GameState.Playing)
                {
                    if (Input.GetMouseButton(0) && !touchDisable)
                    {
                        //Create trails
                        if (leftTrail == null && rightTrail == null)
                        {
                            leftTrail = Instantiate(trailPrefab, leftTrailTrans.position, Quaternion.identity);
                            rightTrail = Instantiate(trailPrefab, rightTrailTrans.position, Quaternion.identity);
                            leftTrail.transform.SetParent(transform);
                            rightTrail.transform.SetParent(transform);
                        }         

                        float x = Camera.main.ScreenToViewportPoint(Input.mousePosition).x;

                        float yAngle = transform.eulerAngles.y;
                        yAngle = (yAngle > 180) ? yAngle - 360 : yAngle;

                        if (x <= 0.5f) //Touch left
                        {
                            hitRightWall = false;

                            if (!hitLeftWall)
                            {
                                //Rotating left
                                if (yAngle > -limitTurningAngle)
                                {
                                    transform.eulerAngles -= new Vector3(0, turningSpeed * Time.deltaTime, 0);
                                }
                                else
                                {
                                    transform.eulerAngles = new Vector3(0, -limitTurningAngle, 0);
                                }

                                movingDirection = transform.TransformDirection(Vector3.forward);
                            }                                
                        }
                        else //Touch right
                        {
                            hitLeftWall = false;

                            if (!hitRightWall)
                            {
                                //Rotating right
                                if (yAngle < limitTurningAngle)
                                {
                                    transform.eulerAngles += new Vector3(0, turningSpeed * Time.deltaTime, 0);
                                }
                                else
                                {
                                    transform.eulerAngles = new Vector3(0, limitTurningAngle, 0);
                                }

                                movingDirection = transform.TransformDirection(Vector3.forward);
                            }
                        }
                    }

                    transform.position += movingDirection * MovingSpeed * Time.deltaTime;

                    if (Input.GetMouseButtonUp(0) && leftTrail != null && rightTrail != null)
                    {
                        leftTrail.transform.SetParent(null);
                        rightTrail.transform.SetParent(null);
                        leftTrail = null;
                        rightTrail = null;
                    }
                }
                }



# Car Turning

https://answers.unity.com/questions/173817/how-to-rotate-a-car-smoothly.html


Rotate should be used only for continuous rotations, because it can accumulate errors and let your model tilted to some weird angle after some time. For tilting and other small angle changes, it's always better to set transform.localEulerAngles.
The function below use this property to turn the car like you said: turns smoothly to the angle during half the time, then turns back to zero in the other half. Additionally, it "banks" to the side automatically, like real cars do when changing lanes - if you don't need this, replace bank by zero when setting the localEulerAngles.
You may place this code in the car script or in any other object, because you must pass the car transform when calling the function. Since it is a coroutine, you just call it when starting changing lanes, and the routine does the rest automatically.
I included a test Update function that I used to fine tune the routine - attach this script to the car and use the keys A or D to change to the right or left lane.

private var turn: float = 0; private var changing = false;

function ChangeLane(car:Transform, angle: float, time: float){ var t: float; var bank: float; if (changing) return; changing = true; for (t = 0; t < 1;){ t += 2*Time.deltaTime/time; turn = Mathf.Lerp(turn, angle, t); bank = 0.5 turn; car.localEulerAngles = Vector3(0, turn, bank); yield; } for (t = 0; t < 1;){ t += 2*Time.deltaTime/time; turn = Mathf.Lerp(turn, 0, t); bank = 0.5 turn; car.localEulerAngles = Vector3(0, turn, bank); yield; } changing = false; }

// This is just for testing purposes - you must call ChangeLane in your script // when changing lanes. Attach this script to the car to test it.

function Update(){ if (Input.GetKeyDown("a")){ ChangeLane(transform, 5, 1); } if (Input.GetKeyDown("d")){ ChangeLane(transform, -5, 1); } }

Add comment ·  Hide 2 · Share

https://forum.unity.com/threads/car-handling.42043/

https://www.youtube.com/watch?v=jC7nVQOFPkA



# Bugs for Demo Release

- on game over first tile goes to black

- cars stop spawning after a certain point

# Button Bug Fix - Raycast Target Off

So we had to turn off "raycast target" for the image not to be part of the button. Because when we enlarged the image the borders became invisible and responsive to the button presses.


# Let's Get Going on a New Day

Okay, so let's get going on a new day of work. We had plans to do in app purchases, but since we will be demoing the game, we should reshift our priorities. 

So some things that will be necessary:  


- Levels

- Sounds?

- car turning

- smooth game over state

- environments

- neon lights and environments, car lights

- sounds

- attractive spinning loot, perhaps variable reward

- gas meter?




So out of all these, we will be working on 3 things, which are necessary in progressing the gameplay along further:

- car turning

- environments

- sounds (simply have at least some traffic sounds for now, maybe tire squeels when breaking abruptly)

- pixel art controls

- different randomized cars

- intro graphics


Then we can work on in app purchases. 


# Setting Up In App Purchases

https://docs.unity3d.com/Manual/UnityIAPSettingUp.html

# Unity Services

https://docs.unity3d.com/Manual/UnityServices.html

# Configuring for Google Play Store

https://docs.unity3d.com/Manual/UnityIAPGoogleConfiguration.html

# Configuring for iOS App Store



# Save System Finally Working!

SaveSystem.cs


        using UnityEngine;
        using System.Collections;
        using System.Collections.Generic;
        using System.Runtime.Serialization.Formatters.Binary;
        using System.IO;


        // https://gamedevelopment.tutsplus.com/tutorials/how-to-save-and-load-your-players-progress-in-unity--cms-20934



        public class SaveSystem : MonoBehaviour
        {

            // public static List<SaveData> savedGames = new List<SaveData>();

            public List<bool> unlockedCars = new List<bool>();



                //it's static so we can call it from anywhere
                public void Save()
                {
                        // SaveSystem.savedGames.Add(SaveData.current);

                        BinaryFormatter bf = new BinaryFormatter();
                        //Application.persistentDataPath is a string, so if you wanted you can put that into debug.log if you want to know where save games are located
                        FileStream file = File.Create(Application.persistentDataPath + "/savedGames.gd"); //you can call it anything you want

                        // bf.Serialize(file, SaveSystem.savedGames);

                        bf.Serialize(file, unlockedCars);

                        file.Close();

                        Debug.Log("File saved");
                }

                public void Load()
                {
                        if (File.Exists(Application.persistentDataPath + "/savedGames.gd"))
                        {
                                BinaryFormatter bf = new BinaryFormatter();
                                FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);

                            // SaveSystem.savedGames = (List<SaveData>)bf.Deserialize(file);

                            unlockedCars = (List<bool>)bf.Deserialize(file);

                            Debug.Log(unlockedCars);

                            foreach (bool item in unlockedCars)
                            {
                                Debug.Log(item);
                            }

                            file.Close();

                            Debug.Log("File loaded");
                        }
                }
        }



Test.cs



        using System.Collections;
        using System.Collections.Generic;
        using UnityEngine;




        public class Test : MonoBehaviour
        {

            SaveSystem saveSystem;

            bool car1 = false;


            void Start()
            {

                saveSystem = FindObjectOfType<SaveSystem>();

                // saveSystem.unlockedCars.Add(car1);

                // saveSystem.Save();

                saveSystem.Load();




                // SaveSystem.Save();
                // SaveSystem.Load();

                // Debug.Log("Car 1 unlocked: " + SaveData.current.car1Unlocked);
                // Debug.Log("Car 2 unlocked: " + SaveData.current.car2Unlocked);

            }

        }





# Code From Below Refactored



        using System.Collections;
        using System.Collections.Generic;
        using UnityEngine;






        public class SaveSystem : MonoBehaviour
        {

                private CarData CreateSaveGameObject()
                {
                        CarData carData = new CarData();
                        carData.car2Unlocked = car2Unlocked;
                        return carData;
                }


                public void Save()
                {
                        CarData carData = CreateSaveGameObject();
                        BinaryFormatter bf = new BinaryFormatter();
                        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
                        bf.Serialize(file, carData);
                        file.Close(); // gold = 0; // resets values for next game
                        Debug.Log("Game Saved");
                }



                public void Load()
                {
                        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
                        {
                                BinaryFormatter bf = new BinaryFormatter();
                                FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
                                CarData carData = (CarData)bf.Deserialize(file);
                                file.Close();
                                car2Unlocked = carData.car2Unlocked; // gold = save.gold;
                                Debug.Log("Game Loaded");
                        }
                        else Debug.Log("No game saved!");
                }


            public void Test()
                {

                }

        }




























# Yet Another Save System

Following this tutorial: https://www.raywenderlich.com/418-how-to-save-and-load-a-game-in-unity#toc-anchor-005

The refactored files are just two:

- Game.cs (contains the save methods)

- Save.cs


///////////////////////////////////////////////


Game.cs: 



        using System;
        using System.Collections;
        using System.Collections.Generic;
        using System.IO;
        using System.Runtime.Serialization.Formatters.Binary;
        using UnityEngine;
        using UnityEngine.UI;




        public class Game : MonoBehaviour
        {

            public int gold = 0;



            public void SaveGame()
            {
                // 1
                Save save = CreateSaveGameObject();

                // 2
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
                bf.Serialize(file, save);
                file.Close();

                // 3

                gold = 0;

                Debug.Log("Game Saved");
            }


            public void NewGame()
            {
                gold = 0;
            }


            private Save CreateSaveGameObject()
            {
                Save save = new Save();

                save.gold = gold;

                return save;
            }



            public void LoadGame()
            {
                // 1
                if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
                {

                    // 2
                    BinaryFormatter bf = new BinaryFormatter();
                    FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
                    Save save = (Save)bf.Deserialize(file);

                    file.Close();

                    // 4

                    gold = save.gold;


                    Debug.Log("Game Loaded");


                }
                else
                {
                    Debug.Log("No game saved!");
                }
            }



            public void SaveAsJSON()
            {
                Save save = CreateSaveGameObject();
                string json = JsonUtility.ToJson(save);

                Debug.Log("Saving as JSON: " + json);
            }
        }



Save.cs:



        using System.Collections;
        using System.Collections.Generic;
        using UnityEngine;



        [System.Serializable]
        public class Save
        {
            public int gold = 0;
        }



# Trying New Save System

https://assetstore.unity.com/detail/tools/integration/quick-save-107676




# Setting Up the Encrypted Save System

- download the asset here: https://assetstore.unity.com/detail/tools/input-management/encrypted-save-154946

- find the example scene folder, drag into hierarchy

- rename "Managers" to Save System. Contents: GameMaster.cs, SaveMaster.cs

- the buttons are linked as follows: GameMaster.SaveGame, GameMaster.LoadGame

- then each of the cubes gets a "TransformSave.cs" with the field, unique ID, (each object should have unique ID) and save order. Save order can be 0.

- that's all!


So let's try saving the state of our unlockable cars.


# Encrypted Save System

So we are going to test this new encrypted save system. Not sure how much encryption will help, (probably will also need obfuscation), but let's give this a try.

YouTube tutorial: https://www.youtube.com/watch?v=H7WuyoXlNZE&feature=youtu.be

Asset "Encrypted Save": https://assetstore.unity.com/detail/tools/input-management/encrypted-save-154946




# Testing Save System

Okay, let's test the save system.

We will need some way for the user to interact with the data. 

Let's just create a button, which increments the gold number. Then when we exit the application, let's initialize the saved state from the previous session.

Actually we will not be able to test because we don't have the android stuff set up on this computer. We can try but we probably can't.


# Further Save State Tutorials

Okay, we will want to continue the tutorials, since they are by the same author and pick up right where we left of.

Tutorial 6: https://www.youtube.com/watch?v=FRKYVhxzrRw

Now the next tutorial is about the shop, so that might fit in nicely with our progression here: 

Tutorial 7 - Shop: https://www.youtube.com/watch?v=fRnSErPTMPM

We are also interested in tutorials 14, and 15, since they are about save states, and tutorial 26, which is about the phone build.

# Encryption

So the below tutorial takes care of everything in terms of saving state, but the author does mention that we do need a way to encrypt the data, which is not required. Let's take a look at the next tutorial and check if he addresses encryption, or if there are any extra parts required for saving. 

If there is encryption we can save that for later.

Should we split data off from savestate?

Well data will automatically reset on each game, and savestate is actually what we are saving. So let's just split for now to keep things simple. 

Let's also write a program, which can save the state on a phone.

# Save System Tutorial Code Files -- All We Need to Save State

So there are three files we need for the save tutorial: https://www.youtube.com/watch?v=LBs6qOgCDOY

Create a game object "Save Manager", and add SaveManager.cs


///////////////////////////////////////////////////////////////


SaveState.cs

SerializationHelper.cs (calls this just Helper.cs)

SaveManager.cs





////////////////////////////////////////////




SaveState.cs

    public class SaveState
    {
        public int gold = 123;
    }


SerializationHelper.cs


    using System.IO;
    using System.Xml.Serialization;



    public static class SerializationHelper
    {

        public static string Serialize<T>(this T toSerialize)
        {
            XmlSerializer xml = new XmlSerializer(typeof(T));
            StringWriter writer = new StringWriter();
            xml.Serialize(writer, toSerialize);
            return writer.ToString();
        }


        public static T Deserialize<T>(this string toDeserialize)
        {
            XmlSerializer xml = new XmlSerializer(typeof(T));
            StringReader reader = new StringReader(toDeserialize);
            return (T)xml.Deserialize(reader);
        }

    }


SaveManager.cs

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    // https://www.youtube.com/watch?v=LBs6qOgCDOY


    public class SaveManager : MonoBehaviour
    {
        public static SaveManager Instance { set; get; }

        public SaveState state;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            Load();

            Debug.Log(SerializationHelper.Serialize<SaveState>(state));
        }

        // save the SaveState script to PlayerPref

        public void Save()
        {
            PlayerPrefs.SetString("save", SerializationHelper.Serialize<SaveState>(state)); // turning state into a string
        }

        // load the previous saved state from player preferences

        public void Load()
        {
           if (PlayerPrefs.HasKey("save"))
           {
                state = SerializationHelper.Deserialize<SaveState>(PlayerPrefs.GetString("save"));
           }

            // if never saved the game

            else
            {
                state = new SaveState();
                Save();
                Debug.Log("No save file found. Creating a new one.");
            }
        }
    }
 


# Save System 


Start with this tutorial:

https://www.youtube.com/watch?v=LBs6qOgCDOY

Write everything to a data class like we had before.

So we are going to use a data class like this:

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;




    public class Data : MonoBehaviour
    {

        public struct Car
        {
            public int ID;
            public bool acquired;
            public int cost;
            public int topSpeed;
            public int acceleration;
            public int deceleration;
            public int turnSpeed;
        }


        Car car1;
        Car car2;
        Car car3;


    }




# Work at the Cafe

Okay, we've uploaded the project into Google Drive, so we can recreate, but we should build out the pieces seperately and modularly to keep focused. 

The things we are building

- save system (for car character data)

- unlockable car screen

- monetization

- car turning

- smooth game over state

- environments

- neon lights and environments, car lights

- sounds

Reasses 


# Saving Data

This tutorial shows how to save player data for both Android and iOS: https://www.youtube.com/watch?v=LBs6qOgCDOY

So through this and create a save system, that we can easily test on Android.

Once this is done, if working at the cafe, then write the whole car inventory screen from scratch, and plug into the project later.


# Daily Rewards

Page 8 of the Crashy Racing template.

"This tempalte has ab uitl in daily reward systme in which the user will be rewarded with coins every predefined interval of time. This is an effective way to increase user engagement and retention for your game. You can configure this feature from the DailyRewardController object in the hierarchy.

That's all the manual says for this. Look for "DailyRewardController.cs" in the project folder.

# Adding Characters

Adding characters is very easy in the Crashy Racing asset (asset #2). 

Add a Character.cs script to the character. Then drag the prefab into the character manager list so the character will be available in the game. 

Perhaps easier would be for us just to design the whole system ourselves, and then just use the necessary functions. This way we will be able to design things easier. But still use the physics methods for moving the characters around, and etc.




# Easy Mobile

Easy Mobile is our monetization system.

"Most	importantly,	this	template	is	pre-integrated	with	Easy	Mobile	plugin,	making	it	a	truly	fully-featured	game	that	is	release-ready.	Easy	Mobile	is	a	comprehensive,	cross-platform	package	that	provides	most	of	desired	features	of	mobile	games:	• Support	for	AdColony,	AdMob,	Chartboost,	Heyzap	and	UnityAds	• In-app	purchasing	• Support	for	Game	Center	(iOS)	and	Google	Play	Games	Services	(Android)	for	leaderboards	and	achievements	• Recording	gameplay	and	exporting	GIF	images	• Sharing	to	social	networks	(PNG	or	GIF	images)	• Push	notification	using	OneSignal	service	• Native	rating	request	popup	(rate	my	app)	*	Being	pre-integrated	means	this	template	is	already	configured	to	work	with	Easy	Mobile.	All	you	need	is	import	Easy	Mobile	and	do	a	few	setup	steps,	and	have	all	the	above	features	readily	implemented.	You	don’t	even	have	to	write	a	single	line	of	integration	code!	*	This	template	DOES	NOT	include	Easy	Mobile.	*	The	use	of	Easy	Mobile	is	totally	optional:	as	long	as	it’s	not	imported,	all	the	integration	code	will	automatically	be	excluded	from	compilation,	so	that	no	impact	will	be	made	on	the	game,	which	is	fully	functioning	on	its	own."	

file:///C:/Users/x/Documents/Crossy%20Road%20Template%202/Assets/_CrashyRacing/UserGuide_UAS.pdf

# Reward System

The reward system in the second asset pack is worth taking a look at. Where we get a package, and that package shakes to give us a gift. What is the purpose of this? Is this somehow tied with the rewarded video ads?

# Character Selection

Let's go through the scene and break down the character selection. But first let's take a look at the other asset pack, and check if they did things better.

Well, I guess lucky for us both asset packs are almost similar, so they were written by the same person.

# New Day

Well, we have a lot done. And today is the anniversary of the Crossy Road release. So let's see how much we can get done today.

How far are we away from finishing the game? Well, indeed there are a lot thing to polish, but if we prevent getting stuck in any time sinks we can actually have the game done in the next 24 - 48 hours.

Now, there might be some inconvience with running someone else's code, but as long as that prevents future code spaghetti, we should run with the implementation, so we can connect the ad services.

So today we are starting off with working on two main things:

- character selection (plus saving)

- monetization

Finishing character selection will naturally take us over to monetization.

After monetization let's do:

- car turning

- smooth game over state

Then what?

- enviroments

- neon lights and environments

- sounds

After all this we should be progressing very nicely. 

Then we are just going to do small polish things until release.

# Onto Character Selection

Start with the asset pack to determine how they are doing the dragging. And diagram out anything here.

So the character scrolling is done through "CharacterScroller"

Okay so we replicated all the code for CharacterScroller.cs. The required dependencies were:

SoundManager.cs

CoinManager.cs


CharacterScroller.cs

CharacterInfo.cs

CharacterManager.cs



So now we will try to build the Characters scene with physical objects, and hook up the scripts.

Of course we would like to build the scene from scratch, but perhaps this time we will just use the asset code base for speed of development.

# Lights

We've completely neglected lights. We will want to take a look at some lighting tutorials. Of course we probably will not use post effects on mobile, but we don't suffer any major perfomance losses by lighting the scene. #TODO




# What's Next?

Now that we've completed the intro sequence we will want to go ahead and implement the car unlockable screen, (transition to Characters scene.)

We will start by observing how this is done in the asset pack.

Also we will need to take a look at the other asset pack, to check how they implemented the coin purchasing mechanism, plus any sort of in app purchasing plugs they are using.

# Intro Sequence Complete

So the intro sequence is complete now. Of course we have to create the intro image sequences, and possibly some sounds. But the implementation is done. Save this for later. #TODO.

We will also need a way to trigger the car, and everything going once the actual game starts, and keep those objects inactive in the hierarchy till then. #TODO

# Intro Animations

We will want some intro animations (even if simple movements) and sounds to set the player. #TODO

We also of course need intro icons and graphics. #TODO.

# Code Solution to Intro Sequence

So here is the code solution to the intro sequence. We are basically using a domino like effect with the cascading coroutines. All the logo game objects are under the "Intro" parent game object, which contains the Intro.cs code. We then want to make sure just the Intro Logo is active, while the rest will get activated and deactivated, starting with the intro app logo getting deactivated. We also have a background image graphic of the same color next to each logo, which is a transparent PNG, which can be overlayed against the background. We could later fine tune the wait times. #TODO

The last one to show is the "overlapping logo", which is the game logo overlapped above the gameplay screen. We might get rid of this when the user starts clicking. Or get rid of this after a set delay. In Crossy Road, the overlapping logo only goes away after we click, and then they use an animation to move out of the way. So we decided to go away with waiting for a tap before turning off. So now we wait for the user to tap before turning off the overlappingLogo. Also important to note, we changed match width height, to 0.5 for 50 percent, since we were getting the sides cut off.


    using UnityEngine;
    using UnityEngine.SceneManagement;
    using System.Collections;




    public class Intro: MonoBehaviour
    {

        public GameObject appLogo;
        public GameObject companyLogo;
        public GameObject gameLogo;
        public GameObject overlappingLogo;


        // called zero
        void Awake()
        {
            Debug.Log("Awake");
        }

        // called first
        void OnEnable()
        {
            Debug.Log("OnEnable called");
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        // called second
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Debug.Log("OnSceneLoaded: " + scene.name);
            Debug.Log(mode);

            Debug.Log("Time to start the game!!!");

            // let's wait a little to show off the intro logo

            StartCoroutine("HideAppLogo");

        }


        private IEnumerator HideAppLogo()
        {
            float delay = 2.0f;
            yield return new WaitForSeconds(delay);
            appLogo.SetActive(false);
            companyLogo.SetActive(true);

            StartCoroutine("HideCompanyLogo");
        }


        private IEnumerator HideCompanyLogo()
        {
            float delay = 2.0f;
            yield return new WaitForSeconds(delay);
            companyLogo.SetActive(false);
            gameLogo.SetActive(true);

            StartCoroutine("HideGameLogo");
        }


        private IEnumerator HideGameLogo()
        {
            float delay = 3.0f;
            yield return new WaitForSeconds(delay);

            gameLogo.SetActive(false);
            overlappingLogo.SetActive(true);
        }


        bool introOver = false;


        void DetectTapToTurnOffOverlappingLogo()
        {
            foreach (Touch touch in Input.touches)
            {

                if (touch.fingerId == 0)
                {
                    // Finger 1 is touching! (remember, we count from 0)
                    overlappingLogo.SetActive(false);
                    introOver = true;
                }

                if (touch.fingerId == 1)
                {
                    // finger 2 is touching! Huzzah!
                    overlappingLogo.SetActive(false);
                    introOver = true;
                }
            }
        }




        // called third
        void Start()
        {
            Debug.Log("Start");
        }

        // called when the game is terminated
        void OnDisable()
        {
            Debug.Log("OnDisable");
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }


        void Update()
        {
            if (!introOver) DetectTapToTurnOffOverlappingLogo();
        }
    }

# Intro Sequence


- Screen 1: App logo (also thumbnail)

- Screen 2: Company logo (with sounds)

- Screen 3: Game logo (zooms in, we can use the cube voxel effect)

- Screen 4: Gameplay


So show two more of these screens, before we go to gameplay.

We can accomplish this all through methods in Intro.cs.



# Wait with the Logo

so after our scene data has been loaded, let's have a couroutine run, which will give us a few seconds of delay before we inactivate the logo.

So we renamed our HasSceneLoaded script to Intro. Since this will handle all the intro stuff for us. Then we set up a coroutine to handle the length of the intro logo:

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);

        Debug.Log("Time to start the game!!!");

        // let's wait a little to show off the intro logo

        StartCoroutine("HideLogo");

    }

    private IEnumerator HideLogo()
    {
        float delay = 2.0f;
        yield return new WaitForSeconds(delay);
        logo.SetActive(false);
    }

So that is working alright. Now let's get the rest of intro sequence going.

# Checking When Scene Loaded

The part that says, "Time to start the game!" is when we can start up the actual gameplay.

We could simply turn on a state bool, and have the game checking this bool before setting everything up, including sounds, and etc.


    using UnityEngine;
    using UnityEngine.SceneManagement;




    public class HasSceneLoaded: MonoBehaviour
    {
        // called zero
        void Awake()
        {
            Debug.Log("Awake");
        }

        // called first
        void OnEnable()
        {
            Debug.Log("OnEnable called");
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        // called second
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Debug.Log("OnSceneLoaded: " + scene.name);
            Debug.Log(mode);

            Debug.Log("Time to start the game!!!");
        }

        // called third
        void Start()
        {
            Debug.Log("Start");
        }

        // called when the game is terminated
        void OnDisable()
        {
            Debug.Log("OnDisable");
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

# When to Exactly Load?

So we set up the Crossy Road chicken logo. The question is when are we loading?

We might be loading during the first chicken logo, so perhaps this is why they would be part of the same scene.

So what if instead we move all this stuff over to the Game scene. In the worst case scenario, we can show a "Loading" thing.

So we will need some code to detect when everything is loaded. Then we will progress to the other screen intro graphics.

So we brought the graphic into Game, right near the top. To make the graphic come to the front, we inputted a "1" for sort order under canvas of the Intro Graphic, game object element.

Let's get started now on the code which detects everything has been loaded.

# Setting Up Scaling on Menu Images

The way that they set up the canvas scaling on Rushy Road is as follows:

Locate the canvas element for the UI.

Go to Canvas Scaler, and for UI scale mode choose "scale with screen size", and then for reference resolution use: x 1080, y 720. Then for Screen match mode use "match width or height". Then drag the height slider all the way to 1 for height.

So that ensures that our logo will be lined up full screen. Now we will need to create another image under the canvas element, and choose simply the fill color of the background graphic. Then we set this size to 2000 by 2000, and dragged this element to the top over the logo, so this shows up in the background.


# Menu Screen Order

- Screen 1: App logo comes up first (just a static image, so if scene is taking a moment to load we will not notice any stuttering with a static image). This is the same logo as the app thumbnail. A voxel chicken.

- Screen 2: After the app is done loading, the app logo goes away. (Or do we load somewhere else?) HIpster Whale logo, so logo of the company vibrating up and down. Water Bubbles Happen.

- Screen 3: Crossy Road logo. The logo zooms in from the back. We are keeping the same nice light lbue for all the screens. We could use yellow here. Does yellow cause people to spend money?

https://www.realsimple.com/work-life/money/color-psychology

"The signature color of sophistication (hello, little black dress), black dominates high-end makeup packaging and can even make inexpensive blushes and lipsticks seem more upscale."

"Patrons are 15 percent more likely to return to stores with blue color schemes than to those with orange color scheme."

"The color is associated with fairness and affordability, which is why you’ll find orange at stores offering good value, like Home Depot and Payless."

"Consequently, a purple box may help persuade us that the product has special properties and is worth a princely sum."

"A mainstay at fast-food restaurants, yellow evokes energy and increases appetite, perhaps explaining why your stomach may start to growl when you pass those golden arches."

So after screen 3 we enter the gameplay screen.

- Screen 4: Gameplay



# Getting Started with Laying Out UI/UX

So first study how the asset packs handle the scene loading to start the game, load the game, and then also how are we loading and transitioning between gameplay and the car purchasing screen.


So here not only do we not have any fancy screen loading, but we actually have a delay variable. Maybe this is to have a delay at the beginning of the game, so the users can notice the intro screens.


Okay, so how are we actually making the transition to the car unlockable screen?

So this button is called "CharacterBtn". Then if we look down to the button properties, we are sending out two events:

- UIManager.PlayButtonSound

- UIManager.CharacterBtn

The CharacterBtn method can be found here in UIManager:

    public void CharacterBtn()
    {
        GameManager.Instance.LoadScene("Character", 0.5f);
    }


Here are some scene loading code from lines 225 of GameManager.cs of Rushy Racing:


    public void LoadScene(string sceneName, float delay)
    {
        StartCoroutine(LoadingScene(sceneName, delay));
    }

    IEnumerator LoadingScene(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
    
    
So now let's take a look at GameManager's LoadScene method. Notice that there is a delay here of half a second.


So that is the complete scene transition. The ironic thing is we were looking for a way to not slow down the scenes, but here we are working with a delay. Perhaps the delay prevents stuttering. 

In either case, let's test this out.

So we probably will want to put the above two methods into the Game Manager. Will we have a problem if the Game Manager is not a singleton?

Well, let's put these two methods in there, and then we can always just simply have a way of getting the scene back. Unless that will take longer, but don't think so, since we have to change scenes anyway with the Game Manager scenario.

Or we can have a dedicated scene loader, which might be better.

So we put the methods in Game.cs:


    public void LoadScene(string sceneName, float delay)
    {
        StartCoroutine(LoadingScene(sceneName, delay));
    }

    IEnumerator LoadingScene(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }


    public void LoadCharactersScene()
    {
        LoadScene("Characters", 0.5f);
    }

One of the interesting things that they did is have the button call two methods. One plays the sound and other does the transitioning. So we will save one slot for this. Perhaps we will need the sound manager to be an instance, so that we don't interrupt the sound playing in the middle.

Well that worked. The 0.5f is almost unnoticable. We can decrease this in the future if needed.

So let's now get the menu screens going, and then come back to the characters purchasing screen.


# Sound Manager

We could just use the SoundManager.cs from Rushy Racing:

    using UnityEngine;
    using System.Collections;
    using System.Collections.Generic;

    namespace OnefallGames
    {
        [RequireComponent(typeof(AudioSource))]
        public class SoundManager : MonoBehaviour
        {
            public static SoundManager Instance { get; private set; }

            [System.Serializable]
            public class Sound
            {
                public AudioClip clip;
                [HideInInspector]
                public int simultaneousPlayCount = 0;
            }

            // List of sounds used in this game
            public Sound background;
            public Sound button;
            public Sound coin;
            public Sound healing;
            public Sound hitCar;
            public Sound rewarded;
            public Sound unlock;
            public Sound tick;


            public delegate void OnMuteStatusChanged(bool isMuted);

            public static event OnMuteStatusChanged MuteStatusChanged;

            public delegate void OnMusicStatusChanged(bool isOn);

            public static event OnMusicStatusChanged MusicStatusChanged;

            enum PlayingState
            {
                Playing,
                Paused,
                Stopped
            }

            public AudioSource AudioSource
            {
                get
                {
                    if (audioSource == null)
                    {
                        audioSource = GetComponent<AudioSource>();
                    }

                    return audioSource;
                }
            }

            private AudioSource audioSource;
            private PlayingState musicState = PlayingState.Stopped;
            private const string MUTE_PREF_KEY = "MutePreference";
            private const int MUTED = 1;
            private const int UN_MUTED = 0;
            private const string MUSIC_PREF_KEY = "MusicPreference";
            private const int MUSIC_OFF = 0;
            private const int MUSIC_ON = 1;

            void Awake()
            {
                if (Instance)
                {
                    DestroyImmediate(gameObject);
                }
                else
                {
                    Instance = this;
                    DontDestroyOnLoad(gameObject);
                }
            }

            void Start()
            {
                // Set mute based on the valued stored in PlayerPrefs
                SetMute(IsMuted());
            }

            /// <summary>
            /// Plays the given sound with option to progressively scale down volume of multiple copies of same sound playing at
            /// the same time to eliminate the issue that sound amplitude adds up and becomes too loud.
            /// </summary>
            /// <param name="sound">Sound.</param>
            /// <param name="autoScaleVolume">If set to <c>true</c> auto scale down volume of same sounds played together.</param>
            /// <param name="maxVolumeScale">Max volume scale before scaling down.</param>
            public void PlaySound(Sound sound, bool autoScaleVolume = true, float maxVolumeScale = 1f)
            {
                StartCoroutine(CRPlaySound(sound, autoScaleVolume, maxVolumeScale));
            }

            IEnumerator CRPlaySound(Sound sound, bool autoScaleVolume = true, float maxVolumeScale = 1f)
            {
                if (sound.simultaneousPlayCount >= 7)
                {
                    yield break;
                }

                sound.simultaneousPlayCount++;

                float vol = maxVolumeScale;

                // Scale down volume of same sound played subsequently
                if (autoScaleVolume && sound.simultaneousPlayCount > 0)
                {
                    vol = vol / (float)(sound.simultaneousPlayCount);
                }

                AudioSource.PlayOneShot(sound.clip, vol);

                // Wait til the sound almost finishes playing then reduce play count
                float delay = sound.clip.length * 0.7f;

                yield return new WaitForSeconds(delay);

                sound.simultaneousPlayCount--;
            }

            /// <summary>
            /// Plays the given music.
            /// </summary>
            /// <param name="music">Music.</param>
            /// <param name="loop">If set to <c>true</c> loop.</param>
            public void PlayMusic(Sound music, bool loop = true)
            {
                if (IsMusicOff())
                {
                    return;
                }

                AudioSource.clip = music.clip;
                AudioSource.loop = loop;
                AudioSource.Play();
                musicState = PlayingState.Playing;
            }

            /// <summary>
            /// Pauses the music.
            /// </summary>
            public void PauseMusic()
            {
                if (musicState == PlayingState.Playing)
                {
                    AudioSource.Pause();
                    musicState = PlayingState.Paused;
                }    
            }

            /// <summary>
            /// Resumes the music.
            /// </summary>
            public void ResumeMusic()
            {
                if (musicState == PlayingState.Paused)
                {
                    AudioSource.UnPause();
                    musicState = PlayingState.Playing;
                }
            }

            /// <summary>
            /// Stop music.
            /// </summary>
            public void StopMusic()
            {
                AudioSource.Stop();
                musicState = PlayingState.Stopped;
            }

            /// <summary>
            /// Determines whether sound is muted.
            /// </summary>
            /// <returns><c>true</c> if sound is muted; otherwise, <c>false</c>.</returns>
            public bool IsMuted()
            {
                return (PlayerPrefs.GetInt(MUTE_PREF_KEY, UN_MUTED) == MUTED);
            }

            public bool IsMusicOff()
            {
                return (PlayerPrefs.GetInt(MUSIC_PREF_KEY, MUSIC_ON) == MUSIC_OFF);
            }

            /// <summary>
            /// Toggles the mute status.
            /// </summary>
            public void ToggleMute()
            {
                // Toggle current mute status
                bool mute = !IsMuted();

                if (mute)
                {
                    // Muted
                    PlayerPrefs.SetInt(MUTE_PREF_KEY, MUTED);

                    if (MuteStatusChanged != null)
                    {
                        MuteStatusChanged(true);
                    }
                }
                else
                {
                    // Un-muted
                    PlayerPrefs.SetInt(MUTE_PREF_KEY, UN_MUTED);

                    if (MuteStatusChanged != null)
                    {
                        MuteStatusChanged(false);
                    }
                }

                SetMute(mute);
            }

            /// <summary>
            /// Toggles the mute status.
            /// </summary>
            public void ToggleMusic()
            {
                if (IsMusicOff())
                {
                    // Turn music ON
                    PlayerPrefs.SetInt(MUSIC_PREF_KEY, MUSIC_ON);
                    if (musicState == PlayingState.Paused)
                    {
                        ResumeMusic();
                    }

                    if (MusicStatusChanged != null)
                    {
                        MusicStatusChanged(true);
                    }
                }
                else
                {
                    // Turn music OFF
                    PlayerPrefs.SetInt(MUSIC_PREF_KEY, MUSIC_OFF);
                    if (musicState == PlayingState.Playing)
                    {
                        PauseMusic();
                    }

                    if (MusicStatusChanged != null)
                    {
                        MusicStatusChanged(false);
                    }
                }
            }

            void SetMute(bool isMuted)
            {
                AudioSource.mute = isMuted;
            }
        }
    }




# Crossy Road UI/UX Analysis

- Screen 1: App logo comes up first (just a static image, so if scene is taking a moment to load we will not notice any stuttering with a static image). This is the same logo as the app thumbnail. A voxel chicken.

- Screen 2: After the app is done loading, the app logo goes away. (Or do we load somewhere else?) HIpster Whale logo, so logo of the company vibrating up and down. Water Bubbles Happen.

- Screen 3: Crossy Road logo. The logo zooms in from the back. We are keeping the same nice light lbue for all the screens. We could use yellow here. Does yellow cause people to spend money?

https://www.realsimple.com/work-life/money/color-psychology

"The signature color of sophistication (hello, little black dress), black dominates high-end makeup packaging and can even make inexpensive blushes and lipsticks seem more upscale."

"Patrons are 15 percent more likely to return to stores with blue color schemes than to those with orange color scheme."

"The color is associated with fairness and affordability, which is why you’ll find orange at stores offering good value, like Home Depot and Payless."

"Consequently, a purple box may help persuade us that the product has special properties and is worth a princely sum."

"A mainstay at fast-food restaurants, yellow evokes energy and increases appetite, perhaps explaining why your stomach may start to growl when you pass those golden arches."

So after screen 3 we enter the gameplay screen.

- Screen 4: Gameplay

So here at the gameplay screen we have the UI/UX set up in the four corners of the screen. 

The upper left hand corner has a timer and a gift box with arrows. Shows 19H left until a free gift.

The icon on the to pin the lower left is a multiplayer icon, which we will not need.

Then there is an icon on the bottom which shows a character, and is flashing at the player to be pressed.

Once we press that, that's where we have our characters. This is one of our main money making mechanisms, so that makes sense to place the button on the lower left. There are some buttons in the character purchasing interface, but we probably will not be using that yet.


Then in the lower right hand corner we have an arrow button. This button actually pops up an interface. A game controller, and a letter which might be the letter G. The game controller seems just like a toggle button, with no immediate effect. The icon on top of that allows us to redeem a code.

So basically we will want two ways of monetizing. Rewarded ads, and purchasable cars. So we can start with having a button, which takes us into the car purchasing screen. What we want to do is figure out the loading screen.


## Rushy Racing



# Starting Off the Workday

Okay, so today we want to get as much done as possible. Let's just go slowly and evenly, since we don't want to wear ourselves out. We have plenty of time to get these tasks done. Rushing only blunts our concentration, and makes us tired.

So today we will want to start off by implementing two major things:

- menu

- unlockable cars screen


Just like that one random forum poster said, they had two apps. Both identical with functionality, but one had a superior UI/UX. The one with the better UI/UX garnered all of the downloads. So people will use an app just because of the sleek UI/UX.

We want the game to have a sleek, feel good, smooth UI/UX. Study Crossy Road for this. One of the things Crossy Road does is have that water splash sound when the logo comes up. That is very professional. Maybe we can have a logo also with a sound effect.

So let's start off with the menu. Both the menu and the car unlockable screen might use the same code from the asset pack.

So first up:

- study Crossy Road order of screens

- study the asset pack (are they using additive scene loading? how to prevent scene loading pause)

- diagram out here the order of screens, we can just place holders for now

- determine how we will set up the interface buttons, how does Crossy Road do this?

# What's Next?

So opposite traffic is finished, and random cars, and also random terrains, so what's next?

- we need to add colliders to the other cars because the player is not colliding, #TODO


## Menu

- study Crossy Road
- study other Asset Packs, take notes how do the menu screen

## Unlockable Cars

- study the asset pack

- replicate their functionality, refactor code down to something simpler


# Generating Opposite Traffic

First generate traffic flowing in the same direction.

Then flip the cars 180.

Then add negative acceleration in the CarEngine script.

We can still pull from the CarsHolder pack, we will just need to flip them 180, and then apply the opposite velocity and acceleration. We will need to adjust the values for both acceleration and deceleration.

Okay, so we actually want to create another separate array in the CarsHolder.cs script:

    public class CarsHolder : MonoBehaviour
    {
        public GameObject[] cars;

        public GameObject[] leftLaneCars; // contains flipped models by 180, and opposite driving direction
    }

This way we can set the car engine opposite directions on the separate set of prefabs we drag into leftLaneCars. We should also rename cars to rightLaneCars. #TODO

So then simply in AI we can do:


    void DeployLeftLaneModerateTrafficWithLootAndRandomCars()
    {

        // LANE 1

        float lane1xPos = -1f;
        float lane1Speed = 10.0f;

        SpawnCar(carsHolder.leftLaneCars[Random.Range(0, carsHolder.leftLaneCars.Length)], GetRandomPos(2, 10, lane1xPos), lane1Speed);
        SpawnCar(carsHolder.leftLaneCars[Random.Range(0, carsHolder.leftLaneCars.Length)], GetRandomPos(15, 20, lane1xPos), lane1Speed);
        SpawnCar(carsHolder.leftLaneCars[Random.Range(0, carsHolder.leftLaneCars.Length)], GetRandomPos(25, 30, lane1xPos), lane1Speed);
        SpawnCar(carsHolder.leftLaneCars[Random.Range(0, carsHolder.leftLaneCars.Length)], GetRandomPos(40, 50, lane1xPos), lane1Speed);
        SpawnCar(carsHolder.leftLaneCars[Random.Range(0, carsHolder.leftLaneCars.Length)], GetRandomPos(55, 62, lane1xPos), lane1Speed);


        // LANE 2

        float lane2xPos = -2.5f;
        float lane2Speed = 8.0f;

        SpawnCar(loot, GetRandomPos(2, 10, lane1xPos), lane1Speed);
        SpawnCar(carsHolder.leftLaneCars[Random.Range(0, carsHolder.leftLaneCars.Length)], GetRandomPos(15, 20, lane2xPos), lane2Speed);
        SpawnCar(carsHolder.leftLaneCars[Random.Range(0, carsHolder.leftLaneCars.Length)], GetRandomPos(25, 30, lane2xPos), lane2Speed);
        SpawnCar(carsHolder.leftLaneCars[Random.Range(0, carsHolder.leftLaneCars.Length)], GetRandomPos(40, 50, lane2xPos), lane2Speed);
        SpawnCar(carsHolder.leftLaneCars[Random.Range(0, carsHolder.leftLaneCars.Length)], GetRandomPos(55, 62, lane2xPos), lane2Speed);

        

        // LANE 3

        float lane3xPos = -4.2f;
        float lane3Speed = 5.0f;

        SpawnCar(carsHolder.leftLaneCars[Random.Range(0, carsHolder.leftLaneCars.Length)], GetRandomPos(2, 10, lane3xPos), lane3Speed);
        SpawnCar(carsHolder.leftLaneCars[Random.Range(0, carsHolder.leftLaneCars.Length)], GetRandomPos(15, 20, lane3xPos), lane3Speed);
        SpawnCar(carsHolder.leftLaneCars[Random.Range(0, carsHolder.leftLaneCars.Length)], GetRandomPos(25, 30, lane3xPos), lane3Speed);
        SpawnCar(carsHolder.leftLaneCars[Random.Range(0, carsHolder.leftLaneCars.Length)], GetRandomPos(40, 50, lane3xPos), lane3Speed);
        SpawnCar(carsHolder.leftLaneCars[Random.Range(0, carsHolder.leftLaneCars.Length)], GetRandomPos(55, 62, lane3xPos), lane3Speed);
    }

We might also take out loot from the left lane.

Once we get everything working we can work on more of a randomization appearance to the traffic by simply calling multiple methods at different times.


So in CarEngine.cs we added an enum, which we will set from the inspector on the attached car prefab.


    public enum Lane
    {
        Right,
        Left
    }

    public Lane lane;
    
    
    
    
Then in Update() of CarEngine.cs:
    
    
    if (lane == Lane.Right) MoveForward();
    if (lane == Lane.Left) MoveBackward();
    
    
    
 And then simply:
 
     private void MoveForward()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }


    // for left lane traffic

    private void MoveBackward()
    {
        transform.Translate(Vector3.back * Time.deltaTime * speed);
    }

So the last thing left is to flip the models by 180 degrees, which is straightforward.

So we simply flipped the rotation from 90 degrees (original), to -90 on the left lane cars.

We also took the loot out because for now we will not be procedurally generating loot on the left lane, but if we are then we have to reverse the direction on the moving loot.



# Generate First Terrain

Generate the first terrain, since we are just simply initializing the original chunk, while we should be initializing a random chunk out of a grasslands array.


# Car Area Clear Bug

So we are not clearing the area properly. We should get a log statement going of our transform position, and our distance position, to indicate where we are clearing. Perhaps the transform is not staying with the car. #TODO

# Solving the Random Car Generation

So the code we need to randomly generate cars can be found here:

    CarsHolder carsHolder; // global
    
    carsHolder = FindObjectOfType<CarsHolder>(); // in Start()


    void DeployModerateTrafficWithLootAndRandomCars()
    {
        // LANE 1

        float lane1xPos = 2.5f;
        float lane1Speed = 8.0f;

        SpawnCar(loot, GetRandomPos(2, 10, lane1xPos), lane1Speed);
        SpawnCar(carsHolder.cars[Random.Range(0, carsHolder.cars.Length)], GetRandomPos(15, 20, lane1xPos), lane1Speed);
        SpawnCar(carsHolder.cars[Random.Range(0, carsHolder.cars.Length)], GetRandomPos(25, 30, lane1xPos), lane1Speed);
        SpawnCar(carsHolder.cars[Random.Range(0, carsHolder.cars.Length)], GetRandomPos(40, 50, lane1xPos), lane1Speed);
        SpawnCar(carsHolder.cars[Random.Range(0, carsHolder.cars.Length)], GetRandomPos(55, 62, lane1xPos), lane1Speed);

        // LANE 2

        float lane2xPos = 0.8f;
        float lane2Speed = 10.0f;

        SpawnCar(carsHolder.cars[Random.Range(0, carsHolder.cars.Length)], GetRandomPos(2, 10, lane2xPos), lane2Speed);
        SpawnCar(carsHolder.cars[Random.Range(0, carsHolder.cars.Length)], GetRandomPos(15, 20, lane2xPos), lane2Speed);
        SpawnCar(carsHolder.cars[Random.Range(0, carsHolder.cars.Length)], GetRandomPos(25, 30, lane2xPos), lane2Speed);
        SpawnCar(carsHolder.cars[Random.Range(0, carsHolder.cars.Length)], GetRandomPos(40, 50, lane2xPos), lane2Speed);
        SpawnCar(carsHolder.cars[Random.Range(0, carsHolder.cars.Length)], GetRandomPos(55, 62, lane2xPos), lane2Speed);

        // LANE 3

        float lane3xPos = 4.2f;
        float lane3Speed = 5.0f;

        SpawnCar(carsHolder.cars[Random.Range(0, carsHolder.cars.Length)], GetRandomPos(2, 10, lane3xPos), lane3Speed);
        SpawnCar(carsHolder.cars[Random.Range(0, carsHolder.cars.Length)], GetRandomPos(15, 20, lane3xPos), lane3Speed);
        SpawnCar(carsHolder.cars[Random.Range(0, carsHolder.cars.Length)], GetRandomPos(25, 30, lane3xPos), lane3Speed);
        SpawnCar(carsHolder.cars[Random.Range(0, carsHolder.cars.Length)], GetRandomPos(40, 50, lane3xPos), lane3Speed);
        SpawnCar(carsHolder.cars[Random.Range(0, carsHolder.cars.Length)], GetRandomPos(55, 62, lane3xPos), lane3Speed);
    }


# CarsHolder.cs

Just like we did for Chunks Holder we will need to do the same thing for CarsHolder.cs.

So create a game object in the hierarchy named "Cars Holder" and attach CarsHolder.cs.

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;




    public class CarsHolder : MonoBehaviour
    {
        public GameObject[] cars;
    }

And then in AI.cs:

    CarsHolder carsHolder;

    carsHolder = FindObjectOfType<carsHolder>();
    
    InstantiateRandomCar(carsHolder.cars);
    
   
    void InstantiateRandomCar(GameObject[] randomCar)
    {
        Instantiate(randomCar[Random.Range(0, randomCar.Length)], pos, Quaternion.Euler(new Vector3(0, 0, 0)));
    }
    
    

# Begin Implementing Procedurally Generated Terrain

Add code from below. Test by changing the color of the platform, and creating extra Chunks from that, labeled as grassLandChunk1, grassLandChunk2, etc.

So there was a problem with placing the public GameObject[] chunks on each instance. The problem was that we had to individually drag in all the game object prefabs, for each chunk (Chunk1Grassland), so that would have been too much repetitive dragging.

Instead we created a public game object "Chunks Holder", and then attached to this ChunksHolder.cs:


    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;


    public class ChunksHolder : MonoBehaviour
    {

        public GameObject[] grassLandChunks;

        // public GameObject[] desertLandChunks;

        // public GameObject[] winterLandChunks;

    }


This gets the job done. Then in Procedural.cs we have:

    ChunksHolder chunksHolder;

    chunksHolder = FindObjectOfType<ChunksHolder>();
    
     InstantiateRandomChunkLand(chunksHolder.grassLandChunks);


# App Promotion

cpimobi.com promises 0.09$ per install.

# Variable Reward Within Variable Reward

So for example, when we generate the gold coins loot, which will be generate from a random assortment of loot items, then we can also vary the gold count reward, so that will give the player an extra layer of variable reward.

# Random Must Have Loot Modifiers

So if we have a type of loot that is required to progress like gas, we could force a spawn, if we don't spawn up to x number of times in a row, so that way the player can proceed in the game. Although there will still be a level of challenge required, when the player must get that gas loot to progress, since we spawned them and all is fair.

# Level Design

We are just creating objects that go on each platform, and labeling each of those as levels. In the future however, we might choose to make the platform tiles bigger...

In which we can case we could just spawn the next few consecutive tiles. Also we could see the horizon this way.

One way to indicate when we should spawn the tiles is to add a count ID property to each tile, so we know the number in the sequence, then once we are in the start method, we can determine if we are at a number divisible by our spawning, so perhaps 5, so every 5 tiles spawn a new sequence of 5 more tiles.

Or spawn 5 tiles the first time, and then spawn n total + 1 tiles on each new tile we arrive into. This is probably the optimial solution. This way we will always get a horizon, since we will always have 5 (or x required) tiles ahead of us. So when we spawn the next tile, this tile will need to be placed 5 tiles back.


# The Polish List

- neon lights and nightmode, back and front lights on cars

- ambient highway sounds, car sounds

- sound fx, score if any

- leaderboard

- color theming

- object pooling for optimized spawning

- different colors on each car model

- rebuilding project to get rid of flashing

- windmills in background

- website and trailer (can work on this post game release)

- allow player to customize the color of their car

- skyboxes

- mooing near cow pastures

- car tire marks, possibly tire smoke / dirt

- logo (try to assemble from cubes)


# What are the Other Core Features to Implement Before Moving on to Polish and More Granular Stuff?

Make a list and put in order.

Also study the Crossy Road GDC talk at this point, and take notes here.

- variable reward loot

- smooth game over sequence (study Crossy Road)

- monetization

- user profile (connect with Android and iOS)

- car turning

- level design

- gas meter (gives the player an extra goal, and ensures gameplay is more challenging, totalling in more restarts, and ad revenue)

- add high score record, plus leaderboard high score compare (what if there are not that many players? well we can make an auto leaderboard, that first scans if there are more than 10, than show leaderboard, then as more players are added divided down to 10 places or 50 or so by region)

- destroy doubled up cars, as a precaution feature, create a script which destroys one car, if two are within close enough of a distance of each other.

# Unlockable Cars

Study the asset first, the user experience, and the game code.

Then use that form to scroll through the available cars, but don't gray them out so the player can preview them.

First just get them working in the scene, then do the stats on each car. So we would have handling, top speed, and other metrics per car to make the purchases seem more exciting. Study the different stats racing games give you, and pick 3 or 4, and then list them here.

# Menu Screen

First take notes and study Crossy Road, Rushing Racing, that one other racing game on the app store, and the assets of course.

Then diagram out how we will want the screens to proceed. Crossy Road might have two company advertisement screens, one for the studio and the other for the game.

After going through and studying the above games, then diagram out here the order of the menus. Use placeholders. Should be pretty straightforward.

Also consider using that yellow background, with the 3D custom voxel letters popping out, or doing a parallax effect in the menu.


# Invincibility Button (For Testing)

The invincibilty button should be pretty straightforward. Right where we set the state to game over, that's where we will need an if statement to bypass:

    if (!godMode) state.game = state.Game.Over;
    
And in the HUD we will make a "God Mode" toggle button.
    
    
    

# Generating Opposite Traffic

To generate traffic going the opposite way we will need a few modifications:

- shift the x component (will be done by AI.cs)

- also flip the car models 180 (can also be done by AI.cs)

- drive the opposite way (will be done by car engine)


The way AI.cs and CarEngine.cs will recognize which direction of the road to spawn traffic, is to have some sort of bool marker on the parent prefab. So at instantiation we get this value, and by this we set the three parameters above.


# Instantiating Different Cars

We can use the same method from instantiating different chunks applied to generating different cars.

From wherever we generate the cars, (AI.cs), let's include up on top:

    public GameObject[] cars;

And then use the same method from Procedural.cs:


    void InstantiateRandomCar()
    {
        Instantiate(cars[Random.Range(0, cars.Length)], pos, Quaternion.Euler(new Vector3(0, 0, 0)));
    }

We could of course use this method from procedural, but this is a short enough method we can keep in AI.cs, especially if we want to customzie this process later.

# Instantiating Different Chunks


This might be as simple as declaring this at the top of Procedural.cs:

    public GameObject[] chunks;
    

And then writing a method which does this:


    void InstantiateRandomChunk()
    {
        Instantiate(chunks[Random.Range(0, chunks.Length)], pos, Quaternion.Euler(new Vector3(0, 0, 0)));
    }

For now we want the simplest working way, but later we can instantiate series of chunks based on biomes. So first let's say we want to spawn 5 tiles of the grasslands biome, all random tiles pulled out of the "grasslands biome" tile bag.

We can test spawning the random chunks, just by simply changing the color of the platform. So we would duplicate everything else, and then rename the game object Chunk to something like Chunk1GrassLand, Chunk2GrassLand... Chunk1DesertLand, Chunk2DesertLand... Chunk1WinterLand, Chunk2WinterLand.

Then can test the procedure for spawning a series of random chunks per biome. We could use different shades of color per biome, so shades of green per grass land, shades of yellow per desert land, etc. And then of course we could even randomize the starting biome, and biome sequence generation.

A simple way to generate random biome specific tiles per sequence could be too simply declare the available chunks up at the top:

    public GameObject[] grassLandChunks;
    public GameObject[] desertLandChunks;
    public GameObject[] winterLandChunks;


And then we when can have a method per each:


    void InstantiateRandomGrassLandChunk()
    {
        Instantiate(grassLandChunks[Random.Range(0, grassLandChunks.Length)], pos, Quaternion.Euler(new Vector3(0, 0, 0)));
    }
    
    void InstantiateRandomDesertLandChunk()
    {
        Instantiate(desertLandChunks[Random.Range(0, desertLandChunks.Length)], pos, Quaternion.Euler(new Vector3(0, 0, 0)));
    }
    
    void InstantiateRandomWinterLandChunk()
    {
        Instantiate(winterLandChunks[Random.Range(0, winterLandChunks.Length)], pos, Quaternion.Euler(new Vector3(0, 0, 0)));
    }


We could condense this to one method:

    void InstantiateRandomChunkLand(GameObject[] chunkLands)
    {
        Instantiate(chunkLands[Random.Range(0, chunkLands.Length)], pos, Quaternion.Euler(new Vector3(0, 0, 0)));
    }
    

So at first we are just going to test one type.

So let's just create the public GameObject[] grassLandChunks in Procedural.cs;

Then when the time comes to spawn a new chunk, we can do:

    InstantiateRandomChunkLand(grassLandChunks); // passing in an array

Out of this we will spawn one random chunk, so we are done testing here.

Then later when we decide to switch the biome from let's say for example grassLandChunks to desertLandChunks, we can simply do:


    InstantiateRandomChunkLand(desertLandChunks); // passing in an array



# Next Up

Let's do different cars, and different levels generated next.


# The Rest of the Masterplan List

Here are the other major todos:



opposite traffic

godmode toggle button for testing

different cars generated

different levels

menu screen

unlockable cars screen



# Side Colliders

Make the length of the side collider the size of the tile on the z axis, 64 in this case. Set the snapping to 2 or something with ProGrids. We are going to call this "Rail" and keep under the Chunk game object.

We are not able to get the colliders to work. Maybe this is because we are adjusting the transform. Well, we still had collisions on the cars with the transform. 

One way of doing this is something like CapX(), and then cap the boundaries of the x on the car, so we cannot move any more. I believe this is what they did in the space game tutorial.

So we were able to do that with this code added to Update() of Control.cs:


    float leftXRail = -13;
    float rightXRail = -3.5f;

    void CapX()
    {
        if (transform.position.x < leftXRail)
        {
            pos = transform.position;
            pos.x = leftXRail;
            transform.position = pos;
        }

        if (transform.position.x > rightXRail)
        {
            pos = transform.position;
            pos.x = rightXRail;
            transform.position = pos;
        }
    }
    

# Car Area Clear

Need to test the threshold. We are calling this once in Start() of Garbage.cs, so we run this once per chunk generated. With a threshold of 100.0f we were deleting too many cars, with a threshold of 1000.0f, we seem to be fine. Do more testing here. #TODO


    public void AreaCars()
    {
        cars = GameObject.FindGameObjectsWithTag("Car");
        foreach (GameObject car in cars)
        {
            float distance = Vector3.Distance(car.transform.position, transform.position);
            Debug.Log("distance: " + distance);

            if (distance > 1000.0f)
            {
                carsDestroyed += 1;

                Debug.Log("Destroying " + carsDestroyed +" cars");
                Destroy(car);
            }
        }
    }


# New Day

Let's go through the masterplan list.

- garbage collection on cars

- side colliders


# Edge Case - Waiting Before Driving

What if the player simply waits with acceleration of 0, and velocity of 0. Then the first wave of cars are spawned and those continue forward. Well, even though they are moving at a constant speed, once the player then speeds up, and the new wave of cars are spawned, what if they are spawned on top of each other? 

We can fix this with having a garbage collector helper. If we detect than any two cars are within some collider distance of each other, then destroy one, and leave the other. By the time the player catches up for the next wave of cars spawned, that would have already happened ahead of him. This would run in the Start() method of Garbage.cs, once everytime a new wave of cars is spawned, since Garbage.cs is attached to the procedural "Chunk".

# Add Invincibility Mode for Testing

Everytime we check whether to destroy the car, in there check the state of the invicibility mode. Build this out as a button toggle.

# Getting Flickering even though all Shaders are Set to Standard

This might be a shader issue. Will have to rebuild project at second to worst, and worst just ship with the bug. Do some research. Figure out how come the asset pack does not have this flickering screen issue. Perhaps this also might be because left the mouse scan codes in. Let's take those out.


# Added Profile (user profile to Data.cs)

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;



    public class Data : MonoBehaviour
    {


        public struct Profile
        {
            public int highScore;
            public float timeSpentPlaying;
        }


        public struct Counts
        {
            public int carCount;
            public int chunkCount;
            public int score;
        }


        Profile profile;
        Counts counts;





        void Start()
        {
            // initialize data values

            counts.carCount = 0;
            counts.chunkCount = 1; // start at 1 because there is a starting chunk in the scene
            counts.score = 0;

        }


    }


# A Way to Design the Procedural Levels

We can test out each method of GenerateModerateTraffic(), GenerateModerateTrafficWithLoot(), maybe even number them and write up a description in the notes.

This way we can test just one iteration over and over and guage how that feels. We can always increase this to 2X repetitions in the actual gameplay, but then we can test the change from GenerateModerateTraffic() to GenerateLightTraffic() and fine tune that through gameplay. Then with enough of these levels we can randomly change from GenerateModerateTraffic() to GenerateLightTraffic() and other combinations.

# Debugging Code - Total Cars Count (game objects with tag)

We added this code to Garbage.cs, to convienently get the total cars count during the Start() method:


    void Start()
    {
        cars = GameObject.FindGameObjectsWithTag("Car");

        int count = 0;

        foreach (GameObject car in cars)
        {
            count += 1;

           //Destroy(car);
        }

        Debug.Log("Total car count in scene: " + count);

        Invoke("DestroyObject", LifeTime);
    }





# Clear.cs Code

Originally we wanted to put these methods inside of Garbage.cs, but garbage destroy the chunk prefab and also auto destroy, so that might not be the best choice, until we somehow have Garbage as a permanent game object member of the hierarchy dolling out that command. Until then we wrote a script called Clear, which gets the job done nicely:


         using System.Collections;
         using System.Collections.Generic;
         using UnityEngine;





         public class Clear : MonoBehaviour
         {

             State state;
             Score score;


             GameObject[] cars;
             GameObject[] loot;
             GameObject[] chunks;




             private void Start()
             {
                 state = FindObjectOfType<State>();
                 score = FindObjectOfType<Score>();
             }




             public void Score()
             {
                 state.score = 0;
                 score.Set(0);
             }


             public void Cars()
             {
                 cars = GameObject.FindGameObjectsWithTag("Car");
                 foreach (GameObject car in cars) Destroy(car);
             }


             public void Loot()
             {
                 loot = GameObject.FindGameObjectsWithTag("Loot");
                 foreach (GameObject l in loot) Destroy(l);
             }


             public void Chunks()
             {
                 chunks = GameObject.FindGameObjectsWithTag("Chunk");
                 foreach (GameObject chunk in chunks) Destroy(chunk);
             }

         }



# Refactor (Move) Clear.Chunks() and others into Garbage.cs

Refactor and fix code, which will break. To keep all the Clear.X() stuff out of Game.cs and instead put in Garbage.cs.

Also refactor "counts" in Data, since we moved them.

# Variable Reward on Loot

So every time there is loot, the type should be different. This way we introduce V A R I A B L E   R E W A R D  into the gameplay.



# Settings.cs Code

         using System.Collections;
         using System.Collections.Generic;
         using UnityEngine;



         public class Settings : MonoBehaviour
         {


             public struct Sound
             {
                 public bool onoff;
                 public float volume;
             }


             Sound sound;



             private void Start()
             {
                 // initialize settings

                 sound.onoff = true;
                 sound.volume = 8.0f;
             }
         }


# Data.cs Code


         using System.Collections;
         using System.Collections.Generic;
         using UnityEngine;



         public class Data : MonoBehaviour
         {


             public struct Counts
             {
                 public int carCount;
                 public int chunkCount;
                 public int score;
             }


             Counts counts;




             void Start()
             {
                 // initialize data values

                 counts.carCount = 0;
                 counts.chunkCount = 1; // start at 1 because there is a starting chunk in the scene
                 counts.score = 0;

             }


         }


# After Masterplan is Complete

After the masterplan is complete do another few rounds of important todos. Then only start on polish, and sound effects, etc.

# Masterplan ToDo in Order

Here are the todos in order:

* garbage collection on the cars (add functionality to Garbage.cs)

* colliders on the sides, so player can't venture out of the road

* opposite traffic

* godmode toggle button for testing

* different cars generated

* different levels

* menu screen

* unlockable cars screen




# What are the Major Things Left to Do?

So before working on the polish stuff, let's work on the big stuff needed to ship the game. 

Let's throw some stuff randomly here, and then organize later:

* colliders on sides, so player can't venture out of the road

* realistic car turning (spring camera will help)

* spring camera

* menu screens

* unlockable cars

* saving unlocked cars to profile (Google + Apple will need different save functionality)

* a few test environments to test how we are generating random chunks, we can just have different colored cubes on the sidelines to test

* what else?

* monetization (study asset code)

* opposite traffic

Play the asset prototypes and determine further what is missing.


# What's Next?

Well we've completed the "Masterplan" to do list:

* Cars spawning correctly
* Game over state
* Pickups
* Score


Now here are a few things we can do to polish the above:

* make sure we are adding an even number on score, why are we detecting 2 on trigger enters?

* let's make pickups spinning, add sound effects, add different sort of pickups

* we want a question mark pickup box (unless that is copyrighted by Nintendo), so we can introduce skinner boxes, and have the player anticipating what they will get in the question mark box

* also let's have regular loot, like coins, and other stuff, perhaps gas

* we can have a gas meter, so the player needs to pick up loot to progress, a nice way of forcing the player to get loot (and increase chances of losing) instead of just avoiding traffic without worrying about loot

* we want a smoother game over state, perhaps fade the screen at least, also check how Crossy Road does this

* perhaps we want the score to enlarge on pickup, something like from Moenen

* different types of cars

* gas meter in HUD

* tire squeels when slamming on breaks

* engine noise

* score

* check that we are using the Standard shader instead of mobile shader on any assets, that's where the glitching graphics bug is likely coming from


But these are all polish things. We should move on to the bigger stuff, before we are going to do this kind of polish, just so that we can get closer to the finished product, which includes screens, unlocking cars, different types of cars. Like with the masterplan we should just have a few simple items to work toward.


# Score and Loot Implementation

So here is the simple score script, which just writes the score to the screen. We attach this score script to the game object "Score" in the hierarchy, which is under the HUD game object, which also contains the movements controls, gas pedal, turning, etc.


         using System.Collections;
         using System.Collections.Generic;
         using UnityEngine;
         using UnityEngine.UI;


         public class Score : MonoBehaviour
         {

             public Text score;


             public void Set(int amount)
             {
                 score.text = amount.ToString();
             }
         }

Then the rest of the functionality which connects loot with scoring can be found in DetectCollision.cs. Like we mentioned in the DetectCollision script comments, we can scan here for all types of loot based on the loot tag and reward the player accordingly.


         using System.Collections;
         using System.Collections.Generic;
         using UnityEngine;




         public class DetectCollision : MonoBehaviour
         {

             State state;
             Score score;


             private void Start()
             {
                 state = FindObjectOfType<State>();
                 score = FindObjectOfType<Score>();
             }


             private void OnTriggerEnter(Collider c)
             {

                 if (c.gameObject.tag == "Car") state.game = State.Game.Over;


                 if (c.gameObject.tag == "Loot")
                 {
                     score.Set(state.score += 10);
                     Destroy(c.gameObject);
                 }

                 // later we will have different types of tags, so LootBonusPoints, LootHealth, etc, and just add things here based on the type
             }

         }

And we also updated Game.cs:


         using System.Collections;
         using System.Collections.Generic;
         using UnityEngine;




         public class Game : MonoBehaviour
         {

             public GameObject player;
             public GameObject chunk;


             public Vector3 playerResetPos = new Vector3(0.0f, 0.0f, 0.0f);
             public Vector3 chunkStartPos = new Vector3(0.0f, 0.0f, 0.0f);


             GameObject[] cars;
             GameObject[] loot;
             GameObject[] chunks;


             State state;
             Score score;


             private void Start()
             {
                 state = FindObjectOfType<State>();
                 score = FindObjectOfType<Score>();
             }


             private void Update()
             {
                 if (state.game == State.Game.Over)
                 {
                     state.game = State.Game.New;
                     NewGame();
                 }
             }



             void NewGame()
             {
                 // deinitialize

                 ClearScore();
                 ClearCars();
                 ClearLoot();
                 ClearChunks();

                 // initialize

                 CreateStartingChunk();
                 ResetPlayerPosition(); 
             }



             void ResetPlayerPosition()
             {
                 player.transform.position = playerResetPos;
             }


             void ClearScore()
             {
                 state.score = 0;
                 score.Set(0);
             }


             void ClearCars()
             {
                 cars = GameObject.FindGameObjectsWithTag("Car");
                 foreach (GameObject car in cars) Destroy(car);
             }


             void ClearLoot()
             {
                 loot = GameObject.FindGameObjectsWithTag("Loot");
                 foreach (GameObject l in loot) Destroy(l);
             }


             void ClearChunks()
             {
                 chunks = GameObject.FindGameObjectsWithTag("Chunk");
                 foreach (GameObject chunk in chunks) Destroy(chunk);
             }



             void CreateStartingChunk()
             {
                 Instantiate(chunk, chunkStartPos, Quaternion.Euler(new Vector3(0, 0, 0)));
             }

         }



# Loot

So instead of putting the OnTriggerEnter() functionality in a Loot.cs script, instead we put this in DetectCollision.cs, since Loot is a type of collision anyway.

Now one important thing is to always first edit the prefab in the scene, then drag that over the prefab in the project window to override, then drag the prefab from the project window into the slot to restore. And also as a last step do this also for the Chunk object. Since part of this object changed, then drag over to the project window, and then drag the chunk from the project window over onto the procedrual slot of the game object chunk in the hierarchy.


Also for some reason we are registering this twice. So we will only add one score thing per this or whatever. We can just brute force a fix. Not sure why we are getting the loot to register double.

But at least now we can connect the loot pickup with the score update functionality.



Here is the code:

         using System.Collections;
         using System.Collections.Generic;
         using UnityEngine;




         public class DetectCollision : MonoBehaviour
         {
             State state;

             private void Start()
             {
                 state = FindObjectOfType<State>();
             }


             private void OnTriggerEnter(Collider c)
             {
                 if (c.gameObject.tag == "Car") state.game = State.Game.Over;

                 if (c.gameObject.tag == "Loot")
                 {
                     Debug.Log("LOOT!");
                 }
             }

         }



# How to Fix the Common Chunk Problem

Whenever we are modifying the chunk, make sure to drag the chunk from the hierarchy over and onto the chunk prefab in the project inspector, so we can modify the prefab. Then drag the prefab from the project window into the slot in Procedural.cs of Chunk game object in the scene.

# What's Next?

Well, we mechanically set up the recognition of a game over state.

We will of course need some smooth transition. But we could do this at another time.

So the next two things we want to do are pickups and score.

Pickups can be generated just like a regular car.

To start do a simple pickup which simply raises our score, which gives us a chance to finish both components.

Then after we get that done we can come back and work on a smooth game over state transition.



So our original plan was:

Cars spawning correctly
Game over state
Pickups
Score

# Recognizing Game State

We are simply using DetectCollision.cs to set game state. This has a box collider is trigger, and also set the DetectCollision.cs script on the same level.

So anyway, check a bool variable 60 times a second should not give us any performance issues so that way we can communicate the state.

Then once DetectCollision registers the game over state, the Game.cs script picks up on this:


From Game.cs:

         private void Update()
            {
                if (state.game == State.Game.Over)
                {
                    state.game = State.Game.New;
                    NewGame();
                }
            }


DetectCollision.cs:


        using System.Collections;
        using System.Collections.Generic;
        using UnityEngine;




        public class DetectCollision : MonoBehaviour
        {
            State state;

            private void Start()
            {
                state = FindObjectOfType<State>();
            }


            private void OnTriggerEnter(Collider c)
            {
                if (c.gameObject.tag == "Car") state.game = State.Game.Over;
            }

        }



# New Game

So here is our functionality for starting a new game. We are doing everything correctly except spawning new cars. Something is preventing the chunk from possibly registering the player to spawn new cars.

So the solution to the new cars not spawning correctly is that the AI object had a question mark / plus, letting us known this was not part of the original prefab, so when dragging the prefab into the chunk slot of procedural, we were not getting the AI attached. So updating the prefab with the AI part, and then dragging the prefab from the project window (not hierarchy) into the procedural slot fixed the problem.


        using System.Collections;
        using System.Collections.Generic;
        using UnityEngine;




        public class Game : MonoBehaviour
        {

            public GameObject player;
            public GameObject chunk;


            public Vector3 playerResetPos = new Vector3(0.0f, 0.0f, 0.0f);
            public Vector3 chunkStartPos = new Vector3(0.0f, 0.0f, 0.0f);


            GameObject[] cars;
            GameObject[] chunks;


            State state;

            private void Start()
            {
                state = FindObjectOfType<State>();
            }


            private void Update()
            {
                if (state.game == State.Game.Over)
                {
                    state.game = State.Game.New;
                    NewGame();
                }
            }



            void NewGame()
            {
                DeleteAllCarsInScene();
                DeleteAllChunksInScene();
                CreateStartingChunk();
                ResetPlayerPosition(); 
            }



            void ResetPlayerPosition()
            {
                player.transform.position = playerResetPos;
            }



            void DeleteAllCarsInScene()
            {
                cars = GameObject.FindGameObjectsWithTag("Car");
                foreach (GameObject car in cars) Destroy(car);
            }



            void DeleteAllChunksInScene()
            {
                chunks = GameObject.FindGameObjectsWithTag("Chunk");
                foreach (GameObject chunk in chunks) Destroy(chunk);
            }


            void CreateStartingChunk()
            {
                Instantiate(chunk, chunkStartPos, Quaternion.Euler(new Vector3(0, 0, 0)));
            }

        }



# Multiple Car Spawning Bug

The reasons we were spawning multiple cars is because we had multiple colliders (probably), so once we cleared this out just to a capsule everything worked. We will need to set up colliders properly.


# Solution to Weird Not Spawning Terrain Bug

Maybe out collider is going out of bounds when we turn sharply to one side.

Well the actual solution might be found here:

"For your objects to receive the OnTriggerEnter, at least one of them has to have the Is Trigger property checked and at least one of them has to have a Rigid Body. If neither of your objects is a Trigger, you can use OnCollisionEnter instead.

Once that's all set, you should check the Layers (not Tags) on your objects. To edit which Layers collide with each other, you can look at Edit -> Project Settings -> Physics.

By default Unity sets all layers to collide with all layers. That's a good works-by-default setup, but you may want to play with it to optimize later on."

By deleting one of the objects on the car, a filler capsule or whatever, perhaps we essentially got rid of the rigid body which was helping us register the OnTriggerEnter() events.

Sure enough, when we delete the "Cube" object, which contains a rigid body, we stop spawning. So having a rigid body on at least one of the objects solves our issue.


# Getting Collisions to Register

Let's do an OnTriggerEnter, or OnCollision, on the player so we can recognize the AI car game object and decide when we lost the game by touching another car.

So we just simply add this script at the same level where we have an is trigger collider. Also make sure that the tag is on the right level. We had the proper "Car" tag at the parent level, but where not registering this at the child level where the tag was left "undefined". After changing to "Car" everything worked correctly.



        using System.Collections;
        using System.Collections.Generic;
        using UnityEngine;




        public class DetectCollision : MonoBehaviour
        {

            private void OnTriggerEnter(Collider c)
            {
                if (c.gameObject.tag == "Car")
                {
                    Debug.Log("Detected AI Car");
                }
            }

        }



# On AI Car Rigidbodies

We have some strange behaviour going on. When we add a rigidbody with "is kinematic" turned off we don't get the car spawning.

Let's just first get the collision to register. Then we can always add the kinematic stuff, and even the apply force when this happens if we have to.



# Goals for Today

So we solved the issue of cars not spawning correctly. One problem was taking out this.transform, and the other was adding the transform to the z position.

Now we might have a bug where we are not spawning the terrain in front of us. If this is the case maybe our collider is not sticking out far enough. 

Also we want to introduce a game over state, and currently we are not getting collisions. So perhaps add a rigid body to fix this.

If we get collisions with the rigid body then we can check out the add force code from the asset pack.


# Low Poly Style

Cars: https://assetstore.unity.com/detail/3d/vehicles/land/low-poly-cars-101798

Terrain: https://assetstore.unity.com/detail/3d/vegetation/trees/low-poly-trees-pack-73954


# AI Polished Final for Now


        using System.Collections;
        using System.Collections.Generic;
        using UnityEngine;



        public class AI : MonoBehaviour
        {

            // public GameObject[] cars;
            // public Vector3[] positions; //  = new Vector3(0, 0, 0);
            // int totalArrayCount;


            public GameObject car1;
            public GameObject car2;

            public GameObject car;

            // GameObject car1GO;
            // GameObject car2GO;

            GameObject carGO; // current game object, overriden by next iteration, mostly used for setting the speed through carGO<GetComponent>

            public Vector3 pos1;
            public Vector3 pos2;

            Vector3 newPos1;
            Vector3 newPos2;


            State state;


            // We can also call HeavyTraffic(), and SparseTraffic()
            // Then each time in the Start() method of this script, we can randomize the traffic density method
            // to do this perhaps randomize a number and then do a switch case statement for each of the random numbers



            void DeployModerateTraffic()
            {
                // LANE 1

                float lane1xPos = 2.5f;
                float lane1Speed = 8.0f;

                SpawnCar(car, GetRandomPos(2, 10, lane1xPos), lane1Speed);
                SpawnCar(car, GetRandomPos(15, 20, lane1xPos), lane1Speed);
                SpawnCar(car, GetRandomPos(25, 30, lane1xPos), lane1Speed);
                SpawnCar(car, GetRandomPos(40, 50, lane1xPos), lane1Speed);
                SpawnCar(car, GetRandomPos(55, 62, lane1xPos), lane1Speed);

                // LANE 2

                float lane2xPos = 0.8f;
                float lane2Speed = 10.0f;

                SpawnCar(car, GetRandomPos(2, 10, lane2xPos), lane2Speed);
                SpawnCar(car, GetRandomPos(15, 20, lane2xPos), lane2Speed);
                SpawnCar(car, GetRandomPos(25, 30, lane2xPos), lane2Speed);
                SpawnCar(car, GetRandomPos(40, 50, lane2xPos), lane2Speed);
                SpawnCar(car, GetRandomPos(55, 62, lane2xPos), lane2Speed);

                // LANE 3

                float lane3xPos = 4.2f;
                float lane3Speed = 5.0f;

                SpawnCar(car, GetRandomPos(2, 10, lane3xPos), lane3Speed);
                SpawnCar(car, GetRandomPos(15, 20, lane3xPos), lane3Speed);
                SpawnCar(car, GetRandomPos(25, 30, lane3xPos), lane3Speed);
                SpawnCar(car, GetRandomPos(40, 50, lane3xPos), lane3Speed);
                SpawnCar(car, GetRandomPos(55, 62, lane3xPos), lane3Speed);
            }


            void SpawnCar(GameObject Car, Vector3 pos, float speed)
            {
                carGO = Instantiate(Car, transform.position + pos, Quaternion.Euler(new Vector3(0, 0, 0)));
                state.carCount += 1;
                carGO.GetComponent<CarEngine>().SetSpeed(speed);
            }


            Vector3 GetRandomPos(int startRange, int endRange, float lane)
            {
                startRange += (int)transform.position.z;
                endRange += (int)transform.position.z;
                int randomNumber = GetRandomNumber(startRange, endRange);
                Vector3 randomPos = new Vector3(lane, -0.5f, (float)randomNumber); // 4.2 is the lane position, and 0.5, the height on the y axis
                return randomPos;
            }


            int GetRandomNumber(int start, int end)
            {
                return Random.Range(start, end);
            }




            private void Start()
            {
                // Debug.Log("Instantiating new AI on new chunk");

                state = FindObjectOfType<State>();


                DeployModerateTraffic();

                /*

                // INSTANTIATE CARS PER ONE LANE

                // each car is moving at a constant speed so they will not collide with another
                // each car also has a buffer of at least 6 units before previously spawned car, set as the first number of the random range



                // LANE 1

                float lane1xPos = 2.5f;
                float speedLane1 = 10.0f;

                SpawnCar(car, GetRandomPos(2, 10, lane1xPos), speedLane1);
                SpawnCar(car, GetRandomPos(40, 50, lane1xPos), speedLane1);


                // LANE 2

                float lane2xPos = 0.8f;
                float speedLane2 = 15.0f;

                SpawnCar(car, GetRandomPos(2, 10, lane2xPos), speedLane2);


                // SpawnCar(ChooseRandomCar(), GetRandomPos(40, 50, lane2), speedLane2);


                // LANE 3

                float lane3xPos = 4.2f;
                float speedLane3 = 7.0f;

                SpawnCar(car, GetRandomPos(2, 10, lane3xPos), speedLane3);






                // #TODO: Later we will need a way to randomly pull a car game object, and also make sure we don't choose too many duplicates, we can also hard code this


                // SpawnCar(car, GetRandomPos(34, 45));
                // SpawnCar(car, GetRandomPos(25, 30));
                // SpawnCar(car, GetRandomPos(36, 40));
                // SpawnCar(car, GetRandomPos(46, 52));
                // SpawnCar(car, GetRandomPos(58, 64));




                // SpawnCarsInLane1();

                */

                /* old way
                totalArrayCount = cars.Length;
                Debug.Log("Spawning cars");
                for (int i = 0; i < totalArrayCount; i++ )
                {
                    Instantiate(cars[i], positions[i], Quaternion.Euler(new Vector3(0, 0, 0)), this.transform);
                }
                */
            }


            /*
            GameObject ChooseRandomCar()
            {
                GameObject test;

                // return test;
            }
            */







            void SpawnCarsInLane1()
            {
                // Debug.Log("Calling spawn cars");

                // spawn car 1

                newPos1 = transform.position + pos1;

                // car1GO = Instantiate(car1, newPos1, Quaternion.Euler(new Vector3(0, 0, 0)));

                state.carCount += 1;

                Debug.Log("newPos1: " + newPos1);

                // spawn car 2

                newPos2 = transform.position + pos2;

                // car2GO = Instantiate(car2, newPos2, Quaternion.Euler(new Vector3(0, 0, 0)));

                state.carCount += 1;

                Debug.Log("newPos2: " + newPos2);


                // set speed

                // car1GO.GetComponent<CarEngine>().SetSpeed(10);
                // car2GO.GetComponent<CarEngine>().SetSpeed(10);

            }



            void SpawnCarsInLane2() { }

            void SpawnCarsInLane3() { }






        }




# Car Spawning Coming Along Nicely


The car spawning is coming along nicely. Since the DebugCarSpawning() method was working, we renamed that to ModerateTraffic().

We will initialize the traffic type in Start(). So for example we will pick a random number, and based on that random number do a switch case thing for the type of traffic: LightTraffic(), ModerateTraffic(), and HeavyTrafic().


However if we change the speeds of any lane, that's when we have to add rays to detect the position of the car in the front. But if we keep the lane speeds constant then we do not have to implement the rays yet. We can also cheat and instaneously change all the speeds on the car's speed car engine component. (FindAllObjectsInScene<>).



        using System.Collections;
        using System.Collections.Generic;
        using UnityEngine;



        public class AI : MonoBehaviour
        {

            // public GameObject[] cars;
            // public Vector3[] positions; //  = new Vector3(0, 0, 0);
            // int totalArrayCount;


            public GameObject car1;
            public GameObject car2;

            public GameObject car;

            // GameObject car1GO;
            // GameObject car2GO;

            GameObject carGO; // current game object, overriden by next iteration, mostly used for setting the speed through carGO<GetComponent>

            public Vector3 pos1;
            public Vector3 pos2;

            Vector3 newPos1;
            Vector3 newPos2;


            State state;


            // We can also call HeavyTraffic(), and SparseTraffic()
            // Then each time in the Start() method of this script, we can randomize the traffic density method
            // to do this perhaps randomize a number and then do a switch case statement for each of the random numbers



            void ModerateTraffic()
            {
                // LANE 1

                float lane1xPos = 2.5f;
                float lane1Speed = 8.0f;
                SpawnCar(car, GetRandomPos(2, 10, lane1xPos), lane1Speed);
                SpawnCar(car, GetRandomPos(15, 20, lane1xPos), lane1Speed);
                SpawnCar(car, GetRandomPos(25, 30, lane1xPos), lane1Speed);
                SpawnCar(car, GetRandomPos(40, 50, lane1xPos), lane1Speed);
                SpawnCar(car, GetRandomPos(55, 62, lane1xPos), lane1Speed);

                // LANE 2

                float lane2xPos = 0.8f;
                float lane2Speed = 10.0f;

                SpawnCar(car, GetRandomPos(2, 10, lane2xPos), lane2Speed);
                SpawnCar(car, GetRandomPos(15, 20, lane2xPos), lane2Speed);
                SpawnCar(car, GetRandomPos(25, 30, lane2xPos), lane2Speed);
                SpawnCar(car, GetRandomPos(40, 50, lane2xPos), lane2Speed);
                SpawnCar(car, GetRandomPos(55, 62, lane2xPos), lane2Speed);

                // LANE 3

                float lane3xPos = 4.2f;
                float lane3Speed = 5.0f;

                SpawnCar(car, GetRandomPos(2, 10, lane3xPos), lane3Speed);
                SpawnCar(car, GetRandomPos(15, 20, lane3xPos), lane3Speed);
                SpawnCar(car, GetRandomPos(25, 30, lane3xPos), lane3Speed);
                SpawnCar(car, GetRandomPos(40, 50, lane3xPos), lane3Speed);
                SpawnCar(car, GetRandomPos(55, 62, lane3xPos), lane3Speed);
            }


            void SpawnCar(GameObject Car, Vector3 pos, float speed)
            {
                Debug.Log("Spawning car at " + (transform.position + pos));
                // Debug.Log("transform.position: " + transform.position);
                carGO = Instantiate(Car, transform.position + pos, Quaternion.Euler(new Vector3(0, 0, 0)));
                state.carCount += 1;
                carGO.GetComponent<CarEngine>().SetSpeed(speed);
            }


            Vector3 GetRandomPos(int startRange, int endRange, float lane)
            {
                startRange += (int)transform.position.z;
                endRange += (int)transform.position.z;
                int randomNumber = GetRandomNumber(startRange, endRange);
                Vector3 randomPos = new Vector3(lane, -0.5f, (float)randomNumber); // 4.2 is the lane position, and 0.5, the height on the y axis
                return randomPos;
            }


            int GetRandomNumber(int start, int end)
            {
                int number;
                number = Random.Range(start, end);
                return number;
            }




            private void Start()
            {
                // Debug.Log("Instantiating new AI on new chunk");

                state = FindObjectOfType<State>();


                ModerateTraffic();

                /*

                // INSTANTIATE CARS PER ONE LANE

                // each car is moving at a constant speed so they will not collide with another
                // each car also has a buffer of at least 6 units before previously spawned car, set as the first number of the random range



                // LANE 1

                float lane1xPos = 2.5f;
                float speedLane1 = 10.0f;

                SpawnCar(car, GetRandomPos(2, 10, lane1xPos), speedLane1);
                SpawnCar(car, GetRandomPos(40, 50, lane1xPos), speedLane1);


                // LANE 2

                float lane2xPos = 0.8f;
                float speedLane2 = 15.0f;

                SpawnCar(car, GetRandomPos(2, 10, lane2xPos), speedLane2);


                // SpawnCar(ChooseRandomCar(), GetRandomPos(40, 50, lane2), speedLane2);


                // LANE 3

                float lane3xPos = 4.2f;
                float speedLane3 = 7.0f;

                SpawnCar(car, GetRandomPos(2, 10, lane3xPos), speedLane3);






                // #TODO: Later we will need a way to randomly pull a car game object, and also make sure we don't choose too many duplicates, we can also hard code this


                // SpawnCar(car, GetRandomPos(34, 45));
                // SpawnCar(car, GetRandomPos(25, 30));
                // SpawnCar(car, GetRandomPos(36, 40));
                // SpawnCar(car, GetRandomPos(46, 52));
                // SpawnCar(car, GetRandomPos(58, 64));




                // SpawnCarsInLane1();

                */

                /* old way
                totalArrayCount = cars.Length;
                Debug.Log("Spawning cars");
                for (int i = 0; i < totalArrayCount; i++ )
                {
                    Instantiate(cars[i], positions[i], Quaternion.Euler(new Vector3(0, 0, 0)), this.transform);
                }
                */
            }


            /*
            GameObject ChooseRandomCar()
            {
                GameObject test;

                // return test;
            }
            */







            void SpawnCarsInLane1()
            {
                // Debug.Log("Calling spawn cars");

                // spawn car 1

                newPos1 = transform.position + pos1;

                // car1GO = Instantiate(car1, newPos1, Quaternion.Euler(new Vector3(0, 0, 0)));

                state.carCount += 1;

                Debug.Log("newPos1: " + newPos1);

                // spawn car 2

                newPos2 = transform.position + pos2;

                // car2GO = Instantiate(car2, newPos2, Quaternion.Euler(new Vector3(0, 0, 0)));

                state.carCount += 1;

                Debug.Log("newPos2: " + newPos2);


                // set speed

                // car1GO.GetComponent<CarEngine>().SetSpeed(10);
                // car2GO.GetComponent<CarEngine>().SetSpeed(10);

            }



            void SpawnCarsInLane2() { }

            void SpawnCarsInLane3() { }


        }



# Masterplan

1. Cars spawning correctly
2. Game over state
3. Pickups
4. Score


# Fix for the AI 0 to 64 Random Offset

So these two lines were the fix: (90% chances). We should double check the positions exactly of everything, and draw this out on paper.

        start += (int)transform.position.z;
        end += (int)transform.position.z;
        
        

    Vector3 GetRandomPos(int start, int end, float lane)
    {
        start += (int)transform.position.z;
        end += (int)transform.position.z;
        int randomNumber = GetRandomNumber(start, end);
        Vector3 randomPos = new Vector3(lane, -0.5f, (float)randomNumber); // 4.2 is the lane position, and 0.5, the height on the y axis
        return randomPos;
    }

# This is the AI Code We need to Somehow Convert from 0 to 64

    void DebugSpawnCars()
    {
        // LANE 1
        float lane1xPos = 2.5f;
        float lane1Speed = 10.0f;
        SpawnCar(car, GetRandomPos(2, 10, lane1xPos), lane1Speed);
        SpawnCar(car, GetRandomPos(40, 50, lane1xPos), lane1Speed);
    }


    void SpawnCar(GameObject Car, Vector3 pos, float speed)
    {
        Debug.Log("Spawning car at " + (transform.position + pos));
        // Debug.Log("transform.position: " + transform.position);
        carGO = Instantiate(Car, transform.position + pos, Quaternion.Euler(new Vector3(0, 0, 0)));
        state.carCount += 1;
        carGO.GetComponent<CarEngine>().SetSpeed(speed);
    }


    Vector3 GetRandomPos(int start, int end, float lane)
    {
        int randomNumber = GetRandomNumber(start, end);
        Vector3 randomPos = new Vector3(lane, -0.5f, (float)randomNumber); // 4.2 is the lane position, and 0.5, the height on the y axis
        return randomPos;
    }


    int GetRandomNumber(int start, int end)
    {
        int number;
        number = Random.Range(start, end);
        return number;
    }

# Working on AI

The AI is coming together nicely. Though we don't know why sometimes two cars will overlap, even though we have properly spaced them apart. Getting unexpected behaviour here.

The spacing here works for a 64 x 64 tile. When we increase the size of the tile, we will also need to adjust the spawning ranges here.


    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;



    public class AI : MonoBehaviour
    {

        // public GameObject[] cars;
        // public Vector3[] positions; //  = new Vector3(0, 0, 0);
        // int totalArrayCount;


        public GameObject car1;
        public GameObject car2;

        public GameObject car;

        // GameObject car1GO;
        // GameObject car2GO;

        GameObject carGO; // current game object, overriden by next iteration, mostly used for setting the speed through carGO<GetComponent>

        public Vector3 pos1;
        public Vector3 pos2;

        Vector3 newPos1;
        Vector3 newPos2;


        State state;



        private void Start()
        {
            Debug.Log("Instantiating new AI on new chunk");

            state = FindObjectOfType<State>();



            // INSTANTIATE CARS PER ONE LANE

            // each car is moving at a constant speed so they will not collide with another
            // each car also has a buffer of at least 6 units before previously spawned car, set as the first number of the random range



            // LANE 1

            float lane1xPos = 2.5f;
            float speedLane1 = 10.0f;

            SpawnCar(car, GetRandomPos(2, 10, lane1xPos), speedLane1);
            // SpawnCar(car, GetRandomPos(20, 30, lane1xPos), speedLane1);
            SpawnCar(car, GetRandomPos(40, 50, lane1xPos), speedLane1);


            // LANE 2

            float lane2xPos = 0.8f;
            float speedLane2 = 15.0f;

            SpawnCar(car, GetRandomPos(2, 10, lane2xPos), speedLane2);
            // SpawnCar(car, GetRandomPos(20, 30, lane2));
            // SpawnCar(car, GetRandomPos(40, 50, lane2));


            // LANE 3

            float lane3xPos = 4.2f;
            float speedLane3 = 7.0f;

            SpawnCar(car, GetRandomPos(2, 10, lane3xPos), speedLane3);






            // #TODO: Later we will need a way to randomly pull a car game object, and also make sure we don't choose too many duplicates, we can also hard code this


            // SpawnCar(car, GetRandomPos(34, 45));
            // SpawnCar(car, GetRandomPos(25, 30));
            // SpawnCar(car, GetRandomPos(36, 40));
            // SpawnCar(car, GetRandomPos(46, 52));
            // SpawnCar(car, GetRandomPos(58, 64));




            // SpawnCarsInLane1();

            /* old way
            totalArrayCount = cars.Length;
            Debug.Log("Spawning cars");
            for (int i = 0; i < totalArrayCount; i++ )
            {
                Instantiate(cars[i], positions[i], Quaternion.Euler(new Vector3(0, 0, 0)), this.transform);
            }
            */
        }





        Vector3 GetRandomPos(int start, int end, float lane)
        {
            int randomNumber = GetRandomNumber(start, end);
            Vector3 randomPos = new Vector3(lane, 0.0f, (float)randomNumber); // 4.2 is the lane position, and 0.5, the height on the y axis
            return randomPos;
        }




        void SpawnCar(GameObject Car, Vector3 pos, float speed)
        {
            carGO = Instantiate(Car, transform.position + pos, Quaternion.Euler(new Vector3(0, 0, 0)));
            state.carCount += 1;
            carGO.GetComponent<CarEngine>().SetSpeed(speed);
        }




        void SpawnCarsInLane1()
        {
            // Debug.Log("Calling spawn cars");

            // spawn car 1

            newPos1 = transform.position + pos1;

            // car1GO = Instantiate(car1, newPos1, Quaternion.Euler(new Vector3(0, 0, 0)));

            state.carCount += 1;

            Debug.Log("newPos1: " + newPos1);

            // spawn car 2

            newPos2 = transform.position + pos2;

            // car2GO = Instantiate(car2, newPos2, Quaternion.Euler(new Vector3(0, 0, 0)));

            state.carCount += 1;

            Debug.Log("newPos2: " + newPos2);


            // set speed

            // car1GO.GetComponent<CarEngine>().SetSpeed(10);
            // car2GO.GetComponent<CarEngine>().SetSpeed(10);

        }



        void SpawnCarsInLane2() { }

        void SpawnCarsInLane3() { }





        int GetRandomNumber(int start, int end)
        {
            int number;
            number = Random.Range(start, end);
            return number;
        }
    }


# Starting Car Position Spawn

An important thing is the starting position of the first car on the new tile. So that way we can fill out all the parts of traffic evenly.

If we take 36 units for the tile. And then find the buffer. Then something like pick any random integer between 3 - 33. And then cast to a float for the position after we get the RandomRange(<int>) answer.


# Generating Random Traffic Step by Step

- generate traffic at equidistant positions (in progress)

- generate traffic in randomized segments of equidistant positions

Randomized segements offer buffer space. So for example, if the car is 1 unit wide, then the road will have at least 2 or 3 units as a buffer. So if we are generating segments between 1 and 10, then we don't actually start the randomization between 3 and 7 let's say.

Draw this out, so we can get a fraction based on the 64 meters of the tile. Also get the car size, since this will affect our fraction.



# Randomizing Cars on the Road 

So one way to spawn the cars randomly is to randomize the position from segments.

So divide up the tile into ten segments. Each segment has a position range along the z we can randomize. So for segment one, we will spawn between point A of segment's start, and point B of segment's end. So we will know that no two cars will collide since there will be a buffer gap between each segment, and since each car will have a set speed they not be running into each other.


# Fix for Recursive Spawning

So the reason we were recursively spawning is because we were using this.transform. Not sure why, but that was the reason. Later investigate this in Discord and Google. 

So taking out, this.transform this works:


    void SpawnCarsInLane1()
    {
        Debug.Log("Calling spawn cars");
        // spawn car 1

        car1GO = Instantiate(car1, transform.position + pos1, Quaternion.Euler(new Vector3(0, 0, 0)));

        // spawn car 2

        car2GO = Instantiate(car2, transform.position + pos2, Quaternion.Euler(new Vector3(0, 0, 0)));


        // set speed

        car1GO.GetComponent<CarEngine>().SetSpeed(10);
        car2GO.GetComponent<CarEngine>().SetSpeed(10);

    }


# AI Spawning Twice in the Same Spot

Since we are generating two tiles at a time, the AI is generating the cars in the same spot. Not sure yet why some of the cars moving faster. Maybe because the public speed value has not been changed.

The AI should be spawning the cars relative to the parent prefab. But instead we are spawning on an absolute Vector3. So both tiles are spawning at vector.zero.

So to fix this we need to spawn the position relative to the parent's position.

We seemingly fixed spawning relative to the parent by adding the original transform. However we are getting some recursive behaviour by increased multiple spawning. 

    void SpawnCarsInLane1()
    {
        Debug.Log("Calling spawn cars");
        // spawn car 1

        car1GO = Instantiate(car1, transform.position + pos1, Quaternion.Euler(new Vector3(0, 0, 0)), this.transform);

        // spawn car 2

        car2GO = Instantiate(car2, transform.position + pos2, Quaternion.Euler(new Vector3(0, 0, 0)), this.transform);


        // set speed

        car1GO.GetComponent<CarEngine>().SetSpeed(10);
        car2GO.GetComponent<CarEngine>().SetSpeed(10);

    }
    

# Work Day Commencing

Okay, so we just want to do things in order today, step by step with no stress. We don't know how long we are going to work, and we don't want to know how long we will work. Today we should be taking off the Sunday. But we will do a few things, as long as we do things step by step in order.

So the first step will be spawn the traffic per lane in the AI. So something like:

    SpawnCarsInLane1() {}

    SpawnCarsInLane2() {}

    SpawnCarsInLane3() {}


In AI.cs:

    void SpawnCarsInLane1()
    {
        // spawn car 1

        Instantiate(car1, pos1, Quaternion.Euler(new Vector3(0, 0, 0)), this.transform);

        // spawn car 2

        Instantiate(car2, pos2, Quaternion.Euler(new Vector3(0, 0, 0)), this.transform);

        // we will also need a way to set a constant speed on the cars
    }
    
We added this in CarEngine.cs, since we want a public method AI.cs can use and control the speed of the different cars. This will be just the speed generated in the Start() method of AI.cs. If the car engine then wants to customize the speed when moving in and out of traffic, that is fine. At that point we will be using rays to make sure we don't collide with another car. But for now we will be starting with constant speed per lane.

    public void SetSpeed(float Speed)
    {
        speed = Speed;
    }
    

So here is how we are initializing the game object's "car" speed:


    void SpawnCarsInLane1()
    {
        // spawn car 1

        car1GO = Instantiate(car1, pos1, Quaternion.Euler(new Vector3(0, 0, 0)), this.transform);

        // spawn car 2

        car2GO = Instantiate(car2, pos2, Quaternion.Euler(new Vector3(0, 0, 0)), this.transform);


        // set speed

        car1GO.GetComponent<CarEngine>().SetSpeed(10);
        car2GO.GetComponent<CarEngine>().SetSpeed(10);

    }
    
    
# Prevent Car from Moving Backwards on Decelerate

    if (speed <= 0) speed = 0;

Added these successfully to the Controls.cs code:

    void CapLowerBoundSpeed()
    {
        if (speed <= 0) speed = 0;
    }

    void CapUpperBoundSpeed()
    {
        if (speed >= maxSpeed) speed = maxSpeed;
    }
    
And then put these in the main update loop of Controls.cs:

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


# Where From Here?

Alright, so we diagrammed out what we have so far. That is great because we have a good pickup point. Until now we were just setting up the base project. So next up will be to make sure the cars are spawning correctly, and to set a collider to get a game over state, and also a seamless way to restart the game.

These will be our 2 priorities to do.

Now we can also study how the assets achieved the game over state to get a smooth loop. Perhaps we will fade the screen in and out to transition, or drop a screen. Check how Crossy Road does this, and the asset packs. Try to come up with a solid approach the first time, so that we are not mangling code.

Also make sure the is trigger colliders are set up correctly for the player. The advantage of using is trigger colliders is that we don't have to deal with the physics systems, but at the time we don't get the physics collisions. So if we go with is trigger, we can just simply change out: OnTriggerEnter() to OnCollisionEnter() later on. The two can also be seperated with the trigger enter ensuring the player restarts the game (after a few seconds), and the physics collider to showing the effect. Then after a few seconds of observing the collision we reset everything.

So we probably should have something like ResetGame() or NewGame() in Game.cs

Then after we get these 2 priorities done, we should get pickup bonuses done. These 3 things will go a long way in advancing our prototype.

After this the other big thing we can do is the score. Let's use the font from the asset pack, since that font seems to work great. That might be too similar to Crossy Road, but if that other game Racy Road uses that on the app store, then we will be alright with going this font. Anyway, we can always change the font later.


# Codebase So Far Review

For the clean modular code from scratch, here are the files:


AI

CarEngine

Controls

Game

Garbage

Procedural

State



Before we dwelve into each of these, let's get a breakdown on the hierarchy.

The standard components in the hierarchy:

EventSystem

Camera

Lights



Now onto the gameobjects in the hierarchy.


State

This contains the state code, which is not being used yet, but has all of the handy state stuff centralized with no complex routing.

Game

This is the game controller. The script Game.cs is placed on the game object game.

Chunk

This is a chunk of our world. At the top level placed on the parent are 3 components:

Procedural (Chunk dragged in that is the parent, and 64 for the offset)
Box Collider (is trigger checked)
Garbage (30 set for lifetime

As a child of the chunk one of the component is AI. The AI has arrays for car game object placement, and matching vector position placement. Though this needs to be reworked because seems like the AI is not spawning the objects correctly.

Then we have a Player game object.

The parent player game object gets a "Controls" script, with a set speed of 4.

On the player is a child object, "Camera". This is for following the player around. The angle set on the camera is:

pos: 7.28, 10, -5.4
rotation: 40.87, 0, 0
scale: 1, 1, 1

In the future we might want the camera to be on an independent game object, especially when using the spring.

Also as a child of the parent player game object is a car model.

The last game object in the hierarchy is the HUD.

The HUD contains four elements:

Gas
Break
Right
Left

Each of these components has an "Event Trigger" added at the level of the button.

For each we are registering two events: pointer down, and pointer up.

Then we drag in the Player game object into the slots, since this gives us the Controls.cs file access.

To give an example, the right button has Controls.OnRightDown tagged to pointer down, and Controls.OnRightRelease tagged to pointer up. This ensures we can keep sending out functionality when the player is holding the button.

That concludes our scene breakdown. Now on to the game code files.


# AI

Our AI class at this point of time is only responsible of spawning game objects cars as a child of the Chunk parent tile object. We had some notion of introducing more complicated AI, but we will simply just spawn the cars by different speed per lane, which makes sense from a traffic view, because cars usually follow each other. We are not sure yet if this will be done by CarEngine, or if AI will handle this, and send along the car speeds to CarEngine. We probably want the AI to handle this.


    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;



    public class AI : MonoBehaviour
    {
        public GameObject[] cars;


        public Vector3[] positions; //  = new Vector3(0, 0, 0);

        int totalArrayCount;

        private void Start()
        {
            totalArrayCount = cars.Length;
            Debug.Log("Spawning cars");
            for (int i = 0; i < totalArrayCount; i++ )
            {
                Instantiate(cars[i], positions[i], Quaternion.Euler(new Vector3(0, 0, 0)), this.transform);
            }

        }
    }




# Car Engine

Car engine is the car controller which moves the car models along the highway. We might simplify this class to pull all the rays out, and uneeded things. And then get the speed from the AI component on start. Later we can send out a signal to change this car speed from AI to CarEngine. But for now we want to at least initialize the speed by AI and send to CarEngine, instead of CarEngine handling the speed.

So the AI will be like the central hive car controller.


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
            speed = RandomizeSpeed(startSpeedRandomRange, endSpeedRandomRange);
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



# Controls

These are the player controls. Turns out the mobile button functionality was easier than expected, other than figuring out we have to use an Event Trigger to detect when a button is released so we can keep sending out functionality in between the button down and released.

We will need to test this out on both iOS and Android. And also make sure the canvas positionings for the buttons are correct when we flip the phone orientation. #TODO

I'm particularly satisfied with how this code turned out:

        if (speedUp) Accelerate();
        if (slowDown) Break();
        if (turnLeft) TurnLeft();
        if (turnRight) TurnRight();
            
 So between the events button down and release we are registering true states for the above and causing the action to happen.



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
            Debug.Log("Pressed mouse");
            speedUp = true;
        }


        public void OnMouseRelease()
        {
            Debug.Log("Released mouse");
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

    }



# Game


A very simple class, that's what we need. We will be using this to register the Game Over state and trigger appropriate actions, since that belongs to the scope of Game.cs


    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;




    public class Game : MonoBehaviour
    {


        void Start()
        {
            Debug.Log("Starting game");
        }

    }


# Garbage

Our garbage collection mechanism. We do not have garbage collection for the cars yet. What we will do to prevent all sorts of errors from happening (example: cars disappear in front of the player after they've been around longer than 30 seconds, which is the chunk self delete timer), is set up a special large colliders on the cars. And then if the car is outside of this collider after let's say 30 seconds, then destroy the car. The collider is anything relevant, and hence visible to the player area, so if the car is somewhere else then just destroy the car. Since we do not want cars from the back catching up, and cars in the front will get extra generated with each tile anyway. #TODO


    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;




    public class Garbage : MonoBehaviour
    {

        // place on Chunk parent game object


        public float LifeTime = 10f;


        bool playerOnTile;





        void Start()
        {
            Invoke("DestroyObject", LifeTime);
        }



        void DestroyObject()
        {

            Destroy(gameObject);

            /*
            if (!playerOnTile)
            {
                Debug.Log("Destroying game object");
                Destroy(gameObject);
            }
            */

            /*
            if (Game.Instance.GameState != GameState.Dead)
            {
                if (!playerOnTile)
                {
                    Debug.Log("Destroying game object");
                    Destroy(gameObject);
                }
            }
            */
        }



        // detect whether the player is in the collider so we don't destroy the tile the player is resting on

        private void OnTriggerEnter(Collider c)
        {
            if (c.gameObject.tag == "Player")
            {
                playerOnTile = true;
            }
        }



        private void OnTriggerExit(Collider c)
        {
            if (c.gameObject.tag == "Player")
            {
                playerOnTile = false;
            }
        }
    }

# Procedural


Going along with making everything as simple as possible, we will want to take a clever and super easy approach. Instead of customizing every single little thing, and setting up procedural generating mechanisms for each thing, instead we just want to have a "bag" of tiles we can pull from.

So let's say we are generating grassland biodomes. We have 10 or so grassland tiles we can pull from. And we prearranged all of the designs on each of these tiles so we don't have to procedurally generate each cow pen from scratch. Instead we just have a random functions which pulls out of the grasslands biodome GameObjects:

    // biodomes (darkness can cover each, so make sure some biodomes have side lamps, or just use the car headlight illumination)

    public GameObject[] grassLand;
    public GameObject[] desertLand;
    public GameObject[] cityLand;
    public GameObject[] winterLand;

So this way we don't have to worry about any involved and perhaps too overly complex procedural mechanisms before being to first just ship the product.

So we will create a number of grass land tiles, with cow pens in different locations, and just throw those into the array. Same for desertLand.

So when we generate the tiles we will have a random tile per each instance. Now how to combine the two together? Well we will need three things:

- how many tiles of each biome
- when are we moving to the next biome
- are we using train tracks to divide the biomes

But even so our procedural generation mechanism will be very simple.


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
    
# State

Also very simple. This is our finite state machine which will keep us out of trouble.



    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;



    class State : MonoBehaviour
    {

        public enum Game
        {
            Start,
            Playing,
            Dead
        }


        public enum Player
        {
            Test1,
            Test2
        }


        public Game game;

        public Player player;




    }





# A Fix to the Variable Traffic Gameplay

What we can do to vary traffic without introducing rays or any advanced AI functionality, is to set different constant speeds for traffic by each lane. 

So lane 1 we can generate a random speed between the lower bound 0 and 10.

Lane 2 we can generate a random speed between 20 - 30.

And lane 3, the fast traffic, we can generate a random speed between 30 and 40.

Of course we will have to adjust these numbers during the gameplay, and store our settings.

Then when the cars are going in their respective lanes, once in a while (in phase 2 of the development of this), then we can increase or decrease the speed. So that way the player never gets stuck in one lane if traffic is too close, and never changing. So this way we prevent the player getting stuck, vary the gameplay, and do not have to rely on any complex AI solutions for the prototype.

# Reference: https://answers.unity.com/questions/1262535/holding-down-ui-mobile-button.html

# Day 1 Commencing

Okay, so we are on Day 1. 

Let's put the list of ToDos in order:

- game over state (possibly AddForce collisions)

- cars spawning properly

- pickups

- score



#  Day 3 - Monetization

Plug in the monetization engine.


# Day 2 - Arrange World and Nightmode

Arrange world tiles, decorations, and also night mode, setting up the lights. (Neon lights and palm trees.)

Take notes and diagram what assortment of objects we have, so changing dimensions will be trivial.


# Day 1 Ludum Dare MVP

Let's try to get these important features done in one day as a Ludum Dare MVP:

- sounds
    - score
    - sound fx: car sounds, engine
   
- game over state
    - smooth game over state and restart
    
- cars spawning properly at equal speeds (randomize after MVP)

- menu screen

- unlocking cars
    - different cars have varying top speed, acceleration, and turn speed
    
- populate traffic on opposite lanes

- pickups
    - gas (we run out of gas as we use acceleration and going at top speed often)
    - gold
    
- score
    - our score points tracks gold -- check how Crossy Road does this, maybe gold and score will be seperate
   
- traffic collisions with AddForce()

# Crossy Road

Study the Crossy Road talk and take notes.

# Train Divider

The train tracks not only could work as an obstacle but also serve as a way to divide the desert from the grasslands, and any other tiles, so that's a clean break in terrain. Also a nice way to punctuate a level change with train tracks.

# Different Garbage Collection Mechanism for Cars versus Terrain

Since the cars are moving, and not static, we can't just simply garbage collect them when the player is not on a tile, since the cars can be many tiles ahead, and if we destroy them they disappear right in front of the player.

So either if the cars are way ahead of the player, or way behind we will destroy the cars to do garbage collection.

# Variable Acceleration

One of the features we have that other Crossy Road clones don't, is variable acceleration on the AI, which makes avoiding colisions much more interesting, especially if have the cars accelerate / decelerate randomly.

# From Here on Out

We are making good progress. We want to keep things modular. So let's get one thing working, and then get another thing working, and slowly bring them all together. Let's build in such a way that we don't leave any holes for us to fall through. Let's build on a solid foundation.

# Arrangement of Screens

We will want to do a case study of the arrangement of screens, menus, and layout of:

- Crossy Road

- Rushy Racing

- The other asset pack

So we can determine where to go from there. The advertising and stuff we will do separate, but we should build out the car unlock functionality without monetization first, and make sure everything is functional.

# Sound Effects 

Incorporate sound effects right away, since they are easy and make a big difference.

We will want sound effects for:

- score

- car engine

- slowing down quickly squeels

- bumping cars / crashing sound effect



# Unlockable Cars

Some cars will offer better turn speeds, better top speeds, and better acceleration. We can also create further variety by making some cars accelerate faster, versus some cars which have a higher top speed.

# Progress So Far

Progress is going great so far with the refactored project. We have the major controls now, gas, break, turn left and right.

We would like to take the approach from the asset pack on actually turning the car model when turning.

Also now we need to put some obstacles in front of the player.

Don't worry about raycasts and AI yet. First populate the world with AI cars, which maintain the same speed.

Then put a game over condition, if we bump into them. And maybe put some physics add force from the asset code, if that is easy to implement.

Then build out the ability to pick up upgrades on the road.

After this we will have accomplished a major part.

Then we can perhaps implement the car body turning.

Also prevent turning if we are not moving, or applying a different slower kind of turning.

# Holding Down Button To Accelerate / Break

https://answers.unity.com/questions/1262535/holding-down-ui-mobile-button.html

# On Biome Generation

Well, we can create our own biome generation mechanism, when we do this by chunks.

So if the next chunk is biome x, then we go through the list and spawn all the associated items with that list. Or to keep things simpler, we could have a few chunks we choose randomly from.

The only problem is the black lines. Study the implementation in the Rushy Road.

# Simple Game Manager Instance

We will keep things very simple and name the Game Manager just Game.cs, and we will not need to use any singleton patterns.

Perhaps we might need a singleton in the future to save the game state. In that case we can easily revert back over to a singleton, while enjoying the benefits of keeping things simple until then. 

Let's just focus on getting the basic core to work, and not get trapped in any time sinks.



    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;




    public class Game : MonoBehaviour
    {


        void Start()
        {
            Debug.Log("Starting game");
        }

    }


# Keep Code Absolutely Simple - John Romero

Let's keep the code absolutely simple so that we can finish and ship the game. The previous example of the state code is great because the code is short, simple, modular, and shows us the state property in the inspector. Very clear design, which is what we want.


# Very Nice State Machine

This is particularly nice because we can view the values in the inspector with declaring the instace in the same class. This will be very helpful for debuggin.

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;



    class State : MonoBehaviour
    {

        public enum Game
        {
            Start,
            Playing,
            Dead
        }


        public enum Player
        {
            Test1,
            Test2
        }


        public Game game;

        public Player player;

    }




# State

I like this simpler way of designating state. Since state really is just a variable, let's just keep this simple for now.

                using System.Collections;
                using System.Collections.Generic;
                using UnityEngine;




                public enum GameState
                {
                    Start,
                    Playing,
                    Dead
                }

So here we would add:

                public enum PlayerState
                {
                    One,
                    Two,
                    Three
                }

# Player Controller

This has the familiar state pattern. We want to implement our own simpler state pattern, however is there any functionality we need that sends out an even when state has changed? Well, if we need events we can implement them ourselves.

PlayerController.cs:

            public PlayerState PlayerState
                {
                    get
                    {
                        return playerState;
                    }

                    private set
                    {
                        if (value != playerState)
                        {
                            value = playerState;
                            PlayerStateChanged(playerState);
                        }
                    }
                }

# Spawning Cars, Coins, and Health Frequency

The coins are created here in PathController.cs:


            public void CreateObjects(int carNumber, float coinFrequency, float healthFrequency)
                {
                    //Create list position
                    listPos.Clear();
                    foreach(Transform o in paths)
                    {
                        listPos.Add(o.position);
                    }

                    StartCoroutine(CreatingObjects(carNumber, coinFrequency, healthFrequency));
                }

# Sound System

The sound system is something we want to work on extracting separetely since that doesn't affect a lot of moving parts.

# ItemController.cs Easy to Extract

This gives us pick up ability of items, and also shows how this calls the sound system. We can probably just call this Item.cs



            using OnefallGames;
            using UnityEngine;

            public class ItemController : MonoBehaviour {

                [SerializeField] private ItemType itemType = ItemType.MoneyPack;

                private BoxCollider boxCollider = null;

                private void Start()
                {
                    boxCollider = GetComponent<BoxCollider>();
                }

                private void Update()
                {
                    if (GameManager.Instance.GameState == GameState.Playing)
                    {
                        transform.eulerAngles += Vector3.up * 150f * Time.deltaTime;
                    }
                }

                private void OnTriggerEnter(Collider other)
                {
                    if (other.CompareTag("Player"))
                    {
                        Vector3 pos = transform.position + Vector3.up * (boxCollider.size.y / 2f);
                        if (itemType == ItemType.MoneyPack)
                        {
                            SoundManager.Instance.PlaySound(SoundManager.Instance.coin);
                            CoinManager.Instance.AddCoins(10);
                            GameManager.Instance.PlayMoneyExplodeParticle(pos);
                        }
                        else if (itemType == ItemType.HealthPack)
                        {
                            PlayerController.Instance.IncreaseHealth();
                        }
                        gameObject.SetActive(false);
                    }
                }
            }


# On the Merits of the Rushy Racing Asset Pack and Where From Here

This is a great pack, but we need to write our code from scratch, since we can't afford to work with problems from another code base.

So the main things we are interested in is the procedural generation.

Freeze our current game. We have a very good core loop, so let's preserve the feel of that. Create a new project, and build the systems modularly from scratch clean working.

There might be some time to figure out the procedural generation. We are interested in two things here:

1) how to get rid of the black thin lines

2) biome generation


To observe the next biomes we will need to override the game over state, since we keep losing and restarting the game.

We should build out a solid core engine with just a steady transform moving of a cube, which represents the player object. We don't want to worry about controls yet, just the procedural generation, the graphics, making sure there are no black lines, and then generating the biomes. We should keep that code minimalist and clean, and then only work on the car controls. We already have the game feel set of the car controls with the camera angle, and the acceleration, and the spring camera. So that is something we can work on once we figure out the procedural generation. Then later, things like ads can also be assembled as separate systems so don't mix things up.

# Procedurally Generating on the Fly: IEnumerator CreateGrounds(Vector3 pathPos)

Does this mean that we have two ways of procedurally generating terrain? 

1) On enter collider
2) On time change

And also, does this require the player to move at a constant position? Because we have acceleration and that will not be possible.

# Procedural Code

The procedural code can be found here in line 300 of GameManager.cs and line 440.


            //Get left ground
            private GameObject GetLeftGround()
            {
                //Find on list
                foreach (GameObject o in listEnvironment[environmentIndex].listLeftGround)
                {
                    if (!o.activeInHierarchy)
                        return o;
                }

                //Didn't fine one -> create new one
                int index = Random.Range(0, listEnvironmentPrefab[environmentIndex].listLeftGround.Count);
                GameObject leftGround = Instantiate(listEnvironmentPrefab[environmentIndex].listLeftGround[index], Vector3.zero, Quaternion.identity);
                leftGround.SetActive(false);
                listEnvironment[environmentIndex].listLeftGround.Add(leftGround);
                return leftGround;
            }
    
Line 440:

            //Create left ground, right ground, left lamp and right lamp
                private IEnumerator CreateGrounds(Vector3 pathPos)
                {
                    //Create left ground
                    yield return null;
                    GameObject leftGround = GetLeftGround();
                    leftGround.transform.position = pathPos + Vector3.left * pathXLength;
                    leftGround.SetActive(true);

                    //Create right ground
                    yield return null;
                    GameObject rightGround = GetRightGround();
                    rightGround.transform.position = pathPos + Vector3.right * pathXLength;
                    rightGround.SetActive(true);

                    //Create lamp
                    lampTurn *= -1;
                    if (lampTurn < 0)//Create on left ground
                    {
                        Vector3 lampPos = leftGround.transform.position + Vector3.left;
                        GameObject leftLamp = GetLeftLamp();
                        leftLamp.transform.position = lampPos;
                        leftLamp.SetActive(true);
                    }
                    else //Create on right ground
                    {
                        Vector3 lampPos = rightGround.transform.position + Vector3.right;
                        GameObject rightLamp = GetRightLamp();
                        rightLamp.transform.position = lampPos;
                        rightLamp.SetActive(true);
                    }
                }

# Logo and Menu Screen Font Design

https://www.dreamstime.com/pixel-style-font-pixel-font-d-retro-video-game-style-alphabet-letters-numbers-image134691639

Build something like this from scratch, with 3D blocks, where we can use a parallax effect, and move around the background through perspective while we are in the main menu.

We could even drop in the cubes one by one to start off the menu titles screen. And also use sound effects to great effect.

The yellow background color for the menu screen is very lively for a design choice.

# Voxel Cars

https://assetstore.unity.com/detail/3d/vehicles/land/20-low-poly-ubic-voxel-cars-141966

This is a nice asset pack. Lots of sports cars here, which could be unlockable. The only thing is, the style is very distinct. So hard to mix and match.

Here are more voxel cars. Perhaps we could mix and match these two.

https://assetstore.unity.com/detail/3d/vehicles/land/simple-voxel-cars-111905

Here is a free one, we could use as a Ferrari.

https://assetstore.unity.com/detail/3d/vehicles/land/1-free-low-poly-cubic-voxel-cars-model-142626

Here is a pack for $5 we could use as passenger cars:

https://assetstore.unity.com/detail/3d/vehicles/land/1-free-low-poly-cubic-voxel-cars-model-142626

Here is another one, but not sure if we could mix and match this, since the resolution seems lower. The monster truck is a nice addition though.

https://assetstore.unity.com/detail/3d/twenty-one-voxel-vehicles-67841

But if we were to use this one, we have 33 voxel cars plus 21 here, so we would have 54, so we would be in really good shape.

Then for the cars we could have these custom parameters, top speed, acceleration, turn speed, and handling. That way the player feels like they are expanding their collection with different featured sets. People are hoarders by nature, so having 50 unlockable characters will give them plenty to do.

Don't know how I missed this, but this a really nice low poly city we could use to make a simple mobile Sim City type game.

https://assetstore.unity.com/detail/3d/low-poly-city-with-cars-65935

# Expanding on the Base Gameplay

To expand on the base gameplay and add more variety, we will need different obstacles. One of these obstacles can be construction cones, which if the player knocks over they lose points or have to start over. Another is a train passing, which we already have the assets for.

# Tomorrow's Work

Tomorrow we should work on things which are independent of the assets. So one of the big ones is environment.

Theoretically we could design the environments quickly before we get the pack.

So spend the whole day on putting the environments together. Once we get the asset pack, if we can change out our environments, (which of course should be easily possible), and add the spring follow to the camera, that should be huge. So theoretically within the next two days we could have the base playable game ready.

We will want to of course add as many of our own custom cars, and probably not reuse any art.

The other thing we can do is search for voxel packs, whether we are using that Unity asset pack, and even consider how we would go about making those. Adding voxel cars might also be doable immediatly post the release.

So let's just go straight to the playable shippable game, and not get stuck in any time sinks.

# Crossy Road Anniversary

The Crossy Road anniversary is coming up on 11 / 20. This is kind of crazy, but let's see if we can finish the game by then. And also release. Because Crossy Road made a lot of money in a very short period of time. Perhaps we can replicate this, and also have a very nice December. Plus we will get more sales. The average time to approve an app is 1 day, so perhaps we will even try to go for 11/19. Not sure if this is possible, but better to release early, and then we can always update the game. 

If those prototypes of complete games work, which they do, then we really do not have much to go over. Let's keep things as simple as possible and try to meet his release deadline.

# YouTube Trailer

We will need a YouTube trailer to show people in our press kit. So once we reach out to a YouTuber or Streamer, we want to include the YouTube link to the trailer, so they can immediately check out the gameplay and decide if that's something they are interested in.

# Neon Palmwave Scene

We want to do a scene with palm trees, and then neon lights, like in the synthwave videos.

# Lots of Voxel Art out there to Spice Things Up

There are lots of voxel models out there to spice things up, like for example, windmills in the background. We want to have lots of voxel art to provide variety for the player. Including birds flying overhead.

# Traffic Jams can be Fun

We want to have traffic jams, which is a unique thing not found in many other games. We want the player to experience getting stuck in a traffic jam, and then breaking free on the clear road.

# Monetization can be Highly Effective

Monetization can be highly effective in this game, if we design cool voxel cars. We also want to have many voxel cars available to unlock so the player can keep playing the game and keep watching reward video ads, generating income.

In Crossy Road they have 50 characters to unlock. For us with cars might be easier to create, so we should have as many as possible, although Crossy Road probably launched with far less.

Also study the time the player needs to grind to actually buy a character in Crossy Road. So let's say they get a few gold coins for watching a video. How many of those gold coins are required to unlock a car? Also follow this formula, to give the players plenty of grind, so they watch the most video ads possible without the experience being too grindy, and hence generating the most capital.

Driving a sports car at night, flooded by neon lights, is something very appealing. So we can definitely get a lot in app purchases going if we have appealing cars.

# Next Steps

The car camera feels amazing. And this is all because of the spring, since we don't have any turns set on the car. So once we combine this with the turns from the asset pack, that should work great.

So the next obviously include waiting for the two asset packs. We might get the third asset pack, if just for the model, but that might seem a waste. We can also get the moving chases, and the wheels turning from that third asset pack, which would make our controls complete.

So once we get the car controls complete, we are assuming the procedural generation, and the cars AI should work from the asset pack.

Now assuming we can plug everything in and get our code working, we want to complete these things:

Phase 1

- study purchased asset packs

- merge code together, including spring camera follow

So let's say we get through phase 1, and everything is working, then we have two main things to do:

- diversify AI traffic

- test procedural generation with new worlds

If we can get the AI cars and the terrain to procedurally generated, then this is a huge win.

Next up is plugging in the menus and the monetization techniques from the asset. That is a separate series of tasks, that we can accomplish without affecting the rest of the project.

To note on monetization, we want to have cool sports cars, like a lamborghini, and others that can be unlocked through the game. We also want to create different handling conditions, and top speeds for each of the cars. And also note the top speed in the menu unlock screen.

So if we can get all that done, we will be really close to shipping. At this point we want to have:

- a smoothly procedurally generated world

- smooth car controls

- diversified traffic, with bonus items dispersed throughout

- game loop

- menus / monetization in place

These 4 things are the pillars to get the game done. We don't have that much work, but we do want to get things to come together smoothly.

With those, the rest of the time we can spend on polish:

- create the neon city at dark

- choosing the right sounds and score



# Realistic Car Controller

https://assetstore.unity.com/packages/tools/physics/realistic-car-controller-16296

$50 but might be worth the price.

# Car Models and Style

The traffic racing complete kit had a very nice Lambourghini looking car. We could offer that to the player to buy for $3.99, or perhaps we could offer regular cars to unlock that much, and the  Lambourghini needs a lot of previous cars to be unlocked to get. So the player spends upwards of $100 to unlock the faster, luxiours cars.

Also this voxel style is very similar to the Lambourghini: https://sketchfab.com/3d-models/car-test-03-e78d694181bd415db00f9c64641dbd48

# Car Racing Template

The smooth camera follow made a HUGE difference to the feel of the gameplay. Also, that sets us apart from other "blocky" racing games.

Another thing we could eventually, is study all the different implementation, whether from assets or tutorials, to make our controls feel really good like in Celeste.

https://assetstore.unity.com/packages/templates/traffic-racing-template-26364


Very similar to ours:

https://assetstore.unity.com/packages/templates/traffic-racing-complete-kit-62954

This one has a very smooth chasis, we might also get this one on Friday: https://assetstore.unity.com/packages/templates/street-racing-engine-87494




# Solution to Shaky Camera Follow

One solution is to turn off the follow when we are near the player, and just set a constant follow, instead of calculating floating points, where our error might be coming from.

Also we want to redo the spring camera tutorial. And even before that we want to test the car asset, and add the camera to that.

# Bumping Chasis

Another thing we could do to add believability is putting some physics on the car chasis. Perhaps as we swing side to side, the car chasis will move with the vibrations. The spring camera is beginning to feel pretty good. So this would complement the effect even more.

# Camera Spring Code

Okay, so we have something that kind of works. Our main problem is that if we accelerate, then the camera stays in the back for some reason. And then when we decelerate, the camera catches up to the back. So the acceleration and deceleration changes in velocity are not picked as a position by the camera. So we are just forever chasing that one position in the back of the car, which might have to do with the stacked game objects in the hierarchy, and maybe the camera is following some parent object, which is not changing velocity.

Anyway, the other thing is that we want to place this code on the player, since that's where we will pick up our transform.position code. Also, have the main camera be seperate in the hierarchy and not as a subset of the player.

We might just have to rebuild the car prefab from scratch, following the spring tutorial.


        using System.Collections;
        using System.Collections.Generic;
        using UnityEngine;

        // starts at 24:00:  https://youtu.be/lCulq9J0Y9E?t=1446


        public class CameraSpring : MonoBehaviour
        {
            Vector3 moveCamTo;
            float spring;

            // public GameObject player;

            public float xOffset = 20.0f;
            float camOffset;
            Vector3 pos;


            private void Update()
            {
                spring = 0.96f; // what we will keep for the next frame



                // moveCamTo = transform.position - transform.forward * 10.0f + Vector3.up * 5.0f; // Vector3.up prevents the camera from getting flipped when player rolls and becomes upside down, instead of doing transform.up

                moveCamTo = transform.position - transform.forward * 1.0f + Vector3.up * 5.0f; // Vector3.up prevents the camera from getting flipped when player rolls and becomes upside down, instead of doing transform.up

                moveCamTo.x += xOffset;

                Camera.main.transform.position = Camera.main.transform.position * spring + moveCamTo * (1.0f - spring); // the spring equation, .5 would be average, explanation at 28:50, works like a spring the further away you are the more force you get

                // camOffset = Camera.main.transform.position.x - xOffset;

                // Camera.main.transform.position.x = camOffset;

                // pos = Camera.main.transform.position;
                // pos.x = Camera.main.transform.position.x - xOffset;
                // Camera.main.transform.position = pos;

                // pos = transform.position;
                // pos.x = transform.position.x + speed * Time.deltaTime;
                // transform.position = pos;


                Camera.main.transform.LookAt(transform.position + transform.forward * 30.0f); // look 30 meters forward, focal point is no longer looking at the car, but where the car is going with this 30 multiplier
            }


        }


# Top Speed

Try to gauge the top speed from this game:

https://www.youtube.com/watch?v=NJOJgGRh0hY


Seems like 25. We should not be going too much faster than this.


# Camera Spring

The camera spring mechanism is here at 18:00

https://www.youtube.com/watch?v=lCulq9J0Y9E&t=1740s

# Camera View

So we discovered there is another game, which has a different camera view. We might try to implement that camera view, because then we can see the car blocks better.

https://www.youtube.com/watch?v=NJOJgGRh0hY

Our original camera settings are:

pos: 

0, 4.75, -11

rotation:

0, -360, 0

scale:

1.25, 1.25, 1.25

The new settings on the camera are:

pos:

-3.7, 33, -10.8

rotation:

36, -360, 0

scale:

1.25, 1.25, 1.25

# This Solves the Competition Issue

This way we are rolling very loaded dice, and should be okay in terms of being successful.

1. Crossy Road Like Car Racer (Mobile)

2. Tenament Building Tamagotchi Simulation (Mobile) (Works on mobile, a simulation you can "take with you", like a pocket pet)

3. Sim City / Sim Esque simulation (Steam)

4. Space Game with the voxel space asset. (Eve meets League of Legends) (Steam or Mobile)

5. Dungeon Voxel VR


The catch is that all of these games are made with voxel art, so we can complete these games within short time scales.

And of course we would rather have "real" art than just voxel art, but with voxel art we can actually ship the game, and still get good sales, because people love voxels.




# Some Other Assets Similar to the Car Assets

https://assetstore.unity.com/packages/templates/packs/blocky-snake-135457 (this game has really smooth turn controls we could use)

https://assetstore.unity.com/packages/templates/packs/crashy-chasy-152150

https://assetstore.unity.com/packages/templates/packs/crossy-bridge-79760

https://assetstore.unity.com/packages/templates/packs/circle-crash-88702

https://assetstore.unity.com/packages/templates/packs/hovershift-107789

https://assetstore.unity.com/packages/templates/packs/bridges-79463

https://assetstore.unity.com/packages/templates/packs/sky-hover-ultimate-space-racing-117011 (Nice for space templates)

https://assetstore.unity.com/packages/templates/packs/snowy-skate-117905 (the score font looks nice)

https://assetstore.unity.com/packages/templates/packs/spacy-hunter-156190

This Spacy Hunter, actually has a very interesting template we could use. This would work for a MMO Space Game like Eve, where players could build and share their ships.

Generating universes, planets, and territories, would be a breeze. The combat is what we're after like Eve, and we have that here.

We could also introduce manufacturing and mining, more on the simulation side. Then people could team up and play together. This could even be a game for mobile.

The nice thing about the combat in this game is that we are fighting in a 2D plane, which makes combat easier and much more streamlined than space sims. In a way this solves our problems, since the combat would be much more like League of Legends.

So what if we made a game like Eve meets League of Legends?

That has all the formulas of success. And even though this might seem like a big game, we could finish this in three months.

Instead of letting the player build the ship, we would have preassembled ships, we can sell them or have them unlock with in app purchases, or simply a mechanism to unlock those in them.

Perhaps with so much competition one of the keys to being able to compete is simply offering the base game for free, and then making money with in game content. Since the people who charge for their game will always have a more challenging time of pushing through the inertia of high downloads.

Even if we release this game on Steam, we want to make the base game playable, with some sort of content we can sell through a DLC.

And then perhaps let the DLC people mix in with the regular people, so they get more jealous of their spaceships. Although running a server for free might cost some money. The best way of course is a subscription model like Eve.


#  Spline Train Tracks / Roads Asset

https://assetstore.unity.com/packages/tools/level-design/curvy-splines-7038


# How to Compete in a Crowded Marketplace

The game markets are becoming much more competitive. So we need to also increase our level of competition.

Here are some game ideas we could implement which would relatively have low competition

- Transcontinental

- Shopping Mall Sim on App store

If this game just does moderately well, then perhaps we should follow Steve Jobs' advice, and push out a game in 3 months, while our competitors take 2 years to make a game. This way we can have 8 products for every 1 of theirs.

I really like the grid layout in this game, Parkasaurus: https://store.steampowered.com/app/591460/Parkasaurus/

This would work well for a mall simulator.

But we have to be aware that a huge signficant chunk of gamers are women. The average age of a female gamer is 38. So making a mall sim game is one step in harnessing that audience. Further steps would be to make more "adult" like games for older female gamers.

Another genre of games, which could be viable would be anything VR.

Perhaps we should just focus on the Transcontinental game after this. Because we are working in a very narrowly defined niche genre. For example, if you make a science fiction game (or fantasy), then even though the game might be different from others, the subject matter takes less precedence and is overshaded by the genre, so we are automatically competing with all the other science fiction games.

But on the other hand if we make a transcontinental railroad genre game, then we are only competing with transcontinental rail road games, which are a handful, and we also have security in knowing that the train genre is an evergreen viable genre.

The other thing we can take advantage of with the Transcontinental game is the Unreal engine. Since competition is getting more fierce, we can at least blow all the Unity developers out of the water, by simply using the Unreal engine. Then with the sparse environment of the continental terrain back then, and a properly modeled train, we can have that scene at least look like a triple A game, which is huge. We can also charge more money for that, provided we have enough content. So this is a very viable game venture.

Another potentially viable game idea would be too do a farming sim in the style of Factorio. So you are gathering resources, and growing stuff.

We have to go through all of our game ideas and pick our strongest ideas, and complete a game every 3 months, which is a huge undertaking, but in the end might be the easiest way of finding success.

Another game genre which is hugely viable is anything with city builders or sims. We could make a game focused just on something like the Sims. This would go along well with our simulation tastes.

Also, we should not be afraid to "copy" a game. There is a lot of competition, and many developers are disheartened because they want to be truly original. Well, don't let this stop you from making a game. Better to take a good idea, improve upon that, and have a financial success then struggle with qualms of originality, while no one truly cares.

Another thing, if ultimately we are fearful if success is possible, then we should be reminded of what we really love to design -- and that's simulation games. Simulation games will always be a viable genre for an indie game developer. They are the type of games that are too small for a big game company to take a chance on, which starves the market for good content. So if we stick with the simulation genre like Sid Meier we will be alright. And similarly the Transcontinental game is a simulation game.

Another way to compete is to simply have the best art. Of course this is hard to do, but if we are spending years struggling as an indie, we might as well take the little effor to improve the art style.

There are also pockets of opportunity on the app store, since most quality games are going to Steam, and lots of shovelware is being uploaded to the app store. So a good game has higher chance of standing out the App store than on Steam, where you are faced with truly strong competition. Of course the downside is that there are many more games being released on the App store, but we might still be able to get into those pockets of opportunity, if we create a quality game in 3 months.

If game does relatively well, like 50K, then we might make another small game in 3 months, before embarking on the Trancontinental game.

Another way of competing is taking a game that people really want to play, a game which is underserved, and then build a very minimum viable product version of that game. So for example take the Sims, and turn that into a simulation game where you caring for people all in one tenament building. Of course those games have been done, but don't let that prevent a good idea getting off the ground.

We could also combine ideas from Tamagotchi pets, where if we let our residents roam around, then we can come back and check what happened to them. So we would also have Roguelike elements here.

We could also have the parallax effect from the Thomas Brush tutorial, when the player moves the camera side to side, and we see the foliage and the mountains move in the background with the parallax effect. Perhaps we can use a skeleton model to animate characters.

One way would be to pay an artist. But perhaps instead of this, we should make a voxel game, since we can guarantee all the art and work towards completion in three months. Plus in 3D we could leverage all of the physics engine. And a game in 3D will generally sell more than a game in 2D.

Also once we get the process down for one building, we can release the game, and then the next game could be a version of the sims in block style, which we could even release on Steam. So something like Factory Town and that other Kuberfactorium game, but with voxels, and very engaging simulation type gameplay.

This seems like a game, which would be bound to do well, especially among the target demographic of middle aged women. They would much rather play a game like this, than a science fiction game, narrowing out much of our competition.





# Interesting Article on Autonomous Agents and Physics Forces Applied to Cars

https://natureofcode.com/book/chapter-6-autonomous-agents/


# Another Amazing Asset 

This one has a good coins screen, and also an unlockable car screen.

https://assetstore.unity.com/detail/templates/packs/crashy-racing-115748



# ToDo List until Friday in Light of the Asset Pack

So the asset pack has a lot of the stuff done for us, so we will have a little different ToDo list.

The good news is that we have the game running on mobile with swipe controls. Even though the movement is jagged and too quantized, the controls do work for testing the game.

So what should we move on to, knowing the asset pack might have us redo major portions of our game. Like the procedural generation and the car movement controls?

We probably then should not work on procedural generation yet, or the car movement controls, since we will want to check the asswet code first.

The spring camera will be nice. So as the player's car speeds up and turns, we will experiment with a spring on the camera. We will also do this after the asset pack.

We might want to have a speed up meter on the right hand side. Just a narrow thin strip, which we can drag quickly up to set the speed. We want to be able to reach top velocity quickly, and also reach the slow velocity quickly. Even though we might need time to accelerate we want the user to reach these speeds quicker, instead of continous tapping. This will be essentially when the player reaches a traffic jam and wants to slow down.

Well, there actually is not much we can do until we get that asset, because so many of the pieces are tied together. So for example if we start working on AI, they might have a totally different AI system. They probably don't have anything fancy which results in congested traffic, but we will want to build that on top of their existing system.

One of the things we can do is study the Crossy Road GDC talk, take notes, and take any steps required. 

Also what we might spend our time on then is Game Design. There is lots of design work we can do, of course this might not involve a lot of programming.

# Amazing Asset Pack

This asset pack does so much of what we are trying to do in the game. Buy this on Friday ($19)

https://assetstore.unity.com/packages/templates/packs/rushy-racing-119676


# Nice Voxel Style Car

https://assetstore.unity.com/?price=0-0&q=mobile%20car&orderBy=0

# Mobile Controls

Here is the code for the new mobile controls. This gets the job done but we will need to spend a lot of time optimizing this and smoothing out the controls, like in Celeste:

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

# Black Lines

The black lines are most likely the result of a shader problem.

"Make sure you're set to Point filtering mode, make sure yo'u using textures with power-of-2 dimensions, and make sure you're only placing them in position that are multiples of 0.02, so set your snap settings for example to 0.16 if you have a 100 pixels to unit ratio. Or set your pixel ration to something like 64 and just move it by single units or half units."

https://forum.unity.com/threads/black-lines-on-the-edges-of-tiled-sprites.226919/


# Organizing ToDos

Okay, so let's not stress out too much. Everything we work on eventually gets completed with no major issues, so we can just relax.

Also, what we write down here gets done. So just write up the things to get done in the right order.


We have a few to do lists here so aggregate them into one master list.

Get them in here, and then rank them afterward.

Todo:

- get game to compile on phone

- set the turn of the car by swiping the screen (invisible controls)

- dig through this document for todos

- rank the todos


So we will copy and paste a bunch of ToDos. But before going there let's just work a little more freer on some stuff.














# Steve Jobs On the Competitive Advantage in Software vs Hardware





“We’re going to knock them out of the box (Sun only people doing this) with Next Step, which lets you build apps 5 times faster. Once you build them they are deployable by mere mortals. 

- Steve Jobs 09:00 

https://www.youtube.com/watch?v=YXUhLbV8Nrg&t=2567s



We go to these companies  that take 2 years to write apps and they can write their apps in about 90s days. If you can create a new product in 90 days versus your competitor in two years -- that’s eight new products you can field for their every one. And you can start to see the competitive advantage being created this way. 

A lot of times you don’t know what your competitive advantage is when you’re creating a product.

We never anticipated desktop publishing, which turned out to be a competitive advantage. Maybe we weren’t smart enough. But we were smart enough to see that happening 9 - 12 months later.

We changed our entire strategy to focus on desktop publishing. And that became the thing which got the Mac into corporate America, where we could show the owners all the other wonderful things.

The purpose is to create apps 5 - 10 times faster.

The commercial apps also allow you to build your own mission critical apps 5 - 10 times faster.
 
This is the biggest problem for every big company and almost all medium sized companies, and you have have a solution in your hands and you dummies don’t even know.

And we changed our sales and marketing strategy and we grew 4X.

We’re talking to customers we wouldn’t have dreamed of talking to us.


Hardware churns every 18 months. Pretty impossible to get a sustained competitive advantage out of hardware. (13:18)

Lucky if you can make something 1.5 or 2 times as much as your competitor, which is not enough to be quite a competitive advantage, and that only lasts for 6 months. But software seems to take a lot longer for people to catch up with. Even 10 years.


The soonest we will have a true competitor will be in 4 to 5 years.


So we have that amount of time to grow, to compete on scale. Today we cannot compete on scale. We never have as many sales people. We don’t have the ad budgets. So we gotta have a better product. (14:00)


So we have the next 3 - 4 years to run really fast where we are at a large enough scale so we can compete. And that's what we are doing with our lives. Spending a lot of time with custmers. Spending a lot of time making Next Step better. That's the strategic basis of what we do. Does that make any sense to you?

Have you run across the concept of custom operational applications? 

Do you have this problem in companies you come from? Pressure to write custom operational applications and very little available. Manufacturing, finance.

Without owning something over an extended period of time, like a few years, where one has a chance where one has a chance to take responsibility for one's recommendations, where one has to see one's recommendations through all action stages and accumulate scar tissue for the mistakes and pick oneself off the ground and dust one self of, one learns a fraction of what one can. Not owning the results, not owning the implementation is a fraction of the value and the fraction of the opportunity to learn. 


# Plans





Make highway lanes narrower


Use lots of generic voxel art for decorations, for example, a plane flying overhead




Show final score in large font at the end of a run


Add game over state


Will look a lot nicer with more variety, chicken pens, etc

Even deserts will totally change up things


World still looks a little barren, there is lots we can add


Nighttime mode will be great


Pressing shift turns up the break lights

Add spring on car BEFORE spending too much time on controls, will change perspective


Also put yourself in the mindset of what a player would want


Follow John Romero tips:

- encapsulate functionality

- no prototypes

- fix bugs right away

- don’t build on a shaky foundation



Some immediate things that will make the game better:

- more environments

- game over state

- show score at the end

- have cows mooing 

- have advanced AI with rays on font so they don’t run into the player

- have traffic jams where the player really has to slow down or they lose

- one hit and you have to start over, like Crossy Road

- city blocks

- flying airplane

- overhead pass sign comes up rarer, is more of a spectacle

- detect when we flip over or get hit by a car to end the game run

- flash the car when we go out of bounds

- a sunset

- neon city with a sunset!



Since this is a semi fantastical game in a very general genre, we can get away with having a synth pop style cyberpunk like city… This would look awesome


Don’t take for granted the monetization strategy from Crossy Road, these ideas are like forms, if you use them, they work.



Getting a high score will feel better when the game is more challenging, when there are more cars you have to watch out for


Leave one lane open for testing, so can just drift through the world with no damage, have a testing state for this, but in the normal game you can lose much quicker than this, so perhaps we will open up the grass lane right to the right of the traffic, since the AI traffic will still occupy the right lane and we have to watch for any traffic on the road to make things simple


When the time comes to marketing, then reach out to all the admins in discord of the servers of popular streamers, especially of app games, and then they can communicate to the community about the game, and ask them to show the streamer, or send the press kit to an admin


Once we narrow down the highway, we should add lanes

We could have a water level, where we take the platform and lower the y coordinate, and then we can have voxel ships down under…


If the game is free to play we can get a lot of downloads… That is key. Another form(ula) from Crossy Road.

Especially if we release the game on Android and iOS we will get maximum traction. And then also concurrently releasing on Chinese stores…

Need a clear contract. Refuse to sign any abusive contracts.


For Chinese stores, will need to localize. That’s why good to keep all the string stuff in the file Constants.cs, and then even in Localization.cs

Where Localization.cs looks at Settings.cs, and then determines current language. If that language is chosen then all the strings in Localization are chosen from a subset of that language. 

We can name the variables in Localization.cs like: someDialogueEnglish, someDialogueChinese


This game is more than long enough to fill a casual gaming spot, and also even more, so we are okay. Let’s just polish the core game loop.


Polish as you go!


The big overpass sign made a huge difference


Having an ocean and seagulls will be amazing… And the sound of the wind…

Sounds can make a huge difference.

Sounds is one of the least effort things to do to get the most effect.

We will want some sound of a machine, not necessarily of an engine, but some hum to represent the car speeding up, might have to take a sound sample and pitch up and down in a sound production software, or even record the exponential pitch up and down to play those when the player steps on the gas, quantize them to have a set we can use in a range of speed.


Beating your high score will feel really good for the player. Also have local leaderboards. To show the player how people did in the local area. 

We will need to get data on how all people did and then pool them together and divide evenly by the areas. 

Not sure about the leaderboard thing? Does Crossy Road do that? All we need is what Crossy Road did.

Birds flying by is one of those touches that can really bring out the game, like salt in a dish. This goes for clouds, and aircraft also.



This is a video game, not business software. We can make the experience really magical for the user.

Like for example the cows mooing. That is priceless. Especially that the effort required to put a sound clip together is minimal.


Let’s also have some bird sounds, when different types of birds fly overhead. A few different types of birds is like using sea salt in a dish, combined with other rare salts.



Determine the time in minutes, because this is very specific, of when we transition into the next level, which will either be desert or the city, perhaps desert, and then after desert grasslands again but with city, and then city in the dark after that, then perhaps grasslands and or desert in the dark





Level 1 - Grasslands

Level 2 - Desert

Level 3 - Grasslands city

Level 4 - Neon city

Level 5 - Grasslands in the night

Level 6 - Desert in the dark 


Repeat


Transition slowly into the game from the menu. So when the player selects continue or next, slowly fade the screen out and fade in, a very pleasant transition


# A Natural Barrier

Maybe instead of the concrete fasad for the barrier of the sides the road, we can instead use the natural response of traction of riding on grass / dirt. So we would apply some force where the car is more likely to slip out of control, and then flash the car if they venture out into the terrain.

# A Local Leaderboard

A local leaderboard is very important.


# Perhaps use Pyramids

Perhaps use those desert pyramids, or probably not. Too out there.

# Show a Dragon or some other Fantastical Creature once in a Long Time

Like Sim City, who showed the Loch Ness if you played a lot, do that also. Show some kind of creature if the player plays for a very long time. A type of Easter Egg.

# Car Turning - Controller from Scratch

We want to see if we can make the controller from scratch. So let's see if adding gentle turning, and other mechanisms, perhaps ones we study from the Internet, can make our own car controller to give us full control, and give the player maximum comfort.


# Button for Cruise Control

Let's have a dedicated button that when toggled activates cruise control mode for the player. So we can do both the tapping acceleration and the cruise control mode.

# End Sequence

At the end do a replay of the final action scene, or a zoom out, panning the camera around. Use cinemachine to do this.


# Do 3 Lanes Like Real Traffic

Do 3 lanes like real traffic, and have the cars drive slower in the slow lanes, average in the medium lane, and generally fast in the left lane, but vary. Plan this well on the AI cars so this is not a hassle.


# An Even Simpler Way to Procedurally Generate Terrain

- create larger tiles (50 x 2, or more)

- create an array of a bunch of variations per one of the 4 biodomes (1. grasslands 2. desert 3. snow 4. city), generate 4 of those variation per biodome

- keep things in multiples of 64, the tiles came pregenerated at a 50 snap resoultion. So that's fine. We can use a system of 10s and 50s instead of multioples of 4 for this one. But generating the next biodomes we can still subdivide into 4 segments per each atomic biome generation

- then after 4 generate the next 4 tiles belong to the next biodome


# A Simple Way to Procedurally Generate Terrain

We can use that for loop code while picking out of a random array of gameobjects. Look this code up if we have to do too much refactoring.

So we then have let's say 4 chunks in a big, each with their own predesigned prefab decorations. Then every four blocks we will be randomly a different one in the series. That will add a lot of variety to start, and starts developing our code for more procedural generation.

In the future we will have a for loop inside a for loop to go through the biomes likes this:


for(<pick random biome>)
        for (<pick random tile from biome>)
        
Or we could do something like:

variable array = <pick random biome>
        
out of that variable go through and for (<pick random tile from biome>)


# Bug

We might have some weird bug to do with Garbage.cs (can turn off to test) and the cars are stoppig to spawn after a few tiles.

# Scoring

Here is how we did scoring. We put an is trigger collider wall on the car. Then on the same parent game object we put this code:

        using System.Collections;
        using System.Collections.Generic;
        using UnityEngine;




        public class Score : MonoBehaviour
        {

            float reward;


            // for some reason on trigger enter works better than on exit

            private void OnTriggerEnter(Collider c)
            {
                if (c.gameObject.tag == "AI")
                {
                    reward = UI.Instance.GetScore() + 100.0f;
                    UI.Instance.SetScore(reward);
                }
            }
        }


We made a new method in UI to allow for this:

        public float GetScore()
            {
                return score;
            }

# Rebuilding Colliders

Make sure that when you are rebuilding the tiles to do a solid job on rebuilding the colliders, so we can package them away like a black box with no errors or hassle.

# Box Collider on the AI Cars for Score Tracking

This is the easiest and most elegant method. We are making a "wall" box collider on the car, perpendicular to the player. So if the player passes an AI car, they will pass through this "wall" and collect points. For the code implementation we will use OnExitCollider(<AI car wall collider>)
    
We might have a potential problem with zeroing out the model... Always zero out the transforms on both the parent and child prefabs... The other problem is only drag prefabs with 0, 0, 0 from the hierarchy to the project window, because often times we are placing the prefabs in game world space, and then dragging that world space and overriting the original 0, 0, 0.


# Car Controls

We can always try to get car controls off the asset store. Or study other car controls for code. This will take a few tries.

# Forcing the Player to Play More Defensively like Dark Souls

We want to create the type of gameplay that if the player is greedy they are punished. In our case, since we are offering points for passing cars, if the player gets greedy they will begin to go really fast to get more points, but then if there is a congestion or more traffic, then the player will lose. The thing is experienced players will learn to take advantage of the system (we gently want to teach them), and they will go fast when they can to get the most points, and then detect a wave of traffic and slow down. To be fair we will not have random traffic. We will have a random chance of a traffic wave. So the action is atomic.

# Random Stretches of Traffic and Congestion

We want to have random stretches (waves) of traffic so the player has to slow down. But the traffic feels like a wave to the player so they can then speed up and take over the road for another atomic block of slower traffic.



# Setting a Cruise Speed

Here we are setting a cruise speed at the maximum acceleration. We don't have the break accounting for this yet.

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
     public float maxSpeed = 10.0f; 
     public float acceleration = 30.0f;
     public float deceleration = 10.0f;
     public float breakDeceleration = 50.0f;

        // turning

     public float turnSpeed = 10.0f;
     public float turnLeftBoundary = 0.0f;
     public float turnRightBoundary = 0.0f;

        private float maxCruiseSpeed = 0.0f;


     Vector3 pos;





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
            if (speed > maxCruiseSpeed) maxCruiseSpeed = speed;




      // transform.position.x = transform.position.x + speed * Time.deltaTime;
      pos = transform.position;
            // pos.x = transform.position.x + speed * Time.deltaTime;
            pos.x = transform.position.x + maxCruiseSpeed * Time.deltaTime;

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



     void RunMovementRoutines()
     {
      Move();
      Turn();
            Break();
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




     void Update()
     {
      RunMovementRoutines();

            CheckForClose();


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

# Some Major Things to Do from Now

Here are some major things to do from here:

- create scoring system, if we run past the collider we get 100 points for passing the car

- add different types of cars

- add different speeds to the cars

- generate cars from further away, perahps fixed with simply making the tile larger

- add object pooling system for efficiency

- create four lane high way

- create biomes

- create procedural generation for those biomes

- work on smoothing out the controls

- add menu screen, and mock monitazation screen like Crossy Road

- also add please rate us type thing like Crossy Road, so we amass much more reviews, and as a result, traffic

- straighten out help with a lerp function if the car moves out of view

- add some camera spring

- add glowing lights in the dark at nighttime

- add headlights to the cars to light up the darkness

- darkness can fall over farmfields also, then we have to rely on our flashlight

- edit miles per hour display, perhaps convert to a meter to show a visual display, or just have the double digit number rounded in nice big letters. Make that look aesthetic.

- make sure camera always returns to the cars center position, so the car through physics can't be knocked off the center of the screen

- add the VoxelPlay clouds, some skies will have clouds, some will not, we can also color code the clouds, especially in neon glow futurewave settings

- adding voxel style buildings, and lights to light them up in the darkness

- add sounds of cows mooing when we drive past them

- randomize the frequency of traffic, allow some wide open stretches of road, especially in the country

- create a central AI.cs which will handle the generation of cars, and their frequency, and any other patterns

- test and build out mobile controls

- review the Crossy Road GDC 

- instead of the 30 second garbage collect on the cars, we need a player vicinity is trigger collider, because for example if we follow the car for longer than 30 seconds, then the car disappears and is garbage collected

- put curbs on the side to punish the player for going out of bounds, they can slow down and then ride over the curb to go explore, but then flash the car, and reset their position, (game over)

- detect if we are upside down, flash the car, then end the game, same goes for AI

- add random breaking to AI cars, and also have the taillights signal on, that would look cool in the darkness, perhaps even increase the frequency in the darkness

# Cars Coming from Back of Player

If the player slows down, we want cars to come from the back, especially on a four lane road.


# Breaking Visuals

We want to apply some dust as to when the player breaks. Perahps this is not too visually intensive on mobile.

# Breaking

We want to have some sort of more sensitive breaking. For now we want to separate out deceleartion into breaking.

(Later: we will also want to have some more sensitive system which breaks quicker (decreases the acceleration quicker) at high speeds, because at high speeds takes us much longer to slow down than at lower speeds.

So we do not want to mess with the general deceleration built into the code because there are lots of if else bad code. We will separate this into a state machine later. We will definitely have to seperate out the Control.cs of the car into a state machine, because all sorts of complex things could happen concurrently.

So for now to fix breaking, we will create a new breaking variable: public float breakDeceleration = <something higher than the regular deceleration>
 
 And then we will have our own method, Break(), which will apply the breaks.
 
 That worked:
 
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



     void RunMovementRoutines()
     {
      Move();
      Turn();
            Break();
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




     void Update()
     {
      RunMovementRoutines();

            CheckForClose();


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


# Cars Stop Coming when Player Stops Driving

Since when the player stops driving there are no more new procedural tiles generating with incoming traffic. In this case we will need to manually keep spawning cars in the direction of the player. Then up to a few cars, because if the player just sits there they will create a traffic jam. If you create a traffic jam for too long you lose the game, and get the game over screen. We can do a holographic albedo flash on the car with an increasing frequency and sound to alert the player something is wrong, in this case they are blocking up the traffic.


# Set Up the Player

To set up the player:

- create parent player prefab, position this where the player will start

- create a "Car" prefab right under the player. Attach a "Controls" script and set the speed and acceleration and other controls

- Under the Car prefab have the Model prefab which contains the mesh renderer and box collider for the car, this is the car object form the asset pack.

- drag the camera under the Car prefab. Having the camera here will will follow the car around

# Driving at Night

Driving at night we want to go for lots of neon colors, futurewave.

# Chunks

We want to do a few things with chunk. We want to first off make chunk two times bigger if not more, so things appear farther in the horizon. Also we want to decrease the garbage collection time to at least 20 secondds.

There are two ways of solving the garbage collection issue, either time or colliders on the player. We have a separate is trigger collider, so if we are within the players very large is trigger collider then don't destroy. Yes, this solution is better than the timed one, and works very well.

Perhaps later we will want to have some "emergency" script which checks if the player's position fell below the floor and then instantiate them on top of the world again. Only in rare cases this might happen, but I think the 30 second time limit solved the disappearing floor bug, but further the is trigger collider check in the if (!playerOnGround) should solve this bug.

We will also want different styled chunks. Almost like a "megafauna" of chunks. We want to do this the simplest, most elegant, and least hassle way. One way of doing this would be to organize chunks by biome.

So biome 1 chunks are the green grasslands. Then these chunks get the associated "random" elements, such as trees, and bushes etc.

Then biome 2 chunks let's say are farmland. Then we can take trees from 1, since those will be common, bushes, but then also animals, and fences.

Then if we are driving through the country the procedural generated could do all the biomes that fit into the larger parent class, "the great outdoors" or something. So then the procedurul parent would do: biome 2, biome 1, biome 2, biome 2, etc...  more random biomes in the division, "the great outdoors."

And then with the biomes organized, we could do custom iterations.

We also want to be able to take all the levels and throw them into one giant procedurally generated level. So we will have grasslands, going into desert, going into cities, going into cities at night with neon lighting, etc. So a big giant loop like Crossy Road. And also something which maintains flow for the player. Always be consious of the flow created by the game for the player.


# Setting Up the Car AI

Setting up the Car AI was a little tricky but we figured out a clean and elegant way.

The main point is: we want to create an individual prefab for both the "Left Lane AI Car", and the "Right Lane AI Car". Later these can be more specific, but we just want a single prefab that will work, a black box we don't have to worry about.

So when creating these for the first time (Attach CarAI.cs to both the Left Lane and Right Lane AI car objects), set the forward bool to true on the Left Lane AI Car, and set the forward bool to false on the Right Lane AI car.

Don't forget to put the garbage collector on the AI cars also to prevent memory leaks.
 

Then drag these to the purely visual game object "Spawn Spot" into the spawn slot. The Spawn Spot prefab will have the "Spawn" script attached, which provides the slot.

All of the Spawn Spots are equal child members of "Chunk", which is the main grass big tile.




# Variable Reward Saves the Day Roguelite Style

We want the player to experience that RNG moment, where they are hoping they get the variable reward to help them carry on through. So one of the main ones will be "potions". Like the items in Mario Kart.


# Next Up

So we will save the below extra check to not destroy the last tile placed. There are a number of brute force ways to do this, so we will just save this for later.

Now next up is adding the car AI. We will want the car AI to be spawned on top of the tiles, and give them an initial velocity.

We could attach a script to each of them that simply says, "Car", and of course this will not be applied to the player.

So then the car will start moving right away in a straight line, which really helps us to narrow down any complexity.

At first we just want to spawn a car. Do everything step by step like John Romero.

So after we spawn the cars we will want to move them, and then after that add colliders for detecting collisions, and implement game over state. Also we want to implement the scoring system. The way we can do this is to put a narrow thin long collider, once the player passes this collider on any of the cars they get awarded 100 points for passing them.

So in order:

- spawn cars on tiles

- assign cars initial velocity

- add colliders for detecting collisions

- implement game over state

- score




# Fixing the "Don't Destroy Tile that the Player is Resting On"

So we fixed the problem where we were destroying the tile the player was resting on. However if the player comes to a stop the tile in front of them gets destroyed after ten seconds. So we want to add an extra check to if (!playerOnTile && DontDestroyTileInFront)

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;




    public class Garbage : MonoBehaviour
    {


        public float LifeTime = 10f;


        bool playerOnTile;





        void Start()
        {
            Invoke("DestroyObject", LifeTime);
        }



        void DestroyObject()
        {
            if (Game.Instance.GameState != GameState.Dead)
            {
                if (!playerOnTile) 
                {
                    Debug.Log("Destroying game object");
                    Destroy(gameObject);
                }
            }
        }



        // detect whether the player is in the collider so we don't destroy the tile the player is resting on

        private void OnTriggerEnter(Collider c)
        {
            if (c.gameObject.tag == Constants.PlayerTag)
            {
                playerOnTile = true;
            }
        }



        private void OnTriggerExit(Collider c)
        {
            if (c.gameObject.tag == Constants.PlayerTag)
            {
                playerOnTile = false;
            }

        }



    }


# Two Things

So we actually had a hidden bug in the below code with this.transform.

Since everything was getting put onto the parent game object with this.transform, we were deleting the entire parent and hence all the generated chunks. Taking out this.transform fixed this.

So the code is now working with garbage collection, which is really good.

However we have one bug. At the end of the procedural generation if we remain on the tile, then the current tile will get destroyed. We need to fix this and somehow detect that the player is still there, so don't destroy until the player leaves the chunk tile.


# Garbage Collection

Now after getting the first draft of the procedural system working, the immediate next step is to employ garbage collection.

We will be using time based garbage collection.

So simply drag this script onto our "Chunk" object, which also contains "Procedural.cs"

After the x LifeTime in seconds, the chunk will auto clean up.



    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;




    public class Garbage : MonoBehaviour
    {

      public float LifeTime = 10f;

      void Start()
      {
        Invoke("DestroyObject", LifeTime);
      }

      void DestroyObject()
      {
        if (Game.Instance.GameState != GameState.Dead)
          Destroy(gameObject);
      }


    }


# First Draft of Procedural Code Working!

  

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


                    Instantiate(chunk, pos, Quaternion.Euler(new Vector3(0, 90, 0)), this.transform);
                }
            }
        }


# Rewriting the N + 1 Code

The N + 1 code is very good, but we can rewrite this just to fit our needs since we will not need multiple path spawn points for the different turns, at least for now.


        // https://github.com/dgkanatsios/InfiniteRunner3D/blob/master/Assets/Scripts/PathSpawnCollider.cs


        using UnityEngine;
        using System.Collections;

        /*
        
        Attach to chunk object, this will talk to the collider that is attached.
        When the player enters the collider this spawns another object.
        
        */
        
        
        public class Procedural : MonoBehaviour {
        
        
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
        
        


# N + 1 Level Generation

So we are generating one level ahead of the player, and this is very simple to do since we are only moving forward. This is good practice also for creating a procedurally generated explorable 360 universe, such as a space sim. The nice thing about procedural generation is that we can truly generate infinite worlds, which is something very attractive to the player. And the process is not especially difficult, just figuring out how to do things for the first time.

So going back to this statement in part 2: 


"Recall that Max runs from path to path. As we are not generating all paths at the beginning of the game (because we want to save on memory and mainly because we do not know how far in the game the player will proceed, i.e. how many paths to generate), we need a mechanism to generate the N+1 path, where N is the path that Max currently steps on. We’ve used a simple trigger BoxCollider to implement this. When Max collides with it, a new path is generated via the PathSpawnCollider script (described in a while). In the straight paths level, the new path is instantiated in the “NewPathSpawn” position, which conveniently happens to be positioned at the far end of the current path."

So the "NewPathSpawn" position is the key here that begins the generation of the next level.


https://dgkanatsios.com/2016/03/09/creating-an-infinite-3d-runner-game-in-unity-like-temple-run-subway-surfers-part-2/


The key is this script, "PathSpawnCollider", which is used in both the straight and the curved level.


        // https://github.com/dgkanatsios/InfiniteRunner3D/blob/master/Assets/Scripts/PathSpawnCollider.cs


        using UnityEngine;
        using System.Collections;

        public class PathSpawnCollider : MonoBehaviour {

            public float positionY = 0.81f;
            public Transform[] PathSpawnPoints;
            public GameObject Path;
            public GameObject DangerousBorder;

            void OnTriggerEnter(Collider hit)
            {
                //player has hit the collider
                if (hit.gameObject.tag == Constants.PlayerTag)
                {
                    //find whether the next path will be straight, left or right
                    int randomSpawnPoint = Random.Range(0, PathSpawnPoints.Length);
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

        }
        
        
        /*
        – PositionY is used to properly place the redBorder on the Y axis
        – The PathSpawnPoints array is used to host the locations that the next path and borders will be instantiated. In the “straight paths” level the array will have only one member (since we’ll only instantiate the next path) whereas in the “rotated paths” level the array will hold three locations, in one of which there will be the new path and in the rest two there will be the red borders that will kill Max upon collision
        – The Path object holds the path prefab
        – The DangerousBorder array holds the RedBorder prefab in the “rotated paths” level whereas it is null in the “straight path” level (where we do not need it)
        
        When Max collides with the PathSpawnCollider game object, game engine randomly chooses whether the next path will be straight, left or right. In the case of the “straight paths” level, we have only one entry in the PathSpawnPoints array (which corresponds to the straight location), so randomSpawnPoint will be 0 and the next path will be instatiated at the straight location. In the “rotated paths” level, we instatiate the next path on the chosen location and we also instantiate the RedBorder prefabs on the other two locations, while we are rotating them by 90 degrees to make them fit properly.
        
        
        */



# Monetization

The monetization might slip 50 / 50, with 50 for rewarded video ads, (give in game reward of some sort like Crossy Road, just use their formula)

And also have the cars be unlockable like the characters from Crossy Road. Make them the models feel very appealing and hipster, spinning against a background, everything color themed for effect.



# Making the Gameplay Addictive

I remember wondering a while ago what made a game addicting, and researching online did not help much. But now we have a much clearer idea of what makes a game addictive:

- goals
- variable reward
- challenging gameplay

We want to have lots of variable reward along with very challenging gameplay, which becomes another goal for the player -- to get past the challenge.


# Part 2 of the Infinite Runner Tutorial - Level Generation on the Fly

"Recall that Max runs from path to path. As we are not generating all paths at the beginning of the game (because we want to save on memory and mainly because we do not know how far in the game the player will proceed, i.e. how many paths to generate), we need a mechanism to generate the N+1 path, where N is the path that Max currently steps on. We’ve used a simple trigger BoxCollider to implement this. When Max collides with it, a new path is generated via the PathSpawnCollider script (described in a while). In the straight paths level, the new path is instantiated in the “NewPathSpawn” position, which conveniently happens to be positioned at the far end of the current path."

So the "NewPathSpawn" position is the key here that begins the generation of the next level.


# Went through Part 1 of the Infinite Runner Tutorial

So we just went through part of the infinite runner tutorial.

There were lots of helpful stuff there, which we implemented.

One of the cool things is we found out we could simply declare our State.cs without a class:

        using System.Collections;
        using System.Collections.Generic;
        using UnityEngine;




        public enum GameState
        {
            Start,
            Playing,
            Dead
        }


That's all! We might not even need the includes up on top.

Then in Game.cs (GameManager.cs from the tutorial) we can reference the enum with:

        GameState = GameState.Start;
        
That's all! That easy.


So we also implemented UI, just like the author, since the UI implementation is rather easy.

Then there is a Gold.cs script, which is the transformation of the author's Candy.cs script. This will take care of the spinning gold points, and collection functionality. Not sure yet if we will have pickable gold coins in the level. Maybe powerups instead, a combination of both or neither.

Then there is an Obstacle.cs script, which will handle any collisions which trigger the end of the game. In our case an obstacle will be a car, and later we can expand this out to the side of the road also, which ends the game.

There is also a GarbageCollection.cs script, which the author originally named Destroyer or something. This script will destroy objects when after some time. So we will have to make sure we are destroying things correctly. Also we need to eventually implement object pooling. #TODO

So that's pretty much all for part 1 of the tutorial. In part 2 we begin to generate the infinite runner.

Read over this, and then in order to get ready, download ProGrids, and come up with the tile chunks we will be using for the infinite runner. We also want to have four lanes of traffic to start, since this is a much better experience. Perhaps even more lanes as the player advances through the levels, which should not be hard to implement. So perhaps each level has a beginning easy mode, and then a harder mode as the lanes move wider, or perhaps the lane width varies.

The author shows also how to do corners, which detect and attach at the specific end of the next segment. This specific attachment functionality we could apply to having the highway diverge into more lanes. This might be too much for MVP. We might just keep this to 4 lanes for now.

Okay so that's all for part 1. We will download ProGrids, do the stuff above, and then be ready to create the procedural levels. At least now we have our game instance and data instances set up, along with our obstacles script.




# Going through the Infinite Runner 3D Codebase tutorial

https://github.com/dgkanatsios/InfiniteRunner3D

Part 1: https://dgkanatsios.com/2016/03/07/creating-an-infinite-3d-runner-game-in-unity-like-temple-run-subway-surfers-part-1/


# Infinite Runner 3D Codebase

So going through infinite runners we found a very simple and elegant tutorial: https://github.com/dgkanatsios/InfiniteRunner3D

So before going through our regular steps, we should probably go through this code first, since there are a lot of helpful things in here, including setting up our Game Manager, and our State.

We will do this as: Game.cs, and State.cs (changed from data), since that just seems to flow better.

So let's go through this tutorial and jot down all the important stuff.

At the end we should have an infinite procedurally generated level.

Of course we want to still go through other tutorials, but this will take some time. While we go through the other infinite runner tutorials we will want to take notes of the implementation, and specifially the implementation of procedurally generating levels.

As a side note, the author of the above tutorial mentions some things, which can be improved, and one of them is an object pooler. This is something we should implement to keep the game running optimized on mobile devices.

So with this we are going to be getting even closer to the vertical slice.

The next steps ahead is to go through all the tutorials, make comments here, and then post the code here and make comments on the code, beginning to rewrite and customize the code for our game.


# Completing Main Tasks - Getting Closer to a Vertical Slice

Completing the main tasks below will get us close to a vertical slice MVP. 

Then we will continue the other tasks. There are no major blockers at this time, which is a relief.

Making this game for mobile will be great for getting good experience, a better resume, and having something finally completed and out there. REAL ARTISTS SHIP.


# Next Main Task - Long Road

So our next main task is to create a long road. Let's not worry too much about aesthetics at this point and just get the road down.

So we will want to install ProGrids, and then make the highway patch into a nice even 1:1 tile, so that we can then snap to grid over a long course.

After this we should be able to drive down a long street to achieve longer gameplay.

Next up will be creating the AI system, and having the cars go in both direction on the road. We are going to start by having the cars going at constant speed. (Later the cars can use their blinkers to give a heads up to the player when they will be turning.)

So once the AI system is in place we would like to be able to tell when we collide with the AI.

If we collide let's get a game over state, and reset our position to the beginning, perhaps with something that requires the player to press a button to restart.

Let's also keep the cars spaced far enough apart where we can go through some of the gameplay without crashing right away.

Also, let's implement then the scoring system. One way to do this would be to have a separate trigger for detecting the player. So if the player enters the trigger, and then their x position is greater then the car, then add 100 points to their score. Also have to have some way of preventing cheating the system.


# Road Boundaries

Perhaps we can have a simple road boundary like a sidewalk which naturally will collide and flip the car at high speeds. At lower speeds the player might get some leeway, so they can ride over the sidewalk, but not much further, or out of bounds.

# Next Steps

So we have some basic movement on the car, a world, and incoming vehicles. So we are getting the basics down.

Now the movement controls will require some great deal of fine tuning, but there are other more important things to do, and we can always fine tune that later.

So now what we want to do is actually having a few minutes of gameplay. This will require:

- a long enough street

- incoming cars with an AI car controller

- colliders on the cars

- a way to detect whether we crashed into another collider, and restart

- a way to give us points for every car that we pass

The game over state will naturally require our Data.cs state machine.

So then that should not be a problem.

For now we don't want to focus on the procedural generation, since that is something completely removed from core mechanics. And procedural generation can have a level of complexity we don't want to get stuck in yet.

Also the procedural generation will have to work along with some of the infinite runner tutorials. So we will leave that.

But for now we want to have a long stretch of road on which to begin testing. We will assemble this stretch of the road manually for now.

Once we have a long enough road we will want to start working on AI.cs

AI.cs will have public slots for each of the cars.

Then the AI.cs will go through these gameobjects and have them coming at the player.

A general AI class like this should scale well with a procedural infinite runner down the line.

Okay, so at this point we want to have a long stretch of the road to playtest on, and incoming cars.

We want to start off by simply having a game over state once we crash into any car collider.

Also we want to record a score. 100 points for each car that we pass.

So once all that is done, we can begin to fine tune the controls.

If we have the car movement controls fine tuned, and the passing cars on the highway gameplay polished, then we can actually begin to work on the procedural level generation. But definitely save the procedural stuff until the end.

So after we get into the procedural level generation stuff, we will want to study the infinite runner tutorials.

At this point we can have at least 3 levels. The green grass level, the desert level, and the snow level. Perhaps the snow level will be the trickiest because of the slipperiness of the road. 

Now we want to make the gameplay long enough with just 3 levels. So each of the levels will have plenty of procedural generation. After that 3 levels are definitely enough to launch with.

So after that we will be in very good shape, having:

1. playable game

2. incoming AI traffic

3. scoring system

4. fine tuned player controls

5. a procedurally generated world


So after all this is done we can move on to the menu screen and settings.

Once that is done we can begin to port the game and playtest on mobile devices.

After that is done we will need to work on the sound effects and the score.

After that the local leaderboard. This is actually kind of important, even if a nuissance, to draw the players into the game.

And after all that is done, perhaps we can work on the bonus upgrades, although we might not need those, like Crossy Road.

Another thing to be done would be to expand the highway, but we might not do that. Although we might want to start with a four lane highway to add much more variety and skill to the gameplay. With four lanes there will be a lot more turning.

Okay, so we have all the major bases covered in terms of planning. We can rest easy and just work through these points. At the end we should have a pretty good prototype. Try to finish this in under a week. 

Then after all that is done we will need to address monetization the Crossy Road way, and marketing.

In Crossy Road there are two main types of monetization, rewarded video ads, and unlocking new characters. Go over the Crossy Road case study again. But the major takeaway is that in Crossy Road the player tries to unlock new characters, in our game we can have the player unlock new cars.



# Leveraging the Physics Engine

Leveraging the physics engine comes to great effect, since we can get a real world type simulation without doing any extra work. This works particularly well for the collisions. To note, if the player flips and lands then they keep on playing, which also provides for plenty of streamable moments.

When the car crashes we will want to apply more friction, so we slow down faster than through regular deceleration.

# Procedural Level Generation

We would like to use procedural level generation on such things as even the trees, and the pens for the hens, and etc. That way the game will look different everytime. And also the player will not be able to memorize the obstacle course.

# Obstacles

There are plenty of obstacles we can use. One cool one would be to have a side road like in the Voxel pack, and then the player has to dodge not only their single lane traffic but also traffic coming on the sides. As the difficulty of the game progresses in a rogue style, there will be more side streets we turn on.

Train tracks is another. We already have the art so we might as well incorporate that. Perhaps the player will get some warning when the train comes, so they will see the flashing lights from a distance, and then that is up to them to slow down.

Speaking of slowing down, we will have a pretty high top speed, but the game makes the choice up to the player how much they want to take advantage of speed -- because there are consquences (example: not stopping enough in time for a train, getting pulled over, etc). So in that way the player will have to get a feel for the game, and this will add gameplay enjoyment through learning the mastery of the game's mechanics.

# Car Movement - Turning

Here is adding turning to our PlayerControls.cs. 

Of course this just works at higher speeds. At lower speeds, when the player turns, we want to turn the wheels, and below a certain threshold, also turn the car.


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

Once the player colliders with a car, restart the game, to begin to give the player a sense of gameplay. Also save the high score so the player can test 
