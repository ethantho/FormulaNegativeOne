using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarGraphics : MonoBehaviour
{

    float accelerationInput = 0;
    float steeringInput = 0;
    float strafingInput = 0;
    Vector3 turnVec;
    Vector3 strafeVec;
    // Start is called before the first frame update
    void Start()
    {
        turnVec = new Vector3(0, 0, 7.5f);
        strafeVec = new Vector3(0, 0, 2.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if(steeringInput < 0)
        {
            
            transform.localEulerAngles = turnVec;
        }
        if(steeringInput > 0)
        {
            transform.localEulerAngles = -turnVec;

        }
        if (steeringInput == 0)
        {
            transform.localEulerAngles = Vector3.zero;          

        }

        if (strafingInput < 0)
        {
            transform.localEulerAngles += strafeVec;
        }

        if (strafingInput > 0)
        {
            transform.localEulerAngles -= strafeVec;
        }
    }

    public void SetInputVector(Vector3 inputVector)
    {
        steeringInput = inputVector.x;
        accelerationInput = inputVector.y;
        strafingInput = inputVector.z;
    }
}
