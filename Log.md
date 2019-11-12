# Basic Starting Game Objects


I. World

So we will want a street view with the camera pointing up ahead. We will also like to have the voxel clouds found in the VoxelPlay app. We also want trees on the side to hover by, as a way of decoration but also a way of guaging speed.

II. Player

The player is a car. They can move the car left to right, and also accelerate and deccelerate.

III. UI

The UI will be designed for a touch screen, so we will need some keyboard shortcuts while we are testing on desktop. For the touch screen interface we will need a way to move left to right and also accelerate and deccelerate.

IV. Gameplay

We will take elements from rogue like gameplay and also Crossy Road.

We will have random cars in front of us, and random cars on the left lane. We have the choice to use the left lane to pass the cars, but as we do so we risk colliding with oncoming traffic. Once we collide with oncoming traffic we lose the game, like in Crossy Road and start over. Focus on making an enjoyable simple core loop, where the player can die and goes back to the beginning, but still enjoys replaying the game.

V. Code

We want to keep our code in a data oriented design, so we will be using the code from our platformer, to keep all of our data in a centralized file, Data.cs


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

