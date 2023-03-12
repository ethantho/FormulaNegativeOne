using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spark : MonoBehaviour
{

    public float side;
    float strafingInput;
    SpriteRenderer spr;
    float counter;


    // Start is called before the first frame update
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        counter = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(strafingInput == side)
        {
            counter = 0.1f;
            
        }

        if(counter > 0)
        {
            spr.enabled = true;
        }
        else
        {
            spr.enabled = false;
        }
        counter-=Time.deltaTime;


    }

    public void SetInputVector(Vector3 inputVector)
    {
        //steeringInput = inputVector.x;
        //accelerationInput = inputVector.y;
        strafingInput = inputVector.z;
    }
}
