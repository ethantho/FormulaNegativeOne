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
    public float damageFactor;
    public float boostTopSpeed;


    float accelerationInput = 0;
    float steeringInput = 0;
    float strafingInput = 0;
    float sideAttackInput = 0;

    public bool gotBoostPower = false;



    //For physics Calculations
    [Header("Physics")]
    [SerializeField] float facingAngle = 0;
    [SerializeField] float travelAngle = 0;
    [SerializeField] float currentSpeed = 0;
    Vector2 strafe;
    [SerializeField] Vector2 sideAttack;
    [SerializeField] Vector2 boostVec;
    [SerializeField] Vector3 jumpVec;


    Rigidbody2D rb;
    CircleCollider2D col;
    EnergyBar nrg;
    //BoxCollider2D deathCol;

    public bool jumping = false;
    public float jumpSpeed;
    public float fallSpeed;
    public int colliding;
    bool deathCheck = false;
    public bool startedMoving;

    public float totalSpeed;

    public bool boosting;

    public bool dead;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();
        nrg = GetComponentInChildren<EnergyBar>();
        //deathCol = GetComponent<BoxCollider2D>();
        sideAttack = Vector2.zero;
        jumpVec = Vector3.zero;
        colliding = 0;

        startedMoving = false;
    }

    private void Update()
    {
        FixAngles();
        DoGrip();
        DoDrag();
        DoSideAttack();

        transform.position += jumpVec * (Time.deltaTime / (1f / 60f));
        if (deathCheck)
        {
            if(colliding > 0)
            {
                Die();
            }
            else
            {
                deathCheck = false;
            }
        }

        if(colliding != 0)
        {
            nrg.depleteEnergy(damageFactor * Time.deltaTime);
        }
        
    }

    private void FixedUpdate()
    {

        RotateCar();
        AccelerateCar();

        selfBoost();

        DoStrafing();

        ApplyVelocity();
        DoCollisions();


        


        sideAttack *= 0.9f;
        boostVec *= 0.99f;

        if(jumping)
        {
            jumpVec += new Vector3(0, 0, fallSpeed);
            col.enabled = false;
        }
        

        if(transform.position.z >= -1 && transform.position.z != -0.1f)
        {
            transform.position = new Vector3 (transform.position.x, transform.position.y, -0.1f);
            jumpVec = Vector3.zero;
            jumping = false;
            col.enabled = true;
            deathCheck = true;
            /*if(colliding > 0)
            {
                Die();
            }*/

        }

        //Debug.Log(Mathf.DeltaAngle(facingAngle, travelAngle));
    }


    void DoSideAttack()
    {
        if (sideAttackInput != 0)
        {
            sideAttack = transform.right * sideAttackInput * sideAttackFactor;
            //travelAngle = Mathf.Lerp(facingAngle, travelAngle, 0.5f);
            travelAngle = facingAngle;
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
            currentSpeed -= (Time.deltaTime / ( 1f / 60f)) * (currentSpeed / topSpeed) * dragFactor;
        }
        if(currentSpeed < 0)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0, 0.5f);
        }

        if(accelerationInput == 0 && currentSpeed > 0)
        {
            currentSpeed -=  accelerationFactor * dragFactor * (Time.deltaTime / (1f / 60f)) * (currentSpeed / topSpeed);
        }
        if (accelerationInput == 0 && currentSpeed < 0)
        {
            currentSpeed += accelerationFactor * dragFactor * (Time.deltaTime / (1f / 60f)) * (currentSpeed / topSpeed);
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
                bonusGrip = 2f;
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
        strafe = transform.right * strafingInput * strafeFactor * (currentSpeed / topSpeed);
        
    }



    void ApplyVelocity()
    {
        Vector2 vel = Quaternion.AngleAxis(-travelAngle, -Vector3.forward) * Vector3.up;
        vel *= currentSpeed;
        vel += strafe;

        sideAttack = Vector3.Project(sideAttack, transform.right);
        vel += sideAttack;

        boostVec = Vector3.Project(boostVec, vel).normalized * boostVec.magnitude;
        vel += boostVec;



        rb.velocity = vel;
        totalSpeed = vel.magnitude;
    }

    void DoCollisions()
    {
        //TODO
    }

    public void AddBoost(Vector3 dir)
    {
        boostVec = dir;
        GetComponent<AudioSource>().Play();
        if (currentSpeed < topSpeed)
            currentSpeed = Mathf.Lerp(currentSpeed, topSpeed, 0.5f );

        //travelAngle = Vector3.Angle(dir, Vector3.up);
    }

    public void selfBoost()
    {
        if (currentSpeed < boostTopSpeed && boosting && gotBoostPower && (nrg.getEnergy() - (accelerationFactor * damageFactor / 8) > 0))
        {
            currentSpeed += accelerationFactor * 2;
            nrg.depleteEnergy( damageFactor / 64);
        }
            
    }

    public void Jump()
    {
        jumpVec = new Vector3(0, 0, -jumpSpeed);
        
        jumping = true;
    }

    public void SlowDown()
    {
        if(currentSpeed > 0)
        {
            currentSpeed -= dragFactor * (Time.deltaTime / (1f / 60f)) * (currentSpeed / topSpeed);
        }
        else if(currentSpeed < 0)
        {
            currentSpeed += dragFactor  * (Time.deltaTime / (1f / 60f)) * (currentSpeed / topSpeed);
        }
    }

    public void SetInputVector(Vector3 inputVector, float saInput, bool boost)
    {
        steeringInput = inputVector.x;
        accelerationInput = inputVector.y;
        strafingInput = inputVector.z;
        sideAttackInput = saInput;
        boosting = boost;

        if(inputVector.magnitude != 0)
        {
            startedMoving = true;
        }
    }

    public void Die()
    {
        Debug.Log("Died");
        SpriteRenderer sp = GetComponentInChildren<SpriteRenderer>();
        sp.enabled = false;
        dead = true;
        GetComponentInChildren<ExplosionController>().explode();
        rb.velocity = Vector2.zero;
        this.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        colliding++;
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        colliding--;
    }

    private void OnCollisionStay(Collision collision)
    {
        SlowDown();
    }


}
