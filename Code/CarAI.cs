using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class CarAI : MonoBehaviour
{

    public float speed = 2.0f;

    public float zPos = -10.0f;

    Vector3 pos;

    public bool forward = true;
    



    private void Update()
    {
        SetVelocity();
    }


    private void SetVelocity()
    {
        pos = transform.position;


        // cars moving in one lane

        if (forward) pos.x = transform.position.x - speed * Time.deltaTime;

        // cars moving the other direction in the other lane

        if (!forward) pos.x = transform.position.x + speed * Time.deltaTime;


        transform.position = pos;
    }




}
