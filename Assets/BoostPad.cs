using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostPad : MonoBehaviour
{
    [SerializeField] Vector3 boostVec;
    // Start is called before the first frame update
    void Start()
    {
        boostVec = transform.up * 40f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CarController cc = collision.GetComponent<CarController>();
        if(cc != null)
        {
            cc.AddBoost(boostVec);
        }
    }
}
