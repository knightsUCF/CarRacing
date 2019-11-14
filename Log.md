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
