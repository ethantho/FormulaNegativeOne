using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInputHandler : MonoBehaviour
{
    CarController cc;
    CarGraphics cg;
    Spark ls;
    Spark rs;

    int LCount;
    float LCool;
    int RCount;
    float RCool;

    float doubleTapThreshold = 0.2f;

    [SerializeField] float sideAttack;
    [SerializeField] bool boost;
    [SerializeField] Vector3 inputVector;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CarController>();
        cg = GetComponentInChildren<CarGraphics>();
        ls = GetComponentsInChildren<Spark>()[0];
        rs = GetComponentsInChildren<Spark>()[1];

        LCount = 0;
        LCool = doubleTapThreshold;
        RCount = 0;
        RCool = doubleTapThreshold;
    }

    // Update is called once per frame
    void Update()
    {
        inputVector = Vector2.zero;
        sideAttack = 0f;

        //inputVector.x = Input.GetAxis("Horizontal");
        if(Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            inputVector.x = 1;
        }
        else if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            inputVector.x = -1;
        }
        else
        {
            inputVector.x = 0;
        }

        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y = 1;
        }
        else
        {
            inputVector.y = 0;
        }


        
        if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
        {
            inputVector.z = 1;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            inputVector.z = -1;
        }
        else
        {
            inputVector.z = 0;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (LCool > 0 && LCount == 1/*Number of Taps you want Minus One*/)
            {
                Debug.Log("Double Tapped right");
                sideAttack = 1;
                
            }
            else
            {
                LCool = doubleTapThreshold;
                LCount += 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (RCool > 0 && RCount == 1/*Number of Taps you want Minus One*/)
            {
                Debug.Log("Double Tapped left");
                sideAttack = -1;

            }
            else
            {
                RCool = doubleTapThreshold;
                RCount += 1;
            }
        }

        if (LCool > 0)
        {

            LCool -= 1 * Time.deltaTime;

        }
        else
        {
            LCount = 0;
        }

        if (RCool > 0)
        {

            RCool -= 1 * Time.deltaTime;

        }
        else
        {
            RCount = 0;
        }


        if (Input.GetKey(KeyCode.Space))
        {
            boost = true;
        }
        else
        {
            boost = false;
        }




        cc.SetInputVector(inputVector, sideAttack, boost);
        cg.SetInputVector(inputVector);
        ls.SetInputVector(inputVector);
        rs.SetInputVector(inputVector);
    }
}
