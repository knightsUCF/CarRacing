using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class CarAI : MonoBehaviour
{

    public float speed = 2.0f;

    public float zPos = -10.0f;

    Vector3 pos;
    

    private void Start()
    {
        SetVelocity();
    } 


    private void Update()
    {
        SetVelocity();
    }


    private void SetVelocity()
    {
        pos = transform.position;
        pos.x = transform.position.x - speed * Time.deltaTime;
    
        transform.position = pos;
    }


}
