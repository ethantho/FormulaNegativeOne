using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Car settings")]
    public float accelerationFactor;// = 30.0f;
    public float turnFactor;// = 3.5f;
    public float gripFactor;// = 0;//.000001f;//TODO
    public float dragFactor;// = 0.01f;
    public float strafeFactor;
    public float sideAttackFactor;
    public float topSpeed;


    float accelerationInput = 0;
    float steeringInput = 0;
    float strafingInput = 0;



    //For physics Calculations
    [Header("Physics")]
    [SerializeField] float facingAngle = 0;
    [SerializeField] float travelAngle = 0;
    [SerializeField] float currentSpeed = 0;
    Vector2 strafe;
    [SerializeField] Vector2 sideAttack;


    Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sideAttack = Vector2.zero;
    }

    private void Update()
    {
        FixAngles();
        DoGrip();
        DoDrag();
        DoSideAttack();
    }

    private void FixedUpdate()
    {

        RotateCar();
        AccelerateCar();

        DoStrafing();

        ApplyVelocity();
        DoCollisions();



        sideAttack *= 0.9f;

        //Debug.Log(Mathf.DeltaAngle(facingAngle, travelAngle));
    }


    void DoSideAttack()
    {
        if (strafingInput != 0)
        {
            sideAttack = transform.right * strafingInput * sideAttackFactor;
            travelAngle = Mathf.Lerp(facingAngle, travelAngle, 0.5f);
        }
            
        //sideAttack *= 0.95f;
    }


    void RotateCar()
    {
        facingAngle -= steeringInput * turnFactor;

        rb.MoveRotation(facingAngle);
    }



    void FixAngles()
    {
        if (facingAngle > 360)
        {
            facingAngle -= 360;
        }
        if (facingAngle < 0)
        {
            facingAngle += 360;
        }
        if (travelAngle > 360)
        {
            travelAngle -= 360;
        }
        if (travelAngle < 0)
        {
            travelAngle += 360;
        }
    }


    void AccelerateCar()
    {
        currentSpeed += accelerationInput * accelerationFactor;//TODO: slower decel
    }

    void DoDrag()
    {
        if(currentSpeed > topSpeed)
        {
            currentSpeed -= accelerationFactor * (Time.deltaTime / ( 1f / 60f));
        }
        if(currentSpeed < 0)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0, 0.5f);
        }

        if(accelerationInput == 0 && currentSpeed > 0)
        {
            currentSpeed -= accelerationFactor * dragFactor * (Time.deltaTime / (1f / 60f));
        }
        if (accelerationInput == 0 && currentSpeed < 0)
        {
            currentSpeed += accelerationFactor * dragFactor * (Time.deltaTime / (1f / 60f));
        }

    }

    void DoGrip()
    {
        //if()  TODO
        
        
        float angleDif = Mathf.DeltaAngle(facingAngle, travelAngle);
        //Debug.Log(angleDif);

        float bonusGrip = 1f;
        
        if(accelerationInput == 0)
        {
            bonusGrip = 3f;
        }
        else
        {
            if(strafingInput != 0)
            {
                bonusGrip = 1.5f;
            }
        }


        if(angleDif < 0)
        {
            travelAngle += gripFactor * bonusGrip * (Time.deltaTime / (1f / 60f));
        }
        if(angleDif > 0)
        {
            travelAngle -= gripFactor * bonusGrip * (Time.deltaTime / (1f / 60f));
        }


    }

    void DoStrafing()
    {
        //strafe = transform.right * strafingInput * strafeFactor * (currentSpeed / topSpeed);
        //rb.MovePosition(rb.position + strafe);
    }



    void ApplyVelocity()
    {
        Vector2 vel = Quaternion.AngleAxis(-travelAngle, -Vector3.forward) * Vector3.up; //TODO: change to travelAngle
        vel *= currentSpeed;
        vel += strafe;
        vel += sideAttack;
        rb.velocity = vel; 
    }

    void DoCollisions()
    {
        //TODO
    }

    public void SetInputVector(Vector3 inputVector)
    {
        steeringInput = inputVector.x;
        accelerationInput = inputVector.y;
        strafingInput = inputVector.z;
    }
}
