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

    public GameObject loot;

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


    void DeployModerateTrafficWithLoot()
    {
        // LANE 1

        float lane1xPos = 2.5f;
        float lane1Speed = 8.0f;

        SpawnCar(loot, GetRandomPos(2, 10, lane1xPos), lane1Speed);
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


        DeployModerateTrafficWithLoot();

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
