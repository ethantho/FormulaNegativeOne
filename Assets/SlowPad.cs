using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowPad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        CarController cc = collision.gameObject.GetComponentInChildren<CarController>();
        Debug.Log(cc);
        if ( cc != null)
        {
            cc.SlowDown();
        }
    }
}
