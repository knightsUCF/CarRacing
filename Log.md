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

