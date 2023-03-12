using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathChecker : MonoBehaviour
{
    CarController cc;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponentInParent<CarController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!cc.jumping && !collision.isTrigger)
        {
            cc.Die();
        }
    }
    
}
